namespace Plugin.Message.ToPlugin
{
    public class RequestOpenConnectionMessage
    {
        public RequestOpenConnectionMessage(SiteId siteId, ConnectionId connId)
        {
            SiteId = siteId;
            ConnId = connId;
        }

        public SiteId SiteId { get; }
        public ConnectionId ConnId { get; }
    }
}