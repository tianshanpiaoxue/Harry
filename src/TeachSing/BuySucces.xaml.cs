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
    /// BuySucces.xaml 的交互逻辑
    /// </summary>
    public partial class BuySucces : Window
    {
        private DispatcherTimer IsGetClass = new DispatcherTimer();
        public BuySucces()
        {
            InitializeComponent();
            this.Topmost = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IsGetClass.Tick += new EventHandler(IsGetClass_Tick);
            IsGetClass.Interval = new TimeSpan(0, 0, 1);
            IsGetClass.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IsGetClass.Stop();
        }
        int time = 3;
        private void IsGetClass_Tick(object sender, EventArgs e)
        {
            if (--time < 0)
            {
                this.Close();
            }
            else
            {
                WifeError.Content = time + "S 后自动关闭";
            }
        }
    }
}
