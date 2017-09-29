﻿using Automation.Core;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Xml;
using YAXLib;

namespace Automation.Test.Plugin
{
    public abstract class HoudiniJob : UbuildJob
    {
        [Editable(ReadOnly = true)]
        public string JobName { get; set; }

        [Editable]
        [YAXErrorIfMissed(YAXExceptionTypes.Ignore, DefaultValue = 0)]
        public int CodeVersionId { get; set; }

        [Editable]
        [YAXErrorIfMissed(YAXExceptionTypes.Ignore, DefaultValue = 0)]
        public int BigFileVersionId { get; set; }

        [Editable]
        [YAXErrorIfMissed(YAXExceptionTypes.Ignore, DefaultValue = 0)]
        public int Data { get; set; }

        public HoudiniJob() : this("HoudiniJob")
        {
            
        }

        public HoudiniJob(string name) : base(name)
        {
        }

        protected override void Cut(int _id, int _nbCut)
        {
            
        }
    }
}