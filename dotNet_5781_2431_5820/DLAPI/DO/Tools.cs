using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public static class Tools
    {
        public static string ToStringProperty<T>(this T t)
        {
            string str = "";
            foreach (PropertyInfo item in typeof(T).GetProperties())
                str += "\n" + item.Name + ": " + item.GetValue(t, null);
            return str;
        }

        public static T GetPropertiesFrom<T, S>(this S from) where T : new()
        {
            T to = new T();
            foreach (PropertyInfo propTo in to.GetType().GetProperties())
            {
                PropertyInfo propFrom = from.GetType().GetProperty(propTo.Name);
                if (propFrom == null)
                    continue;
                var value = propFrom.GetValue(from, null);
                if (value is ValueType || value is string)
                    propTo.SetValue(to, value);
            }
            return to;
        }
    }
}

/*
 using System;
using System.Reflection;
using DalApi;
//using DO;

namespace DL
{
    public static class Tools
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        public static T GetPropertiesFrom<T, S>(this S from) where T : new()
        {
            T to = new T();
            foreach (PropertyInfo propTo in to.GetType().GetProperties())
            {
                PropertyInfo propFrom = from.GetType().GetProperty(propTo.Name);
                if (propFrom == null)
                    continue;
                var value = propFrom.GetValue(from, null);
                if (value is ValueType || value is string)
                    propTo.SetValue(to, value);
            }
            return to;
        }
    }
}*/
