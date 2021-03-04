using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AX.Polygon.Util
{
    public static class Reflect
    {
        /// <summary>
        /// 获取类的 DisplayNameValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetClassDisplayNameValue<T>()
        {
            return GetClassDisplayNameValue(typeof(T));
        }

        public static string GetClassDisplayNameValue(Type type)
        {
            var result = type.GetCustomAttribute<DisplayAttribute>()?.Name;
            return result;
        }

        public static string GetPropertiesDisplayNameValue(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttribute<DisplayAttribute>()?.Name;
        }

        /// <summary>
        /// 获取类的 属性名 与 DisplayNameValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<KeyValuePair<string, string>> GetClassPropertiesDisplayNameValues<T>()
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            var result = new List<KeyValuePair<string, string>>();
            foreach (var p in properties)
            {
                var name = p.Name;
                var displayname = p.GetCustomAttribute<DisplayAttribute>()?.Name;
                result.Add(new KeyValuePair<string, string>(name, displayname));
            }
            return result;
        }

        /// <summary>
        /// 获取类的 DisplayNameValue 与 object值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<KeyValuePair<string, Object>> GetClassPropertiesDisplayNameAndValues<T>(T instance)
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            var result = new List<KeyValuePair<string, object>>();
            foreach (var p in properties)
            {
                var displayname = p.GetCustomAttribute<DisplayAttribute>()?.Name;
                var value = instance.GetType().GetProperty(p.Name).GetValue(instance, null);
                result.Add(new KeyValuePair<string, object>(displayname, value));
            }
            return result;
        }
    }
}