using AylinChat.Repos;
using ChatModels.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AylinChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController(ChatRepo chatRepo) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<GroupChatDTO>>> GetGroupChatsAsync()
            => Ok(await chatRepo.GetGroupChatsAsync()); 
      

        [HttpGet("users")]
        public async Task<ActionResult>  GetUserAsync()
           => Ok(await chatRepo.GetAvailableUsersAsync());

        [HttpGet("individual")]
        public async Task<IActionResult> GetIndividualChatsAsync(RequestChatDto requestChatDTO)
            => Ok(await chatRepo.GetIndividualChatsAsync(requestChatDTO));
    }
}
