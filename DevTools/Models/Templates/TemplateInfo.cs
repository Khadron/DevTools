using System;
using KongQiang.DevTools.Models.DB;

namespace KongQiang.DevTools.Models.Templates
{
    [Serializable]
    public class TemplateInfo
    {
        public DbTable Table { get; set; }

        public string DefaultNamespace { get; set; }

        public string SolutionName { get; set; }

        //public string FunctionName { get; set; }

        public string ClassName { get; set; }

        public string DbName { get; set; }

        public string EntityName { get; set; }
        //引用
        public string Usings { get; set; }

        public string GenerateFileName { get; set; }

        public string ProjectName { get; set; }

    }


    public class CodeTemplate
    {
        /// <summary>
        /// 模板名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 生成文件的扩展名
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// 文件夹名称
        /// </summary>
        public string FolderName { get; set; }
        /// <summary>
        /// 生成的文件名称
        /// </summary>
        public string GenerateFileName
        {
            get
            {
                //return string.Format("{0} {1}{2}{3}", DateTime.Now.Ticks, ModuleName, Name, Extension);
                return string.Format("{0}_{1}{2}", DateTime.Now.Ticks, Name, Extension);
            }
        }
        /// <summary>
        /// 是否是poco模板
        /// </summary>
        public bool IsPocoTemplate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FileNameFormat { get; set; }

        /// <summary>
        /// using代码
        /// </summary>
        public string Usings { get; set; }

        public string ProjectName { get; set; }

        public string SolutionName { get; set; }

        //public string ModuleName { get; set; }
    }

}
