using System;
using System.Globalization;
using System.Text.RegularExpressions;
using EnvDTE80;
using KongQiang.DevTools.Utils;

namespace KongQiang.DevTools.Models.DB
{
    [Serializable]
    public class CodeConfiguration
    {
        public DTE2 Dte { get; set; }
        public bool IsCustomTemplete { get; set; }
        public bool HasOutpath { get; set; }
        public DbConfiguration DbConfiguration { get; set; }
        public GenerateConfiguration GenerateConfiguration { get; set; }

    }

    [Serializable]
    public class DbConfiguration
    {
        public string DbProviderType { get; set; }
        public string Server { get; set; }
        public string Port { get; set; }
        public string DbName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string TableName { get; set; }
        private string _schema = "dbo";
        public string Schema
        {
            get
            {
                return _schema;
            }
            set { _schema = value; }
        }

        public DbConfiguration(DbProviderType dbProviderType, string server, string port, string dbName, string userName, string password, string schema)
        {
            this.DbProviderType = dbProviderType.ToString();
            this.Server = server;
            this.Port = port;
            this.DbName = dbName;
            this.UserName = userName;
            this.Password = password;
            this.Schema = schema;
        }

        private string _entityName;

        public DbConfiguration()
        {

        }

        /// <summary>
        /// 生成实体的名字
        /// </summary>
        public string EntityName
        {
            get
            {

                if (string.IsNullOrEmpty(_entityName))
                {
                    string foo = string.Empty;
                    if (TableName.StartsWith("T", StringComparison.OrdinalIgnoreCase))
                    {
                        foo = TableName.TrimStart(new char[] { 'T' });
                    }

                    var strArray = foo.Split(new char[] { '_', '-', ' ' });

                    string initial;
                    foreach (var curStr in strArray)
                    {
                        if (!string.IsNullOrEmpty(curStr))
                        {
                            initial = curStr[0].ToString(CultureInfo.InvariantCulture);
                            _entityName += Regex.Replace(curStr.ToLower(), @"^\S{1}", initial.ToUpper());
                        }
                    }

                    //initial = Regex.Replace(initial, @"[_\- ]", "");
                    //var charArray = initial.ToCharArray();
                    //string result = string.Empty;
                    //for (int i = 0; i < charArray.Length; i++)
                    //{
                    //    var cur = charArray[i];
                    //    if (i == 0)
                    //    {
                    //        result += cur.ToString(CultureInfo.InvariantCulture).ToUpper();

                    //    }
                    //    else
                    //    {
                    //        result += cur.ToString(CultureInfo.InvariantCulture).ToLower();
                    //    }
                    //}
                }

                return _entityName;
            }
            set
            {
                _entityName = value;
            }
        }

        public string ToConnectionString()
        {
            return string.Format("Server={0};Database={1};User ID={2};Password={3};Trusted_Connection = False", Server, DbName, UserName, Password);
        }
    }

    [Serializable]
    public class GenerateConfiguration
    {

        ///// <summary>
        ///// 项目名称
        ///// </summary>
        //public string ProjectName { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName { get; set; }
        /// <summary>
        /// 输出路径
        /// </summary>
        public string OutputPath { get; set; }
        /// <summary>
        /// 自定义模板路径
        /// </summary>
        public string CustomTempletePath { get; set; }
        /// <summary>
        /// Vspsd文件路径
        /// </summary>
        public string VspsdFilePath { get; set; }

    }

}
