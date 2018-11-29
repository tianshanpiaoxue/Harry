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
    /// ShelterUp.xaml 的交互逻辑
    /// </summary>
    public partial class ShelterUp : Window
    {
        public ShelterUp()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Top = 0;
            this.Left = 1920-1480;
            this.Topmost = true;
        }
    }
}
