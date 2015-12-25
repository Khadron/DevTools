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
        protected abstract void InitSchema(string connectionString, Dictionary<string, DbTable> tableDic);


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

        protected override void InitSchema(string connectionString, Dictionary<string, DbTable> tableDic)
        {


        }
    }
}
