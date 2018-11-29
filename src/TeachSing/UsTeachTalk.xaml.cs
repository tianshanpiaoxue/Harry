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
using BLL;
using Models;
using System.Windows.Controls.Primitives;
using System.Threading;

namespace TeachSing
{
    /// <summary>
    /// UsTeachtalk.xaml 的交互逻辑
    /// </summary>
    public partial class UsTeachTalk : UserControl
    {
        SongList SongListManage = new SongList();
        public event TeachSing.MainWindow.delegateSendMsg SendMsgEvent;


        List<SongInfo> SongList = new List<SongInfo>();
        public static List<SongInfo> SelectedList = new List<SongInfo>();//当前页面的歌曲集合
        public static int PageIndex = 0;
        public static int PageCount = 12;
        int MaxIndex;
        public UsTeachTalk()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 选择类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Type_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Label)
            {
                lblType1.Foreground = lblType2.Foreground = lblType3.Foreground = lblType4.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
                var d = sender as Label;
                d.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                string s = d.Name.Substring(7, d.Name.Length - 7);
                huakuai.SetValue(Grid.ColumnProperty, Convert.ToInt32(s));
                if (s == "4")
                {
                    s = "-1";
                }
                else
                {
                    if (Convert.ToInt32(s) == 3)
                    {
                        s = Convert.ToInt32(s).ToString();
                    }
                    else
                    {
                        s = (Convert.ToInt32(s) - 1).ToString();
                    }
                }

                SongList = SongListManage.GetAll(Common.ApiUrl, Common.ApiKey, s);
                MaxIndex = SongList.Count % PageCount == 0 ? SongList.Count / PageCount : SongList.Count / PageCount + 1;
                PageIndex = 0;
                setControl("InitContent", null);
            }
        }
        bool isSelectSong = false;
        /// <summary>
        /// 选中歌曲
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BLL.Common.doMyError(new Exception(), "执行了点击事件");
            if (isSelectSong)
            {
                _endPoint = Mouse.GetPosition(e.Source as FrameworkElement);
                //X轴滑动的距离
                double offsetX = _startPoint.X - _endPoint.X;
                if (offsetX > 10)
                {
                    //下一页
                    Uppage_MouseLeftButtonUp(null, null);

                }
                else if (offsetX < -10)
                {
                    //上一页
                    Downpage_MouseLeftButtonUp(null, null);
                }
                else if (offsetX < 10 && offsetX > -10)
                {
                    SendMsgEvent("PaymentList");
                    if (sender is Image)
                    {
                        var d = sender as Image;
                        string s = d.Name.Substring(9, d.Name.Length - 9);
                        Common.songInfo = SelectedList[Convert.ToInt32(s) - 1];
                        BLL.Common.doMyError(new Exception(), "执行了" + Common.songInfo.Title);
                    }
                    if (sender is Label)
                    {
                        var d = sender as Label;
                        string s = d.Name.Substring(7, d.Name.Length - 7);
                        Common.songInfo = SelectedList[Convert.ToInt32(s) - 1];
                        BLL.Common.doMyError(new Exception(), "执行了" + Common.songInfo.Title);
                    }

                    ControlTime.IsCreatCl = false;
                    ControlTime.ComeingClass = false;
                    ControlTime.IsWaiteTeach = false;
                    ControlTime.IsPlayMV = false;
                    BLL.Common.doMyError(new Exception(), "执行了" + "SendMsgEvent('PaymentList')");
                }
            }
            else
            {
                isSelectSong = !isSelectSong;
            }
        }

        private void GetAllList()
        {

            Thread thContent = new Thread(delegate()
            {
                if (!string.IsNullOrEmpty(Common.ApiUrl))
                {
                    SongList = SongListManage.GetAll(Common.ApiUrl, Common.ApiKey, "0"); //儿歌
                    MaxIndex = SongList.Count % PageCount == 0 ? SongList.Count / PageCount : SongList.Count / PageCount + 1;
                    setControl("InitContent", null);
                }
            });
            thContent.Start();
        }


        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Uppage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (PageIndex != 0)
            {
                PageIndex--;
                setControl("InitContent", null);
            }
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Downpage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (PageIndex < MaxIndex - 1)
            {
                PageIndex++;
                setControl("InitContent", null);
            }
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            GetAllList();
        }



        /// <summary>
        /// 调用的所有方法
        /// </summary>
        /// <param name="method"></param>
        /// <param name="obj"></param>
        private delegate void SetControlCallback(string method, object obj);

        private void setControl(string method, object obj)
        {
            //if (this.Dispatcher.CheckAccess())
            //{
            //    if (method.Equals("InitContent"))
            //    {
            //        SelectedList = SongList.Skip(PageIndex * PageCount).Take(PageCount).ToList();
            //        foreach (UIElement element in UniformGrid.Children)
            //        {
            //            if (element is Image)
            //            {
            //                Image objImage = (element as Image);
            //                if (objImage.Name.Contains("ImageTalk"))
            //                {
            //                    int count = Convert.ToInt32(objImage.Name.Substring(9, objImage.Name.Length - 9));
            //                    if (count <= SelectedList.Count)
            //                        objImage.Source = new BitmapImage(new Uri(SelectedList[count - 1].TextBookCover, UriKind.RelativeOrAbsolute));
            //                    else
            //                        objImage.Source = null;
            //                }
            //                else if (objImage.Name.Contains("Star"))
            //                {
            //                    int count = Convert.ToInt32(objImage.Name.Substring(4, objImage.Name.Length - 4));
            //                    if (count <= SelectedList.Count)
            //                    {
            //                        switch (SelectedList[count - 1].Difficulty)
            //                        {
            //                            case "0":
            //                                objImage.Source = new BitmapImage(new Uri(@"Image\FirstImage\oneStar.png", UriKind.RelativeOrAbsolute));
            //                                break;
            //                            case "1":
            //                                objImage.Source = new BitmapImage(new Uri(@"Image\FirstImage\threeStar.png", UriKind.RelativeOrAbsolute));
            //                                break;
            //                            case "2":
            //                                objImage.Source = new BitmapImage(new Uri(@"Image\FirstImage\fiveStar.png", UriKind.RelativeOrAbsolute));
            //                                break;
            //                            default:
            //                                break;
            //                        }
            //                    }
            //                    else
            //                    {
            //                        objImage.Source = null;
            //                    }
            //                }
            //            }
            //            else if (element is Label)
            //            {
            //                Label objImage = (element as Label);
            //                if (objImage.Name.Contains("lblTalk"))
            //                {
            //                    int count = Convert.ToInt32(objImage.Name.Substring(7, objImage.Name.Length - 7));
            //                    if (count <= SelectedList.Count)
            //                        objImage.Content = SelectedList[count - 1].Title;
            //                    else
            //                        objImage.Content = null;
            //                }
            //                else if (objImage.Name.Contains("landu"))
            //                {
            //                    int count = Convert.ToInt32(objImage.Name.Substring(5, objImage.Name.Length - 5));
            //                    if (count <= SelectedList.Count)
            //                        objImage.Visibility = Visibility.Visible;
            //                    else
            //                        objImage.Visibility = Visibility.Hidden;
            //                }
            //            }
            //        }

            //        if (PageIndex == 0)
            //        {
            //            this.Uppage.Source = new BitmapImage(new Uri("Image/TeachTalk/left0.png", UriKind.RelativeOrAbsolute));
            //        }
            //        else
            //        {
            //            this.Uppage.Source = new BitmapImage(new Uri("Image/TeachTalk/left1.png", UriKind.RelativeOrAbsolute));
            //        }
            //        if (PageIndex == MaxIndex - 1)
            //        {
            //            this.Downpage.Source = new BitmapImage(new Uri("Image/TeachTalk/right0.png", UriKind.RelativeOrAbsolute));
            //        }
            //        else
            //        {
            //            this.Downpage.Source = new BitmapImage(new Uri("Image/TeachTalk/right1.png", UriKind.RelativeOrAbsolute));
            //        }
            //        if (MaxIndex != 0)
            //        {
            //            this.lblPage.Content = (PageIndex + 1) + "/" + MaxIndex;
            //        }
            //        else
            //        {
            //            this.lblPage.Content = "0/0";
            //        }
            //    }
            //}
            //else
            //{
            //    SetControlCallback d = new SetControlCallback(setControl);
            //    this.Dispatcher.BeginInvoke(d, new object[] { method, obj });
            //}



             if (!this.Dispatcher.CheckAccess())
             {
                 SetControlCallback d = new SetControlCallback(setControl);
                 this.Dispatcher.BeginInvoke(d, new object[] { method, obj });
                 return;
             }
             if (method.Equals("InitContent"))
             {
                 SelectedList = SongList.Skip(PageIndex * PageCount).Take(PageCount).ToList();
                 foreach (UIElement element in UniformGrid.Children)
                 {
                     if (element is Image)
                     {
                         Image objImage = (element as Image);
                         if (objImage.Name.Contains("ImageTalk"))
                         {
                             int count = Convert.ToInt32(objImage.Name.Substring(9, objImage.Name.Length - 9));
                             if (count <= SelectedList.Count)
                                 objImage.Source = new BitmapImage(new Uri(SelectedList[count - 1].TextBookCover, UriKind.RelativeOrAbsolute));
                             else
                                 objImage.Source = null;
                         }
                         else if (objImage.Name.Contains("Star"))
                         {
                             int count = Convert.ToInt32(objImage.Name.Substring(4, objImage.Name.Length - 4));
                             if (count <= SelectedList.Count)
                             {
                                 switch (SelectedList[count - 1].Difficulty)
                                 {
                                     case "0":
                                         objImage.Source = new BitmapImage(new Uri(@"Image\FirstImage\oneStar.png", UriKind.RelativeOrAbsolute));
                                         break;
                                     case "1":
                                         objImage.Source = new BitmapImage(new Uri(@"Image\FirstImage\threeStar.png", UriKind.RelativeOrAbsolute));
                                         break;
                                     case "2":
                                         objImage.Source = new BitmapImage(new Uri(@"Image\FirstImage\fiveStar.png", UriKind.RelativeOrAbsolute));
                                         break;
                                     default:
                                         break;
                                 }
                             }
                             else
                             {
                                 objImage.Source = null;
                             }
                         }
                     }
                     else if (element is Label)
                     {
                         Label objImage = (element as Label);
                         if (objImage.Name.Contains("lblTalk"))
                         {
                             int count = Convert.ToInt32(objImage.Name.Substring(7, objImage.Name.Length - 7));
                             if (count <= SelectedList.Count)
                                 objImage.Content = SelectedList[count - 1].Title;
                             else
                                 objImage.Content = null;
                         }
                         else if (objImage.Name.Contains("landu"))
                         {
                             int count = Convert.ToInt32(objImage.Name.Substring(5, objImage.Name.Length - 5));
                             if (count <= SelectedList.Count)
                                 objImage.Visibility = Visibility.Visible;
                             else
                                 objImage.Visibility = Visibility.Hidden;
                         }
                     }
                 }

                 if (PageIndex == 0)
                 {
                     this.Uppage.Source = new BitmapImage(new Uri("Image/TeachTalk/left0.png", UriKind.RelativeOrAbsolute));
                 }
                 else
                 {
                     this.Uppage.Source = new BitmapImage(new Uri("Image/TeachTalk/left1.png", UriKind.RelativeOrAbsolute));
                 }
                 if (PageIndex == MaxIndex - 1)
                 {
                     this.Downpage.Source = new BitmapImage(new Uri("Image/TeachTalk/right0.png", UriKind.RelativeOrAbsolute));
                 }
                 else
                 {
                     this.Downpage.Source = new BitmapImage(new Uri("Image/TeachTalk/right1.png", UriKind.RelativeOrAbsolute));
                 }
                 if (MaxIndex != 0)
                 {
                     this.lblPage.Content = (PageIndex + 1) + "/" + MaxIndex;
                 }
                 else
                 {
                     this.lblPage.Content = "0/0";
                 }
             }






























        }
        Point _startPoint;
        Point _endPoint;
        private void UniformGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = Mouse.GetPosition(e.Source as FrameworkElement);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            isSelectSong = true;
        }
    }
}
