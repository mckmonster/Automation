using System.Threading;
using Automation.Core;

namespace Automation.Test.Plugin
{
    public class SetSelection : UbuildJob
    {
        [Editable(ReadOnly = true)]
        public string JobName { get; set; }

        [Editable]
        public string WorldName { get; set; }

        private bool NEEDS_SCIMITAR { get; set; }

        public SetSelection() : base("SetSelection")
        {
            JobName = "060-SelectionSets.hip";
            NEEDS_SCIMITAR = true;
        }

        protected override void Execute()
        {
            base.Execute();
            Thread.Sleep(1000);
        }
    }
}