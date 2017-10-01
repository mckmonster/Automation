namespace Automation.Test.Plugin
{
    public class RoadObjectsScatter : HoudiniJob
    {
        public bool NEEDS_TERRAIN { get; set; }

        public RoadObjectsScatter() : base("RoadObjectsScatter")
        {
            JobName = "415-RoadObjectsScatter.hip";
            NEEDS_TERRAIN = true;
        }
    }
}
