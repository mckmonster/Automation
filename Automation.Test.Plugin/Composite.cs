namespace Automation.Test.Plugin
{
    public class Composite : HoudiniJobFrameable
    {
        public int JobCount { get; set; }

        public Composite() : base("Composite")
        {
            JobName = "410-Compisite.hip";
        }
    }
}