using Automation.Core.Attributes;

namespace Automation.Test.Plugin
{
    public class RockAssign : HoudiniJob
    {
        [Editable]
        public string WorldName { get; set; }

        public RockAssign() : base("RockAssign")
        {
            JobName = "130-RockAssign.hip";
        }
    }
}
