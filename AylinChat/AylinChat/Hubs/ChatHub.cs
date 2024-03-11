using AylinChat.Repos;
using ChatModels.DTOs;
using ChatModels.Models;
using Microsoft.AspNetCore.SignalR;

namespace AylinChat.Hubs
{
    public class ChatHub( ChatRepo chatRepo) : Hub
    {

        public async Task SendMessageToGroup(GroupChat chat)
        {
            var saveChatDTO = await chatRepo.AddChatToGroupAsync(chat);
            await Clients.All.SendAsync("ReciveMessage", saveChatDTO);
        }

        public async Task AddAvailableUser(AvailableUser availableUser)
        {
            availableUser.ConnectionId = Context.ConnectionId;
            var savedUser = await chatRepo.AddAvailableUser(availableUser);
            await Clients.All.SendAsync("NotifyAllClient", savedUser);
        }

        public async Task RemoveUser(string userId)
        {
            var removedUser = await chatRepo.RemoveUserAsync(userId);
            await Clients.All.SendAsync("NotifyAllClient", removedUser);
        }

        private async Task AddIndividualChat(IndividualChat individualChat)
        {
            await chatRepo.AddIndividualChatsAsync(individualChat);
            var requestDTO = new RequestChatDto()
            { ReceiverId = individualChat.ReceiverId, SenderId = individualChat.SenderId };
            var getChats = await chatRepo.GetIndividualChatsAsync(requestDTO);
            var prepareIndividualChat = new IndividualChatDTO()
            {
                SenderId = individualChat.SenderId,
                ReceiverId = individualChat.ReceiverId,
                Massage = individualChat.Message,
                Date = individualChat.date,
                ReceiverName = getChats.FirstOrDefault(_ => _.ReceiverId == individualChat.ReceiverId)?.ReceiverName,
                SenderName = getChats.FirstOrDefault(_ => _.SenderId == individualChat.SenderId)?.SenderName
            };
            await Clients.User(individualChat.ReceiverId!).SendAsync("ReceiveIndividualMessage", prepareIndividualChat);
        }
    }
}
