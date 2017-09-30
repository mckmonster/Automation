using Automation.Core.Attributes;
using System.Threading;

namespace Automation.Test.Plugin
{
    public class Composite2 : HoudiniJobFrameable
    {
        [Editable]
        public string WorldName { get; set; }

        [Editable]
        public int JobCount { get; set; }

        [Editable]
        public string User { get; set; }

        public bool NEEDS_TERRAIN { get; set; }

        public Composite2() : base("Composite-02")
        {
            JobName = "411-Composite-02.hip";
            NEEDS_TERRAIN = true;
        }

        protected override void Execute()
        {
            base.Execute();
            Thread.Sleep(5000);
        }
    }
}