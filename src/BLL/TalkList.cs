using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace BLL
{
    public class TalkList
    {
        /// <summary>
        /// 获取聊天主题
        /// </summary>
        /// <returns></returns>
        public List<TalkType> GetTalkList(string ApiUrl, string ApiKey, string BID)  //
        {
            List<TalkType> list = new List<TalkType>();
            string redult;
            StringBuilder sb_url = new StringBuilder();
            sb_url.Append(ApiUrl + "ApiKey=" + ApiKey);
            sb_url.Append("&Action=GetCourseTopicList");
            sb_url.Append("&BID=" + BID);
            redult = Common.doGet(sb_url.ToString());
            if (!string.IsNullOrEmpty(redult))
            {
                JObject json = JObject.Parse(redult);
                if (json["code"] != null && json["code"].ToString() == "0")
                {
                    list = JsonConvert.DeserializeObject<List<TalkType>>(json["data"].ToString());
                }

                //主题图片缓存
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "CourseTopic"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "CourseTopic");
                }
                foreach (var item in list)
                {
                    try
                    {
                        if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "CourseTopic\\" + item.TopicName + ".png"))
                        {
                            Bitmap btm = (Bitmap)Image.FromStream(WebRequest.Create(item.Img).GetResponse().GetResponseStream());
                            btm.Save(AppDomain.CurrentDomain.BaseDirectory + "CourseTopic\\" + item.TopicName + ".png");
                            item.bitmap = btm;
                        }
                        else
                        {
                            item.bitmap = (Bitmap)Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "CourseTopic\\" + item.TopicName + ".png");
                        }
                    }
                    catch
                    {
                        item.bitmap = (Bitmap)Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"Image\PDefault.jpg");
                    }
                }
            }
            return list;
        }
    }
}
