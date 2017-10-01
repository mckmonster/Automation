using Automation.Core.Attributes;
using System.ComponentModel;
using YAXLib;

namespace Automation.Test.Plugin
{
    public abstract class HoudiniJob : UbuildJob
    {
        [Browsable(true)]
        [ReadOnly(true)]
        public string JobName { get; set; }

        [Editable]
        public string WorldName { get; set; }

        [Editable(DisplayName = "Code Version Id")]
        [YAXErrorIfMissed(YAXExceptionTypes.Ignore, DefaultValue = 0)]
        public int CodeVersionId { get; set; }

        [Editable]
        [YAXErrorIfMissed(YAXExceptionTypes.Ignore, DefaultValue = 0)]
        public int BigFileVersionId { get; set; }

        [Editable]
        [YAXErrorIfMissed(YAXExceptionTypes.Ignore, DefaultValue = 0)]
        public int Data { get; set; }

        public HoudiniJob(string name) : base(name)
        {
        }

        protected override void Cut(int _id, int _nbCut)
        {
            
        }
    }
}