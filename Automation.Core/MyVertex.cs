using GraphX.PCL.Common.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System;
using System.Threading.Tasks;
using log4net;

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
        protected ILog _log = log4net.LogManager.GetLogger("Automation.Core");

        public event Action<Job> OnFinished;
        public event Action<Job> OnLaunch;

        private int _nbPreviousJob;

        public string Name
        {
            get;
            private set;
        }
        
        public Job(string name)
        {
            Name = name;
        }

        protected abstract void Execute();
        

        internal void RegisterFinished(Job vertex)
        {
            _nbPreviousJob++;
            _log.Debug($"Register {_nbPreviousJob} {Name}, link on {vertex.Name}");
            vertex.OnFinished += PreviousJob_OnFinished;
        }

        private void PreviousJob_OnFinished(Job _job)
        {
            _nbPreviousJob--;
            _job.OnFinished -= PreviousJob_OnFinished;
            _log.Debug($"{Name} to finished {_nbPreviousJob}");
            if (_nbPreviousJob == 0)
            {                
                Task.Run(() => Launch());
            }
        }

        internal void Launch()
        {
            Start();
            
            Execute();

            Finish();            
        }

        private void Start()
        {
            _log.Info($"Start {Name}");
            OnLaunch?.Invoke(this);
        }

        private void Finish()
        {
            _log.Info($"{Name} finished");
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

        protected override void Execute()
        {
            _log.Info($"Execute {Name}");
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