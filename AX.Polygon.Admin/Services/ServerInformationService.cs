using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace AX.Polygon.Admin.Services
{
    public class ServerInfo
    {
        [Display(Name = "运行框架")]
        public static string FrameworkDescription { get { return RuntimeInformation.FrameworkDescription; } }

        [Display(Name = "操作系统")]
        public static string OSDescription { get { return RuntimeInformation.OSDescription; } }

        [Display(Name = "操作系统版本")]
        public static string OSVersion { get { return Environment.OSVersion.ToString(); } }

        [Display(Name = "平台架构")]
        public static string OSArchitecture { get { return RuntimeInformation.OSArchitecture.ToString(); } }

        [Display(Name = "机器名称")]
        public static string MachineName { get { return Environment.MachineName; } }

        [Display(Name = "用户网络域名")]
        public static string UserDomainName { get { return Environment.UserDomainName; } }

        [Display(Name = "系统目录")]
        public static string SystemDirectory { get { return Environment.SystemDirectory; } }

        [Display(Name = "系统已运行时间(毫秒)")]
        public static int TickCount { get { return Environment.TickCount; } }

        [Display(Name = "是否在交互模式中运行")]
        public static bool UserInteractive { get { return Environment.UserInteractive; } }

        [Display(Name = "当前关联用户名")]
        public static string UserName { get { return Environment.UserName; } }

        [Display(Name = "Web程序核心框架版本")]
        public static string Version { get { return Environment.Version.ToString(); } }
    }

    public class ServerInformationService
    {
        public List<KeyValuePair<string, object>> GetServerInfo()
        {
            return Util.Reflect.GetClassPropertiesDisplayNameAndValues(new ServerInfo());
        }
    }
}