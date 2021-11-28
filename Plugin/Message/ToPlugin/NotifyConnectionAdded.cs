using System;

namespace Plugin.Message.ToPlugin
{
    public class NotifyConnectionAdded
    {
        public NotifyConnectionAdded(Type connType, ConnectionId connId)
        {
            ConnType = connType;
            ConnId = connId;
        }

        public Type ConnType { get; }
        public ConnectionId ConnId { get; }
    }
}