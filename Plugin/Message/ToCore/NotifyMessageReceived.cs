namespace Plugin.Message.ToCore
{
    public class NotifyMessageReceived
    {
        public NotifyMessageReceived(PluginId src, ConnectionId connId, ISiteMessage message)
        {
            Src = src;
            ConnId = connId;
            Message = message;
        }

        public PluginId Src { get; }
        public ConnectionId ConnId { get; }
        public ISiteMessage Message { get; }
    }
}