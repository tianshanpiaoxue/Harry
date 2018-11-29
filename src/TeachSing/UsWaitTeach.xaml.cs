using BLL;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TeachSing
{
    /// <summary>
    /// UsWaitTeach.xaml 的交互逻辑
    /// </summary>
    public partial class UsWaitTeach : UserControl
    {
        bool play_flag = true;  //判断播放状态
       public static  int Wasitcount = 0;
        public event TeachSing.MainWindow.delegateSendMsg SendMsgEvent;
        public  DispatcherTimer TeachTeachTick = new DispatcherTimer();

        public UsWaitTeach()
        {
            InitializeComponent();
        }

        private void QS_Movie_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> Movie_Uri = new List<string>();
            // Movie_Uri.Add("F:/MV/Shape of My Heart.mp4");
            Movie_Uri.Clear();
            Movie_Uri.Add(Common.pathSong + "" + Common.songInfo.Title + ".mp4");
            QS_Movie.Source = new Uri(Movie_Uri[0], UriKind.RelativeOrAbsolute);
            QS_Movie.Play();

        }
        /// <summary>
        /// MV
        /// 播放  暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QS_Movie_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (play_flag == true)
            {
                QS_Movie.Play();
            }
            if (play_flag == false)
            {
                QS_Movie.Pause();
            }
            play_flag = !play_flag;
        }

        //【1 创建课程】
        private void SetClass()//d(string ApiUrl, string ApiKey, string BID, string TopicType, int CourseType, string MID, int SurplusTime)
        {
            if (!ControlTime.IsCreatCl)
            {
                ControlTime.IsCreatCl = true;
                Common.bcrid = GetClassManage.BoxNewCourseClassRecord(Common.ApiUrl, Common.ApiKey, Common.songInfo.ID, Common.BID, "0", 1, Common.MID, 20);
                MainWindow.StopTeachReadTime = true;
            }
        }
        bool Locakwin = false;
        private void timecout_Tick(object sender, EventArgs e)
        {
            Wasitcount++;
            this.lblTime.Content = "已用时  " + BLL.Common.TransTimeSecondIntToString(Wasitcount);
            if (ControlTime.IsWaiteTeach) 
            {
                TeachTeachTick.Stop();
                SendMsgEvent("TeachSingOver");
            }
            if(ControlTime.ComeingClass)
            {
                QS_Movie.Pause();
            }
            
            if (Wasitcount < 120)
            {
                Locakwin = false;
            }
            if (!ControlTime.ComeingClass && Wasitcount > 120 && !Locakwin)    //
            { 
                SendMsgEvent("IsReturnList");
                Locakwin = true;
            }
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            TeachTeachTick.Tick += new EventHandler(timecout_Tick);
            TeachTeachTick.Interval = new TimeSpan(0, 0, 1);
            ControlTime.IsCreatCl = false;
            QS_Movie.MediaEnded += (Sender, E) =>
            {//播放结束后 又重新播放
                QS_Movie.Position = new TimeSpan(0);
            };
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetClass();
            TeachTeachTick.Start();
            Wasitcount = 0;
        }
    }
}
