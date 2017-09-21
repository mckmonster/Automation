using System.Collections.Generic;
using log4net;

namespace AutomationScript.xml
{
    public interface IJob
    {
        int ActionId { get; set; }
        int BranchId { get; set; }
        bool IsFailed { get; }
        string Name { get; set; }
        List<IParameter> Parameters { get; set; }

        bool IsFinished(ILog _log);
    }
}