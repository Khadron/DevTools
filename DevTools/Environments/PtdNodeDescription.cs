using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KongQiang.DevTools.Environments
{
    /// <summary>
    /// 项目结构模板节点描述
    /// </summary>
    internal class PtdNodeDescription
    {
        public const string SolutionNode = "SolutionNode";
        public const string SolutionNameAttribute = "SolutionName";

        public const string ProjectsNode = "Projects";
        public const string ProjectNode = "Project";
        public const string ProjectNameAttribute = "ProjectName";

        public const string TemplatesNode = "Templates";
        public const string TemplateNode = "Template";
        public const string TemplateNameAttribute = "TemplateName";
        public const string DirectoryAttribute = "Directory";
        public const string IsPocoEntityAttribute = "IsPocoEntity";
        public const string FileExtensionAttribute = "FileExtension";

        public const string CodeLocationsNode = "CodeLocations";
        public const string CodeLocationNode = "CodeLocation";
        public const string TargetPathAttribute = "TargetPath";
        public const string LocationAttribute = "Location";

    }

}
