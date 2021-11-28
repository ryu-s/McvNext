using Akka.Actor;
using Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcv.Plugin.MainViewPlugin
{
    public class MainViewPlugin : IPlugin
    {
        public PluginId Id { get; } = new PluginId(Guid.NewGuid());
        public string Name { get; } = "MainViewPlugin";

        public Props GetProps()
        {
            return Props.Create(() => new MainViewActor(Id)).WithDispatcher("akka.actor.synchronized-dispatcher");
        }
    }
    //public abstract class PluginBase:IPlugin
    //{
    //    public abstract PluginId Id { get; }
    //    public abstract string Name { get; }
    //    public abstract IActorRef CreateActor(IUntypedActorContext context);
    //}
    //public abstract class PluginActorBase
    //{
    //    protected virtual void OnMessageReceived() { }
    //}
    //class PluginTest : PluginBase
    //{
    //    public override PluginId Id { get; }=new PluginId(Guid.NewGuid());
    //    public override string Name { get; } = "";
    //    public override IActorRef CreateActor(IUntypedActorContext context)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    //class PluginActorTest : PluginActorBase
    //{

    //}
}
