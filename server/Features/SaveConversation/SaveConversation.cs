using Amazon.DynamoDBv2.DataModel;
using MediatR;

namespace server.Features.SaveConversation;

public static class SaveConversation
{
    public record AddConversationCommand(ConversationDto Conversation) : IRequest<IResult>;

    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/conversation", Change);
    }

    static async Task Change(AddConversationCommand command, IMediator mediator) => await mediator.Send(command);

    public class AddConversationHandler(IDynamoDBContext dbContext) : IRequestHandler<AddConversationCommand, IResult>
    {
        public async Task<IResult> Handle(AddConversationCommand request, CancellationToken token)
        {
            await dbContext.SaveAsync(request.Conversation, token);
            return Results.Ok("Conversation added successfully");
        }
    }
}
