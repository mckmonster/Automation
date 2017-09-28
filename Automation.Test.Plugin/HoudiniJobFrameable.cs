using Automation.Core;
using GraphX.PCL.Common;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Xml;
using YAXLib;

namespace Automation.Test.Plugin
{
    public abstract class HoudiniJobFrameable : HoudiniJob
    {
        [Editable]
        public string Frames { get; set; }

        public HoudiniJobFrameable(string name) : base(name)
        {
        }

        protected override void Cut(int _id, int _nbCut)
        {
            if (string.IsNullOrEmpty(Frames))
            {
                return;
            }
            var frames = Frames.Split('-');
            var start = int.Parse(frames[0].Trim());
            var end = int.Parse(frames[1].Trim());

            var step = (end - start) / _nbCut;
            var newstart = (step * _id) + 1;
            var newend = step + newstart;
            Frames = $"{newstart}-{newend}";
        }
    }
}