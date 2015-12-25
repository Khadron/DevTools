using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;

namespace KongQiang.DevTools.Utils
{
    public class CodeTemplateHost : ITextTemplatingEngineHost, ITextTemplatingSessionHost
    {
        internal string TemplateFileValue;
        public string TemplateFile
        {
            get { return TemplateFileValue; }
        }

        private string _fileExtensionValue = ".cs";
        public string FileExtension
        {
            get { return _fileExtensionValue; }
        }

        private Encoding _fileEncodingValue = Encoding.UTF8;
        public Encoding FileEncoding
        {
            get { return _fileEncodingValue; }
        }

        private CompilerErrorCollection _errorsValue;
        public CompilerErrorCollection Errors
        {
            get { return _errorsValue; }
        }

        public IList<string> StandardAssemblyReferences
        {
            get
            {
                return new string[] { typeof(System.Uri).Assembly.Location };
            }
        }


        public IList<string> StandardImports
        {
            get { return new string[] { "System" }; }
        }

        public bool LoadIncludeText(string requestFileName, out string content, out string location)
        {
            content = System.String.Empty;
            location = System.String.Empty;

            if (File.Exists(requestFileName))
            {
                content = File.ReadAllText(requestFileName);
                return true;
            }
            else
            {
                return false;
            }


        }

        public object GetHostOption(string optionName)
        {
            object result;
            switch (optionName)
            {
                case "CacheAssemblies":
                    result = true;
                    break;
                default:
                    result = null;
                    break;
            }

            return result;
        }

        public string ResolveAssemblyReference(string assemblyReference)
        {
            if (File.Exists(assemblyReference))
            {
                return assemblyReference;
            }

            string candidate = Path.Combine(Path.GetDirectoryName(this.TemplateFile), assemblyReference);
            if (File.Exists(candidate))
            {
                return candidate;
            }

            return "";
        }

        public Type ResolveDirectiveProcessor(string processorName)
        {
            throw new NotImplementedException();
        }

        public string ResolvePath(string path)
        {
            if (path == null)
            {
                throw new ArgumentException("the file path cannot be null");
            }

            if (File.Exists(path))
            {
                return path;
            }

            string candidate = Path.Combine(Path.GetDirectoryName(this.TemplateFile), path);
            if (File.Exists(candidate))
            {
                return candidate;
            }
            return path;
        }

        public string ResolveParameterValue(string directiveId, string processorName, string parameterName)
        {
            if (directiveId == null)
            {
                throw new ArgumentException("the dirctiveId cannot be null");
            }

            if (processorName == null)
            {
                throw new ArgumentException("the processorName cannot be null");
            }

            if (parameterName == null)
            {
                throw new ArgumentException("the parameterName cannot be null");
            }
            return string.Empty;
        }


        public void SetFileExtension(string extension)
        {
            _fileExtensionValue = extension;
        }

        public void SetOutputEncoding(Encoding encoding, bool fromOutputDirective)
        {
            _fileEncodingValue = encoding;
        }

        public void LogErrors(CompilerErrorCollection errors)
        {
            _errorsValue = errors;
        }

        public AppDomain ProvideTemplatingAppDomain(string content)
        {
            return AppDomain.CreateDomain("Generation App Domain");
        }

        public ITextTemplatingSession CreateSession()
        {
            return new TextTemplatingSession();
        }

        public ITextTemplatingSession Session
        {
            get;
            set;
        }
    }
}
