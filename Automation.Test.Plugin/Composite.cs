using System.Threading;
using Automation.Core.Attributes;

namespace Automation.Test.Plugin
{
    public class Composite : HoudiniJobFrameable
    {
        [Editable]
        public string WorldName { get; set; }

        [Editable]
        public int JobCount { get; set; }

        public Composite() : base("Composite")
        {
            JobName = "410-Compisite.hip";
        }

        protected override void Execute()
        {
            base.Execute();
            Thread.Sleep(5000);
        }
    }
}