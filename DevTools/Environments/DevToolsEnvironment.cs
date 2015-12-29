using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KongQiang.DevTools.Environments
{
    internal class DevToolsEnvironment
    {
        public const string DefaultDir = "_DevTools";
        public const string DefaultTemplateDir = "Templates";

        public const string DefaultDri = "Default";

        public const string TemplateParameterName = "CurrentTemplateInfo";

        public const string DbConfigSection = "DbConfigSection";

        public const string ConfigFileName = "DevToolsCfg.config";
        public const string ConfigTemplateName = "_DevToolsCfg.ct";

        public static string MainNamespace = Assembly.GetExecutingAssembly().GetName().Name;
        public static string ModelNamespace = MainNamespace + ".Models.Templates";
        public static string PackageDllPath = Assembly.GetExecutingAssembly().Location;
        public static string PackageFilePath = Path.Combine(PackageDllPath.Replace(Path.GetFileName(PackageDllPath), ""), "Environments");
        public static string ConfigFilePath = Path.Combine(GetDefaultDirPath(), ConfigFileName);

        public static string TemplateParameter = string.Format("<#@ assembly name=\"{3}\" #>\n<#@ import namespace=\"{2}\" #>\n<#@ parameter type=\"{0}\" name=\"{1}\" #>\n",
            ModelNamespace + ".TemplateInfo",
            TemplateParameterName, ModelNamespace,
            PackageDllPath);

        public static string GetSystemTempletePath()
        {
            var exePath = Assembly.GetExecutingAssembly().Location;
            var path = exePath.Replace(Path.GetFileName(exePath), @"\");
            return path + DefaultTemplateDir;
        }

        public static string GetDefaultDirPath()
        {
            try
            {
                DriveInfo[] dss = DriveInfo.GetDrives();
                var tds = dss.Where(curDri => curDri.DriveType == DriveType.Fixed).ToList();
                if (tds.Count != 0)
                {
                    var d = tds[tds.Count - 1];
                    return Path.Combine(d.RootDirectory.FullName, DefaultDir);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null;
        }

        public static string GetMaxDirPath()
        {
            string result = "C:\\";
            try
            {
                DriveInfo[] dss = DriveInfo.GetDrives();
                var tds = dss.Where(curDri => curDri.DriveType == DriveType.Fixed).ToList();
                List<KeyValuePair<long, string>> list = new List<KeyValuePair<long, string>>(tds.Count);
                foreach (var driveInfo in tds)
                {
                    list.Add(new KeyValuePair<long, string>(driveInfo.AvailableFreeSpace, driveInfo.RootDirectory.FullName));
                }

                result = list.OrderByDescending(r => r.Key).Select(r => r.Value).First();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
        }



    }

    public enum InsertLocation
    {
        Class = 1,
        Constructor = 2,
        Method = 3
    }

    public enum GenerateMode
    {
        AutoCode = 1,
        PocoCode = 2,
        CustomCode = 3
    }

    public enum OutPutMode
    {
        Solution = 1,
        SpecifiedPath = 2
    }

    public enum TableType
    {
        Table = 1,
        View = 2
    }

}
