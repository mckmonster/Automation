﻿using GraphX.PCL.Common.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Threading.Tasks;
using log4net;

namespace GraphX_test
{

    public abstract class Job : VertexBase, INotifyPropertyChanged
    {
        protected ILog _log = log4net.LogManager.GetLogger("Automation.Core");

        public event Action<Job> OnFinished;
        public event Action<Job> OnLaunch;
        public event PropertyChangedEventHandler PropertyChanged;

        private int _nbPreviousJob;

        private JobState _state = JobState.NONE;
        public JobState State
        {
            get => _state;
            set
            {
                if (_state != value)
                {
                    _state = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("State"));
                }
            }
        }
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
                Launch();
            }
        }

        internal void Launch()
        {
            Task.Run(() =>
            {
                Start();

                Execute();

                Finish();
            });
        }

        private void Start()
        {
            _log.Info($"Start {Name}");
            OnLaunch?.Invoke(this);
            State = JobState.INPROGRESS;
        }

        private void Finish()
        {
            _log.Info($"{Name} finished");
            OnFinished?.Invoke(this);
        }
    }
}