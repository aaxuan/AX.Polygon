using System;
using System.Reflection;

namespace AX.Polygon.Util
{
    public static class VersionManager
    {
        public static string GetVersion()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            return version.Major + "." + version.Minor;
        }
    }
}