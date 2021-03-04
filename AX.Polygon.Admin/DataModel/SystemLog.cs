using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;

namespace AX.Polygon.Admin.DataModel
{
    [Display(Name = "系统日志")]
    [Table("base_systemlog")]
    public class SystemLog : BaseModel
    {
        [Display(Name = "日志类别")]
        public string LogType { get; set; }

        [Display(Name = "日志内容")]
        public string LogMessage { get; set; }
    }
}