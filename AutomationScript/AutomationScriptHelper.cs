using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationScript.xml;
using log4net;

namespace AutomationScript
{
    public class AutomationScriptHelper
    {
        private Automation m_AutomationScript = null;
        private Dictionary<int, string> m_ChildJobs = new Dictionary<int, string>();
        public Dictionary<int, string> ChildJobs
        {
            get { return m_ChildJobs; }
        }

        private int m_DashboardCl = -1;
        public int DashboardCl
        {
            get { return m_DashboardCl; }
            set { m_DashboardCl = value; }
        }

        public void LoadAutomation(string _filename)
        {
            m_AutomationScript = LoadSettings.LoadXmlAbsolute<Automation>(_filename);

            m_AutomationScript.Resolve();
        }

        public void Execute(Dictionary<string,string> _replacements, ILog _log)
        {
            if (DashboardCl == -1)
            {
                _log.Error("DashboardCL has not been set! Can't launch job");
                return;
            }

            m_AutomationScript.Execute(_replacements, _log, m_ChildJobs, DashboardCl);
        }
    }
}
