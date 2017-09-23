using System;
using GraphX.PCL.Common.Models;

namespace Automation.Core
{
    public partial class MyEdge : EdgeBase<Job>
    {
        public MyEdge(Job source, Job target, double weight = 1) : base(source, target, weight)
        {
        }

        public MyEdge()
            : base(null, null, 1) { }

        public override string ToString()
        {
            return string.Empty;
        }
    }
}