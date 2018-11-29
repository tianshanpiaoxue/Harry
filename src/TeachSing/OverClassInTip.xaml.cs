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
using System.Windows.Shapes;

namespace TeachSing
{
    /// <summary>
    /// OverClassInTip.xaml 的交互逻辑
    /// </summary>
    public partial class OverClassInTip : Window
    {
        public OverClassInTip()
        {
            InitializeComponent();
            this.Topmost = true;
        }

        /// <summary>
        /// 确定退出ClassIn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ControlTime.StudentOverClass = true;
            this.Close();
        }

        /// <summary>
        /// 取消退出ClassIn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
