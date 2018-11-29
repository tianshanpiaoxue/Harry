using BLL;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Windows.Threading;

namespace TeachSing
{
    /// <summary>
    /// TeachRead.xaml 的交互逻辑
    /// </summary>
    public partial class TeachRead : Window
    {
        private DispatcherTimer IsGetClass = new DispatcherTimer();
        public TeachRead()
        {
            InitializeComponent();

        }
        int waitTime = 0;
        int sw = 0;
        private void IsGetClass_Tick(object sender, EventArgs e)
        {
            try
            {

                this.lblTalk.Content = "已用时  " + BLL.Common.TransTimeSecondIntToString(waitTime);
                waitTime++;
                if (!ControlTime.OpenClassIn && waitTime%3==0)
                {
                    // 
                    string url = GetClassManage.GetResponseUrl(Common.ApiUrl, Common.ApiKey, Common.bcrid, Common.BID, (ControlTime.Totaltime / 60).ToString());
                    if (!string.IsNullOrEmpty(url))
                    {
                        try
                        {
                            ControlTime.CountPlayTime = true;
                            //===============================================
                            //==============吊起ClassIn=======================
                            //===============================================
                            //ChangePic();
                            Common.ClassInUrl = url;
                            BLL.Common.doException(new Exception(), Common.ClassInUrl);
                            OenClassIn();
                            BLL.MianForm.BoxUserFunnel(Common.ApiUrl, Common.ApiKey, Common.BID, "3");
                        }
                        catch 
                        { 

                        }
                        // break;
                    }
                    else if (waitTime > 120)
                    {
                        IsGetClass.Stop();
                        Common.bcrid = "";
                        ControlTime.ComeingClass = false;
                        this.Close();
                    }
                }
                //if (ControlTime.OpenClassIn && waitTime % 3 == 0)
                if ( waitTime % 2 == 0)
                {
                    if (GetClassManage.GetEndClass(Common.ApiUrl, Common.ApiKey, Common.BID, Common.bcrid, "20"))
                    {
                        //try
                        //{
                        //===============================================
                        //==============老师结束课程=====================
                        //===============================================

                        EndClassIn();
                        ControlTime.OpenClassIn = false;
                        IsGetClass.Stop();
                        Common.talkType = null;
                        CloseForm(null, null);
                        ControlTime.IsWaiteTeach = true;
                        Common.bcrid = "";
                        BLL.MianForm.BoxUserFunnel(Common.ApiUrl, Common.ApiKey, Common.BID, "4");
                    }

                    sw++;
                    if (sw < 10 )
                    {
                        frmSuggest_Click(null, null);
                    }


                }
                //===============================================
                //==============学生主动结束课程=====================
                //===============================================
                if (ControlTime.StudentOverClass)
                {
                    EndClassIn();
                    ControlTime.OpenClassIn = false;
                    IsGetClass.Stop();
                    Common.talkType = null;
                    CloseForm(null, null);
                    ControlTime.IsWaiteTeach = true;
                    Common.bcrid = "";
                    ControlTime.StudentOverClass = false;
                    BLL.MianForm.BoxUserFunnel(Common.ApiUrl, Common.ApiKey, Common.BID, "4");
                }

            }
            catch (Exception ex)
            {
                BLL.Common.doException(ex, "吊起classIn 和结束课程" + Common.ClassInUrl);
            }
        }

        /// <summary>
        /// 打开classIn
        /// </summary>
        private void OenClassIn()
        {
            try
            {


                Common.frmShelterUp = new ShelterUp();
                Common.frmShelterUp.Show();
                Common.frmShelterRight = new ShelterRight();
                Common.frmShelterRight.Show();
                Common.frmShelterDown = new ShelterDown();
                Common.frmShelterDown.Show();
                Common.frmShelterButton = new ShelterButton();
                Common.frmShelterButton.Show();

                WebBrowser web = new WebBrowser();
                web.Navigate(Common.ClassInUrl);
                ControlTime.OpenClassIn = true;
            }
            catch (Exception ex)
            {
                BLL.Common.doException(ex, "打开ClassIn");
            }
        }
        /// 关闭ClassIn
        /// </summary>
        private static void EndClassIn()
        {

            if (Common.frmShelterUp != null)
            {
                Common.frmShelterUp.Close();
            }
            if (Common.frmShelterDown != null)
            {
                Common.frmShelterDown.Close();
            }
            if (Common.frmShelterRight != null)
            {
                Common.frmShelterRight.Close();
            }
            if (Common.frmShelterButton != null)
            {
                Common.frmShelterButton.Close();
            }

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    Process[] pros = Process.GetProcessesByName("ClassIn");
                    if (pros.Length > 0)
                    {
                        try
                        {
                            pros[0].Kill();
                        }
                        catch
                        {
                            pros[0].CloseMainWindow();
                        }
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    BLL.Common.doException(ex, "EndClassIn() 方法");
                }
            }
        }
        private void CloseForm(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }


        private void Window_Initialized(object sender, EventArgs e)
        {
            IsGetClass.Tick += new EventHandler(IsGetClass_Tick);
            IsGetClass.Interval = new TimeSpan(0, 0, 1);

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IsGetClass.Start();
            sw = 0;
        }




        #region  //寻找固定点进行单击
        const int MOUSEEVENTF_MOVE = 0x0001;
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        const int MOUSEEVENTF_LEFTUP = 0x0004;
        [DllImport("user32", CharSet = CharSet.Unicode)]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);
        private void frmSuggest_Click(object sender, EventArgs e)
        {
            // string pName = "dvdplay";//要启动的进程名称，可以在任务管理器里查看，一般是不带.exe后缀的;  
            Process[] temp = Process.GetProcessesByName("ClassIn");//在所有已启动的进程中查找需要的进程；  
            if (temp.Length > 0)//如果查找到  
            {
                IntPtr handle = temp[0].MainWindowHandle;
                SwitchToThisWindow(handle, true);    // 激活，显示在最前  
                list.Clear();
                EnumChildWindows(temp[0].Handle, this.EnumWindowsMethod, IntPtr.Zero);

                //移动鼠标指针并点击
                mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, 10000, 32000, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 10000, 32000, 0, 0);

            }
        }
        #endregion

        #region 将ClassInRoom显示到最前面
        private bool EnumWindowsMethod(int hWnd, int lParam)
        {
            IntPtr lpString = Marshal.AllocHGlobal(200);
            GetWindowText(hWnd, lpString, 200);
            var text = Marshal.PtrToStringAnsi(lpString);
            if (!string.IsNullOrWhiteSpace(text))
                list.Add(text);
            return true;
        }
        public delegate bool EnumWindowsProc(int hWnd, int lParam);

        List<string> list = new List<string>();
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(int hWnd, IntPtr lpString, int nMaxCount);
        #endregion

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
