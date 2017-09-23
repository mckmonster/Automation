using System.Threading;

namespace GraphX_test
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