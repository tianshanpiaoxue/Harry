using Models;
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
using System.Windows.Threading;

namespace TeachSing
{
    /// <summary>
    /// ShelterRight.xaml 的交互逻辑
    /// </summary>
    public partial class ShelterRight : Window
    {
        private DispatcherTimer IsGetClass = new DispatcherTimer();
        public ShelterRight()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Top = 1080-680;
            this.Left = 1920 - 179;
            this.Topmost = true;
            IsGetClass.Tick += new EventHandler(IsGetClass_Tick);
            IsGetClass.Interval = new TimeSpan(0, 0, 1);
            IsGetClass.Start();
        }
        private void IsGetClass_Tick(object sender, EventArgs e)
        {
            timeshow.Content = BLL.Common.TransTimeSecondIntToString(ControlTime.Totaltime);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IsGetClass.Stop();
        }


    }
}
