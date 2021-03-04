using Microsoft.AspNetCore.Http;
using System;

namespace AX.Polygon.Util
{
    public class CookieHelper
    {
        public static void Write(string name, string value, bool httpOnly = true)
        {
            Write(name, value, 5, httpOnly);
        }

        public static void Write(string name, string value, int expires, bool httpOnly = true)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTimeOffset.UtcNow.AddMinutes(expires);
            option.HttpOnly = httpOnly;
            IOCManager.HttpContextAccessor?.HttpContext?.Response.Cookies.Append(name, value, option);
        }

        public static string GetValue(string name)
        {
            return IOCManager.HttpContextAccessor?.HttpContext?.Request.Cookies[name];
        }

        public static void Remove(string name)
        {
            IOCManager.HttpContextAccessor?.HttpContext?.Response.Cookies.Delete(name);
        }
    }
}