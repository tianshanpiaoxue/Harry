using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GetClassManage
    {
        /// <summary>
        /// 创建课程【整合】
        /// </summary>
        /// <param name="ApiUrl"></param>
        /// <param name="ApiKey"></param>
        /// <param name="BID"></param>
        /// <param name="TopicType">聊天话题ID ，默认0</param>
        /// <param name="CourseType">课程类型 1：老师教唱  0：单曲独唱 2：FreeTalk模式【聊天模式】 3：朗读模式 4：6.1活动课程</param>
        /// <param name="MID"></param>
        /// <param name="SurplusTime">课程剩余时长【单位：分钟】</param>
        /// <returns></returns>
        public static string BoxNewCourseClassRecord(string ApiUrl, string ApiKey,string LID, string BID, string TopicType, int CourseType, string MID, int SurplusTime)
        {
            string bcrid = "";
            try
            {
                string result = "";
                StringBuilder sb_url = new StringBuilder();
                sb_url.Append(ApiUrl + "ApiKey=" + ApiKey);
                sb_url.Append("&Action=BoxNewCourseClassRecord");
                sb_url.Append("&BID=" + BID);
                sb_url.Append("&LID=" + LID);
                sb_url.Append("&SurplusTime=" + SurplusTime);
                sb_url.Append("&TopicType=" + TopicType);
                sb_url.Append("&CourseType=" + CourseType);
                sb_url.Append("&MID=" + MID);

                result = Common.doGet(sb_url.ToString());
                if (!string.IsNullOrEmpty(result))
                {
                    JObject json = JObject.Parse(result);
                    if (json["code"] != null && json["code"].ToString() == "1")
                    {
                        bcrid = json["data"].ToString();
                    }
                    Common.doException(new Exception(), "BoxNewCourseClassRecord请求结果" + result);
                }
            }
            catch (Exception ex)
            {
                Common.doException(ex, "创建课程【整合】 BoxNewCourseClassRecord");
            }
            return bcrid;
        }

        /// <summary>
        /// 学生取消课程
        /// </summary>
        /// <param name="ApiUrl"></param>
        /// <param name="ApiKey"></param>
        /// <param name="BID">BID</param>
        /// <param name="bcrid">课程ID</param>
        /// <returns></returns>
        public static bool BoxCancelCourse(string ApiUrl, string ApiKey, string BID, string bcrid)
        {
            bool fig = false;
            try
            {
                string result = "";
                StringBuilder sb_url = new StringBuilder();
                sb_url.Append(ApiUrl + "ApiKey=" + ApiKey);
                sb_url.Append("&Action=BoxCancelCourse");
                sb_url.Append("&BID=" + BID);
                sb_url.Append("&bcrid=" + bcrid);

                result = Common.doGet(sb_url.ToString());
                if (!string.IsNullOrEmpty(result))
                {
                    JObject json = JObject.Parse(result);
                    if (json["code"] != null && json["code"].ToString() == "1")
                    {
                        fig = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.doException(ex, "学生取消课程 BoxCancelCourse");
            }
            return fig;
        }

        /// <summary>
        /// 老师接课，但未进入教室
        /// </summary>
        /// <param name="ApiUrl"></param>
        /// <param name="ApiKey"></param>
        /// <param name="BID"></param>
        /// <param name="CLID"></param>
        /// <returns></returns>
        public static  bool  BoxIsClassRecord(string ApiUrl, string ApiKey, string BID, string bcrid)
        {
            bool fig = false;
            try
            {
                string result = "";
                StringBuilder sb_url = new StringBuilder();
                sb_url.Append(ApiUrl + "ApiKey=" + ApiKey);
                sb_url.Append("&Action=BoxIsClassRecord");
                sb_url.Append("&BID=" + BID);
                sb_url.Append("&bcrid=" + bcrid);

                result = Common.doGet(sb_url.ToString());
                if (!string.IsNullOrEmpty(result))
                {
                    JObject json = JObject.Parse(result);
                    if (json["code"] != null && json["code"].ToString() == "1")
                    {
                        fig = true;
                    }
                }
                Common.doException(new Exception(), "BoxIsClassRecord请求结果" + result);
            }
            catch (Exception ex)
            {
                Common.doException(ex, "老师接课，但未进入教室 BoxIsClassRecord");
            }
            return fig;
        }
       

       /// <summary>
        /// 捕捉老师进入教室，获取吊起上课工具的链接  【由此连接代表老师已经进入教室】
       /// </summary>
       /// <param name="ApiUrl"></param>
       /// <param name="ApiKey"></param>
       /// <param name="bcrid">课程ID</param>
       /// <param name="BID"></param>
       /// <param name="StopTime"></param>
       /// <returns></returns>
        public static string GetResponseUrl(string ApiUrl, string ApiKey, string bcrid, string BID, string StopTime = "")
        {
            string responseUrl = "";
            try
            {
                string result = "";
                StringBuilder sb_url = new StringBuilder();
                sb_url.Append(ApiUrl + "ApiKey=" + ApiKey);
                sb_url.Append("&Action=GetLoginLinked");
                sb_url.Append("&bcrid=" + bcrid);
                sb_url.Append("&userType=2");
                sb_url.Append("&BID=" + BID);
                if (!string.IsNullOrEmpty(StopTime))
                {
                    sb_url.Append("&StopTime=" + StopTime);
                } result = Common.doGet(sb_url.ToString());

                if (!string.IsNullOrEmpty(result))
                {
                    JObject json = JObject.Parse(result);
                    if (json["code"] != null && json["code"].ToString() == "0")
                    {
                        if (json["data"] != null && json["data"]["url"] != null)
                        {
                            responseUrl = json["data"]["url"].ToString();
                        }
                    }
                    Common.doException(new Exception(), "GetResponseUrl请求结果" + result);
                }
            }
            catch (Exception ex)
            {
                Common.doException(ex, "捕捉老师进入教室，获取吊起上课工具的链接  【由此连接代表老师已经进入教室】 GetResponseUrl");
            }
            return responseUrl;
        }
        /// <summary>
        /// 获取是否结束了课程
        /// </summary>
        /// <param name="ApiUrl"></param>
        /// <param name="ApiKey"></param>
        /// <param name="BID"></param>
        /// <returns></returns>
        public static bool GetEndClass(string ApiUrl, string ApiKey, string BID,  string bcrid,string StopTime = "")
        {
            bool fig = false;
            try
            {
                StringBuilder sb_url = new StringBuilder();
                sb_url.Append(ApiUrl + "ApiKey=" + ApiKey);
                sb_url.Append("&Action=NewGetStudentCurriStatus");
                sb_url.Append("&BID=" + BID);
                sb_url.Append("&bcrid=" + bcrid);
                if (!string.IsNullOrEmpty(StopTime))
                {
                    sb_url.Append("&StopTime=" + StopTime);
                }
                string result = "";
              

                result = Common.doGet(sb_url.ToString());
                JObject obj = JObject.Parse(result);
                if (obj["code"] != null && obj["code"].ToString() == "0")
                {
                    fig = true;
                }
            }
            catch (Exception ex)
            {
                Common.doException(ex, "获取是否结束了课程 GetEndClass");
            }
            return fig;
        }   
    }
}
