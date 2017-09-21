using GraphX.PCL.Common.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace GraphX_test
{
    public class Property : DependencyObject
    {
        internal static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(Property));
        internal static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(Property));

        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }
        public object Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }

    public abstract class Job : VertexBase
    {
        public string Name
        {
            get;
            private set;
        }

        public Job(string name)
        {
            Name = name;
        }
    }

    public class HoudiniJob : Job
    {
        [Editor]
        public string World
        {
            get;
            set;
        }

        [Editor]
        public string Property2 { get; set; }

        [Editor]
        public string CodeVersion { get; set; }

        public HoudiniJob() : this("HoudiniJob")
        {

        }

        public HoudiniJob(string name) : base(name)
        {
        }
    }

    public class RockDeformation : HoudiniJob
    {
        public RockDeformation() : base("RockDeform")
        {
            World = "STP_Japan";
        }
    }

    public class Composite : HoudiniJob
    {
        public Composite() : base("Composite")
        {

        }
    }

    public class SetSelection : HoudiniJob
    {
        public SetSelection() : base("SetSelection")
        { }
    }
}