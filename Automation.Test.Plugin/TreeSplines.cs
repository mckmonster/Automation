using System.Threading;
using Automation.Core;

namespace Automation.Test.Plugin
{
    public class TreeSplines : UbuildJob
    {
        [Editable(ReadOnly = true)]
        public string JobName { get; set; }

        [Editable]
        public string WorldName { get; set; }

        public TreeSplines() : base("TreeSplines")
        {
            JobName = "250-TreeSplines.hip";
        }

        protected override void Execute()
        {
            base.Execute();
            Thread.Sleep(1000);
        }
    }
}