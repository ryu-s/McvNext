using NUnit.Framework;
using Mcv.Plugin.MainViewPlugin;
using Moq;
using System.Collections.Generic;
using Mcv.Plugin.MainViewPlugin.Settings;

namespace MainViewPluginTests;
public class MainViewModelTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var adapterMock = new Mock<IAdapter>();
        adapterMock.Setup(x => x.IsPixelScrolling).Returns(true);
        adapterMock.Setup(x=>x.ConnectionsViewSelectionDisplayIndex).Returns(99);
        var mvm = new MainViewModel(adapterMock.Object, new SettingsContext(new List<ISiteStrategy>()));
        Assert.AreEqual(System.Windows.Controls.ScrollUnit.Pixel, mvm.CommentVm.ScrollUnit);
        Assert.AreEqual(99, mvm.ConnectionsVm.ConnectionsViewSelectionDisplayIndex);
    }
}