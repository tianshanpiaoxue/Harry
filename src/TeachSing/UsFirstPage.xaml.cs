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

namespace TeachSing
{
    /// <summary>
    /// UsFirstPage.xaml 的交互逻辑
    /// </summary>
    public partial class UsFirstPage : UserControl
    {
        public UsFirstPage()
        {
            InitializeComponent();
        }
        public event TeachSing.MainWindow.delegateSendMsg SendMsgEvent;
        private void FreeTalk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ControlTime.WeChatBuyType = "2";
            //first1.Source = new BitmapImage(new Uri(@"Image\FirstImage\firstcopy1.png", UriKind.RelativeOrAbsolute));
            //first2.Source = new BitmapImage(new Uri(@"Image\FirstImage\first2.png", UriKind.RelativeOrAbsolute));
            SendMsgEvent("FreeTalkList");
        }

        private void TeachTalk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ControlTime.WeChatBuyType = "1";
            //first2.Source = new BitmapImage(new Uri(@"Image\FirstImage\firstcopy2.png", UriKind.RelativeOrAbsolute));
            //first1.Source = new BitmapImage(new Uri(@"Image\FirstImage\first1.png", UriKind.RelativeOrAbsolute));
            SendMsgEvent("TeachTalkList");
        }
    
    }
}
