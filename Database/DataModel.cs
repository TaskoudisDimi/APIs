using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class DataModel
    {

        public static T Select<T>(Type table) where T : class, new()
        {
            //OpenConnection
            //find the Type of calling object
            //find datatables executing the query based on the object Type
            //convert the datatables to List
            DataContext.Instance.CheckConnection();

            Type t = new T().GetType();
            List<T> test = new List<T>();

            return test.FirstOrDefault();
        }



        public static T Delete<T>() where T : class, new()
        {
            string testProperty = "Test";
            PropertyInfo test2 = testProperty.GetType().GetProperty("test");

            Type t = new T().GetType();
            List<T> test = new List<T>();

            return test.FirstOrDefault();
        }

        public static T Update<T>() where T : class, new()
        {
            Type t = new T().GetType();
            List<T> test = new List<T>();

            return test.FirstOrDefault();
        }

        public static T Create<T>() where T : class, new()
        {
            Type t = new T().GetType();
            List<T> test = new List<T>();

            return test.FirstOrDefault();
        }



        #region Helpers

        public static List<T> GetListFromDataTable<T>(DataTable table) where T : class, new()
        {
            List<T> list = new List<T>();
            if (table != null)
            {
                foreach (var row in table.AsEnumerable())
                {
                    list.Add(GetObjectFromDataRow<T>(row));
                }
                table.Dispose();
            }
            return list;
        }
        public static T GetObjectFromDataRow<T>(DataRow row) where T : class, new()
        {

            T obj = new T();
            foreach (var prop in obj.GetType().GetProperties())
            {
                PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                Type propType = propertyInfo.PropertyType;

                // If there is no setter for this property
                if (propertyInfo.SetMethod == null)
                    continue;

                string ColumnName = prop.Name;

                if (row.Table.Columns.Contains(ColumnName))
                {
                    object value = row[ColumnName];

                    if (value == null || value == DBNull.Value)
                    {
                        if (propertyInfo.PropertyType == typeof(string))
                            propertyInfo.SetValue(obj, string.Empty, null);
                        else
                            propertyInfo.SetValue(obj, null, null);
                    }
                    else
                    {
                        // If it is nullable
                        if (propType.IsGenericType && propType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                        {
                            if (propType == typeof(int?))
                            {
                                value = Utils.GetNullInt(value);
                                propertyInfo.SetValue(obj, ChangeType<int>(value), null);
                            }
                            else if (propType == typeof(long?))
                            {
                                value = Utils.GetNullLong(value);
                                propertyInfo.SetValue(obj, ChangeType<long>(value), null);
                            }
                            else if (propType == typeof(decimal?))
                            {
                                value = Utils.GetNullDecimal(value);
                                propertyInfo.SetValue(obj, ChangeType<decimal>(value), null);
                            }
                            else if (propType == typeof(float?))
                            {
                                value = Utils.GetNullFloat(value);
                                propertyInfo.SetValue(obj, ChangeType<float>(value), null);
                            }
                            else if (propType == typeof(double?))
                            {
                                value = Utils.GetNullDouble(value);
                                propertyInfo.SetValue(obj, ChangeType<double>(value), null);
                            }
                            else if (propType == typeof(bool?))
                            {
                                value = Utils.GetNullBool(value);
                                propertyInfo.SetValue(obj, ChangeType<bool>(value), null);
                            }
                            else if (propType == typeof(DateTime?))
                            {
                                //TODO propType = typeof(DateTime); DateTime temp; if (DateTime.TryParse(Utils.GetString(value), out temp)) { value = temp; } else { value = new DateTime(1900, 1, 1); }
                                propertyInfo.SetValue(obj, ChangeType<DateTime>(value), null);
                            }
                        }
                        else// If the type is not nullable
                        {
                            if (propType == typeof(int)) { value = Utils.GetInt(value, 0); }
                            else if (propType == typeof(long)) { value = Utils.GetLong(value, 0); }
                            else if (propType == typeof(float)) { value = Utils.GetFloat(value, 0); }
                            else if (propType == typeof(double)) { value = Utils.GetDouble(value, 0); }
                            else if (propType == typeof(decimal)) { value = Utils.GetDecimal(value, 0); }
                            else if (propType == typeof(bool)) { value = Utils.GetBool(value, false); }
                            else if (propType == typeof(DateTime)) { value = Utils.GetDate(value, new DateTime(1700, 1, 1)); }
                            propertyInfo.SetValue(obj, Convert.ChangeType(value, propType), null);
                        }
                    }
                }
            }
            return obj;
        }

        public static T ChangeType<T>(object value)
        {
            var t = typeof(T);

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return default(T);
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return (T)Convert.ChangeType(value, t);
        }


        #endregion
    }
}
