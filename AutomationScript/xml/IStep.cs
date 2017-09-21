using System.Collections.Generic;
using log4net;

namespace AutomationScript.xml
{
    public interface IStep
    {
        List<IAutomation> Automations { get; set; }
        List<string> AutomationScripts { get; set; }
        bool IsFailed { get; }
        List<IJob> Jobs { get; set; }

        bool IsFinished(ILog _log);
    }
}