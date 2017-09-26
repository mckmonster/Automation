using System.Threading;
using Automation.Core;

namespace Automation.Test.Plugin
{
    public class Composite : UbuildJob
    {

        [Editable(ReadOnly = true)]
        public string JobName { get; set; }

        [Editable]
        public string WorldName { get; set; }

        [Editable]
        public  string Frames { get; set; }

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