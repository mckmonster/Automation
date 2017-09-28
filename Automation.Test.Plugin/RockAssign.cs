using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Core;

namespace Automation.Test.Plugin
{
    public class RockAssign : HoudiniJob
    {
        [Editable]
        public string WorldName { get; set; }

        public RockAssign() : base("RockAssign")
        {
            JobName = "130-RockAssign.hip";
        }
    }
}
