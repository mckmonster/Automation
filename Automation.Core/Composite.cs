using System.Threading;

namespace GraphX_test
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