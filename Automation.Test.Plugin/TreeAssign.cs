using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Core.Attributes;

namespace Automation.Test.Plugin
{
    public class TreeAssign : HoudiniJob
    {
        public TreeAssign() : base("TreeAssign")
        {
            JobName = "240-TreeAssign.hip";
        }
    }
}
