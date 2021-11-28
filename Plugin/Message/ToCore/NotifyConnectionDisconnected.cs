namespace Plugin.Message.ToCore
{
    public class NotifyConnectionDisconnected
    {
        public NotifyConnectionDisconnected(ConnectionId connId)
        {
            ConnId = connId;
        }

        public ConnectionId ConnId { get; }
    }
}