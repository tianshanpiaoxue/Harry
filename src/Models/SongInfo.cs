using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public  class SongInfo
    {
        /// <summary>
        /// 歌曲ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 歌曲名
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 教材分类（成人、儿童）
        /// </summary>
        public string SingerName { get; set; }

        /// <summary>
        /// 教材难度等级
        /// </summary>
        public string Difficulty { get; set; }

        /// <summary>
        /// 教材图标
        /// </summary>
        public string TextBookCover { get; set; }

        /// <summary>
        /// 歌词文件路径
        /// </summary>
        public string LyricsFileUrl { get; set; }

        /// <summary>
        /// 歌词概要文件路径
        /// </summary>
        public string LyricsSummaryFileUrl { get; set; }

        /// <summary>
        /// 歌词视频路径
        /// </summary>
        public string LyricsVideoUrl { get; set; }

        /// <summary>
        /// 时间长度
        /// </summary>
        public int Second { get; set; }

        /// <summary>
        /// 是否选择
        /// </summary>
        public bool isClick { get; set; }

       
        //显示的图片
        public string bitmap { get; set; }


        public SongInfo()
        {
            ID = "";
            Title = "";
            Describe = "";
            SingerName = "";
            Difficulty = "";
            TextBookCover = "";
            LyricsFileUrl = "";
            LyricsSummaryFileUrl = "";
            LyricsVideoUrl = "";
            Second = 0;
            isClick = false;
        }
    }
}
