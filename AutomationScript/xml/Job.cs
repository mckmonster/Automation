using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using log4net;

namespace AutomationScript.xml
{
    public class Job : IJob
    {
        [XmlAttribute]
        public string Name
        {
            get;
            set;
        }

        [XmlAttribute]
        public int BranchId
        {
            set;
            get;
        }

        [XmlAttribute]
        public int ActionId
        {
            set;
            get;
        }

        [XmlElement("Parameter")]
        public List<IParameter> Parameters
        {
            get;
            set;
        }

        public virtual bool IsFinished(ILog _log)
        {
            throw new Exception("You should redefine it");
        }

        public virtual bool IsFailed
        {
            get
            {
                throw new Exception("You should redefine it");
            }
        }
       
        internal virtual void Create(Dictionary<string,string> _replacements, ILog _log, Dictionary<int, string> _addedJobs, int _dashboardCl)
        {
            throw new Exception("You should redefine it");
        }

        internal virtual void UpdateReplacement(ref Dictionary<string, string> _replacements, ILog _log)
        {
            throw new Exception("You should redefine it");
        }
    }
}
