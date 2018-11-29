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
    /// NetError.xaml 的交互逻辑
    /// </summary>
    public partial class NetError : Window
    {
        private DispatcherTimer IsGetClass = new DispatcherTimer();
        public NetError()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IsGetClass.Tick += new EventHandler(IsGetClass_Tick);
            IsGetClass.Interval = new TimeSpan(0, 0, 3);
            IsGetClass.Start();
        }
        private void IsGetClass_Tick(object sender, EventArgs e)
        {
          if(BLL.Common.CheckForInternetConnection(Common.PingURL))
          {
              IsGetClass.IsEnabled = false;
              this.Close();
          }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IsGetClass.IsEnabled = false;
        }
    }
}
