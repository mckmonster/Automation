using Automation.Core.Attributes;

namespace Automation.Test.Plugin
{
    public class RockAssign : HoudiniJob
    {
        public RockAssign() : base("RockAssign")
        {
            JobName = "130-RockAssign.hip";
        }
    }
}
