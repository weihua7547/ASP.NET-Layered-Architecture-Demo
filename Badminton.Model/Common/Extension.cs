using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Model.Common
{
    public static class Extension
    {
        public static T? GetAttribute<T>(this Enum obj) where T : Attribute
        {
            FieldInfo[] fields = obj.GetType().GetFields();
            FieldInfo[] array = fields;
            foreach (FieldInfo fieldInfo in array)
            {
                if (Attribute.GetCustomAttribute(fieldInfo, typeof(T)) is T result && fieldInfo.Name == obj.ToString())
                {
                    return result;
                }
            }

            return null;
        }

        public static T? GetAttribute<T>(this MemberInfo prop, bool inherit = false) where T : Attribute
        {
            Type typeFromHandle = typeof(T);
            if (prop == null)
            {
                return null;
            }

            return (T)prop.GetCustomAttributes(typeFromHandle, inherit).FirstOrDefault();
        }
    }
}
