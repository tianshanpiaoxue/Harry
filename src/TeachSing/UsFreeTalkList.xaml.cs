using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using BLL;
using Models;
using System.Threading;

namespace TeachSing
{
    /// <summary>
    /// UsFreeTalkList.xaml 的交互逻辑
    /// </summary>
    public partial class UsFreeTalkList : UserControl
    {
        public event TeachSing.MainWindow.delegateSendMsg SendMsgEvent;
        TalkList listManage = new TalkList();
        List<TalkType> objTalkList = new List<TalkType>();
        public UsFreeTalkList()
        {
            InitializeComponent();
        }

        bool isSelcetTalk = false;
        /// <summary>
        /// 选中聊天话题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isSelcetTalk)
            {
                if (sender is Image)
                {
                    var d = sender as Image;
                    string s = d.Name.Substring(9, d.Name.Length - 9);
                    Common.talkType = objTalkList[Convert.ToInt32(s) - 1];
                }
                if (sender is Label)
                {
                    var d = sender as Label;
                    string s = d.Name.Substring(7, d.Name.Length - 7);
                    Common.talkType = objTalkList[Convert.ToInt32(s) - 1];
                }
                ControlTime.IsCreatCl = false;
                ControlTime.ComeingClass = false;
                ControlTime.IsWaiteTeach = false;
                SendMsgEvent("PaymentList");
            }
            else{
                isSelcetTalk=!isSelcetTalk;
            }
           
        }



        /// <summary>
        /// 调用的所有方法
        /// </summary>
        /// <param name="method"></param>
        /// <param name="obj"></param>
        private delegate void SetControlCallback(string method, object obj);

        private void setControl(string method, object obj)
        {
            if (this.Dispatcher.CheckAccess())
            {
                if (method.Equals("InitContent"))
                {
                    foreach (UIElement element in UniformGrid.Children)
                    {
                        if (element is Image)
                        {
                            Image objImage = (element as Image);
                            if (objImage.Name.Contains("ImageTalk"))
                            {
                                int count = Convert.ToInt32(objImage.Name.Substring(9, objImage.Name.Length - 9));
                                if (count <= objTalkList.Count)
                                    objImage.Source = new BitmapImage(new Uri(objTalkList[count - 1].Img, UriKind.RelativeOrAbsolute));
                            }

                        }
                        else if (element is Label)
                        {
                            Label objImage = (element as Label);
                            if (objImage.Name.Contains("lblTalk"))
                            {
                                int count = Convert.ToInt32(objImage.Name.Substring(7, objImage.Name.Length - 7));
                                if (count <= objTalkList.Count)
                                    objImage.Content = objTalkList[count - 1].TopicName;
                            }
                        }
                    }
                }
            }
            else
            {
                SetControlCallback d = new SetControlCallback(setControl);
                this.Dispatcher.BeginInvoke(d, new object[] { method, obj });
            }
        }
        /// <summary>
        /// 绑定显示主题
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            isSelcetTalk = true;
            Thread thContent = new Thread(delegate()
            {
                objTalkList = listManage.GetTalkList(Common.ApiUrl, Common.ApiKey, "");
                setControl("InitContent", null);
            });
            thContent.Start();
        }


    }
}
