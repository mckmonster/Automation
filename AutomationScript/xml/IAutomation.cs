using System.Collections.Generic;
using log4net;

namespace AutomationScript.xml
{
    public interface IAutomation
    {
        bool IsFailed { get; }
        List<IStep> Steps { get; set; }

        bool IsFinished(ILog _log);
    }
}