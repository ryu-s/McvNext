using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace Mcv.Plugin.MainViewPlugin.Settings;
/// <summary>
/// Interaction logic for SettingsView.xaml
/// </summary>
internal partial class SettingsView : Window
{
    public SettingsView()
    {
        InitializeComponent();
    }
    List<IOptionsTabPage> _pagePanels = new List<IOptionsTabPage>();
    public void AddTabPage(IOptionsTabPage page)
    {
        var tabPage = new TabItem()
        {
            Header = page.HeaderText,
            Content = page.TabPagePanel,
        };
        tabControl.Items.Add(tabPage);
        _pagePanels.Add(page);
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            _pagePanels.ForEach(panel => panel.Apply());
            Close();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            _pagePanels.ForEach(panel => panel.Cancel());
            Close();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}
