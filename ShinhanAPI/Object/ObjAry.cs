using AxGIEXPERTCONTROLLib;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ShinhanAPI.Object
{
    public static class ObjAry
    {
        public static Type ToClass(string name)
        {
            return Assembly.GetExecutingAssembly().GetType($"ShinhanAPI.Object.{name}");
        }

        #region Object

        public static List<object> SetMultiValue(Type type, AxGiExpertControl indi)
        {
            return SetMultiValue(indi, type, type.GetProperties());
        }

        public static List<object> SetMultiValue(AxGiExpertControl indi, Type type, string[] valName)
        {
            int j = valName.Length;
            List<PropertyInfo> properties = new List<PropertyInfo>();

            for (int i = 0; i < j; i++)
                properties.Add(type.GetProperty(valName[i], BindingFlags.Public));

            return SetMultiValue(indi, type, properties.ToArray());
        }

        public static List<object> SetMultiValue(AxGiExpertControl indi, Type type, PropertyInfo[] properties)
        {
            List<object> objs = new List<object>();
            int rowSize = indi.GetMultiRowCount(), j = properties.Length;

            for (int i = 0; i < rowSize; i++)
            {
                object obj = Activator.CreateInstance(type);
                for (int l = 0; l < j; l++)
                    if (properties[l] != null)
                        properties[l].SetValue(obj, (string)indi.GetMultiData((short)i, (short)l));

                objs.Add(obj);
            }

            return objs;
        }

        public static object SetSingleValue(Type type, AxGiExpertControl indi)
        {
            return SetSingleValue(indi, type, type.GetProperties());
        }

        public static object SetSingleValue(AxGiExpertControl indi, Type type, string[] valName)
        {
            int j = valName.Length;
            List<PropertyInfo> properties = new List<PropertyInfo>();

            for (int i = 0; i < j; i++)
                properties.Add(type.GetProperty(valName[i], BindingFlags.Public));

            return SetSingleValue(indi, type, properties.ToArray());
        }

        public static object SetSingleValue(AxGiExpertControl indi, Type type, PropertyInfo[] properties)
        {
            int j = properties.Length;
            object obj = Activator.CreateInstance(type);

            for (int i = 0; i < j; i++)
            {
                if (properties[i] != null)
                    properties[i].SetValue(obj, (string)indi.GetSingleData((short)i));
            }

            return obj;
        }

        #endregion Object

        #region Generic

        public static List<T> SetMultiValue<T>(AxGiExpertControl indi)
        {
            Type type = typeof(T);

            return SetMultiValue<T>(indi, type.GetProperties());
        }

        public static List<T> SetMultiValue<T>(AxGiExpertControl indi, string[] valName)
        {
            Type type = typeof(T);
            int j = valName.Length;
            List<PropertyInfo> properties = new List<PropertyInfo>();

            for (int i = 0; i < j; i++)
                properties.Add(type.GetProperty(valName[i], BindingFlags.Public));

            return SetMultiValue<T>(indi, properties.ToArray());
        }

        public static List<T> SetMultiValue<T>(AxGiExpertControl indi, PropertyInfo[] properties)
        {
            List<T> objs = new List<T>();
            Type type = typeof(T);
            int rowSize = indi.GetMultiRowCount(), j = properties.Length;

            for (int i = 0; i < rowSize; i++)
            {
                T obj = (T)Activator.CreateInstance(type);
                for (int l = 0; l < j; l++)
                    if (properties[l] != null)
                        properties[l].SetValue(obj, (string)indi.GetMultiData((short)i, (short)l));

                objs.Add(obj);
            }

            return objs;
        }

        public static T SetSingleValue<T>(AxGiExpertControl indi)
        {
            Type type = typeof(T);

            return SetSingleValue<T>(indi, type.GetProperties());
        }

        public static T SetSingleValue<T>(AxGiExpertControl indi, string[] valName)
        {
            Type type = typeof(T);
            int j = valName.Length;
            List<PropertyInfo> properties = new List<PropertyInfo>();

            for (int i = 0; i < j; i++)
                properties.Add(type.GetProperty(valName[i], BindingFlags.Public));

            return SetSingleValue<T>(indi, properties.ToArray());
        }

        public static T SetSingleValue<T>(AxGiExpertControl indi, PropertyInfo[] properties)
        {
            Type type = typeof(T);
            int j = properties.Length;
            T obj = (T)Activator.CreateInstance(type);

            for (int i = 0; i < j; i++)
            {
                if (properties[i] != null)
                    properties[i].SetValue(obj, (string)indi.GetSingleData((short)i));
            }

            return obj;
        }

        #endregion Generic
    }
}