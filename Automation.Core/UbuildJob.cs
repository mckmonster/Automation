using GraphX_test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Automation.Core
{
    public abstract class UbuildJob : Job
    {
        private static Random _rand = new Random();
        
        public UbuildJob(string name) : base(name)
        {
        }

        protected override void Execute()
        {
            _log.Info($"Execute {Name}");

            Thread.Sleep(6000);

            if (_rand.Next(100) < 50)
            {
                State = JobState.FAILED;
            }
            else
            {
                State = JobState.SUCCEED;
            }
        }
    }
}
