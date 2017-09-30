using Automation.Core.Attributes;

namespace Automation.Test.Plugin
{
    public class RoadObjectsScatter : HoudiniJob
    {
        [Editable]
        public string WorldName { get; set; }

        public bool NEEDS_TERRAIN { get; set; }

        public RoadObjectsScatter() : base("RoadObjectsScatter")
        {
            JobName = "415-RoadObjectsScatter.hip";
            NEEDS_TERRAIN = true;
        }
    }
}
