using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using KongQiang.DevTools.Environments;
using KongQiang.DevTools.Utils;

namespace KongQiang.DevTools.Models.DB
{

    /// <summary>
    /// 表结构
    /// </summary>
    [Serializable]
    public class DbTable
    {
        protected const string TABLENAME = "table_name";

        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }

        public TableType TableType { get; set; }

        /// <summary>
        /// 是否含有主键
        /// </summary>
        public bool HasPrimaryKey
        {
            get { return Columns.Count(r => r.IsPrimaryKey) > 0; }
        }

        public DbTable()
        {
        }

        public DbTable(string tableName)
        {
            TableName = tableName;
            Columns = new List<DbColumn>();
        }

        public IList<DbColumn> Columns { get; set; }

        public string GetPrimaryKeyName()
        {
            string result = string.Empty;
            if (Columns != null)
            {
                result = Columns.Where(r => r.IsPrimaryKey).Select(r => r.ColumnName).FirstOrDefault();
            }
            return result ?? "";
        }
    }

    /// <summary>
    /// 表字段结构
    /// </summary>
    [Serializable]
    public class DbColumn
    {
        /// <summary>
        /// 字段ID
        /// </summary>
        public string ColumnID { get; set; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string ColumnType { get; set; }

        /// <summary>
        /// 数据库类型对应的C#类型
        /// </summary>
        public Type CSharpType { get; set; }

    }

    //#region Sql Server

    //public class SqlServerTable : DbTable
    //{
    //    private const string Table_Type = "table_type";
    //    private const string Column_Name = "column_name";

    //    public SqlServerTable(string tableName)
    //        : base(tableName)
    //    {
    //    }

    //    //public override void Set(string connectionString)
    //    //{
    //    //    var dbUtility = new DbUtility(connectionString, DbProviderType.SqlServer);
    //    //    DataTable table = dbUtility.GetSchema("TABLE_NAME");

    //    //    string tableName = string.Empty;
    //    //    foreach (DataRow row in table.Rows)
    //    //    {
    //    //        tableName = row[TABLENAME].ToString();
    //    //        if (tableName.Equals(TableName))
    //    //        {
    //    //            this.TableName = tableName;
    //    //            this.TableType = (TableType)Enum.Parse(typeof(TableType), row[Table_Type].ToString());
    //    //            break;
    //    //        }
    //    //    }

    //    //    table = dbUtility.GetSchema("IndexColumns");

    //    //    string primaryKeyColumn = string.Empty;
    //    //    foreach (DataRow row in table.Rows)
    //    //    {
    //    //        if (tableName.Equals(row[TABLENAME]))
    //    //        {
    //    //            primaryKeyColumn = row["column_name"].ToString();
    //    //            break;
    //    //        }
    //    //    }

    //    //    table = dbUtility.GetSchema("Columns");
    //    //    foreach (DataRow row in table.Rows)
    //    //    {
    //    //        if (tableName.Equals(row[TABLENAME]))
    //    //        {
    //    //            var name = row[Column_Name].ToString();
    //    //            Columns.Add(new SqlServerColumn
    //    //            {
    //    //                ColumnID = Guid.NewGuid().ToString("N"),
    //    //                ColumnName = name,
    //    //                ColumnType = row["data_type"].ToString(),
    //    //                IsPrimaryKey = primaryKeyColumn == name
    //    //            });
    //    //        }
    //    //    }


    //    //}


    //}

    //public class SqlServerColumn : DbColumn
    //{
    //    public override string CSharpType()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}


    //#endregion

    //#region Oracle

    //public class OracleTable : DbTable
    //{
    //    //public override void Set(DbUtility utility)
    //    //{
    //    //    throw new NotImplementedException();
    //    //}
    //}

    //public class OracleColumn : DbColumn
    //{
    //    public override string CSharpType()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //#endregion

    //#region MySQL

    //public class MySQLTable : DbTable
    //{
    //    //public override void Set(DbUtility utility)
    //    //{
    //    //    throw new NotImplementedException();
    //    //}
    //}

    //public class MySQLColumn : DbColumn
    //{
    //    public override string CSharpType()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}


    //#endregion

}
