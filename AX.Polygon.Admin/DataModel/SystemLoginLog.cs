using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace AX.Polygon.Admin.DataModel
{
    [Display(Name = "系统登录日志")]
    [Table("base_systemloginlog")]
    public class SystemLoginLog : BaseModel,BaseCreateModel
    {
        [Display(Name = "登录名")]
        public string LoginName { get; set; }

        [Display(Name = "类型")]
        public string Type { get; set; }

        [Display(Name = "登录IP")]
        public string Ip { get; set; }

        [Display(Name = "登录地")]
        public string IpLocation { get; set; }

        [Display(Name = "浏览器")]
        public string Browser { get; set; }

        [Display(Name = "系统")]
        public string OS { get; set; }
        public DateTime? BaseCreateTime { get; set; }
        public string BaseCreatorId { get; set; }
    }
}