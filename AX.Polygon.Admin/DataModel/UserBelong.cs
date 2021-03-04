using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace AX.Polygon.Admin.DataModel
{
    [Display(Name = "用户身份分配")]
    [Table("base_userbelong")]
    public class UserBelong : BaseModel, BaseCreateModel, BaseModifyModel
    {
        [Display(Name = "用户ID")]
        public string UserId { get; set; }

        [Display(Name = "所属Id")]
        public string BelongId { get; set; }

        [Display(Name = "所属分类 角色/部门")]
        public string BelongType { get; set; }
        public DateTime? BaseCreateTime { get; set; }
        public string BaseCreatorId { get; set; }
        public DateTime? BaseModifyTime { get; set; }
        public string BaseModifierId { get; set; }
    }
}