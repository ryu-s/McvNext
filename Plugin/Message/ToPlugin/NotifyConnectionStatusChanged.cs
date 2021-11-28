namespace Plugin.Message.ToPlugin
{
    public class NotifyConnectionStatusChanged
    {
        public NotifyConnectionStatusChanged(ConnectionId connId, IConnectionStatusDiff diff)
        {
            ConnId = connId;
            Diff = diff;
        }

        public ConnectionId ConnId { get; }
        public IConnectionStatusDiff Diff { get; }
        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}