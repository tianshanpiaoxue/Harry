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
    /// ComingEnd.xaml 的交互逻辑
    /// </summary>
    public partial class ComingEnd : Window
    {
        public ComingEnd()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 购买套餐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuyTime_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PayMentList objPayMentList = new PayMentList();
            objPayMentList.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cansal_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;
        }
    }
}
