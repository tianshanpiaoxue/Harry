using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BoxPackage
    {
        /// <summary>
        /// 套餐ID
        /// </summary>
        public int CID { get; set; }
        /// <summary>
        /// 套餐名
        /// </summary>
        public string PackageName { get; set; }
        
        /// <summary>
        /// 时长
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 套餐价格
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public double OriginalPrice { get; set; }

        /// <summary>
        /// 1：免费    2：打折
        /// </summary>
        public int IsFreeAdmission { get; set; }

        /// <summary>
        /// 是否到了换班点 1：no   2：yes
        /// </summary>
        public int IsGooffwork { get; set; }

        /// <summary>
        /// 优惠券ID
        /// </summary>
        public string CouponId { get; set; }

        /// <summary>
        /// 机器折扣 默认：100
        /// </summary>
        public string MachineDiscount { get; set; }

        /// <summary>
        /// 标签 ,示例：【1,2】 0：无，1：最热，2：优惠, 有的时候可能会是空字符串
        /// </summary>
        public string Label { get; set; }

    }
}
