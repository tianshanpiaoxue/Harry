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
    /// ClassInError.xaml 的交互逻辑
    /// </summary>
    public partial class ClassInError : Window
    {
        public ClassInError()
        {
            InitializeComponent();
            this.Topmost = true;
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
