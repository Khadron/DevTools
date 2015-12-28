using System;
using System.Collections.Generic;
using System.Data;
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
    }

    public class SchemaParamter
    {
        public DbProviderType DbProviderType { get; set; }
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

        private readonly List<string> _tableNames;
        private readonly Dictionary<string, DbTable> _tableDic;
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
            _tableDic = new Dictionary<string, DbTable>();
            _tableNames = new List<string>();
            InitSchema(connectionString, _tableDic);
        }

        public abstract List<string> GetTableNames();
        public abstract List<DbTable> GetTables();
        public abstract SchemaParamter GenerateParamter();

        protected virtual void InitSchema(string connectionString, Dictionary<string, DbTable> tableDic)
        {
            var schemaParamter = GenerateParamter();
            var dbUtility = new DbUtility(_connectionString, schemaParamter.DbProviderType);
            DataTable table = dbUtility.GetSchema(schemaParamter.TablesKey);
            DataTable columns = dbUtility.GetSchema(schemaParamter.ColumnsKey);

            string tableName;
            foreach (DataRow row in table.Rows)
            {
                tableName = row[schemaParamter.TableName].ToString();
                _tableNames.Add(tableName);
                var dbt = new DbTable
                {
                    TableName = tableName,
                    TableType = (TableType)Enum.Parse(typeof(TableType), row[schemaParamter.TableType].ToString()),
                    Columns = new List<DbColumn>()
                };



                string primaryKeyColumn = schemaParamter.DbProviderType == DbProviderType.Oracle ? dbUtility.ExecuteScalar(string.Format(schemaParamter.PrimaryKey, tableName)).ToString() : dbUtility.ExecuteScalar(string.Format(schemaParamter.PrimaryKey, _dbName, tableName)).ToString();

                foreach (DataRow cr in columns.Rows)
                {
                    if (tableName.Equals(cr[schemaParamter.TableName]))
                    {
                        var name = cr[schemaParamter.ColumnName].ToString();
                        dbt.Columns.Add(new DbColumn
                        {
                            ColumnID = Guid.NewGuid().ToString("N"),
                            ColumnName = name,
                            ColumnType = cr[schemaParamter.DataType].ToString(),
                            IsPrimaryKey = primaryKeyColumn == name
                        });
                    }
                }

                if (_tableDic.ContainsKey(tableName))
                {
                    _tableDic[tableName] = dbt;
                }
                else
                {
                    _tableDic.Add(tableName, dbt);
                }


            }
        }


    }

    public class SqlServerSchema : DbSchema
    {
        public SqlServerSchema(string connectionString)
            : base(connectionString, DbProviderType.SqlServer)
        {
        }

        public override List<string> GetTableNames()
        {
            throw new NotImplementedException();
        }

        public override List<DbTable> GetTables()
        {
            throw new NotImplementedException();
        }

        public override SchemaParamter GenerateParamter()
        {
            throw new NotImplementedException();
        }
    }
}
