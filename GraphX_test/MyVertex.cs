using GraphX.PCL.Common.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace GraphX_test
{
    public class Property
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }

    public abstract class Job : VertexBase
    {
        public string Name
        {
            get;
            private set;
        }

        public Job(string name)
        {
            Name = name;
        }
    }

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

        public HoudiniJob(string name) : base(name)
        {
        }
    }
}