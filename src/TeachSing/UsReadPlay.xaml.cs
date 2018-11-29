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

namespace TeachSing
{
    /// <summary>
    /// UsReadPlay.xaml 的交互逻辑
    /// </summary>
    public partial class UsReadPlay : UserControl
    {
        public event TeachSing.MainWindow.delegateSendMsg SendMsgEvent;
        public UsReadPlay()
        {
            InitializeComponent();
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SendMsgEvent("ReadyPlayGo");
        }

        private void Image_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
