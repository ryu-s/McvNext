using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Plugin;
using ToCore = Plugin.Message.ToCore;
namespace UnknownLiveSiteCommentProviderPlugin
{
    public class PluginMain : IPlugin
    {
        public string Name { get; } = "未知のサイトプラグイン";
        public PluginId Id { get; } = new PluginId(Guid.NewGuid());

        public Props GetProps()
        {
            return MainActor.Props(Id);
        }
    }

}
