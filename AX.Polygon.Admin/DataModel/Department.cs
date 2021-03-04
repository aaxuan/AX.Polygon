using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace AX.Polygon.Admin.DataModel
{
    [Display(Name = "部门表")]
    [Table("base_department")]
    public class Department : BaseModel, BaseCreateModel, BaseModifyModel, BaseOrderModel
    {
        [Display(Name = "父部门ID")]
        public string ParentId { get; set; }

        [Display(Name = "部门名称")]
        public string Name { get; set; }

        [Display(Name = "部门电话")]
        public string Telephone { get; set; }

        [Display(Name = "部门Email")]
        public string Email { get; set; }

        [Display(Name = "部门负责人Id")]
        public string AdminUserId { get; set; }

        [Display(Name = "备注")]
        public string Remark { get; set; }

        public DateTime? BaseCreateTime { get; set; }
        public string BaseCreatorId { get; set; }
        public DateTime? BaseModifyTime { get; set; }
        public string BaseModifierId { get; set; }
        public int BaseOrder { get; set; }
    }
}