using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mcv.Plugin.MainViewPlugin.Site.A17Live;
/// <summary>
/// Interaction logic for YouTubeLiveOptionsPanel.xaml
/// </summary>
public partial class A17LiveOptionsPanel : UserControl
{
    public A17LiveOptionsPanel()
    {
        InitializeComponent();
    }
    internal void SetViewModel(A17LiveSiteSettingsViewModel vm)
    {
        DataContext = vm;
    }
    internal A17LiveSiteSettingsViewModel GetViewModel()
    {
        return (A17LiveSiteSettingsViewModel)DataContext;
    }
}
