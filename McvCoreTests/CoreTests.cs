using Akka.Actor;
using Mcv.Core;
using Moq;
using NUnit.Framework;
using Plugin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToPlugin = Plugin.Message.ToPlugin;
namespace McvCoreTests;
class TestActor : IMcvActor
{
    public Task<T> Ask<T>(object message)
    {
        throw new NotImplementedException();
    }

    public void Tell(object message)
    {
        throw new NotImplementedException();
    }
}
class TestActorFactory : IActorFactory
{
    private readonly TestActor _actor;

    public IMcvActor CreateActor(Props props)
    {
        return _actor;
    }
    public TestActorFactory(TestActor actor)
    {
        _actor = actor;
    }
}
public class CoreTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void 初期状態のConnection数は0()
    {
        //Arrange
        List<ToPlugin.ConnectionInfo>? connections=null;
        var actorMock = new Mock<IMcvActor>();
        actorMock.Setup(a => a.Tell(It.IsAny<object>())).Callback<object>(o =>
        {
            if(o is ToPlugin.AnswerConnections ans)
            {
                connections = ans.Connections;
            }
        });
        var factoryMock = new Mock<IActorFactory>();
        factoryMock.Setup(f => f.CreateActor(It.IsAny<Props>())).Returns(new Mock<IMcvActor>().Object);
        var core = new Mcv.Core.Core(factoryMock.Object);

        //Act
        core.OnAskConnections(actorMock.Object);

        //Assert
        Assert.IsNotNull(connections);
        Assert.AreEqual(0, connections!.Count);
    }
    [Test]
    public void Connectionを追加できるか()
    {
        //Arrange
        List<ToPlugin.ConnectionInfo>? connections = null;
        var actorMock = new Mock<IMcvActor>();
        actorMock.Setup(a => a.Tell(It.IsAny<object>())).Callback<object>(o =>
        {
            if (o is ToPlugin.AnswerConnections ans)
            {
                connections = ans.Connections;
            }
        });
        var factoryMock = new Mock<IActorFactory>();
        factoryMock.Setup(f => f.CreateActor(It.IsAny<Props>())).Returns(new Mock<IMcvActor>().Object);
        var core = new Mcv.Core.Core(factoryMock.Object);

        //Act
        core.OnRequestAddNormalConnection();
        core.OnAskConnections(actorMock.Object);

        //Assert
        Assert.IsNotNull(connections);
        Assert.AreEqual(1, connections!.Count);
    }
    [Test]
    public void ConnectionStatusを変更できるか()
    {
        //Arrange
        INormalConnection? connSt = null;
        ConnectionId? connId = null;

        var pluginActorMock = new Mock<IMcvActor>();

        var factoryMock = new Mock<IActorFactory>();
        factoryMock.Setup(f => f.CreateActor(It.IsAny<Props>())).Returns(pluginActorMock.Object);
        var core = new Mcv.Core.Core(factoryMock.Object);
        var pluginId = new PluginId(Guid.NewGuid());
        pluginActorMock.Setup(p => p.Tell(It.IsAny<object>())).Callback<object>(o =>
        {
            if (o is ToPlugin.Hello)
            {
                core.OnHello(pluginId, new List<IPluginType>());
            }
            else if (o is ToPlugin.NotifyConnectionAdded added)
            {
                connId = added.ConnId;
            }
        });
        var testPluginMock = new Mock<IPlugin>();
        testPluginMock.Setup(t => t.Id).Returns(pluginId);
        core.AddPlugin(testPluginMock.Object);

        var actorMock = new Mock<IMcvActor>();
        actorMock.Setup(a => a.Tell(It.IsAny<object>())).Callback<object>(o =>
        {
            if (o is ToPlugin.AnswerConnectionStatus ans && ans.ConnSt is INormalConnection normal)
            {
                connSt = normal;
            }
        });

        //Act
        core.OnRequestAddNormalConnection();
        core.OnRequestChangeConnectionStatus(connId!, new NormalConnectionDiff { Input = "abc" });
        core.OnAskConnectionStatus(connId!, actorMock.Object);

        //Assert
        Assert.IsNotNull(connSt);
        Assert.AreEqual("abc", connSt!.Input);
    }
}