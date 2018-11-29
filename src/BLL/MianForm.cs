using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MianForm
    {
        /// <summary>
        ///漏斗类型  0:新注册  1：已付款   2：已选类容   3：已选老师   4：正常结束
        /// </summary>
        /// <param name="ApiUrl"></param>
        /// <param name="ApiKey"></param>
        /// <param name="BID"></param>
        /// <param name="StartTime"></param>
        /// <returns></returns>
        public static bool BoxUserFunnel(string ApiUrl, string ApiKey, string BID, string FunnelType)
        {
            bool fig = false;
            try
            {
                if (!string.IsNullOrEmpty(BID))
                {
                    StringBuilder sb_url = new StringBuilder();
                    sb_url.Append(ApiUrl + "ApiKey=" + ApiKey);
                    sb_url.Append("&Action=BoxUserFunnel");
                    sb_url.Append("&BID=" + BID);
                    sb_url.Append("&FunnelType=" + FunnelType);
                    string result = Common.doGet(sb_url.ToString());
                    JObject json = JObject.Parse(result);
                    if (json["code"] != null && json["code"].ToString() == "1")
                    {
                        fig = true;
                    }
                    Common.doException(new Exception(), "BoxUserFunnel" + result);
                }
            }
            catch (Exception ex)
            {
                Common.doException(ex, "BoxUserFunnel");
            }
            return fig;
        }

      /// <summary>
      /// 盒子用户停留时间统计
      /// </summary>
      /// <param name="ApiUrl"></param>
      /// <param name="ApiKey"></param>
      /// <param name="BID">盒子用户ID</param>
      /// <param name="CourseType">课程类型   1：老师教唱     0：单曲独唱    2：FreeTalk模式【聊天模式】    3：朗读模式</param>
      /// <param name="StopType">停留类型  1：选择套餐、2：支付套餐、3：选择歌曲类型、4：选择歌曲、5：唱歌练习、6：分享练习、7：匹配老师、8：上课、9：选择内容类型、10：选择内容、</param>
      /// <param name="StopTime">停留时间【单位：秒】</param>
      /// <returns></returns>
        public static bool BoxUserStopTime(string ApiUrl, string ApiKey, string BID, string CourseType, string StopType, string StopTime)
        {
            bool fig = false;
            try
            {
                if (!string.IsNullOrEmpty(BID))
                {
                    StringBuilder sb_url = new StringBuilder();
                    sb_url.Append(ApiUrl + "ApiKey=" + ApiKey);
                    sb_url.Append("&Action=BoxUserStopTime");
                    sb_url.Append("&BID=" + BID);
                    sb_url.Append("&CourseType=" + CourseType);
                    sb_url.Append("&StopType=" + StopType);
                    sb_url.Append("&StopTime=" + StopTime);

                    string result = Common.doGet(sb_url.ToString());
                    JObject json = JObject.Parse(result);
                    if (json["code"] != null && json["code"].ToString() == "1")
                    {
                        fig = true;
                    }
                    Common.doException(new Exception(), "BoxUserStopTime" + result);
                }
            }
            catch (Exception ex)
            {
                Common.doException(ex, "BoxUserStopTime");
            }
            return fig;
        }

    }
}
