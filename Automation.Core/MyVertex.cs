using GraphX.PCL.Common.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System;
using System.Threading.Tasks;

namespace GraphX_test
{
    public class Property : DependencyObject
    {
        public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(Property));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(Property));

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
        public event Action<Job> OnFinished;
        public event Action<Job> OnLaunch;

        private int _nbPreviousJob;

        public string Name
        {
            get;
            private set;
        }
        public object InEdges { get; internal set; }

        public Job(string name)
        {
            Name = name;
        }

        protected virtual void Execute()
        {

        }

        internal void RegisterFinished(Job vertex)
        {
            _nbPreviousJob++;
            vertex.OnFinished += PreviousJob_OnFinished;
        }

        private void PreviousJob_OnFinished(Job _job)
        {
            _nbPreviousJob--;
            Task.Run( () => Launch());
        }

        internal void Launch()
        {
            Start();
            
            Execute();

            Finish();            
        }

        private void Start()
        {
            OnLaunch?.Invoke(this);
        }

        private void Finish()
        {
            OnFinished?.Invoke(this);
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