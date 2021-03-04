using System.Collections.Generic;

namespace AX.Polygon.DataRepository.Model
{
    public class SearchArguments
    {
        public SearchArguments()
        {
        }

        public string SelectField { get; set; }

        public string Order { get; set; }

        /// <summary>
        /// 第一页从零开始
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// -1则为不分页
        /// </summary>
        public int PageItemCount { get; set; }

        public List<SearchFilter> SearchFilter { get; set; }

        public bool UsePage { get { return PageItemCount == -1; } }
    }

    public class SearchFilter
    {
        public string FilterName { get; set; }

        public object FilterValue { get; set; }

        /// <summary>
        /// 过滤类型
        /// =
        /// >
        /// <
        /// in
        /// not in
        /// like
        /// </summary>
        public string FilterType { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.FilterName) ||
                    string.IsNullOrWhiteSpace(this.FilterType) ||
                    this.FilterValue == null)
                {
                    return false;
                }
                return true;
            }
        }
    }
}