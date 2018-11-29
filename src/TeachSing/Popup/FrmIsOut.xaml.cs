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
using Models;

namespace TeachSing
{
    /// <summary>
    /// FrmIsOut.xaml 的交互逻辑
    /// </summary>
    public partial class FrmIsOut : Window
    {
        public FrmIsOut()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 退出系统，回到首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BLL.LoginManage.LoginOut(Common.ApiUrl, Common.ApiKey, Common.BID, DateTime.Now,Common.MID);
            ControlTime.Totaltime = 0;
            Common.BID = "";
            ControlTime.WeChatName = "";
            ControlTime.WeChatHeadPic = null;
            ControlTime.CountPlayTime = false;
            Common.songInfo =null;
            Common.talkType = null;
            Common.UID = "";
            Common.COID = "";
            Common.ClassInUrl = "";
            Common.bcrid = "";
            ControlTime.GoHome = true;
            ControlTime.IsCreatCl = false;
            ControlTime.ComeingClass = false;
            ControlTime.IsPlayMV = false;
            ControlTime.IsWaiteTeach = false;
            ControlTime.OpenClassIn = false;
            ControlTime.Totaltime = 0;
            ControlTime.WeChatBuyType = "0";
            ControlTime.WherePay = "0";
            this.Close();
            Application.Current.Shutdown();
        }
        /// <summary>
        /// 关闭弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cansol_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FristPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
