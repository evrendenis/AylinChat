using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;


namespace AylinChat.Client.ChatServices
{
    public class MyHubConnectionService
    {
        private readonly HubConnection _hubConnection;
        
        public bool IsConnected { get; set; }

        public MyHubConnectionService(NavigationManager navigationManager)
        {
            _hubConnection = new HubConnectionBuilder().WithUrl(navigationManager.ToAbsoluteUri("/Hubs")).Build();

            _hubConnection.StartAsync();
            GetConnectionState();
        }
        public HubConnection GetHubConnection() => _hubConnection;

        public bool GetConnectionState()
        {
            var hubConnection = _hubConnection;
            IsConnected = hubConnection != null;
            return IsConnected;
        }
    }
}
