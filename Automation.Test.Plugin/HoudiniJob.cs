using System.ComponentModel;
using YAXLib;

namespace Automation.Test.Plugin
{
    public abstract class HoudiniJob : UbuildJob
    {
        [Browsable(true)]
        [ReadOnly(true)]
        public string JobName { get; set; }

        public string WorldName { get; set; }

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore, DefaultValue = 0)]
        public int CodeVersionId { get; set; }
        
        [YAXErrorIfMissed(YAXExceptionTypes.Ignore, DefaultValue = 0)]
        public int BigFileVersionId { get; set; }

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore, DefaultValue = 0)]
        [Description("CL Data to create Job")]
        public int Data { get; set; }

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore, DefaultValue = 0)]
        [Description("CL Rawdata to create Job")]
        public int Rawdata { get; set; }

        public HoudiniJob(string name) : base(name)
        {
        }
    }
}