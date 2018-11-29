using Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// PayMentList.xaml 的交互逻辑
    /// </summary>
    public partial class PayMentList : Window
    {
        public DispatcherTimer PayTick = new DispatcherTimer();

        public static List<BoxPackage> BoxPackageList = new List<BoxPackage>();
        public static BoxPackage SelectedBoxPackage;

        int pushNum = -1;
        public PayMentList()
        {
            InitializeComponent();
            this.Topmost = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllList();
            PayTick.Tick += new EventHandler(PayTick_Tick);
            PayTick.Interval = new TimeSpan(0, 0, 1);
            Common.COID = "";
            returnPage();
        }

        private void GetAllList()
        {
            Thread thContent = new Thread(delegate()
            {
                BoxPackageList = BLL.PayManage.GetBoxMergePackageList(Common.ApiUrl, Common.ApiKey, Common.MID, Convert.ToInt32(ControlTime.WeChatBuyType),Common.BID);
                setControl("InitContent", null);
            });
            thContent.Start();
        }

        string BID = "";
        static object PayTick_TickLock = new object();
        private void PayTick_Tick(object sender, EventArgs e)
        {
            lock (PayTick_TickLock)
            {
                int status = BLL.PayManage.GetBoxOrderStatus(Common.ApiUrl, Common.ApiKey, Common.COID);
                if (status == 1)
                {
                    PayTick.Stop();
                    ControlTime.Totaltime += Convert.ToInt32(BoxPackageList[pushNum].Time) * 60;
                    Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
                    BuySucces objBuysuss = new BuySucces();
                    objBuysuss.ShowDialog();
                    Loay.Background = null;
                }

            }
            if(!BID.Equals(Common.BID))
            {
                GetAllList();
                BID = Common.BID;
            }
        }


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
                    if (BoxPackageList.Count > 0)
                    {
                        PackageName0.Content = BoxPackageList[0].PackageName;
                        PackageName1.Content = BoxPackageList[1].PackageName;
                        PackageName2.Content = BoxPackageList[2].PackageName;
                        Price0.Content = BoxPackageList[0].Price;
                        Price1.Content = BoxPackageList[1].Price;
                        Price2.Content = BoxPackageList[2].Price;
                        TPrice0.Content = "支付金额：" + BoxPackageList[0].Price + "元";
                        TPrice1.Content = "支付金额：" + BoxPackageList[1].Price + "元";
                        TPrice2.Content = "支付金额：" + BoxPackageList[2].Price + "元";
                        if (BoxPackageList[0].Label.Contains("1"))
                        {
                            PayTag0.Source = new BitmapImage(new Uri(@"Image\PayMent\BestHot.png", UriKind.RelativeOrAbsolute));
                            PayTag0.Visibility = Visibility;
                        }
                        else if (BoxPackageList[0].Label.Contains("2"))
                        {
                            PayTag0.Source = new BitmapImage(new Uri(@"Image\PayMent\Offer.png", UriKind.RelativeOrAbsolute));
                            PayTag0.Visibility = Visibility;
                        }
                        if (BoxPackageList[1].Label.Contains("1"))
                        {
                            PayTag1.Source = new BitmapImage(new Uri(@"Image\PayMent\BestHot.png", UriKind.RelativeOrAbsolute));
                            PayTag1.Visibility = Visibility;
                        }
                        else if (BoxPackageList[1].Label.Contains("2"))
                        {
                            PayTag1.Source = new BitmapImage(new Uri(@"Image\PayMent\Offer.png", UriKind.RelativeOrAbsolute));
                            PayTag1.Visibility = Visibility;
                        }
                        if (BoxPackageList[2].Label.Contains("1"))
                        {
                            PayTag2.Source = new BitmapImage(new Uri(@"Image\PayMent\BestHot.png", UriKind.RelativeOrAbsolute));
                            PayTag2.Visibility = Visibility;
                        }
                        else if (BoxPackageList[2].Label.Contains("2"))
                        {
                            PayTag2.Source = new BitmapImage(new Uri(@"Image\PayMent\Offer.png", UriKind.RelativeOrAbsolute));
                            PayTag2.Visibility = Visibility;
                        }
                    }
                }
                if (method.Equals("SetBoxUserPay"))
                {
                    // objImage.Source = new BitmapImage(new Uri(SelectedList[count - 1].TextBookCover, UriKind.RelativeOrAbsolute));
                    var bitmap = ExtensionClass.ChangeBitmapToBitmapSource((Bitmap)obj);
                    TCord0.Source = TCord1.Source = TCord2.Source = bitmap;
                }
            }
            else
            {
                SetControlCallback d = new SetControlCallback(setControl);
                this.Dispatcher.BeginInvoke(d, new object[] { method, obj });
            }
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            PayTick.Start();
        }

        /// <summary>
        /// 还原页面
        /// </summary>
        private void returnPage()
        {
            this.firstMoney.Visibility = Visibility.Visible;
            this.secondMoney.Visibility = Visibility.Visible;
            this.threeMoney.Visibility = Visibility.Visible;
            this.cordOne.Visibility = Visibility.Hidden;
            this.cordTwo.Visibility = Visibility.Hidden;
            this.cordThree.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// 按下后  显示支付二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void secondMoney_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (sender is Grid)
            {
                if (Common.BID.Length > 0)
                {
                    TCord0.Source = TCord1.Source = TCord2.Source = null;

                    var d = sender as Grid;
                    switch (d.Name)
                    {
                        case "firstMoney":
                            if (DateTime.Now.AddMinutes(Convert.ToInt32(BoxPackageList[0].Time)) > Convert.ToDateTime("22:00") || DateTime.Now < Convert.ToDateTime("10:00"))
                            {
                                Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
                                BuyError objBuyError = new BuyError();
                                objBuyError.ShowDialog();
                                Loay.Background = null;
                            }
                            else
                            {
                                returnPage();
                                this.firstMoney.Visibility = Visibility.Hidden;
                                this.cordOne.Visibility = Visibility.Visible;
                                pushNum = 0;
                                GetPayCord(0);
                            }

                            break;
                        case "secondMoney":
                            if (DateTime.Now.AddMinutes(Convert.ToInt32(BoxPackageList[1].Time)) > Convert.ToDateTime("22:00") || DateTime.Now < Convert.ToDateTime("10:00"))
                            {
                                Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
                                BuyError objBuyError = new BuyError();
                                objBuyError.ShowDialog();
                                Loay.Background = null;
                            }
                            else
                            {
                                returnPage();
                                this.secondMoney.Visibility = Visibility.Hidden;
                                this.cordTwo.Visibility = Visibility.Visible;
                                pushNum = 1;
                                GetPayCord(1);
                            }
                            break;
                        case "threeMoney":
                            if (DateTime.Now.AddMinutes(Convert.ToInt32(BoxPackageList[2].Time)) > Convert.ToDateTime("22:00") || DateTime.Now < Convert.ToDateTime("10:00"))
                            {
                                Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
                                BuyError objBuyError = new BuyError();
                                objBuyError.ShowDialog();
                                Loay.Background = null;
                            }
                            else
                            {
                                returnPage();
                                this.threeMoney.Visibility = Visibility.Hidden;
                                this.cordThree.Visibility = Visibility.Visible;
                                pushNum = 2;
                                GetPayCord(2);
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    returnPage();
                }
            }
        }

        /// <summary>
        /// 产生支付二维码
        /// </summary>
        /// <param name="Text"></param>
        private void GetPayCord(int Text)
        {
            Thread th = new Thread(delegate()
            {
                Bitmap bitmap = null;
              
                bitmap = BLL.PayManage.BoxMergeUserPay(Common.ApiUrl, Common.ApiKey, Common.BID, Common.UID, "1", BoxPackageList[Text].CID.ToString(), Common.MID, ControlTime.WeChatBuyType, ref Common.COID, BoxPackageList[Text].CouponId, BoxPackageList[Text].MachineDiscount);
                
                setControl("SetBoxUserPay", bitmap);
                //}
            });
            th.Start();
        }

        /// <summary>
        /// 手动支付完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PayTick.Stop();
            ControlTime.Totaltime += Convert.ToInt32(BoxPackageList[pushNum].Time) * 60;
            Loay.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
            BuySucces objBuysuss = new BuySucces();
            objBuysuss.ShowDialog();
            Loay.Background = null;
        }

        private void Image_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            PayTick.Stop();
            this.Close();
        }

    }
    
}
