using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Core;

namespace Automation.Test.Plugin
{
    public class TreeAssign : HoudiniJob
    {
        [Editable]
        public string WorldName { get; set; }

        public TreeAssign() : base("TreeAssign")
        {
            JobName = "240-TreeAssign.hip";
        }
    }
}
