using System.Threading;
using Automation.Core;

namespace Automation.Test.Plugin
{
    public class RockDeformation : HoudiniJobFrameable
    {
        [Editable]
        public string WorldName { get; set; }

        [Editable]
        public int JobCount { get; set; }

        public RockDeformation() : base("RockDeform")
        {
            JobName = "152-RockDeformation.hip";
        }

        protected override void Execute()
        {
            base.Execute();
            Thread.Sleep(1000);
        }
    }
}