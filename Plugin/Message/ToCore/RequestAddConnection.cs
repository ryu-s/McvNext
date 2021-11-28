using System;

namespace Plugin.Message.ToCore
{
    public class RequestAddConnection
    {
        public Type ConnType { get; }
        public RequestAddConnection(Type connType)
        {
            ConnType = connType;
        }
    }
}