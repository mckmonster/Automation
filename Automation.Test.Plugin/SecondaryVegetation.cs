using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Core;

namespace Automation.Test.Plugin
{
    public class SecondaryVegetation : HoudiniJob
    {
        [Editable(ReadOnly = true)]
        public string JobName { get; set; }

        [Editable]
        public string WorldName { get; set; }

        public SecondaryVegetation() : base("SecondaryVegetation")
        {
            JobName = "260-SecondaryVegetation.hip";
        }
    }
}
