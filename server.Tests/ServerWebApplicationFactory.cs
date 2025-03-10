using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using server;

namespace Server.Tests;

public class ServerWebApplicationFactory : WebApplicationFactory<Program>
{
    Conversation[] defaults =
    [
        new(Guid.NewGuid(), "User1", DateTime.UtcNow),
        new(Guid.NewGuid(), "User2", DateTime.UtcNow)
    ];

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IDynamoDBContext));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            var mockDynamoDbContext = new Mock<IDynamoDBContext>();
            var asyncSearchMock = new Mock<AsyncSearch<ConversationDto>>();

            asyncSearchMock
                .Setup(x => x.GetRemainingAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => defaults.Select(x => new ConversationDto
                {
                    ChatId = Guid.NewGuid(),
                    Conversation = x
                }).ToList());

            mockDynamoDbContext
                .Setup(db => db.QueryAsync<ConversationDto>(It.IsAny<Guid>(), default))
                .Returns(asyncSearchMock.Object);

            mockDynamoDbContext
                .Setup(x => x.SaveAsync(It.IsAny<ConversationDto>(), default))
                .Callback<ConversationDto, CancellationToken>((item, _) =>
                {
                    defaults = [.. defaults, item.Conversation];
                })
                .Returns(Task.CompletedTask);

            services.AddSingleton(mockDynamoDbContext.Object);
        });
    }
}
