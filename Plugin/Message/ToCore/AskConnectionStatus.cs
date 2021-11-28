namespace Plugin.Message.ToCore
{
    public class AskConnectionStatus
    {
        public AskConnectionStatus(ConnectionId connId)
        {
            ConnId = connId;
        }

        public ConnectionId ConnId { get; }
    }
}