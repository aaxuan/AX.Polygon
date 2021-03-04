using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace AX.Polygon.Admin.DataModel
{
    [Display(Name = "用户")]
    [Table("base_user")]
    public class User : BaseModel, BaseCreateModel, BaseModifyModel, BaseEnabledModel
    {
        [Display(Name = "用户名称")]
        public string UserName { get; set; }

        [Display(Name = "登录名")]
        public string LoginName { get; set; }

        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "盐值")]
        public string Salt { get; set; }

        [Display(Name = "性别")]
        public string Gender { get; set; }

        [Display(Name = "生日")]
        public DateTime Birthday { get; set; }

        [Display(Name = "性别")]
        public string Email { get; set; }

        [Display(Name = "手机号")]
        public string Mobile { get; set; }

        [Display(Name = "QQ")]
        public string QQ { get; set; }

        [Display(Name = "Wechat")]
        public string Wechat { get; set; }

        [Display(Name = "登录总次数")]
        public int LoginCount { get; set; }

        [Display(Name = "是否在线")]
        public bool IsOnline { get; set; }

        [Display(Name = "是否管理员")]
        public bool IsAdmin { get; set; }

        [Display(Name = "首次登录时间")]
        public DateTime? FirstVisit { get; set; }

        [Display(Name = "上一次登录时间")]
        public DateTime? PreviousVisit { get; set; }

        [Display(Name = "最后一次登录时间")]
        public DateTime? LastVisit { get; set; }

        [Display(Name = "备注")]
        public string Remark { get; set; }

        public DateTime? BaseCreateTime { get; set; }
        public string BaseCreatorId { get; set; }
        public DateTime? BaseModifyTime { get; set; }
        public string BaseModifierId { get; set; }
        public bool BaseEnabled { get; set; }
    }
}