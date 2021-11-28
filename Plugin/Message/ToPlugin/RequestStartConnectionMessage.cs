namespace Plugin.Message.ToPlugin
{
    public class RequestStartConnectionMessage
    {
        public RequestStartConnectionMessage(ConnectionId connId, INormalConnection normal)
        {
            ConnId = connId;
            Input = normal.Input;
        }

        public ConnectionId ConnId { get; }
        public string Input { get; }
    }
}