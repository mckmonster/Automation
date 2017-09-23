using System;
using System.ComponentModel;
using System.Threading;

namespace Automation.Core
{
    public class HoudiniJob : UbuildJob
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


    }
}