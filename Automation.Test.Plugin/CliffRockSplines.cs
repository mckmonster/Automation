using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Core;

namespace Automation.Test.Plugin
{
    public class CliffRockSplines : HoudiniJob
    {
        [Editable(ReadOnly = true)]
        public string JobName { get; set; }

        [Editable]
        public string WorldName { get; set; }

        public CliffRockSplines() : base("CliffRockSplines")
        {
            JobName = "127-CliffRockSplines.hip";
        }
    }
}
