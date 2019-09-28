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
using System.Windows.Shapes;

namespace SideAssistant
{
    /// <summary>
    /// SettingsWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            loadSetting();
        }

        private void loadSetting()
        {
            //設定読み込み
            if (Properties.Settings.Default.rss_link != null)
            {
                SettingRSSLink.Text = Properties.Settings.Default.rss_link;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //保存
            Properties.Settings.Default.rss_link = SettingRSSLink.Text;
            Properties.Settings.Default.Save();
        }
    }
}
