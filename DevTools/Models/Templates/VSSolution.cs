using System.Collections.Generic;
using KongQiang.DevTools.Environments;
using KongQiang.DevTools.Models.DB;

namespace KongQiang.DevTools.Models.Templates
{
    public class VSSolution
    {
        public string Name { get; set; }
        public List<VSProject> Projects { get; set; }
    }

    public class VSProject
    {
        public string Name { get; set; }
        public List<CodeTemplate> Templates { get; set; }
        public List<CodeLocation> Codes { get; set; }

    }


    public class CodeLocation
    {
        public string Target { get; set; }
        public InsertLocation Location { get; set; }
        public string Content { get; set; }
    }


}
