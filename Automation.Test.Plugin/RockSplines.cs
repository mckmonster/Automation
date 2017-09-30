using Automation.Core.Attributes;

namespace Automation.Test.Plugin
{
    public class RockSplines : HoudiniJob
    {
        [Editable]
        public string WorldName { get; set; }

        public RockSplines() : base("RockSplines")
        {
            JobName = "140-RockSplines.hip";
        }
    }
}