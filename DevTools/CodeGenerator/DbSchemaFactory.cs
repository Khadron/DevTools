using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using KongQiang.DevTools.Environments;
using KongQiang.DevTools.Models.DB;
using KongQiang.DevTools.Utils;

namespace KongQiang.DevTools.CodeGenerator
{
    internal class DbSchemaFactory
    {
        public static DbSchema GetDbSchema(DbProviderType type, string connectionString)
        {
            DbSchema result;
            switch (type)
            {
                case DbProviderType.SqlServer:
                    result = new SqlServerSchema(connectionString);
                    break;
                case DbProviderType.Oracle:
                    result = new OracleSchema(connectionString);
                    break;
                case DbProviderType.MySql:
                    result = new MySQLSchema(connectionString);
                    break;
                default:
                    throw new ArgumentException("未知的DbProviderType");
            }

            return result;
        }
    }

    public class SchemaParamter
    {
        public string TablesKey { get; set; }
        public string ColumnsKey { get; set; }
        public string PrimaryKey { get; set; }
        public string IndexColumnsKey { get; set; }

        public string TableName { get; set; }
        public string TableType { get; set; }
        public string ColumnName { get; set; }
        public string DataType { get; set; }

        public string[] RestrictionValues { get; set; }


        public SchemaParamter()
        {
        }

        public SchemaParamter(string[] restriction)
        {
            RestrictionValues = restriction;
        }
    }

    public abstract class DbSchema
    {
        protected readonly List<string> TableNames;
        protected readonly Dictionary<string, DbTable> TableDic;
        protected DbUtility DbUtility;

        private readonly string _connectionString;
        public string ConnectionString
        {
            get { return _connectionString; }
        }

        protected DbSchema(string connectionString, DbProviderType dbProviderType)
        {
            _connectionString = connectionString;
            DbUtility = new DbUtility(connectionString, dbProviderType);
            TableDic = new Dictionary<string, DbTable>();
            TableNames = new List<string>();
            InitSchema(connectionString, TableDic);
        }

        public abstract List<string> GetTableNames(Func<string, bool> func);
        public abstract List<DbTable> GetTables(IEnumerable<string> tableNameFilter);
        protected abstract SchemaParamter GenerateParamter();

        protected virtual void InitSchema(string connectionString, Dictionary<string, DbTable> tableDic)
        {
            var schemaParamter = GenerateParamter();
            DataTable table = DbUtility.GetSchema(schemaParamter.TablesKey);

            foreach (DataRow row in table.Rows)
            {
                var tableName = row[schemaParamter.TableName].ToString();
                TableNames.Add(tableName);

                var tableType = row[schemaParamter.TableType].ToString().Contains(TableType.Table.ToString())
                    ? TableType.Table
                    : TableType.View;
                var dbt = new DbTable
                {
                    TableName = tableName,
                    TableType = tableType,
                    Columns = new List<DbColumn>()
                };

                DataTable tableSchema = DbUtility.GetTableSchema(tableName);

                var pkns = tableSchema.PrimaryKey.Select(r => r.ColumnName).ToList();

                foreach (DataColumn column in tableSchema.Columns)
                {
                    dbt.Columns.Add(new DbColumn
                     {
                         ColumnID = Guid.NewGuid().ToString("N"),
                         ColumnName = column.ColumnName,
                         ColumnType = column.DataType.FullName,
                         AllowDbNull = column.AllowDBNull,
                         IsPrimaryKey = pkns.Contains(column.ColumnName)
                     });
                }

                if (TableDic.ContainsKey(tableName))
                {
                    TableDic[tableName] = dbt;
                }
                else
                {
                    TableDic.Add(tableName, dbt);
                }


            }
        }

        public DbTable this[string tableName]
        {
            get { return TableDic[tableName]; }
        }
    }

    public class SqlServerSchema : DbSchema
    {
        public SqlServerSchema(string connectionString)
            : base(connectionString, DbProviderType.SqlServer)
        {
        }


        public override List<string> GetTableNames(Func<string, bool> func)
        {
            return func == null ? TableNames : TableNames.Where(func).ToList();
        }

        public override List<DbTable> GetTables(IEnumerable<string> tableNameFilter)
        {
            return tableNameFilter == null
                  ? TableDic.Values.ToList()
                  : TableDic.Values.Where(r => tableNameFilter.Contains(r.TableName)).ToList();
        }


        protected override SchemaParamter GenerateParamter()
        {
            return new SchemaParamter
            {
                TablesKey = "Tables",
                TableName = "table_name",
                TableType = "table_type"
            };
        }

    }

    public class OracleSchema : DbSchema
    {
        public OracleSchema(string connectionString)
            : base(connectionString, DbProviderType.Oracle)
        {
        }

        public override List<string> GetTableNames(Func<string, bool> func)
        {
            throw new NotImplementedException();
        }

        public override List<DbTable> GetTables(IEnumerable<string> tableNameFilter)
        {
            throw new NotImplementedException();
        }

        protected override SchemaParamter GenerateParamter()
        {
            throw new NotImplementedException();
        }

    }

    public class MySQLSchema : DbSchema
    {
        public MySQLSchema(string connectionString)
            : base(connectionString, DbProviderType.MySql)
        {
        }

        public override List<string> GetTableNames(Func<string, bool> func)
        {
            throw new NotImplementedException();
        }

        public override List<DbTable> GetTables(IEnumerable<string> tableNameFilter)
        {
            throw new NotImplementedException();
        }

        protected override SchemaParamter GenerateParamter()
        {
            throw new NotImplementedException();
        }


    }

}
