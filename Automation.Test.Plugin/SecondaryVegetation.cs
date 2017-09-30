using Automation.Core.Attributes;

namespace Automation.Test.Plugin
{
    public class SecondaryVegetation : HoudiniJob
    {
        [Editable]
        public string WorldName { get; set; }

        public SecondaryVegetation() : base("SecondaryVegetation")
        {
            JobName = "260-SecondaryVegetation.hip";
        }
    }
}
