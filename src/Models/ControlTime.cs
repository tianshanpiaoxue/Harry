using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class ControlTime
    {
        /// <summary>
        /// 购买总计时（统计总时长）
        /// </summary>
        public static int AllTotaltime
        {
            get;
            set;
        }
        /// <summary>
        /// 总倒计时（倒计时）
        /// </summary>
        public static int Totaltime
        {
            get;
            set;
        }
      


        /// <summary>
        /// 总倒计时开启
        /// </summary>

        public static bool CountPlayTime
        {
            get;
            set;
        }

        /// <summary>
        /// 微信头像
        /// </summary>

        public static string WeChatHeadPic
        {
            get;
            set;
        }
        /// <summary>
        /// 微信名称
        /// </summary>

        public static string WeChatName
        {
            get;
            set;
        }

        /// <summary>
        /// 选择类型接口 1：老师教唱     0：单曲独唱   2：FreeTalk模式【聊天模式】
        /// </summary>

        public static string WeChatBuyType
        {
            get;
            set;
        }
        /// <summary>
        /// 套餐ID
        /// </summary>

        public static string COID
        {
            get;
            set;
        }
        /// <summary>
        /// 套餐时间
        /// </summary>
        public static string PackageTime
        {
            get;
            set;
        }
        /// <summary>
        /// 判断是从那个地方进入付款页面(1 顺序流程  2  快捷进入)
        /// </summary>
        public static string WherePay
        {
            get;
            set;
        }
        /// <summary>
        /// 回到首页
        /// </summary>
        public static bool GoHome = false;    
        /// <summary>
        /// 是否创建课程，防止Load事件执行两遍  false代表没有创建课程也就是上课已结束  true  代表创建了课程，正在上课。
        /// </summary>
        public static bool IsCreatCl = false;    
        
        /// <summary>
        /// 老师是否进入教室
        /// </summary>
        public static bool ComeingClass = false;
        /// <summary>
        /// 是否打开ClassIn，防止多次吊起事件执行两遍
        /// </summary>
        public static bool OpenClassIn = false;

        /// <summary>
        /// 是否上完课，进入下个页面
        /// </summary>
        public static bool IsWaiteTeach = false;

        /// <summary>
        /// 是否进入了唱歌页面
        /// </summary>
        public static bool IsPlayMV = false;

        /// <summary>
        /// 学生主动结束课程
        /// </summary>
        public static bool StudentOverClass = false;

        /// <summary>
        /// 是否断网
        /// </summary>
        public static bool NetError = false;

    }
}
