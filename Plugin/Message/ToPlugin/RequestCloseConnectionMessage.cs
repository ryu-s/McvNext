namespace Plugin.Message.ToPlugin
{
    public class RequestCloseConnectionMessage
    {
        public RequestCloseConnectionMessage(ConnectionId connId)
        {
            ConnId = connId;
        }

        public ConnectionId ConnId { get; }
    }
}