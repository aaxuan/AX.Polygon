using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace AX.Polygon.Admin.DataModel
{
    [Display(Name = "授权表")]
    [Table("base_systemauthorize")]
    public class SystemAuthorize : BaseModel, BaseCreateModel, BaseModifyModel, BaseOrderModel
    {
        [Display(Name = "授权类别 用户/角色/部门")]
        public string AuthorizeType { get; set; }

        [Display(Name = "授权id")]
        public string AuthorizeId { get; set; }

        [Display(Name = "授权内容")]
        public string AuthorizeCode { get; set; }
        public DateTime? BaseCreateTime { get; set; }
        public string BaseCreatorId { get; set; }
        public DateTime? BaseModifyTime { get; set; }
        public string BaseModifierId { get; set; }
        public int BaseOrder { get; set; }
    }
}