using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using log4net;

namespace AutomationScript.xml
{
    public class Automation : IAutomation
    {
        [XmlElement("Step")]
        public List<IStep> Steps
        {
            get;
            set;
        }

        public bool IsFinished(ILog _log)
        {
            return Steps.Last().IsFinished(_log);
        }

        public bool IsFailed
        {
            get
            {
                return Steps.Find(test => test.IsFailed) != null;
            }
        }

        internal void Execute(Dictionary<string, string> _replacements, ILog _log, Dictionary<int, string> _addedJobs, int _dashboardCl)
        {
            _log.Info("++++++++++++++++++++++++++++++++++");
            _log.Info("        Execute Automation        ");
            foreach (Step step in Steps)
            {
                step.Execute(_replacements, _log, _addedJobs, _dashboardCl);

                do
                {
                    _log.Info("Wait End of current step");
                    Thread.Sleep(60000);                   
                } while (!step.IsFinished(_log));

                _log.Info("Step is finished");

                if (step.IsFailed)
                {
                    _log.ErrorFormat("Error on this step");
                    _log.ErrorFormat("Stop the process here");
                    break;
                }
                step.UpdateReplacement(ref _replacements, _log);
            }
            _log.Info("++++++++++++++++++++++++++++++++++");
        }

        internal void Resolve()
        {
            foreach (Step step in Steps)
            {
                step.Resolve();
            }
        }

        internal void UpdateReplacement(ref Dictionary<string, string> _replacements, ILog _log)
        {
            foreach (Step step in Steps)
            {
                step.UpdateReplacement(ref _replacements, _log);
            }
        }        
    }
}
