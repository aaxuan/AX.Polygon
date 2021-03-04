using System.Collections.Generic;

namespace AX.Polygon.DataRepository.Model
{
    public class PageData<T>
    {
        /// <summary>
        /// 数据总量
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public List<T> Items { get; set; } = new List<T>();

        /// <summary>
        /// 本页开始索引
        /// </summary>
        public int BeginIndex { get; set; }

        /// <summary>
        /// 本页结束索引
        /// </summary>
        public int EndIndex { get; set; }

        /// <summary>
        /// 页面索引
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 总页面数
        /// </summary>
        public int TotalPageCount { get; set; }
    }
}