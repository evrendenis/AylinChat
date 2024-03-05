using AylinChat.Repos;
using ChatModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AylinChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController(ChatRepo chatRepo) : ControllerBase
    {
        [HttpGet] 
        public async Task<ActionResult<List<Chat>>> GetChatAsync()
            => Ok(await chatRepo.GetChatsAsync());
    }
}
