namespace Plugin.Message.ToPlugin
{
    public class AnswerConnectionStatus
    {
        public AnswerConnectionStatus(ConnectionId connId, IConnectionStatus connSt)
        {
            ConnId = connId;
            ConnSt = connSt;
        }

        public ConnectionId ConnId { get; }
        public IConnectionStatus ConnSt { get; }
    }
}