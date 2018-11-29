using BLL;
using Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
    /// login.xaml 的交互逻辑
    /// </summary>
    public partial class login : Window
    {
        private DispatcherTimer IsGetClass = new DispatcherTimer();
        public login()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 关闭弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cansol_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //容器Grid
            this.Close();
            
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
           
            string ImageURL = string.Empty;
            try
            {
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "NewBarCode" + Common.MID + ".png"))
                {
                    ImageURL = BLL.LoginManage.GetImageURL("http://ies.acadsoc.com.cn/Box/BoxAPI.ashx?action=Box_PCGetWeChatCodeUrl&ApiKey=uNHgU29tygAPXMTKLPbhnVx454GcC4AhYOfBU4bBnd4%3D&MID=", Common.MID, "&Width=350");
                    var WebSteam = WebRequest.Create(ImageURL).GetResponse();
                    if (WebSteam.ContentLength <= 0)
                        MessageBox.Show("重新点击登录按钮，直到生成二维码，");

                    //休眠1秒
                    System.Threading.Thread.Sleep(1000);
                    System.IO.Stream facepicstream = WebSteam.GetResponseStream();
                    System.Drawing.Image bit = System.Drawing.Image.FromStream(facepicstream);

                    bit.Save(AppDomain.CurrentDomain.BaseDirectory + "NewBarCode" + Common.MID + ".png");
                    //picBarCode.Image = KTVBLL.Common.CreateQRCode("http://ies.acadsoc.com.cn/Box/PayReg.htm?MID=" + Common.MID);
                }
                else
                {
                    this.LoginCord.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "NewBarCode" + Common.MID + ".png", UriKind.RelativeOrAbsolute));
                }
            }
            catch
            {
            }

            IsGetClass.Tick += new EventHandler(IsGetClass_Tick);
            IsGetClass.Interval = new TimeSpan(0, 0, 1);
            IsGetClass.Start();
        }

        int Count = 40;
        private void IsGetClass_Tick(object sender, EventArgs e)
        {
            if (Count > 0)
            {
                Count--;
            }
            else
            {
                IsGetClass.Stop();
                this.Close();
            }
        }

    }
}
