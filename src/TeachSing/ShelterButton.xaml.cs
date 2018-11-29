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
using WPFSetVolume.VolumeHelper;

namespace TeachSing
{
    /// <summary>
    /// ShelterButton.xaml 的交互逻辑
    /// </summary>
    public partial class ShelterButton : Window
    {
        public ShelterButton()
        {
            InitializeComponent();
            this.Left = 1920 - 117;
            this.Top = 930;
            WPFSetVolume.VolumeHelper.VolumeHelper.Init();
            WPFSetVolume.VolumeHelper.VolumeHelper.AddVolumeChangeNotify(VolumeChange);
            VolumeChange();
            this.Topmost = true;
        }

        ClassInError ObjClassError;
        OverClassInTip objOverClassTip;

        bool buttonDown = false;
        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (buttonDown)
            {
                buttonDown = !buttonDown;
                ImageButton.Source = new BitmapImage(new Uri(@"Image\FirstImage\righrButton.png", UriKind.RelativeOrAbsolute));
                this.Left = 1920 - 117;
            }
            else
            {
                buttonDown = !buttonDown;
                ImageButton.Source = new BitmapImage(new Uri(@"Image\FirstImage\leftButtn.png", UriKind.RelativeOrAbsolute));
                this.Left = 1920 - this.Width;
            }
        }

        /// <summary>
        /// 结束课程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OverClass_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            objOverClassTip = new OverClassInTip();
            objOverClassTip.ShowDialog();
        }

        /// <summary>
        /// 故障反馈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BugShow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ObjClassError = new ClassInError();
            ObjClassError.ShowDialog();
        }


        #region

        bool isVolumeChange = false;

        private void VolumeChange()
        {
            isVolumeChange = true;
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                //cbkIsMute.IsChecked = VolumeHelper.VolumeHelper.IsMute();
                //tbVolume.Text = VolumeHelper.VolumeHelper.GetVolume().ToString();
                textNum.Content = "耳机音量:" + VolumeHelper.GetVolume().ToString();
                slVolume.Value = WPFSetVolume.VolumeHelper.VolumeHelper.GetVolume();
            }));
            isVolumeChange = false;
        }

        private void slVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (isVolumeChange)
            {
                return;
            }
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                WPFSetVolume.VolumeHelper.VolumeHelper.SetVolume((int)slVolume.Value);
                //tbVolume.Text = ((int)slVolume.Value).ToString();
                textNum.Content ="耳机音量:" + ((int)slVolume.Value).ToString();
            }));
        }
        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                ObjClassError.Close();
            }
            catch { }
        }
    }
}
