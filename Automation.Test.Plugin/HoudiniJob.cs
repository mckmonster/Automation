using System;
using System.ComponentModel;
using System.Threading;

namespace Automation.Test.Plugin
{
    public class HoudiniJob : UbuildJob
    {
        public string World
        {
            get;
            set;
        }

        public string Property2 { get; set; }

        public string CodeVersion { get; set; }

        public HoudiniJob() : this("HoudiniJob")
        {
            
        }

        public HoudiniJob(string name) : base(name)
        {
        }


    }
}