using Automation.Core;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Xml;

namespace Automation.Test.Plugin
{
    public class HoudiniJob : UbuildJob
    {
        public static readonly DependencyProperty WorldNameProperty = DependencyProperty.Register("WorldName", typeof(string), typeof(HoudiniJob));

        [Editable]
        public string WorldName
        {
            get;
            set;
        }

        [Editable]
        public string User
        {
            get;
            set;
        }

        [Editable]
        public bool NEEDS_TERRAIN { get; set; }

        [Editable]
        public string Frames { get; set; }

        [Editable]
        public string Start_Frame { get; set; }

        [Editable]
        public string Tile_Min { get; set; }

        [Editable]
        public bool DONTCLEAN { get; set; }

        [Editable]
        public bool NEEDS_SCIMITAR { get; set; }
        
        [Editable]
        public string CodeVersionId { get; set; }

        [Editable]
        public string PCCodeVersionId { get; set; }

        [Editable]
        public string BigFileCodeVersionId { get; set; }

        [Editable]
        public string MapName { get; set; }

        [Editable]
        public string StatFile { get; set; }

        public HoudiniJob() : this("HoudiniJob")
        {
            
        }

        public HoudiniJob(string name) : base(name)
        {
        }

        public override void Save(XmlWriter wr)
        {
            wr.WriteElementString("WorldName", WorldName);
            wr.WriteElementString("User", User);
            wr.WriteElementString("NEEDS_TERRAIN", NEEDS_TERRAIN.ToString());
        }
    }
}