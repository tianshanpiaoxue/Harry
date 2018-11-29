using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL
{
    public class SongList
    {
        /// <summary>
        /// 获取歌曲集合
        /// </summary>
        /// <param name="ApiUrl"></param>
        /// <param name="ApiKey"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public List<SongInfo> GetAll(string ApiUrl, string ApiKey, string Type)
        {
            List<SongInfo> list = new List<SongInfo>();  //string ApiUrl, string ApiKey, string Type
            try
            {
                string result = "";
                //【-1 全部 0少儿 1流行 2经典 3影视】

                StringBuilder sb_url = new StringBuilder();
                sb_url.Append(ApiUrl + "ApiKey=" + ApiKey);
                sb_url.Append("&Action=GetNewBoxTeachingMaterial");
                //sb_url.Append("&Action=GetBoxTeachingMaterial");
                sb_url.Append("&Type=" + Type);
                result = Common.doGet(sb_url.ToString());
                if (!string.IsNullOrEmpty(result))
                {
                    JObject json = JObject.Parse(result);
                    if (json["code"] != null && json["code"].ToString() == "0")
                    {
                        list = JsonConvert.DeserializeObject<List<SongInfo>>(json["data"][0]["Data"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Common.doException(ex, "GetAll");
            }
            return list;
        }
        


   
    }
}
