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
using Models;
using BLL;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net;
using System.Reflection;
using Transitionals;
using System.Collections.ObjectModel;
using WPFSetVolume.VolumeHelper;

namespace TeachSing
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private DispatcherTimer ManageTime = new DispatcherTimer();

       public static bool StopTeachReadTime = false;
        System.Timers.Timer t = new System.Timers.Timer(1000);
        public static bool HelpFormClose = false;
        public static HelpGuide objHelp = null;
        public static FrmIsOut objOUT = null;
        public MainWindow()
        {
            InitializeComponent();

        }
        Common common = new Common();
        UsFirstPage firstPage = new UsFirstPage();
        UsFreeTalkList freeTalk = new UsFreeTalkList();
        UsTeachTalk teachTalk = new UsTeachTalk();
        UsPayMentList payMent = new UsPayMentList();
        UsWaitTalk waiteTalk = new UsWaitTalk();
        UsWaitTeach waiteTeach = new UsWaitTeach();
        TeachRead teachread = new TeachRead();
        UsReadPlay readyPlay = new UsReadPlay();
        UsMVplay mvplay = new UsMVplay();
        public delegate void delegateSendMsg(string msg);

        /// <summary>
        /// 加载首个页面
        /// </summary>
        public void AddFristPage()
        {
            firstPage.SendMsgEvent += new delegateSendMsg(this.ChangePage);
            freeTalk.SendMsgEvent += new delegateSendMsg(this.ChangePage);
            teachTalk.SendMsgEvent += new delegateSendMsg(this.ChangePage);
            payMent.SendMsgEvent += new delegateSendMsg(this.ChangePage);
            waiteTalk.SendMsgEvent += new delegateSendMsg(this.ChangePage);
            waiteTeach.SendMsgEvent += new delegateSendMsg(this.ChangePage);
            readyPlay.SendMsgEvent += new delegateSendMsg(this.ChangePage);
            mvplay.SendMsgEvent += new delegateSendMsg(this.ChangePage);
            this.ReturnPage.Visibility = Visibility.Hidden;
            Transitionals.Transitions.FadeAndBlurTransition t = new Transitionals.Transitions.FadeAndBlurTransition();
            mainp.Transition = t;
            mainp.Content = firstPage;
        }
        /// <summary>
        /// 购买套餐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuyTime_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (mainp.Content == firstPage)
            {
                ControlTime.WherePay = "2";
            }
            else
            {
                ControlTime.WherePay = "1";
            }
            Common.talkType = null;
            Common.songInfo = null;
            mainp.Content = payMent;
            //mainp.Content = mvplay;
            this.ReturnPage.Visibility = Visibility;
            BuyTime.Visibility = Visibility.Hidden;
            ReturnPage.SetValue(Grid.ColumnProperty, 15);
        }


        /// <summary>
        /// 帮助指引
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Help_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));

            objHelp = new HelpGuide();
            HelpFormClose = false;
            objHelp.ShowDialog();
            Loay.Background = null;
        }
        /// <summary>
        /// 返回上个页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Transitionals.Transitions.RollTransition t = new Transitionals.Transitions.RollTransition();
            mainp.Transition = t;
            switch (mainp.Content.ToString())
            {
                case "TeachSing.UsFreeTalkList":
                    ControlTime.WeChatBuyType = "0";
                    mainp.Content = firstPage;
                    this.BuyTime.Visibility = Visibility.Visible;
                    this.ReturnPage.Visibility = Visibility.Hidden;
                    Common.talkType = null;
                    break;
                case "TeachSing.UsTeachTalk":
                    ControlTime.WeChatBuyType = "0";
                    mainp.Content = firstPage;
                    this.BuyTime.Visibility = Visibility.Visible;
                    this.ReturnPage.Visibility = Visibility.Hidden;
                    Common.songInfo = null;
                    break;
                case "TeachSing.UsPayMentList":
                    payMent.PayTick.Stop();
                    // 判断是那种教学模式，，返回相应的上级窗体
                    if (ControlTime.WeChatBuyType == "2")
                    {
                        mainp.Content = freeTalk;
                    }
                    else if (ControlTime.WeChatBuyType == "1")
                    {
                        mainp.Content = teachTalk;
                    }
                    else
                    {
                        this.ReturnPage.Visibility = Visibility.Hidden;
                        mainp.Content = firstPage;
                    }
                    ReturnPage.SetValue(Grid.ColumnProperty, 13);
                    BuyTime.Visibility = Visibility;
                    break;
                case "TeachSing.UsWaitTalk":
                    // 判断是那种教学模式，，返回相应的上级窗体
                    Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
                    FailWaitTeach objFail = new FailWaitTeach("1");
                    StopTeachReadTime = false;
                    if (objFail.ShowDialog() == true)
                    {
                        waiteTalk.player.Stop();
                        waiteTalk.WaitTalkTick.Stop();
                        mainp.Content = freeTalk;
                        BuyTime.Visibility = Visibility;
                        ReturnPage.SetValue(Grid.ColumnProperty, 13);
                    }
                    else
                    {
                        StopTeachReadTime = true;
                    }
                    Loay.Background = null;
                  
                    break;
                case "TeachSing.UsWaitTeach":
                    // 判断是那种教学模式，，返回相应的上级窗体
                    Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
                    FailWaitTeach objFailT = new FailWaitTeach("1");
                    StopTeachReadTime = false;
                    if (objFailT.ShowDialog() == true)
                    {
                        waiteTeach.TeachTeachTick.Stop();
                        mainp.Content = teachTalk;
                        BuyTime.Visibility = Visibility;
                        ReturnPage.SetValue(Grid.ColumnProperty, 13);
                    }
                    else
                    {
                        StopTeachReadTime = true;
                    }
                    Loay.Background = null;
                    break;
                case "TeachSing.UsReadPlay":
                    // 判断是那种教学模式，，返回相应的上级窗体
                    mainp.Content = mvplay;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OUT_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Common.BID.Length > 0)
            {
                Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
                objOUT = new FrmIsOut();
                objOUT.ShowDialog();
                Loay.Background = null;
            }
            else
            {
                sasa_Click(null, null);
            }
        }
        /// <summary>
        /// 委托改变页面
        /// </summary>
        /// <param name="PageName"></param>
        public void ChangePage(string PageName)
        {
            PostionCount = 1;
            Transitionals.Transitions.FadeAndBlurTransition t = new Transitionals.Transitions.FadeAndBlurTransition();
            mainp.Transition = t;
            switch (PageName)
            {
                case "FristPage":
                    mainp.Content = firstPage;
                    this.ReturnPage.Visibility = Visibility.Hidden;
                    BuyTime.Visibility = Visibility.Visible;

                    break;
                case "FreeTalkList":
                    mainp.Content = freeTalk;
                    this.ReturnPage.Visibility = Visibility;
                    BuyTime.Visibility = Visibility.Visible;
                    choseModel.Content = "已选模式:自由交谈";
                    ReturnPage.SetValue(Grid.ColumnProperty, 13);
                    break;
                case "TeachTalkList":
                    mainp.Content = teachTalk;
                    this.ReturnPage.Visibility = Visibility;
                    BuyTime.Visibility = Visibility.Visible;
                    choseModel.Content = "已选模式:老师教唱";
                    ReturnPage.SetValue(Grid.ColumnProperty, 13);
                    break;
                case "PaymentList":
                    BLL.Common.doMyError(new Exception(), "执行了 委托跳转");
                    if (ControlTime.Totaltime > 0)
                    {
                        if (ControlTime.WeChatBuyType == "1")
                        {
                            mainp.Content = waiteTeach;
                        }
                        else
                        {
                            mainp.Content = waiteTalk;
                        }   

                        this.ReturnPage.Visibility = Visibility.Visible;
                        BuyTime.Visibility = Visibility.Hidden;
                        ReturnPage.SetValue(Grid.ColumnProperty, 15);
                    }
                    else
                    {
                        mainp.Content = payMent;
                        //mainp.Content = mvplay;
                        this.ReturnPage.Visibility = Visibility;
                        BuyTime.Visibility = Visibility.Hidden;
                        ReturnPage.SetValue(Grid.ColumnProperty, 15);
                    }

                    break;
                case "TalkWateTeach":
                    Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
                    BuySucces objBuySuss = new BuySucces();
                    objBuySuss.ShowDialog();
                    Loay.Background = null;
                    mainp.Content = waiteTalk;
                    this.ReturnPage.Visibility = Visibility.Visible;
                    BuyTime.Visibility = Visibility.Hidden;
                    ReturnPage.SetValue(Grid.ColumnProperty, 15);
                    break;
                case "teachWateTeach":
                    Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
                    BuySucces objBuySussed = new BuySucces();
                    objBuySussed.ShowDialog();
                    Loay.Background = null;
                    mainp.Content = waiteTeach;
                    this.ReturnPage.Visibility = Visibility.Visible;
                    BuyTime.Visibility = Visibility.Hidden;
                    ReturnPage.SetValue(Grid.ColumnProperty, 15);
                    break;

                case "goTalkList":
                    Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
                    BuySucces objBuySussE = new BuySucces();
                    objBuySussE.ShowDialog();
                    Loay.Background = null;
                    mainp.Content = freeTalk;
                    this.ReturnPage.Visibility = Visibility.Visible;
                    BuyTime.Visibility = Visibility.Visible;
                    ReturnPage.SetValue(Grid.ColumnProperty, 13);
                    break;
                case "goSongList":
                    Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
                    BuySucces objBuySussedA = new BuySucces();
                    objBuySussedA.ShowDialog();
                    Loay.Background = null;
                    mainp.Content = teachTalk;
                    this.ReturnPage.Visibility = Visibility.Visible;
                    BuyTime.Visibility = Visibility.Visible;
                    ReturnPage.SetValue(Grid.ColumnProperty, 13);
                    break;
                case "goForst":
                    Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
                    BuySucces objBuySussedd = new BuySucces();
                    objBuySussedd.ShowDialog();
                    Loay.Background = null;
                    mainp.Content = firstPage;
                    this.ReturnPage.Visibility = Visibility.Hidden;
                    BuyTime.Visibility = Visibility.Visible;
                    break;
                //case "TeachRead":  //吊起classIn
                //    //teachread.ChangePic();
                //    break;
                case "TeachOver":  //聊天关闭课程
                    ControlTime.CountPlayTime = false;
                    waiteTalk.WaitTalkTick.Stop();
                   

                    mainp.Content = firstPage;
                    BuyTime.Visibility = Visibility;
                    this.ReturnPage.Visibility = Visibility.Hidden;
                    ReturnPage.SetValue(Grid.ColumnProperty, 13);
                    ControlTime.IsCreatCl = ControlTime.OpenClassIn = false;
                    break;
                case "TeachSingOver":  //教唱关闭课程
                    waiteTeach.TeachTeachTick.Stop();
                    mainp.Content = readyPlay;
                    this.ReturnPage.Visibility = Visibility.Hidden;
                    ControlTime.IsCreatCl = ControlTime.OpenClassIn = false;
                    break;
                case "ReadyPlayGo":  //开始唱歌
                    mainp.Content = mvplay;
                    this.ReturnPage.Visibility = Visibility.Hidden;
                    this.OverPlay.Visibility = Visibility.Visible;
                    ReturnPage.SetValue(Grid.ColumnProperty, 13);
                    break;
                case "MVplayOver":  //结束

                    try
                    {
                        if (objHelp != null)
                        {
                            objHelp.Close();
                        }
                        if (objOUT != null)
                        {
                            objOUT.Close();
                        }
                    }
                    catch { }

                    ControlTime.CountPlayTime = false;
                    mainp.Content = firstPage;
                    this.ReturnPage.Visibility = Visibility.Hidden;
                    this.OverPlay.Visibility = Visibility.Hidden;
                    BuyTime.Visibility = Visibility.Visible;
                    break;
                case "needLogin":  //登录

                    sasa_Click(null, null);
                    break;

                case "BuyError":

                    Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
                    BuyError objBuyError = new BuyError();
                    objBuyError.ShowDialog();
                    Loay.Background = null;
                    break;

                //============
                // 判断是那种教学模式，，返回相应的上级窗体
                case "IsReturnList":  //是否返回到集合列表页面
                    //if (Loay.Background == null)
                    //{
                    try
                    {
                        if (objHelp != null)
                        {
                            objHelp.Close();
                        }
                        if (objOUT != null)
                        {
                            objOUT.Close();
                        }
                    }
                    catch { }
                        StopTeachReadTime = false;
                        Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
                        FailWaitTeach objFail = new FailWaitTeach("0");
                        if (objFail.ShowDialog() == true)
                        {
                            if (mainp.Content.ToString() == "TeachSing.UsWaitTalk")
                            {
                                waiteTalk.WaitTalkTick.Stop();
                               
                                mainp.Content = freeTalk;
                                BuyTime.Visibility = Visibility;
                                ReturnPage.SetValue(Grid.ColumnProperty, 13);
                            }
                            else
                            {
                                waiteTeach.TeachTeachTick.Stop();
                               
                                mainp.Content = teachTalk;
                                BuyTime.Visibility = Visibility;
                                ReturnPage.SetValue(Grid.ColumnProperty, 13);
                            }
                        }
                        else
                        {
                            StopTeachReadTime = true;
                        }
                        Loay.Background = null;
                        UsWaitTalk.Watiecount = 0;
                        UsWaitTeach.Wasitcount = 0;
                    //}
                    break;

                default:
                    break;
            }
        }

        #region 两个定时器分别控制业务跟时间显示以及倒计时

        int teacCou = 0;
        /// <summary>
        /// 整个业务处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManageTime_Tick(object sender, EventArgs e)
        {
            //【1】当前时间显示
            // this.ShowNowTime.Content = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
            //【2】是否登陆成功
            if (Common.BID.Length > 1 && Common.UID.Length > 1)
            {
                player.Stop();
                if (ShowNanme.Content == "请先登录")
                {
                    HeadImg.Source = new BitmapImage(new Uri(ControlTime.WeChatHeadPic, UriKind.RelativeOrAbsolute));
                    ShowNanme.Content = ControlTime.WeChatName;
                    OutBtn.Source = new BitmapImage(new Uri(@"Image\Head\退出.png", UriKind.RelativeOrAbsolute));
                }
                if (objLogin != null)
                {
                    objLogin.Close();
                }

                //【6】是否接课



                //是否有老师确认 【老师接课，但未进入教室】
                if (Common.bcrid.Length > 0&& StopTeachReadTime)
                {
                    setControl("InitContent", null);
                }
            }
            else
            {
                player.Play();
                if (ControlTime.GoHome)
                {
                    Transitionals.Transitions.FadeAndBlurTransition t = new Transitionals.Transitions.FadeAndBlurTransition();
                    mainp.Transition = t;
                    mainp.Content = firstPage;
                    BuyTime.Visibility = Visibility;
                    this.ReturnPage.Visibility = Visibility.Hidden;
                    ControlTime.GoHome = false;
                }
                string result = LoginManage.GetNewBoxUser(Common.ApiUrl, Common.ApiKey, Common.MID);
                Common.BID = result.Split('*')[0].ToString();
                Common.UID = result.Split('*')[1].ToString();
                Common.OpenID = result.Split('*')[2].ToString();
                ControlTime.WeChatHeadPic = result.Split('*')[3].ToString();
                ControlTime.WeChatName = result.Split('*')[4].ToString();
                //【3】Main业务（微信头像名称）
                if (Common.BID.Length > 0)
                {
                    HeadImg.Source = new BitmapImage(new Uri(ControlTime.WeChatHeadPic, UriKind.RelativeOrAbsolute));
                    ShowNanme.Content = ControlTime.WeChatName;
                    OutBtn.Source = new BitmapImage(new Uri(@"Image\Head\退出.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    HeadImg.Source = new BitmapImage(new Uri(@"Image\Head\默认头像.png", UriKind.RelativeOrAbsolute));
                    ShowNanme.Content = "请先登录";
                    OutBtn.Source = new BitmapImage(new Uri(@"Image\Head\登录.png", UriKind.RelativeOrAbsolute));
                }
            }


            if (teacCou == 10)
            {
                setControl("TeachMun", null);
                teacCou = 0;
            }

            teacCou++;
        }


        int NetWord = 1;
        /// <summary>
        /// 左上角时间显示，剩余套餐时间显示，套餐使用完退出系统
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void theout(object source, System.Timers.ElapsedEventArgs e)
        { //【1】当前时间显示
            setControl("onesecondtime", null);

            if (PostionCount % 180 == 0 || (PostionCount % 90 == 0 && Common.BID.Length==0))
            {
                setControl("OutTimeVoid", null);
            }
            PostionCount++;
            if (!BLL.Common.CheckForInternetConnection(Common.PingURL) && ++NetWord%5==0)
            {
                if (NetWord == 5)
                {
                    setControl("NETError", null);
                }
                NetWord = 1; 
            }
        }

        #endregion


        #region 滑动控制声音大小

        bool isVolumeChange = false;

        private void VolumeChange()
        {
            isVolumeChange = true;
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                //cbkIsMute.IsChecked = VolumeHelper.VolumeHelper.IsMute();
                //tbVolume.Text = VolumeHelper.VolumeHelper.GetVolume().ToString();
                textNum.Content = VolumeHelper.GetVolume().ToString();
                slVolume.Value = WPFSetVolume.VolumeHelper.VolumeHelper.GetVolume();
            }));
            isVolumeChange = false;
        }

        private void slVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (isVolumeChange)
            {
                return;
            }
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                WPFSetVolume.VolumeHelper.VolumeHelper.SetVolume((int)slVolume.Value);
                //tbVolume.Text = ((int)slVolume.Value).ToString();
                textNum.Content = ((int)slVolume.Value).ToString();
            }));
        }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Initialized(object sender, EventArgs e)
        {
            ManageTime.Tick += new EventHandler(ManageTime_Tick);
            ManageTime.Interval = new TimeSpan(0, 0, 1);
            ManageTime.Start();

            WPFSetVolume.VolumeHelper.VolumeHelper.Init();
            WPFSetVolume.VolumeHelper.VolumeHelper.AddVolumeChangeNotify(VolumeChange);
            VolumeChange();
            AddFristPage();


            player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + "背景音乐//bgMusic.mp3", UriKind.Relative));
            player.MediaEnded += (Sender, E) =>
            {//播放结束后 又重新播放
                player.Position = new TimeSpan(0);

            };

            player.Play();
        }

        /// <summary>
        /// 点击显示登录
        /// </summary>
        login objLogin;
        private void sasa_Click(object sender, RoutedEventArgs e)
        {
            Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
            objLogin = new login();
            objLogin.ShowDialog();
            Loay.Background = null;

        }


        #region 委托动态改变UI页面的内容

        /// <summary>
        /// 调用的所有方法
        /// </summary>
        /// <param name="method"></param>
        /// <param name="obj"></param>
        private delegate void SetControlCallback(string method, object obj);

        private void setControl(string method, object obj)
        {
            if (this.Dispatcher.CheckAccess())
            {
                if (method.Equals("InitContent"))
                {
                    if (GetClassManage.BoxIsClassRecord(Common.ApiUrl, Common.ApiKey, Common.BID, Common.bcrid))
                    {
                        if (!ControlTime.ComeingClass)
                        {
                            ControlTime.ComeingClass = true;
                            try
                            {
                                if (!HelpFormClose)
                                {
                                    HelpFormClose = !HelpFormClose;
                                    objHelp.Close();
                                }
                            }
                            catch { }
                            //===============================================
                            //==============老师接课=========================
                            //===============================================
                            waiteTalk.player.Stop();
                            Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
                            TeachRead objTeachRead = new TeachRead();
                            objTeachRead.ShowDialog();
                            Loay.Background = null;
                            StopTeachReadTime = false;
                        }
                    }
                }
                else if (method.Equals("TeachMun"))
                {
                    int s = PayManage.GetTeachNum(Common.ApiUrl, Common.ApiKey);
                    TeachLine.Content = s.ToString();
                }

                else if (method.Equals("onesecondtime"))
                {
                    this.ShowNowTime.Content = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");

                    //【5】是否使用完套餐
                    if (ControlTime.Totaltime > 0)
                    {
                        welcome.Visibility = Visibility.Hidden;
                        choseModel.Visibility = Visibility.Visible;
                        OverTime.Visibility = Visibility.Visible;
                        OverTime.Content = "套餐剩余时间:" + BLL.Common.TransTimeSecondIntToString(ControlTime.Totaltime);
                    }
                    else
                    {
                        welcome.Visibility = Visibility.Visible;
                        choseModel.Visibility = Visibility.Hidden;
                        OverTime.Visibility = Visibility.Hidden;
                        if (Common.BID.Length > 0 && ControlTime.CountPlayTime)
                        {
                            BLL.LoginManage.LoginOut(Common.ApiUrl, Common.ApiKey, Common.BID, DateTime.Now, Common.MID);
                            Common.BID = "";
                            ControlTime.WeChatName = "";
                            ControlTime.WeChatHeadPic = null;
                            ControlTime.GoHome = true;
                            EndClassIn();
                            Application.Current.Shutdown();
                        }
                    }

                    if (ControlTime.CountPlayTime && ControlTime.Totaltime > 0)
                    {
                        ControlTime.Totaltime--;
                        if(ControlTime.Totaltime==120)
                        {
                            Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));

                            ComingEnd objComingEnd = new ComingEnd();
                            objComingEnd.ShowDialog();
                            Loay.Background = null;
                        }
                    }

                }

                else if (method.Equals("OutTimeVoid"))
                {
                    if (mainp.Content.ToString() == "TeachSing.UsFreeTalkList" || mainp.Content.ToString() == "TeachSing.UsTeachTalk" || mainp.Content.ToString() == "TeachSing.UsPayMentList" || mainp.Content.ToString() == "TeachSing.UsFirstPage" || mainp.Content.ToString() == "TeachSing.UsReadPlay")
                    {
                        try
                        {
                            if (!HelpFormClose)
                            {
                                HelpFormClose = !HelpFormClose;
                                objHelp.Close();
                            }
                        }
                        catch { }
                        if (Common.BID.Length > 0)
                        {
                            //===========添加是否还在设备前提示==================
                            Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
                            TimeoutReminder objTimeRe = new TimeoutReminder();

                            if (objTimeRe.ShowDialog() == true)
                            {
                                BLL.LoginManage.LoginOut(Common.ApiUrl, Common.ApiKey, Common.BID, DateTime.Now, Common.MID);
                                Common.BID = "";
                                ControlTime.WeChatName = "";
                                ControlTime.WeChatHeadPic = null;
                                ControlTime.GoHome = true;
                                EndClassIn();
                                Application.Current.Shutdown();
                            }
                            Loay.Background = null;
                        }
                        else
                        {
                            mainp.Content = firstPage;
                            this.ReturnPage.Visibility = Visibility.Hidden;
                            this.BuyTime.Visibility = Visibility.Visible;
                            banner.Visibility = Visibility;
                        }
                        
                    }

                }
                else if(method.Equals("NETError"))
                {
                    if(!ControlTime.NetError)
                    {
                        Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
                        ControlTime.NetError = true;
                        NetError objNetError = new NetError();
                        objNetError.ShowDialog();
                        ControlTime.NetError = false;
                        Loay.Background = null;
                    }
                }
            }
            else
            {
                SetControlCallback d = new SetControlCallback(setControl);
                this.Dispatcher.BeginInvoke(d, new object[] { method, obj });
            }
        }

        #endregion


        private void OverPlay_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ControlTime.CountPlayTime = false;
            mainp.Content = firstPage;
            this.OverPlay.Visibility = Visibility.Hidden;
            BuyTime.Visibility = Visibility.Visible;
         
            Common.songInfo = null;
        }

        MediaPlayer player = new MediaPlayer();
        private ObservableCollection<Type> transitionTypes = new ObservableCollection<Type>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTransitions(Assembly.GetAssembly(typeof(Transition)));
            t.Elapsed += new System.Timers.ElapsedEventHandler(theout);
            t.AutoReset = true;
            t.Enabled = true;
            t.Start();
           
        }
        public void LoadTransitions(Assembly assembly)
        {

            foreach (Type type in assembly.GetTypes())
            {
                // Must not already exist
                if (transitionTypes.Contains(type)) { continue; }

                // Must not be abstract.
                if ((typeof(Transition).IsAssignableFrom(type)) && (!type.IsAbstract))
                {
                    transitionTypes.Add(type);
                }
            }
        }


        private static void EndClassIn()
        {
            if (Common.frmShelterUp != null)
            {
                Common.frmShelterUp.Close();
            }
            if (Common.frmShelterDown != null)
            {
                Common.frmShelterDown.Close();
            }
            if (Common.frmShelterRight != null)
            {
                Common.frmShelterRight.Close();
            }

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    Process[] pros = Process.GetProcessesByName("ClassIn");
                    if (pros.Length > 0)
                    {
                        try
                        {
                            pros[0].Kill();
                        }
                        catch
                        {
                            pros[0].CloseMainWindow();
                        }
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                catch 
                {

                }
            }

        }

        Point LastPostion = new Point();
        Point NowPostion = new Point();
        int PostionCount = 1;
        /// <summary>
        /// 获取位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NowPostion = Mouse.GetPosition(e.Source as FrameworkElement);
           // postion.Content = string.Format("X:{0}   Y:{1}", pp.X, pp.Y);

            if (NowPostion != LastPostion)
            {
                LastPostion = NowPostion;
                PostionCount = 1;
            }
        }

        private void banner_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            banner.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 固定登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeadImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                //双击时执行
                string bid = BLL.Common.GetKTVConfig("BID");
                BLL.LoginManage.UserLogin(Common.ApiUrl, Common.ApiKey, Common.MID, bid);
            }
            
        }
       
    }

}
