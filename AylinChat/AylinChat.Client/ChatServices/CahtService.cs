using ChatModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Data;

namespace AylinChat.Client.ChatServices
{
    public class ChatService
    {
        public Action? InvokeChatDisplay { get; set; }
        public List<Chat> Chats { get; set; } = [];
        private readonly HubConnection _hubConnection;
        
        public bool IsConnected { get; set; }

        public ChatService(NavigationManager navigationManager)
        {
            _hubConnection = new HubConnectionBuilder().WithUrl(navigationManager.ToAbsoluteUri("/Hubs")).Build();

        }
        public void ReciveMessage()
        {
            _hubConnection.On<Chat>("ReciveMessage", (chat) =>
            {
                Chats.Add(chat);
                InvokeChatDisplay?.Invoke();
            });
        }
        public async Task StartConnectionAsync()
        {
            await _hubConnection.StartAsync();
            IsConnected = _hubConnection!.State == HubConnectionState.Connected;
        }
        public Task SendChat(Chat chat) => _hubConnection!.SendAsync("SendMessage", chat);
    }

}
