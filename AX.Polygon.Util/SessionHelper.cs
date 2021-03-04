using Newtonsoft.Json;
using System.Text;

namespace AX.Polygon.Util
{
    public class SessionHelper
    {
        public static void Write<T>(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key)) { return; }
            var valueJsonStr = JsonConvert.SerializeObject(value);
            IOCManager.HttpContextAccessor?.HttpContext?.Session.Set(key, Encoding.Unicode.GetBytes(valueJsonStr));
        }

        public static T GetValue<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) { return default(T); }
            byte[] value = new byte[] { };
            if (IOCManager.HttpContextAccessor?.HttpContext?.Session.TryGetValue(key, out value) == true)
            {
                return JsonConvert.DeserializeObject<T>(Encoding.Unicode.GetString(value));
            }
            return default(T);
        }

        public static void Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) { return; }
            IOCManager.HttpContextAccessor?.HttpContext?.Session.Remove(key);
        }
    }
}