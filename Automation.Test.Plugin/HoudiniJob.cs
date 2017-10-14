using System.ComponentModel;
using YAXLib;

namespace Automation.Test.Plugin
{
    public enum WorldEnum
    {
        STP_Alpes,
        STP_Alaska,
        STP_Japan
    }

    public abstract class HoudiniJob : UbuildJob
    {
        [Browsable(true)]
        [ReadOnly(true)]
        public string JobName { get; set; }

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore, DefaultValue = WorldEnum.STP_Japan)]
        [DisplayName("World Name")]
        public WorldEnum WorldName { get; set; }

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore, DefaultValue = 0)]
        [Category("Sync Settings")]
        [Description("Version of Code needed")]
        public int CodeVersionId { get; set; }

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore, DefaultValue = 0)]
        [Category("Sync Settings")]
        [Description("Version of BigFile needed")]
        public int BigFileVersionId { get; set; }

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore, DefaultValue = 0)]
        [Category("Sync Settings")]
        [Description("CL Data to create Job")]
        public int Data { get; set; }

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore, DefaultValue = 0)]
        [Category("Sync Settings")]
        [Description("CL Rawdata to create Job")]
        public int Rawdata { get; set; }

        public HoudiniJob(string name) : base(name)
        {
            WorldName = WorldEnum.STP_Japan;
        }
    }
}