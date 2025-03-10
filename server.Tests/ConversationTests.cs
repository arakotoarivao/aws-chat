using FluentAssertions;
using Server.Tests;
using System.Net;
using System.Net.Http.Json;
using static server.Features.SaveConversation.SaveConversation;

namespace server.Tests;

public class ConversationTests(ServerWebApplicationFactory factory) : IClassFixture<ServerWebApplicationFactory>
{
    readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Conversation_should_be_added_and_retrieved_successfully()
    {
        var chatId = Guid.NewGuid();
        await GetChatAndAssert(chatId, 2);

        var command = new AddConversationCommand(new ConversationDto
        {
            ChatId = chatId,
            Conversation = new Conversation(Guid.NewGuid(), "Fake User", DateTime.UtcNow)
        });

        await InsertConversationAndAssert(command);
        await GetChatAndAssert(chatId, 3, command.Conversation.Conversation);

        command = new AddConversationCommand(new ConversationDto
        {
            ChatId = chatId,
            Conversation = new Conversation(Guid.NewGuid(), "Fake User 2", DateTime.UtcNow)
        });

        await InsertConversationAndAssert(command);
        await GetChatAndAssert(chatId, 4, command.Conversation.Conversation);
    }

    async Task InsertConversationAndAssert(AddConversationCommand command)
    {
        var response = await _client.PostAsJsonAsync("/conversation", command);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    async Task GetChatAndAssert(Guid chatId, int expectedCount, Conversation? expectedConversation = null)
    {
        var data = await _client.GetFromJsonAsync<ChatDto>($"/chat/{chatId}");
        data.Should().NotBeNull();
        data.Conversations.Should().HaveCount(expectedCount);

        if (expectedConversation != null)
            data.Conversations.Should().ContainEquivalentOf(expectedConversation);
    }
}