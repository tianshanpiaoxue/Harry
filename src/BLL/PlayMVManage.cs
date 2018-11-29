using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json.Linq;

namespace BLL
{
    public class PlayMVManage
    {
        /// <summary>
        /// 获取MV时间段和伴唱(开始练习)
        /// </summary>
        /// <returns></returns>
        public static Models.Music GetBoxKyxPracticeSentencesList(string ApiUrl, string ApiKey, string BID, string LID, string StopTime = "")
        {
            Music model = new Music();
            try
            {
                string result = "";
                StringBuilder sb_url = new StringBuilder();
                sb_url.Append(ApiUrl + "ApiKey=" + ApiKey);
                sb_url.Append("&Action=GetBoxKyxPracticeSentencesList");
                sb_url.Append("&BID=" + BID);
                sb_url.Append("&LID=" + LID);
                //sb_url.Append("&LID=" + 64);
                result = Common.doGet(sb_url.ToString());
                if (!string.IsNullOrEmpty(result))
                {
                    JObject json = JObject.Parse(result);
                    if (json["code"] != null && json["code"].ToString() == "0")
                    {
                        if (json["data"] != null && json["data"].Count() != 0)
                        {
                            model.BackgMusic = json["data"][0]["BackgMusic"] != null ? json["data"][0]["BackgMusic"].ToString() : "";
                            foreach (var item in json["data"][0]["Data"])
                            {
                                var period = new MusicPeriod();
                                period.PsID = item["PsID"] != null ? item["PsID"].ToString() : "";
                                period.Sentences = item["Sentences"] != null ? item["Sentences"].ToString() : "";
                                var timeArray = item["rec_start_end"].ToString().Split('-');
                                if (timeArray.Length > 1)
                                {
                                    period.StartTime =Convert.ToDouble(timeArray[0]);
                                    period.EndTime = Convert.ToDouble(timeArray[1]);
                                }
                                else if (timeArray.Length > 0)
                                {
                                    period.StartTime =Convert.ToDouble( timeArray[0]);
                                }
                                model.MusicPeriodList.Add(period);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.doException(ex, "GetBoxKyxPracticeSentencesList");
            }
            return model;
        }
    }
}
