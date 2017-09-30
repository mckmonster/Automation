using System.Threading;
using Automation.Core.Attributes;

namespace Automation.Test.Plugin
{
    public class RockDeformation : HoudiniJobFrameable
    {
        [Editable]
        public int JobCount { get; set; }

        public RockDeformation() : base("RockDeform")
        {
            JobName = "152-RockDeformation.hip";
        }
    }
}