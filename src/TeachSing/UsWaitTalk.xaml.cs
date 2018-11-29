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
using System.Windows.Threading;
using BLL;
using System.Diagnostics;
using Models;

namespace TeachSing
{
    /// <summary>
    /// UsWaitTalk.xaml 的交互逻辑
    /// </summary>
    public partial class UsWaitTalk : UserControl
    {
        public DispatcherTimer WaitTalkTick = new DispatcherTimer();
        public event TeachSing.MainWindow.delegateSendMsg SendMsgEvent;

        public static int Watiecount = 1;
        public MediaPlayer player = new MediaPlayer();

        public UsWaitTalk()
        {
            InitializeComponent();
        }

        //【1 创建课程】
        private void SetClass()//d(string ApiUrl, string ApiKey, string BID, string TopicType, int CourseType, string MID, int SurplusTime)
        {
            if (!ControlTime.IsCreatCl)
            {
                player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + "背景音乐//BackMusic.mp3", UriKind.Relative));
                player.MediaEnded += (Sender, E) =>
                {//播放结束后 又重新播放
                    player.Position = new TimeSpan(0);

                };
                player.Play();
                ControlTime.IsCreatCl = true;
                Common.bcrid = GetClassManage.BoxNewCourseClassRecord(Common.ApiUrl, Common.ApiKey,"", Common.BID, Common.talkType.TopicID.ToString(), 2, Common.MID, 20);
                MainWindow.StopTeachReadTime = true;
            }
        }
        bool Locakwin = false;
        private void timecout_Tick(object sender, EventArgs e)
        {         
            Watiecount++;
            if(ControlTime.ComeingClass)
            {
                player.Stop();
            }
            this.lblTime.Content = "已用时  " + BLL.Common.TransTimeSecondIntToString(Watiecount);
            if (ControlTime.IsWaiteTeach)
            {
                WaitTalkTick.Stop();
                SendMsgEvent("TeachOver");
            }
          
            if(Watiecount<120)
            {
                Locakwin = false;
            }
            if(!ControlTime.ComeingClass && Watiecount > 120 && !Locakwin)
            {
                player.Stop();
                SendMsgEvent("IsReturnList");
                Locakwin = true;
            }
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            WaitTalkTick.Tick += new EventHandler(timecout_Tick);
            WaitTalkTick.Interval = new TimeSpan(0, 0, 1);
            ControlTime.IsCreatCl = false;
        }
        string imagePath;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetClass();
           
            WaitTalkTick.Start();
            Watiecount = 0;
            type.Content = Common.talkType.TopicName;
            PageIndex = 2;
            imagePath = "Image/books/" + Common.talkType.TopicName + "/幻灯片2.png";
            book.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
            changebtn();
        }
        int PageIndex = 2;
        private void Uppage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (PageIndex > 2)
            {
                PageIndex--;
                imagePath = "Image/books/" + Common.talkType.TopicName + "/幻灯片"+PageIndex+".png";
                book.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
            }
            changebtn();
        }

        private void Downpage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (PageIndex <6)
            {
                PageIndex++;
                imagePath = "Image/books/" + Common.talkType.TopicName + "/幻灯片" + PageIndex + ".png";
                book.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
            }

            changebtn();
        }

        private void changebtn()
        {
            if (PageIndex == 2)
            {
                this.Uppage.Source = new BitmapImage(new Uri("Image/TeachTalk/left0.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                this.Uppage.Source = new BitmapImage(new Uri("Image/TeachTalk/left1.png", UriKind.RelativeOrAbsolute));
            }
            if (PageIndex == 6)
            {
                this.Downpage.Source = new BitmapImage(new Uri("Image/TeachTalk/right0.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                this.Downpage.Source = new BitmapImage(new Uri("Image/TeachTalk/right1.png", UriKind.RelativeOrAbsolute));
            }
            this.lblPage.Content = PageIndex-1 + "/5";
        }

    }
}
