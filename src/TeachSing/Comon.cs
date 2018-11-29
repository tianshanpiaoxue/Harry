using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using System.Drawing;
using System.Net;

namespace TeachSing
{
    public class Common
    {
        public Common()
        {
            ApiUrl = BLL.Common.GetKTVConfig("ApiUrl");
            ApiKey =BLL. Common.GetKTVConfig("ApiKey");
            pathSong =BLL. Common.GetKTVConfig("pathSong");
            MID = BLL.Common.GetKTVConfig("MID");
            IPAddress[] ips = Dns.GetHostAddresses("ies.acadsoc.com.cn");
            PingURL = ips[0].ToString();
        }
        public static string ApiUrl = "";
        public static string ApiKey = "";
        public static string BID = "";
        public static string MID = "";

        public static string UID = "";
        public static string OpenID = "";
        //订单ID
        public static string COID = ""; 
        public static string pathSong = "";
        public static string bcrid = "";
        public static string ClassInUrl = "";
        public static ShelterUp frmShelterUp;
        public static ShelterDown frmShelterDown;
        public static ShelterRight frmShelterRight;
        public static ShelterButton frmShelterButton;
        //选中歌曲
        public static Models.SongInfo songInfo;
        //选中套餐
        public static Models.TalkType talkType;

        public static string PingURL = "www.baidu.com";
    }
}
