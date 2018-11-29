using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PayManage
    {
        /// <summary>
        /// new 获取盒子支付套餐描述   2018.5.11  （判断免费还是打折  返回字段 IsFreeAdmission ）
        /// </summary>
        /// <param name="ApiUrl"></param>
        /// <param name="ApiKey"></param>
        /// <param name="BID"></param>
        public static List<BoxPackage> GetBoxMergePackageList(string ApiUrl, string ApiKey, string MID, int CourseType,string BID)
        {
            List<BoxPackage> list = new List<BoxPackage>();
            try
            {
                string result = "";
                StringBuilder sb_url = new StringBuilder();
                sb_url.Append(ApiUrl + "ApiKey=" + ApiKey);
                sb_url.Append("&Action=GetBoxMachineMergePackageList");
                sb_url.Append("&MID=" + MID);
                sb_url.Append("&BID=" + BID);
                sb_url.Append("&CourseType=" + CourseType);
                result = Common.doGet(sb_url.ToString());
                if (!string.IsNullOrEmpty(result))
                {
                    JObject json = JObject.Parse(result);
                    if (json["code"] != null && json["code"].ToString() == "0")
                    {
                        list = JsonConvert.DeserializeObject<List<BoxPackage>>(json["data"][0]["Data"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Common.doException(ex, "GetBoxMergePackageList");
            }
            return list;
        }

        /// <summary>
        ///   new 下单(获取二维码)  2018.5.11
        /// </summary>
        /// <param name="ApiUrl"></param>
        /// <param name="ApiKey"></param>
        /// <param name="BID"></param>
        /// <param name="UID"></param>
        /// <param name="PayType">支付类型（1为微信，0为支付宝）</param>
        /// <param name="CID">套餐ID</param>
        /// <param name="BoxCOID">订单ID</param>
        /// <returns></returns>
        public static Bitmap BoxMergeUserPay(string ApiUrl, string ApiKey, string BID, string UID, string PayType, string CID, string MID, string CourseType, ref string BoxCOID, string CouponId, string MachineDiscount)
        {
            Bitmap bitmap = null;
            try
            {
                string result = "";
                StringBuilder sb_url = new StringBuilder();
                sb_url.Append(ApiUrl + "ApiKey=" + ApiKey);
                sb_url.Append("&Action=BoxMachineMergeUserPay");
                sb_url.Append("&BID=" + BID);
                sb_url.Append("&UID=" + UID);
                sb_url.Append("&CourseType=" + CourseType);
                sb_url.Append("&PayType=" + PayType);
                sb_url.Append("&CID=" + CID);
                sb_url.Append("&MID=" + MID);
                sb_url.Append("&CouponId=" + CouponId);
                sb_url.Append("&MachineDiscount=" + MachineDiscount);
                result = Common.doGet(sb_url.ToString());
                if (!string.IsNullOrEmpty(result))
                {
                    JObject json = JObject.Parse(result);
                    if (json["code"] != null && json["code"].ToString() == "0")
                    {
                        BoxCOID = json["data"]["BoxCOID"] != null ? json["data"]["BoxCOID"].ToString() : "";
                        Common.doException(new Exception(), "产生的COID" + BoxCOID + "  BID:" + BID);
                        if (json["data"]["code"] != null && json["data"]["code"].ToString() == "0")
                        {
                            if (json["data"]["data"] != null && json["data"]["data"].ToString() != "")
                            {
                                if (!string.IsNullOrEmpty(json["data"]["data"].ToString()))
                                {
                                    bitmap = Common.CreateQRCode(json["data"]["data"].ToString(), null);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.doException(ex, "BoxMergeUserPay");
            }
            return bitmap;
        }


        /// <summary>
        /// 获取支付状态
        /// </summary>
        /// <param name="ApiUrl"></param>
        /// <param name="ApiKey"></param>
        /// <param name="BoxCOID">订单号</param>
        /// <returns></returns>
        public static int GetBoxOrderStatus(string ApiUrl, string ApiKey, string BoxCOID)
        {
            int Status = 0;
            try
            {
                string result = "";
                StringBuilder sb_url = new StringBuilder();
                sb_url.Append(ApiUrl + "ApiKey=" + ApiKey);
                sb_url.Append("&Action=GetBoxOrderStatus");
                sb_url.Append("&BoxCOID=" + BoxCOID);
                result = Common.doGet(sb_url.ToString());
                if (!string.IsNullOrEmpty(result))
                {
                    JObject json = JObject.Parse(result);
                    if (json["code"] != null && json["code"].ToString() == "0")
                    {
                        Status = json["data"]["Status"] != null ? Convert.ToInt32(json["data"]["Status"].ToString()) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.doException(ex, "GetBoxOrderStatus");
            }
            return Status;
        }


        /// <summary>
        /// 获取老师在线人数
        /// </summary>
        /// <param name="ApiUrl"></param>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        public static int GetTeachNum(string ApiUrl, string ApiKey)
        {
            int Status = 0;
            try
            {
                string result = "";
                StringBuilder sb_url = new StringBuilder();
                sb_url.Append(ApiUrl + "ApiKey=" + ApiKey);
                sb_url.Append("&Action=GetBoxTeacherCount");
                result = Common.doGet(sb_url.ToString());
                if (!string.IsNullOrEmpty(result))
                {
                    JObject json = JObject.Parse(result);
                    if (json["code"] != null && json["code"].ToString() == "0")
                    {
                        Status = json["data"]!= null ? Convert.ToInt32(json["data"].ToString()) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.doException(ex, "GetTeachNum");
            }
            return Status;
        }
    }
}
