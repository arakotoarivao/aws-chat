using Amazon.DynamoDBv2.DataModel;
using MediatR;

namespace server.Features.GetChatById;

public static class GetChatById
{
    public record GetChatByIdQuery(Guid ChatId) : IRequest<ChatDto>;

    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("/chat/{id}", Get);
    }

    static async Task<ChatDto> Get(Guid id, IMediator mediator) => await mediator.Send(new GetChatByIdQuery(id));

    public class GetChatByIdQueryHandler(IDynamoDBContext dbContext) : IRequestHandler<GetChatByIdQuery, ChatDto>
    {
        public async Task<ChatDto> Handle(GetChatByIdQuery request, CancellationToken token)
        {
            var query = dbContext.QueryAsync<ConversationDto>(request.ChatId);

            var conversationDtos = await query.GetRemainingAsync();
            var conversations = conversationDtos.Select(dto => dto.Conversation).ToArray();

            return new(request.ChatId, conversations);
        }
    }
}
