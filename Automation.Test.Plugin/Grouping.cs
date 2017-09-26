using Automation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Test.Plugin
{
    public class Grouping : HoudiniJob
    {
        [Editable(ReadOnly = true)]
        public string JobName { get; set; }

        [Editable]
        public string WorldName { get; set; }

        [Editable]
        public string User { get; set; }

        public bool NEEDS_TERRAIN { get; set; }

        public Grouping() : base("Grouping")
        {
            JobName = "420-Grouping.hip";
            NEEDS_TERRAIN = true;
        }
    }
}
