using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MusicPeriod
    {//歌词id
        public string PsID { get; set; }
        //歌词内容
        public string Sentences { get; set; }
        //歌词时间段
        public double StartTime { get; set; }

        public double EndTime { get; set; }

        public bool IsSend { get; set; }

        public int Overall { get; set; }

        public int Fluency { get; set; }

        public string Pron { get; set; }
    }

    public class Music
    {
        public string BackgMusic { get; set; }

        public List<MusicPeriod> MusicPeriodList = new List<MusicPeriod>();
    }


    public class WordScore
    {
        public string Word { get; set; }
        public int Score { get; set; }
    }
}
