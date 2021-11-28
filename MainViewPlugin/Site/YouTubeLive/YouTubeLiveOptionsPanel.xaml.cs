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

namespace Mcv.Plugin.MainViewPlugin.Site.YouTubeLive;
/// <summary>
/// Interaction logic for YouTubeLiveOptionsPanel.xaml
/// </summary>
public partial class YouTubeLiveOptionsPanel : UserControl
{
    public YouTubeLiveOptionsPanel()
    {
        InitializeComponent();
    }
    internal void SetViewModel(YouTubeLiveSiteSettingsViewModel vm)
    {
        DataContext = vm;
    }
    internal YouTubeLiveSiteSettingsViewModel GetViewModel()
    {
        return (YouTubeLiveSiteSettingsViewModel)DataContext;
    }
}
