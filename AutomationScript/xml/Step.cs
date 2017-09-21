using System.Collections.Generic;
using System.Xml.Serialization;
using log4net;

namespace AutomationScript.xml
{
    public class Step
    {
        [XmlElement("Job")]
        public List<Job> Jobs
        {
            get;
            set;
        }

        [XmlElement("Automation")]
        public List<Automation> Automations
        {
            set;
            get;
        }

        [XmlElement("AutomationScript")]
        public List<string> AutomationScripts
        {
            set;
            get;
        }

        public bool IsFinished(ILog _log)
        {
            bool result = (Jobs.TrueForAll(test => test.IsFinished(_log)) && Automations.TrueForAll(test => test.IsFinished(_log)));

            return result;
        }

        public bool IsFailed
        {
            get
            {
                bool result = (Jobs.Find(test => test.IsFailed) != null) || (Automations.Find(test => test.IsFailed) != null);

                return result;
            }
        }

        internal void Execute(Dictionary<string, string> _replacements, ILog _log, Dictionary<int, string> _addedJobs, int _dashboardCl)
        {
            _log.Info("----------------------------------");
            _log.Info("          Execute Step            ");
            foreach (Job job in Jobs)
            {
                job.Create(_replacements, _log, _addedJobs, _dashboardCl); 
            }

            foreach (Automation automation in Automations)
            {
                automation.Execute(_replacements, _log, _addedJobs, _dashboardCl);
            }

            _log.Info("----------------------------------");
        }

        internal void Resolve()
        {
            //Load all AutomationScript to add to Automations
            if (this.AutomationScripts != null)
            {
                foreach (string scriptfilename in this.AutomationScripts)
                {
                    Automation automation = LoadSettings.LoadXmlAbsolute<Automation>(scriptfilename);
                    Automations.Add(automation);
                }
            }
        }

        internal void UpdateReplacement(ref Dictionary<string, string> _replacements, ILog _log)
        {
            foreach (Job job in Jobs)
            {
                job.UpdateReplacement(ref _replacements, _log);
            }

            foreach (Automation automation in Automations)
            {
                automation.UpdateReplacement(ref _replacements, _log);
            }
        }
    }
}
