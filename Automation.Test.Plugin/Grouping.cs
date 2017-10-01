namespace Automation.Test.Plugin
{
    public class Grouping : HoudiniJob
    {
        public string User { get; set; }

        public bool NEEDS_TERRAIN { get; set; }

        public Grouping() : base("Grouping")
        {
            JobName = "420-Grouping.hip";
            NEEDS_TERRAIN = true;
        }
    }
}
