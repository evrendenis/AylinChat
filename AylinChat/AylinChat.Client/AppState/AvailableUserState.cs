namespace AylinChat.Client.AppState
{
    public class AvailableUserState
    {
        public string ReceiverId { get; set; } = string.Empty;

        public string Fullname { get; set; } = string.Empty;

        public void SetStates(string receiverId, string fullname)
        {
            Fullname = fullname;
            ReceiverId = receiverId;
        }
    }
}
