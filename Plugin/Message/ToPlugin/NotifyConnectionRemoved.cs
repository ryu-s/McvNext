namespace Plugin.Message.ToPlugin
{
    public class NotifyConnectionRemoved
    {
        public NotifyConnectionRemoved(ConnectionId connId)
        {
            ConnId = connId;
        }

        public ConnectionId ConnId { get; }
    }
}