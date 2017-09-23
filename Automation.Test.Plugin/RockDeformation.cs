using System.Threading;

namespace Automation.Test.Plugin
{
    public class RockDeformation : HoudiniJob
    {
        public RockDeformation() : base("RockDeform")
        {
            WorldName = "STP_Japan";
        }

        protected override void Execute()
        {
            base.Execute();
            Thread.Sleep(1000);
        }
    }
}