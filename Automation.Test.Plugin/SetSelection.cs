using System.Threading;
using Automation.Core.Attributes;

namespace Automation.Test.Plugin
{
    public class SetSelection : HoudiniJob
    {
        private bool NEEDS_SCIMITAR { get; set; }

        public SetSelection() : base("SetSelection")
        {
            JobName = "060-SelectionSets.hip";
            NEEDS_SCIMITAR = true;
        }
    }
}