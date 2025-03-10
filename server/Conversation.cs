using Amazon.DynamoDBv2.DataModel;

namespace server;

public record Conversation(Guid Id, string Sender, DateTime CreationDate);

public record ChatDto(Guid Id, Conversation[] Conversations);

[DynamoDBTable("conversation")]
public class ConversationDto
{
    [DynamoDBHashKey]
    public Guid ChatId { get; set; }

    [DynamoDBProperty]
    public required Conversation Conversation { get; set; }
}