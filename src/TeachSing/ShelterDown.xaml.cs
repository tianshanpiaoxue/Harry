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
    /// ShelterDown.xaml 的交互逻辑
    /// </summary>
    public partial class ShelterDown : Window
    {
        public ShelterDown()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Top = 1080-40;
            this.Left = 0;
            this.Topmost = true;
        }
    }
}
