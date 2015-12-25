using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DevTools.Test
{
    [TestClass]
    public class Test
    {
        private DbUtility _dbUtility;
        public Test()
        {
            const string conStr = "";
            _dbUtility = new DbUtility(conStr, DbProviderType.SqlServer);
        }

        [TestMethod]
        public void SchemaTest()
        {
            DataTable table = _dbUtility.GetSchema("Tables", new[] { null, null, "" });
            Console.Clear();
            foreach (DataRow row in table.Rows)
            {
                Debug.WriteLine("----------------------");
                foreach (DataColumn column in table.Columns)
                {
                    Debug.WriteLine(column.ColumnName + ":" + row[column.ColumnName]);
                }

                Debug.WriteLine("----------------------");
            }



        }
    }
}
