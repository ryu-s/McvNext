namespace Plugin.Message.ToPlugin
{
    public class RequestStopConnectionMessage
    {
        public RequestStopConnectionMessage(ConnectionId connId)
        {
            ConnId = connId;
        }

        public ConnectionId ConnId { get; }
    }
}