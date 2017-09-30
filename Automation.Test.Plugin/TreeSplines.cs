using System.Threading;
using Automation.Core.Attributes;

namespace Automation.Test.Plugin
{
    public class TreeSplines : HoudiniJob
    {
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