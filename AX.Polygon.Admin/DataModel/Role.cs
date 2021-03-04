using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace AX.Polygon.Admin.DataModel
{
    [Display(Name = "角色")]
    [Table("base_role")]
    public class Role : BaseModel, BaseCreateModel, BaseModifyModel, BaseOrderModel, BaseEnabledModel
    {
        [Display(Name = "角色名称")]
        public string Name { get; set; }

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