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
    /// FailWaitTeach.xaml 的交互逻辑
    /// </summary>
    public partial class FailWaitTeach : Window
    {
        private DispatcherTimer CloseThis = new DispatcherTimer();
        public FailWaitTeach(string text)
        {
            InitializeComponent();
            if(text=="1")
            {
               background.Source= new BitmapImage(new Uri(@"Image\PopupImage\isback.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                background.Source = new BitmapImage(new Uri(@"Image\PopupImage\failwaitTeach.png", UriKind.RelativeOrAbsolute));
            }
        }

        //继续等待
        private void wait_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CloseThis.Stop();
            this.Close();
        }

        //返回上一级
        private void return_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //取消课程
            if (Common.bcrid != "")
            {

                bool result = BLL.GetClassManage.BoxCancelCourse(Common.ApiUrl, Common.ApiKey, Common.BID, Common.bcrid);
                if (result)
                {
                    DialogResult = true;

                    CloseThis.Stop();
                    this.Close();
                }
                else
                {
                    lblTime.Content = "取消失败";
                    this.Close();
                }
            }
            else
            {
                DialogResult = true;

                CloseThis.Stop();
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CloseThis.Tick += new EventHandler(CloseThis_Tick);
            CloseThis.Interval = new TimeSpan(0, 0, 1);
            CloseThis.Start();
            count = 0;
        }
        int count = 0;
        private void CloseThis_Tick(object sender, EventArgs e)
        {
            count++;
            if (count > 30)
            {
                if (!ControlTime.ComeingClass)
                {
                    DialogResult = true;
                }
                CloseThis.Stop();
                if (Common.bcrid != "")
                {
                    BLL.GetClassManage.BoxCancelCourse(Common.ApiUrl, Common.ApiKey, Common.BID, Common.bcrid);
                }
                this.Close();
            }
        }
    }
}
