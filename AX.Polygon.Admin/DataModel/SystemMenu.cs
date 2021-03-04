using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace AX.Polygon.Admin.DataModel
{
    [Display(Name = "菜单")]
    [Table("base_systemmenu")]
    public class SystemMenu : BaseModel, BaseCreateModel, BaseModifyModel, BaseOrderModel, BaseEnabledModel
    {
        [Display(Name = "父菜单Id")]
        public string ParentId { get; set; }

        [Display(Name = "菜单名称")]
        public string Name { get; set; }

        [Display(Name = "菜单图标")]
        public string Icon { get; set; }

        [Display(Name = "菜单Url")]
        public string Url { get; set; }

        [Display(Name = "打开方式")]
        public string Target { get; set; }

        [Display(Name = "权限标识")]
        public string AuthCode { get; set; }

        [Display(Name = "备注")]
        public string Remark { get; set; }
        public DateTime? BaseCreateTime { get; set; }
        public string BaseCreatorId { get; set; }
        public DateTime? BaseModifyTime { get; set; }
        public string BaseModifierId { get; set; }
        public int BaseOrder { get; set; }
        public bool BaseEnabled { get; set; }
    }
}