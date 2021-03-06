﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using EnvDTE;
using EnvDTE80;
using KongQiang.DevTools.Environments;
using KongQiang.DevTools.Models.DB;
using KongQiang.DevTools.Models.Templates;
using KongQiang.DevTools.Utils;
using KongQiang.DevTools.Utils.Helper;

namespace KongQiang.DevTools.CodeGenerator
{
    internal class TemplateContext
    {
        private readonly DTE2 _dte;
        private VSSolution _vsSolution;
        private readonly DbSchema _schema;
        private readonly Dictionary<string, string> _dicTempletePath;
        private readonly GenerateConfiguration _generateConfiguration;
        private readonly DbConfiguration _dbConfiguration;

        private readonly CodeConfiguration _codeConfiguration;
        public CodeConfiguration SettingInfo
        {
            get { return _codeConfiguration; }
        }

        private bool _hasError;
        public bool HasError
        {
            get { return _hasError; }
            private set
            {
                _hasError = value;
            }
        }

        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            private set
            {
                _message = value;
            }
        }

        public TemplateContext(CodeConfiguration configuration, DbSchema schema)
        {
            _codeConfiguration = configuration;
            _schema = schema;
            _dte = _codeConfiguration.Dte;
            _generateConfiguration = _codeConfiguration.GenerateConfiguration;
            _dbConfiguration = _codeConfiguration.DbConfiguration;
            _dicTempletePath = new Dictionary<string, string>();
        }

        public void GenerateCode()
        {
            if (!PreProcess())
            {
                HasError = true;
                return;
            }

            if (!Process())
            {
                HasError = true;
                return;
            }

            if (!AfterProcess())
            {
                //HasError = true;
                return;
            }
        }

        protected bool PreProcess()
        {
            try
            {
                StringCollection ttCollection;
                string ptcPath;
                if (_codeConfiguration.IsCustomTemplete)
                {
                    //获取_DevTools目录
                    string dirPath = DevToolsEnvironment.GetDefaultDirPath();
                    if (string.IsNullOrEmpty(dirPath))
                    {
                        RecordError("没有找到_DevTools目录");
                        return false;
                    }

                    string curTempletePath = _generateConfiguration.CustomTempletePath;
                    var vspsd = FileHelper.GetAllFiles(curTempletePath, "*.vspsd");
                    if (vspsd.Count == 0)
                    {
                        RecordError("没有找到vspsd文件");
                        return false;
                    }

                    ptcPath = vspsd[0];

                    _vsSolution = ResolveVspsd(ptcPath);

                    if (_vsSolution == null)
                    {
                        return false;
                    }

                    string destPath = Path.Combine(dirPath, DevToolsEnvironment.DefaultTemplateDir,
                        _vsSolution.Name);
                    ttCollection = FileHelper.GetAllFiles(curTempletePath, "*.tt");

                    foreach (var path in ttCollection)
                    {
                        //复制文件
                        FileHelper.CopyFile(path, destPath, Path.GetFileName(path));
                    }

                }
                else
                {
                    //
                    ptcPath = _codeConfiguration.GenerateConfiguration.VspsdFilePath;
                    _vsSolution = ResolveVspsd(ptcPath);
                    if (_vsSolution == null)
                    {
                        return false;
                    }

                    string defaultTemplatePath = Path.Combine(DevToolsEnvironment.GetSystemTempletePath(), DevToolsEnvironment.DefaultDri);
                    ttCollection = FileHelper.GetAllFiles(defaultTemplatePath, "*.tt");
                }

                string val;
                foreach (var path in ttCollection)
                {
                    var fileName = Path.GetFileName(path);
                    fileName = fileName.Replace(Path.GetExtension(fileName), "");

                    if (!_dicTempletePath.TryGetValue(fileName, out val))
                    {
                        _dicTempletePath.Add(fileName, path);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                RecordLog(ex.Message);
                _hasError = true;
                return false;
            }
        }

        protected bool Process()
        {
            try
            {
                if (_codeConfiguration.HasOutpath)
                {
                    //自定义输出
                    string outDir = _generateConfiguration.OutputPath;

                    if (!Directory.Exists(outDir))
                    {
                        Directory.CreateDirectory(outDir);
                    }

                    foreach (var myProject in _vsSolution.Projects)
                    {
                        string proName = myProject.Name;

                        var templates = myProject.Templates;
                        foreach (var template in templates)
                        {
                            OutputFile(_dicTempletePath[template.Name],
                                Path.Combine(outDir, template.Name + template.Extension), template);
                        }
                    }

                    return true;
                }
                else
                {

                    //输出到VS里
                    var projects = _dte.Solution.Projects;
                    if (projects == null)
                        return false;

                    ProjectItems pitems = null;
                    EditPoint2 epStart = null;
                    //EditPoint2 epEnd = null;
                    foreach (var myProject in _vsSolution.Projects)
                    {
                        foreach (Project project in projects)
                        {
                            pitems = project.ProjectItems;
                            if (!myProject.Name.Equals(project.Name))
                                continue;

                            if (!ResolveCodes(pitems, epStart, myProject.Codes))
                                return false;

                            var fullName = project.FullName;
                            var projectPath = fullName.Replace(Path.GetFileName(fullName), "");
                            if (!ResolveTemplates(pitems, projectPath, myProject.Templates))
                                return false;

                            project.Save();
                            break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                RecordLog(ex.Message);
                _hasError = true;

                return false;
            }

            return true;
        }

        protected bool AfterProcess()
        {
            //todo App.config

            return true;
        }

        private ProjectItem FindProjectItem(string name, ProjectItems items)
        {
            ProjectItem item = null;
            foreach (ProjectItem projectItem in items)
            {
                if (projectItem.Name.Equals(name))
                {
                    item = projectItem;
                    break;
                }

                item = FindProjectItem(name, projectItem.ProjectItems);
            }
            return item;
        }

        private void RecordLog(string message)
        {
            _message = message;
            //todo: other thing
        }

        private void RecordError(string message)
        {
            RecordLog(message);
            _hasError = true;
        }

        private bool ResolveTemplates(ProjectItems pitems, string projectPath, IEnumerable<CodeTemplate> templates)
        {
            string filePath;
            ProjectItem subItem = null;
            foreach (var template in templates)
            {
                //subItem = null;
                string folderName = template.FolderName;

                filePath = Path.Combine(projectPath, folderName);
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                    subItem = pitems.AddFromDirectory(filePath); //: pitems.AddFolder(folderName);
                }

                string newFile = CreateFile(template, filePath);
                if (string.IsNullOrEmpty(newFile))
                    continue;

                //subItem = FindProjectItem(folderName, pitems);
                if (subItem == null)
                {
                    pitems.AddFromFile(newFile);
                }
                else
                {
                    var fileItem = FindProjectItem(Path.GetFileName(newFile), subItem.ProjectItems);
                    if (fileItem != null)
                        fileItem.Remove();
                    subItem.ProjectItems.AddFromFile(newFile);
                }
            }
            return true;
        }

        private bool ResolveCodes(ProjectItems pitems, EditPoint2 epStart, IEnumerable<CodeLocation> codes)
        {
            foreach (var code in codes)
            {
                if (string.IsNullOrEmpty(Path.GetExtension(code.Target)))
                {
                    RecordError("Vspsd文件解析出错：TargetPath属性值必须要有文件后缀名");
                    return false;
                }

                var paths = code.Target.Split(new char[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
                if (paths.Length == 0)
                    return false;

                string head = paths[0];
                string extension = Path.GetExtension(code.Target);
                string fileName = string.IsNullOrEmpty(Path.GetExtension(head)) ? paths[paths.Length - 1] : head;

                ProjectItem item = FindProjectItem(fileName, pitems);
                if (item != null)
                {
                    var fcm = item.FileCodeModel;

                    foreach (CodeElement2 elt in fcm.CodeElements)
                    {
                        Debug.WriteLine(elt.Kind);
                        if (elt.Kind != vsCMElement.vsCMElementNamespace)
                            continue;

                        var classElement = elt.Children;
                        foreach (CodeElement2 codeElement in classElement)
                        {
                            if (codeElement.Kind != vsCMElement.vsCMElementClass)
                                continue;

                            var cc2 = (CodeClass2)codeElement;

                            switch (code.Location)
                            {
                                case InsertLocation.Class:
                                    epStart = cc2.StartPoint.CreateEditPoint() as EditPoint2;
                                    epStart.LineDown(1);
                                    InsertCode(code.Content, epStart);
                                    break;
                                case InsertLocation.Constructor:

                                    foreach (CodeElement2 ce in cc2.Members)
                                    {
                                        var func2 = (CodeFunction2)ce;
                                        if (func2.FunctionKind == vsCMFunction.vsCMFunctionConstructor)
                                        {
                                            epStart = ce.EndPoint.CreateEditPoint() as EditPoint2;
                                            InsertCode(code.Content, epStart);
                                        }
                                    }
                                    break;
                                case InsertLocation.Method:
                                    foreach (CodeElement2 ce in cc2.Members)
                                    {
                                        var func2 = (CodeFunction2)ce;
                                        if (func2.FunctionKind == vsCMFunction.vsCMFunctionFunction)
                                        {
                                            epStart = ce.EndPoint.CreateEditPoint() as EditPoint2;
                                            InsertCode(code.Content, epStart);
                                        }
                                    }
                                    break;
                                default:
                                    throw new ConfigurationException("未知的Location属性");
                            }
                        }
                    }
                }
            }

            return true;
        }

        private void InsertCode(string code, EditPoint2 point)
        {
            point.EndOfLine();
            point.CharLeft(1);
            point.Insert(code);
            point.InsertNewLine();
        }

        public VSSolution ResolveVspsd(string path)
        {
            try
            {
                var root = XElement.Load(path);

                var solution = new VSSolution { Name = root.Attribute(VspsdNodeDescription.SolutionNameAttribute).Value, Projects = new List<VSProject>() };


                foreach (var xElement in root.Elements(VspsdNodeDescription.ProjectNode))
                {
                    var project = new VSProject
                    {
                        Name = xElement.Attribute(VspsdNodeDescription.ProjectNameAttribute).Value,
                        Templates = new List<CodeTemplate>()
                    };
                    var templateElement = xElement.Element(VspsdNodeDescription.TemplatesNode);
                    if (templateElement == null)
                    {
                        throw new NullReferenceException("没有找到Templates节点");
                    }

                    foreach (var element in templateElement.Elements(VspsdNodeDescription.TemplateNode))
                    {
                        var template = new CodeTemplate
                        {
                            //ModuleName = _generateConfiguration.FunctionName,
                            ProjectName = project.Name,
                            SolutionName = solution.Name,
                            Name = element.Attribute(VspsdNodeDescription.TemplateNameAttribute).Value
                        };

                        var folder = element.Attribute(VspsdNodeDescription.DirectoryAttribute);
                        //template.FolderName = folder == null ? _generateConfiguration.FunctionName : folder.Value;
                        template.FolderName = folder == null ? "NewCode" : folder.Value;

                        var isPoco = element.Attribute(VspsdNodeDescription.IsPocoEntityAttribute);

                        if (isPoco != null)
                        {
                            template.IsPocoTemplate = Convert.ToBoolean(isPoco.Value);
                        }

                        var extension = element.Attribute(VspsdNodeDescription.FileExtensionAttribute);
                        if (extension != null)
                        {
                            template.Extension = extension.Value;
                        }
                        else
                        {
                            template.Extension = ".cs";
                        }

                        project.Templates.Add(template);
                    }

                    project.Codes = new List<CodeLocation>();
                    var codes = xElement.Element(VspsdNodeDescription.CodeLocationsNode);

                    if (codes != null)
                    {
                        foreach (var code in codes.Elements(VspsdNodeDescription.CodeLocationNode))
                        {
                            project.Codes.Add(new CodeLocation()
                            {
                                Target = code.Attribute(VspsdNodeDescription.TargetPathAttribute).Value,
                                Location = (InsertLocation)Enum.Parse(typeof(InsertLocation),
                                    code.Attribute(VspsdNodeDescription.LocationAttribute).Value),
                                Content = code.Value
                            });
                        }
                    }

                    solution.Projects.Add(project);
                }
                return _vsSolution = solution;
            }
            catch (Exception ex)
            {
                RecordError(ex.Message);
                return null;
            }
        }

        #region IO

        private string CreateFile(CodeTemplate template, string filePath)
        {
            string result = null;
            var templateName = template.Name;
            if (!_dicTempletePath.ContainsKey(templateName))
            {
                RecordError("未找到模板");
                return result;
            }

            var text = GenerateT4Text(_dicTempletePath[templateName], template);
            //if (text == null)
            //{
            //    //MessageBox.Show("模版内容为空");
            //    return result;
            //}
            StreamWriter sw;
            result = string.Format(@"{0}\{1}", filePath, template.GenerateFileName);

            if (File.Exists(result))
            {
                var fi = new FileInfo(result);
                var auldSize = fi.Length;
                byte[] newSize = Encoding.UTF8.GetBytes(text);
                if (auldSize == newSize.Length)
                {
                    return null;
                }
            }

            using (sw = new StreamWriter(result, false, Encoding.UTF8))
            {
                sw.WriteLine(text);
            }

            return result;
        }

        public bool OutputFile(string t4Path, string outputPath, CodeTemplate template)
        {
            var text = GenerateT4Text(t4Path, template);
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            //if (File.Exists(outputPath))
            //{
            //    File.Delete(outputPath);
            //}
            try
            {
                using (var sw = new StreamWriter(outputPath, false, Encoding.UTF8))                // File.CreateText(outputPath))
                {
                    sw.WriteLine(text);
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                RecordError(ex.Message);
                return false;
            }
        }

        public string GenerateT4Text(string t4Path, CodeTemplate template)
        {
            string content = File.ReadAllText(t4Path, Encoding.Unicode);
            content = DevToolsEnvironment.TemplateParameter + content;

            var host = new CodeTemplateHost { TemplateFileValue = t4Path };

            var templateInfo = new TemplateInfo
            {
                ClassName = template.GenerateFileName,
                ProjectName = template.ProjectName,
                DbName = _dbConfiguration.DbName,
                EntityName = _dbConfiguration.EntityName,
                GenerateFileName = template.GenerateFileName,
                Usings = "",
                //FunctionName = _generateConfiguration.FunctionName,
                SolutionName = template.SolutionName,
                DefaultNamespace = string.Format("{0}.{1}", template.ProjectName, template.FolderName),
                //string.Format("{0}.{1}.{2}", template.ProjectName, template.FolderName,_generateConfiguration.FunctionName),
                Table =
                    template.IsPocoTemplate
                        ? _schema[_dbConfiguration.TableName]
                        : new DbTable { TableName = _dbConfiguration.TableName }
            };


            host.Session = host.CreateSession();
            host.Session.Add(DevToolsEnvironment.TemplateParameterName, templateInfo);

            var engine = new Microsoft.VisualStudio.TextTemplating.Engine();
            string result = engine.ProcessTemplate(content, host);
            Debug.WriteLine(host.Errors.HasErrors);
            return host.Errors.HasErrors ? host.Errors[0].ErrorText : result;
        }

        private string GenerateT4Text(string fullName, TemplateInfo info)
        {
            Type t = Type.GetType(fullName);
            if (t == null)
            {
                return null;
            }

            object t4 = Activator.CreateInstance(t);

            var property = t.GetProperty(DevToolsEnvironment.TemplateParameterName);
            property.SetValue(t4, info);

            var method = t.GetMethod("TransformText");

            object returnValue = method.Invoke(t4, BindingFlags.Public | BindingFlags.Instance, Type.DefaultBinder, null,
                null);
            return returnValue.ToString();

        }

        private string GetDirectory(ProjectItem prjItem)
        {
            return prjItem.Properties.Item("FullPath").Value.ToString();
        }

        private void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

        }

        #endregion
    }
}
