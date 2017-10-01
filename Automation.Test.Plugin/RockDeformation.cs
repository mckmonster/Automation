using System.Threading;

namespace Automation.Test.Plugin
{
    public class RockDeformation : HoudiniJobFrameable
    {
        public int JobCount { get; set; }

        public RockDeformation() : base("RockDeform")
        {
            JobName = "152-RockDeformation.hip";
        }
    }
}