using Automation.Core.Attributes;

namespace Automation.Test.Plugin
{
    public class SecondaryVegetation : HoudiniJob
    {
        public SecondaryVegetation() : base("SecondaryVegetation")
        {
            JobName = "260-SecondaryVegetation.hip";
        }
    }
}
