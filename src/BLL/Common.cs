using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ThoughtWorks.QRCode.Codec;
using System.Windows;

namespace BLL
{
    public  class Common
    {
        public static string path = "C:\\KTVLog\\";
        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetKTVConfig(string name)
        {
            string result = "";
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(AppDomain.CurrentDomain.BaseDirectory + "KTV.xml");
                XmlNode node = document.SelectSingleNode("KTVConfig");
                if (node != null)
                {
                    result = node.SelectSingleNode(name).InnerText;
                }
            }
            catch (Exception ex)
            {
                doException(ex, "读取配置文件");
            }
            return result;
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string doGet(string url)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.ContentType = "application/x-www-form-urlencoded";

            try
            {
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                Stream stream = resp.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }

            }
            catch (Exception ex)
            {
                doException(ex, url);
            }

            return result;
        }
        /// <summary>
        /// 生成二维码，如果有Logo，则在二维码中添加Logo
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static Bitmap CreateQRCode(string content, Bitmap logo = null)
        {
            QRCodeEncoder qrEncoder = new QRCodeEncoder();
            qrEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrEncoder.QRCodeScale = 5;
            qrEncoder.QRCodeVersion = 7;
            qrEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            try
            {
                Bitmap qrcode = qrEncoder.Encode(content, Encoding.UTF8);
                //画logo
                if (logo != null)
                {
                    Graphics g = Graphics.FromImage(qrcode);
                    Bitmap bitmapLogo = logo;
                    int logoSize = 60;
                    bitmapLogo = new Bitmap(bitmapLogo, new System.Drawing.Size(logoSize, logoSize));
                    PointF point = new PointF(qrcode.Width / 2 - logoSize / 2, qrcode.Height / 2 - logoSize / 2);
                    g.DrawImage(bitmapLogo, point);
                }
                return qrcode;
            }
            catch (IndexOutOfRangeException ex)
            {
                //MessageBox.Show("超出当前二维码版本的容量上限，请选择更高的二维码版本！", "系统提示");
                doException(ex, "生成二维码 CreateQRCode 超出当前二维码版本的容量上限，请选择更高的二维码版本！");
                return new Bitmap(100, 100);
               
            }
            catch (Exception ex)
            {
                //MessageBox.Show("生成二维码出错！", "系统提示");
                return new Bitmap(100, 100);
            }
        }

     

        /// <summary>
        /// 把时间秒数转化成字符串00:00:00
        /// </summary>
        /// <param name="second">时间秒数</param>
        /// <returns>字符串00:00:00</returns>
        public static string TransTimeSecondIntToString(long second)
        {
            string str = "";
            try
            {
                long hour = second / 3600;
                long min = second % 3600 / 60;
                long sec = second % 60;
                if (hour < 10)
                {
                    str += "0" + hour.ToString();
                }
                else
                {
                    str += hour.ToString();
                }
                if (str == "00")
                {
                    str = "";
                }
                else
                {
                    str += ":";
                }

                if (min < 10)
                {
                    str += "0" + min.ToString();
                }
                else
                {
                    str += min.ToString();
                }
                str += ":";
                if (sec < 10)
                {
                    str += "0" + sec.ToString();
                }
                else
                {
                    str += sec.ToString();
                }
            }
            catch (System.Exception ex)
            {
                doException(ex, "把时间秒数转化成字符串00:00:00   TransTimeSecondIntToString");
            }
            return str;
        }

     

        public static void doException(Exception ex, string message)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                FileStream fs = new FileStream(path + "\\" + DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + ".txt", FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("-----------------------------------------");
                sw.WriteLine("时间：" + DateTime.Now.ToString());
                sw.WriteLine("消息:" + ex.Message);
                sw.WriteLine("数据：" + ex.Data);
                sw.WriteLine("源:" + ex.Source);
                sw.WriteLine("--类型:" + message);
                sw.WriteLine("-----------------------------------------\r\n");
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            catch
            { }
        }


        public static void doMyError(Exception ex, string message)
        {
            try
            {
                if (!Directory.Exists("C:\\KTVLog\\"))
                {
                    Directory.CreateDirectory("C:\\KTVLog\\");
                }
                FileStream fs = new FileStream("C:\\KTVLog\\" + "\\" + DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + ".txt", FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("-----------------------------------------");
                sw.WriteLine("时间：" + DateTime.Now.ToString());
                sw.WriteLine("消息:" + ex.Message);
                sw.WriteLine("数据：" + ex.Data);
                sw.WriteLine("源:" + ex.Source);
                sw.WriteLine("--类型:" + message);
                sw.WriteLine("-----------------------------------------\r\n");
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            catch
            { }
        }

        /// <summary>
        /// 判断是否有网
        /// </summary>
        /// <returns></returns>
        public static bool CheckForInternetConnection(string PingURL)
        {
            try
            {
                if( new Ping().Send(PingURL, 3000).Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

     
    }
}
