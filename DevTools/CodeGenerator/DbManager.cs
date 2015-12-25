using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KongQiang.DevTools.Environments;
using KongQiang.DevTools.Models.DB;
using KongQiang.DevTools.Utils;

namespace KongQiang.DevTools.CodeGenerator
{
    internal class DbManager
    {
        private readonly string _dbName;
        private readonly string _connectionString;
        private readonly DbProviderType _type;
        private readonly Dictionary<string, DbTable> _tableDic;
        private readonly List<string> _tableNames;

        public DbManager(string dbName, string connectionString, DbProviderType type)
        {
            _connectionString = connectionString;
            _type = type;
            _tableDic = new Dictionary<string, DbTable>();
            _dbName = dbName;
            _tableNames = new List<string>();
            Init();
        }

        private const string TableNameTag = "table_name";

        public List<string> GetTableNames()
        {
            return _tableNames;
        }

        public DbTable GetTable(string tableName)
        {
            DbTable table;
            if (_tableDic.TryGetValue(tableName, out table))
            {
                return table;
            }
            throw new ArgumentException("未找到表");
        }

        private void Init()
        {
            SchemaParamter schemaParamter;
            switch (_type)
            {
                case DbProviderType.SqlServer:
                    schemaParamter = new SchemaParamter
                    {
                        TablesKey = "Tables",
                        TableName = "table_name",
                        TableType = "table_type",
                        ColumnsKey = "Columns",
                        ColumnName = "column_name",
                        DataType = "data_type",
                        PrimaryKey = "SELECT sc.name FROM {0}.sys.indexes idx INNER JOIN {0}.sys.index_columns ic ON idx.index_id = ic.index_id AND idx.object_id = ic.object_id INNER JOIN {0}.sys.columns AS sc ON sc.column_id=ic.column_id WHERE  idx.object_id =OBJECT_ID('{1}') AND idx.is_primary_key=1 AND sc.object_id=OBJECT_ID('{1}')"
                    };
                    break;
                case DbProviderType.MySql:
                    schemaParamter = new SchemaParamter
                    {
                        PrimaryKey = "select column_name from information_schema.columns where table_schema='{0}' and table_name='{1}' and column_key='PRI';"
                    };
                    break;
                case DbProviderType.Oracle:
                    schemaParamter = new SchemaParamter
                    {
                        TablesKey = "Tables",
                        TableName = "TABLE_NAME",
                        TableType = "TYPE",
                        ColumnsKey = "Columns",
                        ColumnName = "COLUMN_NAME",
                        DataType = "DATATYPE",
                        PrimaryKey = "select column_name from user_cons_columns cu, user_constraints au where cu.constraint_name = au.constraint_name and au.constraint_type = 'P' and au.table_name = '{0}'"
                    };
                    break;
                default:
                    throw new ArgumentException("暂未实现该数据库操作");
            }


            var dbUtility = new DbUtility(_connectionString, _type);
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

                string primaryKeyColumn = _type == DbProviderType.Oracle ? dbUtility.ExecuteScalar(string.Format(schemaParamter.PrimaryKey, tableName)).ToString() : dbUtility.ExecuteScalar(string.Format(schemaParamter.PrimaryKey, _dbName, tableName)).ToString();

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

        private class SchemaParamter
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

    }
}

