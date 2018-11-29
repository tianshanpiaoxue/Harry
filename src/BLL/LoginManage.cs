using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LoginManage
    {

        /// <summary>
        /// 获取去微信小程序二维码      
        /// </summary>
        /// <returns></returns>
        public static string GetImageURL(string ApiUrl, string ApiKey, string MID)
        {
            string ImageURL = "";
            try
            {

                string result = "";
                //请求的http路径
                string requestUrl = string.Format(@"{0}{1}{2}", ApiUrl, ApiKey, MID);

                result = Common.doGet(requestUrl);


                if (!string.IsNullOrEmpty(result))
                {
                    var json = JObject.Parse(result);
                    if (json["code"] != null && json["code"].ToString() == "0")
                    {
                        ImageURL = json["data"] != null ? json["data"].ToString() : "";
                    }
                }
                Common.doException(new Exception(), "获取去微信小程序二维码" + result);
            }
            catch (Exception ex)
            {
                Common.doException(ex, "获取去微信小程序二维码GetImageURL");
            }
            return ImageURL;
        }


        /// <summary>
        /// 用户扫码注册获取UID，微信头像，微信名称
        /// </summary>
        /// <returns></returns>
        public static string GetNewBoxUser(string ApiUrl, string ApiKey, string MID)
        {
            string BID = "";
            string UID = "";
            string OpenID = "";
            string WeChatFaceImage = "";
            string WeChatNickName = "";
            DateTime StartTime = Convert.ToDateTime("10:00");
            DateTime EndTime = Convert.ToDateTime("22:00");
            if (DateTime.Now > StartTime && DateTime.Now < EndTime)
            {
                try
                {
                    string result = "";
                    //请求的http路径
                    string requestUrl = string.Format(@"{0}&Action=GetNewBoxUser&ApiKey={1}&MID={2}", ApiUrl, ApiKey, MID);

                    result = Common.doGet(requestUrl);

                    if (!string.IsNullOrEmpty(result))
                    {
                        var json = JObject.Parse(result);
                        if (json["code"] != null && json["code"].ToString() == "0")
                        {
                            BID = json["data"]["BID"] != null ? json["data"]["BID"].ToString() : "";
                            UID = json["data"]["UID"] != null ? json["data"]["UID"].ToString() : "";
                            OpenID = json["data"]["OpenID"] != null ? json["data"]["OpenID"].ToString() : "";
                            WeChatFaceImage = json["data"]["WeChatFaceImage"] != null ? json["data"]["WeChatFaceImage"].ToString() : "";
                            WeChatNickName = json["data"]["WeChatNickName"] != null ? json["data"]["WeChatNickName"].ToString() : "";
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    Common.doException(ex, "GetBoxUser");
                }
            }
            return BID + "*" + UID + "*" + OpenID + "*" + WeChatFaceImage + "*" + WeChatNickName;
        }




        /// <summary>
        /// 固定登录
        /// </summary>
        /// <returns></returns>
        public static bool UserLogin(string ApiUrl, string ApiKey, string MID, string BID)
        {
           
                try
                {
                    string result = "";
                    //请求的http路径
                    string requestUrl = string.Format(@"{0}&Action=BoxflyStudentLogion&ApiKey={1}&MID={2}&BID={3}", ApiUrl, ApiKey, MID, BID);

                    result = Common.doGet(requestUrl);

                    if (!string.IsNullOrEmpty(result))
                    {
                        var json = JObject.Parse(result);
                        if (json["code"] != null && json["code"].ToString() == "0")
                        {
                            return true;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Common.doException(ex, "BoxflyStudentLogion");
                }

                return false;
        }







        /// <summary>
        /// 登出，解绑程序
        /// </summary>
        /// <param name="ApiUrl"></param>
        /// <param name="ApiKey"></param>
        /// <param name="BID"></param>
        /// <param name="StartTime"></param>
        /// <returns></returns>
        public static bool LoginOut(string ApiUrl, string ApiKey, string BID, DateTime StartTime,string MID)
        {
            bool fig = false;
            try
            {
                if (!string.IsNullOrEmpty(BID))
                {
                    TimeSpan longTime = ((DateTime.Now.Subtract(StartTime)).Duration());
                    int s = (int)(longTime.TotalSeconds);
                    StringBuilder sb_url = new StringBuilder();
                    sb_url.Append(ApiUrl + "ApiKey=" + ApiKey);
                    sb_url.Append("&Action=PageTimeout");
                    sb_url.Append("&BID=" + BID);
                    sb_url.Append("&MID=" + MID);
                    sb_url.Append("&StopTime=" + s);
                    string result = Common.doGet(sb_url.ToString());
                    JObject json = JObject.Parse(result);
                    if (json["code"] != null && json["code"].ToString() == "1")
                    {
                        fig = true;
                    }
                    Common.doException(new Exception(), "LoginOut" + result);
                }
            }
            catch (Exception ex)
            {
                Common.doException(ex, "LoginOut");
            }
            return fig;
        }
    }
}
