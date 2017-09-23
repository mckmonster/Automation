using System.Threading;

namespace Automation.Core
{
    public class Composite : HoudiniJob
    {
        public Composite() : base("Composite")
        {
        }

        protected override void Execute()
        {
            base.Execute();
            Thread.Sleep(5000);
        }
    }
}