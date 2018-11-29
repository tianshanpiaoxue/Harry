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
using Models;
using System.Collections.ObjectModel;
using System.Windows.Media.Animation;
using System.Threading;
using System.Windows.Threading;

namespace TeachSing
{
    /// <summary>
    /// UsMVplay.xaml 的交互逻辑
    /// </summary>
    public partial class UsMVplay : UserControl
    {
        public UsMVplay()
        {
            InitializeComponent();
        }

        private DispatcherTimer ShowTime = new DispatcherTimer();
        public event TeachSing.MainWindow.delegateSendMsg SendMsgEvent;
        private int i = 0, n = 0;
        //右边歌词集合
        private List<Itemdata> idata = new List<Itemdata>(); 
        private ObservableCollection<Itemdata> otherdata = new ObservableCollection<Itemdata>();
        private Storyboard storyBoard;
        System.Threading.Timer _timer;

        public static Models.Music music;
        delegate void UpdateTimer();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!ControlTime.IsPlayMV)
            {
                ControlTime.IsPlayMV = true;
                if (_timer!=null)
                _timer.Dispose();
                i = 0; n = 0;
                SongName.Content = Common.songInfo.Title;
                music = BLL.PlayMVManage.GetBoxKyxPracticeSentencesList(Common.ApiUrl, Common.ApiKey, Common.BID, Common.songInfo.ID);
                var sorted = music.MusicPeriodList.OrderBy(x => x.StartTime); //OrderBy(x => x.Age);
           
                idata.Clear();
                foreach (var u in sorted)
                {
                    idata.Add(new Itemdata()
                    {
                        ent = u.Sentences,
                        timespan = Convert.ToInt32(Convert.ToDouble(u.EndTime))

                    });
                }
                startStoryboard();
            }
        }

        private void QS_Movie_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> Movie_Uri = new List<string>();
            // Movie_Uri.Add("F:/MV/Shape of My Heart.mp4");
            Movie_Uri.Clear();
            Movie_Uri.Add(Common.pathSong + "" + Common.songInfo.Title + ".mp4");
            QS_Movie.Source = new Uri(Movie_Uri[0], UriKind.RelativeOrAbsolute);
            QS_Movie.Stop();
            btnplay.Visibility = Visibility.Visible;
        }
        void startStoryboard()
        {
            GridPrev.DataContext = null;
            GridNow.DataContext = idata[i];
            if (idata.Count > 1)
            {
                GridNext.DataContext = idata[i + 1];
            }
            else
            {
                GridNext.DataContext = null;
            }
            for (int j = 2; j < idata.Count; j++)
            {
                otherdata.Add(idata[j]);
            }
            ItemsControl1.ItemsSource = otherdata;
            _timer = new System.Threading.Timer(new TimerCallback(UpdatetimerDelegate));

            storyBoard = this.FindResource("Storyboard1") as Storyboard;

            
           
        }

        /// <summary>
        /// 时间检测
        /// </summary>
        /// <param name="state"></param>
        void UpdatetimerDelegate(object state)
        {
            if (idata.Count - i > 0)
            {
                if (n >= Convert.ToInt32(idata[i].timespan))
                {
                    i++;
                    this.Dispatcher.BeginInvoke(new UpdateTimer(StoryBoardFunc));
                }
                n++;
            }
        }

        void StoryBoardFunc()
        {
            storyBoard.Begin();
        }
        private void Storyboard1_OnCompleted(object sender, EventArgs e)
        {
            
            switch (idata.Count - i)
            {
                case 0:
                    GridPrev.DataContext = idata[i - 1];
                    GridNow.DataContext = null;
                    GridNext.DataContext = null;

                    _timer.Dispose();
                    break;
                case 1:
                    GridPrev.DataContext = idata[i - 1];
                    GridNow.DataContext = idata[i];
                    GridNext.DataContext = null;
                    break;
                default:
                    GridPrev.DataContext = idata[i - 1];
                    GridNow.DataContext = idata[i];
                    GridNext.DataContext = idata[i + 1];
                    break;
            }
            if (otherdata.Count > 0)
            {
                otherdata.Remove(otherdata[0]);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            webbro.InvokeScript("getrefText", new object[] { (string)"" });


            _timer.Change(0, 1000);
            QS_Movie.Play();
            ShowTime.Tick += new EventHandler(ShowTime_Tick);
            ShowTime.Interval = new TimeSpan(0, 0, 0,0,300);
            ShowTime.Start();
            btnplay.Visibility = Visibility.Hidden;
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
           
           
        }

        private void ShowTime_Tick(object sender, EventArgs e)
        {
            progressbar.Value = QS_Movie.Position.TotalSeconds;
        }

        private void QS_Movie_MediaOpened(object sender, RoutedEventArgs e)
        {
            progressbar.Maximum = QS_Movie.NaturalDuration.TimeSpan.TotalSeconds;
            
        }

        private void QS_Movie_MediaEnded(object sender, RoutedEventArgs e)
        {
            QS_Movie.Stop();
            ShowTime.Stop();
            SendMsgEvent("MVplayOver");
            Common.songInfo = null;
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            progressbar.Value = 0;
        }
        
    }


    public class Itemdata
    {
        public string ent { get; set; }
        public int timespan { get; set; }
    }
}
