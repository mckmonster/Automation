using System.Threading;
using Automation.Core;

namespace Automation.Test.Plugin
{
    public class RockSplines : UbuildJob
    {
        [Editable(ReadOnly = true)]
        public string JobName { get; set; }

        [Editable]
        public string WorldName { get; set; }

        public RockSplines() : base("RockSplines")
        {
            JobName = "140-RockSplines.hip";
        }

        protected override void Execute()
        {
            base.Execute();
            Thread.Sleep(1000);
        }
    }
}