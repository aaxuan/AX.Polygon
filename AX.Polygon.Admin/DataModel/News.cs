using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace AX.Polygon.Admin.DataModel
{
    [Display(Name = "新闻公告")]
    [Table("base_news")]
    public class News : BaseModel, BaseCreateModel, BaseModifyModel, BaseOrderModel
    {
        [Display(Name = "新闻标题")]
        public string Title { get; set; }

        [Display(Name = "内容")]
        public string Content { get; set; }

        [Display(Name = "新闻标签")]
        public string NewsTag { get; set; }

        [Display(Name = "缩略图")]
        public string ThumbImage { get; set; }

        [Display(Name = "发布者")]
        public string Author { get; set; }

        [Display(Name = "发布时间")]
        public DateTime NewsDate { get; set; }

        [Display(Name = "状态")]
        public int State { get; set; }

        [Display(Name = "查看次数")]
        public int ViewCount { get; set; }
        public DateTime? BaseCreateTime { get; set; }
        public string BaseCreatorId { get; set; }
        public DateTime? BaseModifyTime { get; set; }
        public string BaseModifierId { get; set; }
        public int BaseOrder { get; set; }
    }
}