using System.Threading;

namespace Automation.Core
{
    public class SetSelection : HoudiniJob
    {
        public SetSelection() : base("SetSelection")
        { }

        protected override void Execute()
        {
            base.Execute();
            Thread.Sleep(1000);
        }
    }
}