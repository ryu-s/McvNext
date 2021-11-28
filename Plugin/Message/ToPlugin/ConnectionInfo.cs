namespace Plugin.Message.ToPlugin
{
    public class ConnectionInfo
    {
        public ConnectionInfo(ConnectionId connId, IConnectionStatus connSt)
        {
            ConnId = connId;
            ConnSt = connSt;
        }

        public ConnectionId ConnId { get; }
        public IConnectionStatus ConnSt { get; }
    }
}