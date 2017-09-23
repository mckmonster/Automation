using System.ComponentModel;
using System.Threading;

namespace GraphX_test
{
    public class HoudiniJob : Job
    {
        [Editor]
        public string World
        {
            get;
            set;
        }

        [Editor]
        public string Property2 { get; set; }

        [Editor]
        public string CodeVersion { get; set; }

        public HoudiniJob() : this("HoudiniJob")
        {
            
        }

        public HoudiniJob(string name) : base(name)
        {
        }

        protected override void Execute()
        {
            _log.Info($"Execute {Name}");

            Thread.Sleep(6000);

            State = JobState.SUCCEED;
        }
    }
}