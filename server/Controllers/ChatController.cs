using Microsoft.AspNetCore.Mvc;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    [HttpGet]
    public IEnumerable<ChatDto> Get()
    {
        return [];
    }

    [HttpPost]
    public async Task PostConversation(ConversationDto conversationDto)
    {
        await Task.CompletedTask;
    }
}
