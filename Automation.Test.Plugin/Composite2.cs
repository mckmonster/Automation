namespace Automation.Test.Plugin
{
    public class Composite2 : HoudiniJobFrameable
    {
        public int JobCount { get; set; }

        public string User { get; set; }

        public bool NEEDS_TERRAIN { get; set; }

        public Composite2() : base("Composite-02")
        {
            JobName = "411-Composite-02.hip";
            NEEDS_TERRAIN = true;
        }
    }
}