using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class DBAttributeHelper
    {

        #region Comments
        

        //#region Attribute Properties

        //public static string GetPropertyDatatableName(PropertyInfo property)
        //{
        //    string result = string.Empty;
        //    object[] attrs = property.GetCustomAttributes(true);
        //    foreach (object attr in attrs)
        //    {
        //        DatabaseColumn attribute = attr as DatabaseColumn;
        //        if (attribute != null)
        //        {
        //            result = attribute.Name;
        //            break;
        //        }
        //    }
        //    return string.IsNullOrEmpty(result) ? property.Name : result;
        //}

        //public static string DatabaseTable(this Type t)
        //{
        //    Attribute[] attrs = Attribute.GetCustomAttributes(t);
        //    foreach (Attribute attr in attrs)
        //        if (attr is DatabaseTable)
        //            return ((DatabaseTable)attr).Table;

        //    return null;
        //}

        ///// <summary>
        ///// Default "". Returns the description of this Type, if exists
        ///// </summary>
        //public static string DatabaseTableDescription(this Type t)
        //{
        //    Attribute[] attrs = Attribute.GetCustomAttributes(t);
        //    foreach (Attribute attr in attrs)
        //        if (attr is DatabaseTable)
        //            return string.IsNullOrWhiteSpace(((DatabaseTable)attr).Description) == false ? ((DatabaseTable)attr).Description : ((DatabaseTable)attr).Table;

        //    return string.Empty;
        //}

        //public static string DatabaseJoinTables(this Type t)
        //{
        //    Attribute[] attrs = System.Attribute.GetCustomAttributes(t);
        //    foreach (Attribute attr in attrs)
        //        if (attr is DatabaseTable)
        //            return Utils.GetString(((DatabaseTable)attr).JoinTables);

        //    return string.Empty;
        //}

        //public static DatabaseColumn DatabasePrimaryKey(this Type t)
        //{
        //    PropertyInfo primarykey = t.GetProperties().SingleOrDefault(prop => Attribute.IsDefined(prop, typeof(DatabaseColumn)) && ((DatabaseColumn)Attribute.GetCustomAttribute(prop, typeof(DatabaseColumn))).IsPrimaryKey);
        //    if (primarykey != null)
        //        return ((DatabaseColumn)(Attribute.GetCustomAttribute(primarykey, typeof(DatabaseColumn))));

        //    throw new KeyNotFoundException("No primary key defined for " + t.Name);
        //}

        //public static string GetSortField(this Type t, string propertyName)
        //{
        //    var property = t.GetProperties().SingleOrDefault(x => Attribute.IsDefined(x, typeof(DatabaseColumn)) && x.Name.Equals(propertyName));
        //    if (property != null)
        //    {
        //        string sortQuery = ((DatabaseColumn)Attribute.GetCustomAttribute(property, typeof(DatabaseColumn))).SortQuery;
        //        if (string.IsNullOrWhiteSpace(sortQuery) == false)
        //            return sortQuery;
        //    }
        //    return propertyName;
        //}

        //public static IEnumerable<DatabaseAttribute> DatabaseAllObjectFieldsAttributes(this Type t)
        //{
        //    return t.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(DatabaseColumn)) || Attribute.IsDefined(prop, typeof(DatabaseVirtualColumn))).Select(x => (DatabaseAttribute)Attribute.GetCustomAttribute(x, typeof(DatabaseAttribute)));
        //}

        //public static IEnumerable<PropertyInfo> DatabaseAllObjectFieldProperties(this Type t)
        //{
        //    return t.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(DatabaseAttribute)));
        //}

        //public static List<DatabaseColumn> DatabaseFieldsAttributes(this Type t)
        //{
        //    var fields = t.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(DatabaseColumn))).Select(x => (DatabaseColumn)Attribute.GetCustomAttribute(x, typeof(DatabaseColumn))).ToList();
        //    // Set tables
        //    fields.ForEach(x => x.Table = t.DatabaseTable());

        //    return fields;
        //}

        //public static IEnumerable<DatabaseVirtualColumn> DatabaseVirtualFieldsAttributes(this Type t)
        //{
        //    return t.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(DatabaseVirtualColumn))).Select(x => (DatabaseVirtualColumn)Attribute.GetCustomAttribute(x, typeof(DatabaseVirtualColumn)));
        //}

        //public static IEnumerable<PropertyInfo> DatabaseFieldsProperties(this Type t)
        //{
        //    return t.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(DatabaseColumn)));
        //}

        //public static IEnumerable<string> DatabaseNotMappedFields(this Type t)
        //{
        //    return t.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(DatabaseColumn)) == false && Attribute.IsDefined(prop, typeof(DatabaseColumn)) == false).Select(x => x.Name).OrderBy(x => x);
        //}

        //#endregion

        //#region Select

        //public static string DatabaseSelectTables(this Type t)
        //{
        //    string table = t.DatabaseTable();
        //    string joinTables = t.DatabaseJoinTables();

        //    string cmd = $"{table} {joinTables}";

        //    return string.Format(cmd);
        //}

        //public static string DatabaseSelectFields(this Type t, string[] exclude = null, string[] include = null)
        //{
        //    List<DatabaseColumn> fields = t.DatabaseFieldsAttributes().Where(x => x.Read).ToList();
        //    List<DatabaseVirtualColumn> virtualFields = t.DatabaseVirtualFieldsAttributes().ToList();

        //    if (include != null)
        //    {
        //        fields = fields.Where(x => include.Contains(x.Name)).ToList();
        //        virtualFields = virtualFields.Where(x => include.Contains(x.Name)).ToList();
        //    }
        //    else if (exclude != null)
        //    {
        //        fields = fields.Where(x => exclude.Contains(x.Name) == false).ToList();
        //        virtualFields = virtualFields.Where(x => exclude.Contains(x.Name) == false).ToList();
        //    }

        //    string cmd = string.Format("{0}{1}",
        //                       string.Join(", ", fields.Select(x => (x.Encrypted ? $"{GetDecryptQuery(x.Table, x.Name)} AS '{x.Name}'" : $"[{x.Table}].[{x.Name}]"))),
        //                       virtualFields.Count() > 0 ? "," + string.Join(", ", virtualFields.Select(x => x.GetSelectCommand())) : string.Empty);

        //    return string.Format(cmd.Trim().Trim(','), Globals.CurrentUser != null ? Globals.CurrentUser.usr_id : -1, t.DatabaseTable());
        //}

        //private static string GetSelectCommand(this DatabaseVirtualColumn column)
        //{
        //    if (string.IsNullOrWhiteSpace(column.Query))
        //        throw new Exception("Missing Query for DatabaseVirtualColumn: " + column.Name);

        //    return string.Format("{0} as '{1}'", column.Query, column.Name);
        //}

        //public static string GetDecryptQuery(string table, string column)
        //{
        //    return $" CAST(DECRYPTBYPASSPHRASE('{Globals.EncryptionKey}', [{table}].[{column}]) AS NVARCHAR(1000)) ";
        //}

        //#endregion

        //#region Insert

        //public static string DatabaseInsertTable(this Type t, object item)
        //{
        //    string table = t.DatabaseTable();
        //    IEnumerable<DatabaseColumn> fields = t.DatabaseFieldsAttributes().Where(x => x.Insert);
        //    if (t.Equals(typeof(RadioTaxi_Appointment)))
        //    {
        //        return $@" INSERT INTO [{table}] ({string.Join(",", fields.Select(x => string.Format("[{0}]", x.Name)))})
        //                    VALUES
        //                    ({string.Join(",", fields.Select(x => item.GetPropValueForSql(x)))});";
        //    }
        //    if (t.Equals(typeof(IQ_Trip)) || t.Equals(typeof(IQ_Insurance)) || t.Equals(typeof(IQ_ServiceCode)))// Those tables have a trigger and for that reason we cannot use output Inserted.... see: https://stackoverflow.com/questions/13198476/cannot-use-update-with-output-clause-when-a-trigger-is-on-the-table
        //    {
        //        return $@" INSERT INTO [{table}] ({string.Join(",", fields.Select(x => string.Format("[{0}]", x.Name)))}) 
        //                    VALUES
        //                    ({string.Join(",", fields.Select(x => item.GetPropValueForSql(x)))}); SELECT @@IDENTITY;";
        //    }
        //    else if (t.Equals(typeof(IQ_Report)))   // Reports have been tested and verified through trip changes that scope_identity works as it should. @@IDENTITY had the issue where it returned the ID that was inserted by the trigger, meaning the TripID, not the ReportID that was needed.
        //    {
        //        return $@" INSERT INTO [{table}] ({string.Join(",", fields.Select(x => string.Format("[{0}]", x.Name)))}) 
        //                    VALUES
        //                    ({string.Join(",", fields.Select(x => item.GetPropValueForSql(x)))}); SELECT SCOPE_IDENTITY();";
        //    }
        //    else
        //    {
        //        return $@" SET ANSI_WARNINGS OFF;
        //                    INSERT INTO [{table}] ({string.Join(",", fields.Select(x => string.Format("[{0}]", x.Name)))}) 
        //                    OUTPUT Inserted.{t.DatabasePrimaryKey().Name} VALUES 
        //                    ({string.Join(",", fields.Select(x => item.GetPropValueForSql(x)))});
        //                    SET ANSI_WARNINGS ON;";
        //    }
        //}

        //#endregion

        //#region Update

        //public static string DatabaseUpdateTable(this Type t, object item, string[] includeOnly = null)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    string table = t.DatabaseTable();
        //    IEnumerable<DatabaseColumn> fields = t.DatabaseFieldsAttributes().Where(x => x.Update);

        //    if (includeOnly != null && includeOnly.Count() > 0)
        //    {
        //        fields = fields.Where(x => includeOnly.Contains(x.Name));
        //    }

        //    string pkId = Utils.GetString(item.GetPropValueForSql(t.DatabasePrimaryKey()));
        //    if (fields.Count() > 0)
        //    {
        //        sb.Append(string.Format("update {0} set {1} where {2} = {3}; select {3};",
        //                                    table,
        //                                    string.Join(",", fields.Select(x => string.Format("[{0}].[{1}] = {2}", table, x.Name, item.GetPropValueForSql(x)))),
        //                                    t.DatabasePrimaryKey().Name,
        //                                    pkId));
        //    }

        //    return sb.ToString();
        //}

        //#endregion

        //#region Delete

        //public static string DatabaseDeleteItem(this Type t, object item)
        //{
        //    string table = t.DatabaseTable();
        //    StringBuilder sb = new StringBuilder();
        //    long id = Utils.GetLong(item.GetPropValueForSql(t.DatabasePrimaryKey()));

        //    return $"delete {table} where [{t.DatabasePrimaryKey().Name}] = {id};";
        //}

        //public static string DatabaseDeleteItems(this Type t, List<object> items)
        //{
        //    StringBuilder cmd = new StringBuilder();
        //    foreach (var item in items)
        //    {
        //        cmd.Append(t.DatabaseDeleteItem(item));
        //    }
        //    return cmd.ToString();
        //}

        //#endregion

        //#region Helpers

        //public static bool InheritsFrom(this Type type, Type baseType)
        //{
        //    // null does not have base type
        //    if (type == null)
        //    {
        //        return false;
        //    }

        //    //// only interface can have null base type
        //    //if (baseType == null)
        //    //{
        //    //    return type.IsInterface;
        //    //}

        //    //// check implemented interfaces
        //    //if (baseType.IsInterface)
        //    //{
        //    //    return type.GetInterfaces().Contains(baseType);
        //    //}

        //    // check all base types
        //    var currentType = type;
        //    while (currentType != null)
        //    {
        //        if (currentType.BaseType == baseType)
        //            return true;
        //        currentType = currentType.BaseType;
        //    }

        //    return false;
        //}

        ////public static string DatabaseFilterAllFields(this Type t, string filter, string[] exclude = null)
        ////{
        ////    if (exclude == null)
        ////        exclude = new string[0];

        ////    // Get current type fields
        ////    List<DatabaseColumn> fields = t.DatabaseFieldsAttributes().Where(x => exclude.Contains(x.Name) == false && x.Read).ToList();
        ////    List<DatabaseColumn> fieldsML = t.DatabaseFieldsAttributes().Where(x => exclude.Contains(x.Name) == false && x.Read).ToList();

        ////    fields.ForEach(x => x.Table = t.DatabaseTable());

        ////    return " and (" + string.Join(" or ", fields.Select(x => string.Format("{0}.[{1}] like '%{2}%'", x.Table, x.Name, Utils.GetString(filter, true)))) + ")";
        ////}

        //public static object GetPropValueForSql(this object src, DatabaseColumn databaseColumn)
        //{
        //    PropertyInfo prop = src.GetType().GetProperty(databaseColumn.Name.Trim('#'));
        //    if (prop == null)
        //        throw new MissingMemberException("The property " + databaseColumn.Name + " does not exist in type " + src.GetType().Name);

        //    if (databaseColumn.Name.Equals("DateCreated") || databaseColumn.Name.Equals("DateModified") || databaseColumn.Name.Equals("DateLoggedIn"))
        //    {
        //        return string.Format("'{0}'", DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));
        //    }

        //    object val = prop.GetValue(src, null);

        //    if (val == null || val == DBNull.Value)
        //        return "Null";

        //    return GetPropValueForSql(prop, val, databaseColumn);
        //}

        //public static object GetPropValueForSql(PropertyInfo prop, object val, DatabaseColumn databaseColumn)
        //{
        //    if (prop.PropertyType == typeof(DateTime))
        //    {
        //        return string.Format("'{0}'", Utils.GetDate(val, new DateTime(1700, 1, 1)).ToString("yyyy-MM-dd H:mm:ss"));
        //    }
        //    else if (prop.PropertyType == typeof(DateTime?))
        //    {
        //        return string.Format("'{0}'", Utils.GetDate(val).Value.ToString("yyyy-MM-dd H:mm:ss"));
        //    }
        //    else if (prop.PropertyType == typeof(int))
        //    {
        //        int result = Utils.GetInt(val);
        //        if (databaseColumn.IsForeignKey)// If this is a foreign key column and <= 0 then set to -1
        //        {
        //            return result > 0 ? result.ToString() : "-1";
        //        }
        //        else// If it is just a numeric value, set the value
        //        {
        //            return result.ToString();
        //        }
        //    }
        //    else if (prop.PropertyType == typeof(int?))
        //    {
        //        int result = Utils.GetInt(val);
        //        if (databaseColumn.IsForeignKey)// If this is a foreign key column and <= 0 then set to null
        //        {
        //            return result > 0 ? result.ToString() : "Null";
        //        }
        //        else// If it is just a numeric value, set the value
        //        {
        //            return result.ToString();
        //        }
        //    }
        //    else if (prop.PropertyType == typeof(float))
        //    {
        //        return Utils.GetString(Utils.GetFloat(val)).Replace(",", ".");
        //    }
        //    else if (prop.PropertyType == typeof(float?))
        //    {
        //        float? result = Utils.GetNullFloat(val);
        //        return result == null ? null : Utils.GetString(result).Replace(",", ".");
        //    }
        //    else if (prop.PropertyType == typeof(double))
        //    {
        //        return Utils.GetString(Utils.GetDouble(val)).Replace(",", ".");
        //    }
        //    else if (prop.PropertyType == typeof(double?))
        //    {
        //        double? result = Utils.GetNullDouble(val);
        //        return result == null ? null : Utils.GetString(result).Replace(",", ".");
        //    }
        //    else if (prop.PropertyType == typeof(decimal))
        //    {
        //        decimal val1 = Utils.GetDecimal(val);
        //        string strVal = Utils.GetString(val1);
        //        return strVal.Replace(",", ".");
        //    }
        //    else if (prop.PropertyType == typeof(decimal?))
        //    {
        //        decimal? result = Utils.GetNullDecimal(val);
        //        return result == null ? null : Utils.GetString(result).Replace(",", ".");
        //    }
        //    else if (prop.PropertyType == typeof(long))
        //    {
        //        return Utils.GetInt(val);
        //    }
        //    else if (prop.PropertyType == typeof(long?))
        //    {
        //        long result = Utils.GetInt(val);
        //        if (databaseColumn.IsForeignKey)// If this is a foreign key column and <= 0 then set to null
        //        {
        //            return result > 0 ? result.ToString() : "Null";
        //        }
        //        else// If it is just a numeric value, set the value
        //        {
        //            return result.ToString();
        //        }
        //    }
        //    else if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
        //    {
        //        return Utils.GetBool(val) ? "1" : "0";
        //    }

        //    // In any other case, return as string
        //    string strValue = Utils.GetStringMaxChars(val, databaseColumn.StringLength, true);

        //    strValue = $"N'{strValue}'";

        //    if (databaseColumn.Encrypted)
        //        strValue = $"ENCRYPTBYPASSPHRASE('{Globals.EncryptionKey}', {strValue})";

        //    return strValue;
        //}

        ///// <summary>
        ///// Serializes and de-serializes to the desired object. Use it to convert from parent object to child object
        ///// </summary>
        //public static T CastAs<T>(this object item)
        //{
        //    var serializedParent = JsonConvert.SerializeObject(item);
        //    return JsonConvert.DeserializeObject<T>(serializedParent);
        //}

        //public static DataRow ToDataRow<T>(this T dataItem)
        //{
        //    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
        //    DataTable table = new DataTable();
        //    foreach (PropertyDescriptor prop in properties)
        //        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        //    DataRow row = table.NewRow();
        //    foreach (PropertyDescriptor prop in properties)
        //        row[prop.Name] = prop.GetValue(dataItem) ?? DBNull.Value;
        //    return row;
        //}

        //public static DataTable ToDataTable<T>(this List<T> data)
        //{
        //    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
        //    DataTable table = new DataTable();
        //    foreach (PropertyDescriptor prop in properties)
        //        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        //    foreach (T item in data)
        //    {
        //        DataRow row = table.NewRow();
        //        foreach (PropertyDescriptor prop in properties)
        //            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
        //        table.Rows.Add(row);
        //    }
        //    return table;
        //}

        //#endregion

        #endregion

    }
}
