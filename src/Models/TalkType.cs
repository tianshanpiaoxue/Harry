using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 聊天话题列表
    /// </summary>
    public class TalkType
    {
        public int TopicID { get; set; }
        public string TopicName { get; set; }
        public string Img { get; set; }
        public Bitmap  bitmap { get; set; }
    }
    
}
