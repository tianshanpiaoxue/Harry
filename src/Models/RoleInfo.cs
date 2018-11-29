using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{

    /// <summary>
    /// 歌曲类别
    /// </summary>
    public class RoleInfo
    {
        public RoleInfo()
        {
            level = "";
            CategoryId = "";
            CategoryName = "";
            FitCrowd = "";
        }
        /// <summary>
        /// 等级
        /// </summary>
        public string level { get; set; }
        /// <summary>
        /// 角色代码
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 适合人群
        /// </summary>
        public string FitCrowd { get; set; }
    }
}
