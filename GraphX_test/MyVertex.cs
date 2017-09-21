using GraphX.PCL.Common.Models;
using System.Collections.Generic;

namespace GraphX_test
{
    public class Property
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public abstract class Job : VertexBase
    {
        public string Name
        {
            get;
            private set;
        }

        public List<Property> Properties
        {
            get;
            set;
        }

        public Job(string name)
        {
            Name = name;
        }
    }

    public class HoudiniJob : Job
    {
        public HoudiniJob(string name) : base(name)
        {
            Properties = new List<Property>()
            {
                new Property()
                {
                    Name = "World",
                    Value = ""
                },
                new Property()
                {
                    Name = "Property2",
                    Value = ""
                },
                new Property()
                {
                    Name = "CodeVersion",
                    Value = ""
                }
            };
        }
    }
}