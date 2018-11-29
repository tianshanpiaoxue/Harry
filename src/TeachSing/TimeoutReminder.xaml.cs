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
    /// TimeoutReminder.xaml 的交互逻辑
    /// </summary>
    public partial class TimeoutReminder : Window
    {
        private DispatcherTimer IsGetClass = new DispatcherTimer();
        public TimeoutReminder()
        {
            InitializeComponent();
        }
        int Count = 30;
        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsGetClass.IsEnabled = false;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IsGetClass.Tick += new EventHandler(IsGetClass_Tick);
            IsGetClass.Interval = new TimeSpan(0, 0, 1);
            IsGetClass.Start();
            Count = 29;
        }
        private void IsGetClass_Tick(object sender, EventArgs e)
        {
            if (Count > 0)
            {
                lblTalk.Content = Count + "s";
                Count--;
            }
            else
            {
                IsGetClass.Stop();
                setControl("InitContent", null);
                this.Close();
            }
        }

         private delegate void SetControlCallback(string method, object obj);

         private void setControl(string method, object obj)
         {
             if (this.Dispatcher.CheckAccess())
             {
                 if (method.Equals("InitContent"))
                 {
                     this.DialogResult = true;
                 }

             }
             else
             {
                 SetControlCallback d = new SetControlCallback(setControl);
                 this.Dispatcher.BeginInvoke(d, new object[] { method, obj });
             }
         }
    }
}
