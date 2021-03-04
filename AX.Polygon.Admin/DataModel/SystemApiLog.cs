using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace AX.Polygon.Admin.DataModel
{
    [Display(Name = "系统请求日志")]
    [Table("base_systemapilog")]
    public class SystemApiLog : BaseModel, BaseCreateModel
    {
        [Display(Name = "请求状态")]
        public string LogStatus { get; set; }

        [Display(Name = "请求路径")]
        public string Url { get; set; }

        [Display(Name = "请求参数")]
        public string Param { get; set; }

        [Display(Name = "请求返回")]
        public string Result { get; set; }
        public DateTime? BaseCreateTime { get; set; }
        public string BaseCreatorId { get; set; }
    }
}