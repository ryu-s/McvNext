
using Akka.Actor;

namespace Plugin
{
    public interface IPlugin
    {
        string Name { get; }
        PluginId Id { get; }
        //IActorRef CreateActor(IActorContext context);
        Props GetProps();
    }
}
