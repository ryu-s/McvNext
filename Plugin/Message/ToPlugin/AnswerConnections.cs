using System.Collections.Generic;

namespace Plugin.Message.ToPlugin
{
    public class AnswerConnections
    {
        public AnswerConnections(List<ConnectionInfo> connections)
        {
            Connections = connections;
        }

        public List<ConnectionInfo> Connections { get; }
    }
}