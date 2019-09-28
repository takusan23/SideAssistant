using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SideAssistant
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        Content.RSSParser rss = new Content.RSSParser();

        public MainWindow()
        {
            InitializeComponent();

            this.MouseLeftButtonDown += (sender, e) => this.DragMove();

            RSSListView.ItemsSource = rss.list;

            setTimer();

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            //閉じるボタン
            Close();
        }

        private void MiniWindowButton_Click(object sender, RoutedEventArgs e)
        {
            //最小化ボタン
            WindowState = WindowState.Minimized;
        }

        private void setTimer()
        {
            //時間設定
            //初期化、普通にする際はプロパティはNormalでよいかと
            var dispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal);
            //左から　日数、時間、分、秒、ミリ秒で設定　今回は10ミリ秒ごとつまり1秒あたり100回処理します
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1); //間隔
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //時刻設定
            var dateTime = DateTime.Now;
            ClockTextBlock.Text = dateTime.ToString("HH:mm:ss");
            DateTextBlock.Text = dateTime.ToString("MM/dd dddd");

            //電池
            setBattery();

            setCPU();
            setRAM();

        }

        private void RSSLoadButton_Click(object sender, RoutedEventArgs e)
        {
            //RSS読み込み
            rss.loadRSS();
        }



        private string getBatteryLife(int second)
        {
            var minute = second / 60;
            var finalSecond = second % 60;
            var hour = minute / 60;
            minute = minute % 60;
            return hour + ":" + minute + ":" + finalSecond;
        }

        private void setCPU()
        {
            //CPU使用率取得
            Task.Run(() =>
            {
                var pfcounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                float cpu = pfcounter.NextValue();
                Thread.Sleep(1000);
                cpu = pfcounter.NextValue();
                this.Dispatcher.Invoke(() =>
                {
                    CPUUseTextBlock.Text = (int)cpu + "%";
                });
            });

        }



        private void setRAM()
        {
            //メモリ使用量
            var useRAM = Environment.WorkingSet / (1024 * 1024);
            RAMUseTextBlock.Text = useRAM.ToString().Insert(useRAM.ToString().Length - 1, ".") + "GB";
        }


        private void setBattery()
        {
            //電池残量
            var level = SystemInformation.PowerStatus.BatteryLifePercent;
            BatteryLevelTextBlock.Text = level.ToString() + "%";
            var life = SystemInformation.PowerStatus.BatteryLifeRemaining;
            BatteryLifeTextBlock.Text = "残り " + getBatteryLife(life);

            //電池のアイコンも変える
            var batteryStatus = SystemInformation.PowerStatus.BatteryChargeStatus;
            if (level >= 100)
            {
                if (batteryStatus == BatteryChargeStatus.Charging)
                {
                    BatteryIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.BatteryCharging100;
                }
                else
                {
                    BatteryIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Battery100;
                }
            }
            else if (level >= 90)
            {
                if (batteryStatus == BatteryChargeStatus.Charging)
                {
                    BatteryIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.BatteryCharging90;
                }
                else
                {
                    BatteryIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Battery90;
                }
            }
            else if (level >= 80)
            {
                if (batteryStatus == BatteryChargeStatus.Charging)
                {
                    BatteryIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.BatteryCharging80;
                }
                else
                {
                    BatteryIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Battery80;
                }
            }
            else if (level >= 70)
            {
                if (batteryStatus == BatteryChargeStatus.Charging)
                {
                    BatteryIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.BatteryCharging70;
                }
                else
                {
                    BatteryIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Battery70;
                }
            }
            else if (level >= 60)
            {
                if (batteryStatus == BatteryChargeStatus.Charging)
                {
                    BatteryIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.BatteryCharging60;
                }
                else
                {
                    BatteryIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Battery60;
                }
            }
            else if (level >= 50)
            {
                if (batteryStatus == BatteryChargeStatus.Charging)
                {
                    BatteryIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.BatteryCharging50;
                }
                else
                {
                    BatteryIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Battery50;
                }
            }
            else if (level >= 40)
            {
                if (batteryStatus == BatteryChargeStatus.Charging)
                {
                    BatteryIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.BatteryCharging40;
                }
                else
                {
                    BatteryIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Battery40;
                }
            }
            else if (level >= 30)
            {
                if (batteryStatus == BatteryChargeStatus.Charging)
                {
                    BatteryIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.BatteryCharging30;
                }
                else
                {
                    BatteryIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Battery30;
                }
            }
            else if (level >= 20)
            {
                if (batteryStatus == BatteryChargeStatus.Charging)
                {
                    BatteryIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.BatteryCharging20;
                }
                else
                {
                    BatteryIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Battery20;
                }

            }
            else if (level >= 10)
            {
                if (batteryStatus == BatteryChargeStatus.Charging)
                {
                    BatteryIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.BatteryCharging10;
                }
                else
                {
                    BatteryIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Battery10;
                }
            }
        }



        private void ListViewTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var textBlock = sender as TextBlock;
            System.Diagnostics.Process.Start(textBlock.Tag.ToString());
        }

        private void SettingsWindowButton_Click(object sender, RoutedEventArgs e)
        {
            var settingWindow = new SettingsWindow();
            settingWindow.Show();
        }
    }
}
