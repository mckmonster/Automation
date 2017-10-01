﻿using GraphX.PCL.Common.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Threading.Tasks;
using log4net;
using YAXLib;

namespace Automation.Core
{
    public class MyVertex : VertexBase, INotifyPropertyChanged
    {
        protected ILog Log = LogManager.GetLogger("Automation.Core");

        public event Action<MyVertex> OnFinished;
        public event Action<MyVertex> OnLaunch;
        public event PropertyChangedEventHandler PropertyChanged;

        private int _nbPreviousJob;
        private bool _previousStopped;
        internal bool Canceled { get; set; }

        public IJob Job { get; private set; }

        private JobState _state = JobState.NONE;
        private const int Nbretrymax = 3;

        [Browsable(false)]
        public JobState State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    RaisePropertyChanged("State");
                }
            }
        }

        [Browsable(false)]
        public bool Selected
        {
            get;
            set;
        }

        private bool _isExtended = true;
        [Browsable(false)]
        public bool IsExtended
        {
            get { return _isExtended; }
            set
            {
                if (_isExtended != value)
                {
                    _isExtended = value;
                    RaisePropertyChanged("IsExtended");
                }
            }
        }

        public MyVertex(string jobType)
        {
            Job = JobFactory.CreateJob(jobType);
            RaisePropertyChanged("Name");
        }

        internal MyVertex(IJob job)
        {
            Job = job;
            RaisePropertyChanged("Name");
        }

        public void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        internal void RegisterFinished(MyVertex vertex)
        {
            Canceled = false;
            _nbPreviousJob++;
            _previousStopped = false;
            Log.Debug($"Register {_nbPreviousJob} {Job.Name}, link on {vertex.Job.Name}");
            vertex.OnFinished += PreviousJob_OnFinished;
        }

        private void PreviousJob_OnFinished(MyVertex vertex)
        {
            _nbPreviousJob--;
 
            vertex.OnFinished -= PreviousJob_OnFinished;

            Log.Debug($"{Job.Name} to finished {_nbPreviousJob}");
            if (vertex.State != JobState.SUCCEED)
            {
                Log.Debug($"{vertex.Job.Name} is failed stop {Job.Name}");
                _previousStopped = true;
            }
            if (_nbPreviousJob == 0)
            {
                if (!_previousStopped && !Canceled)
                {
                    Launch();
                }
                else
                {
                    Log.Debug($"Stop {Job.Name} because previous are failed");
                    Finish();
                }
            }
        }

        internal void Launch()
        {
            switch (State)
            {
                case JobState.SUCCEED:
                    Finish();
                    return;
                case JobState.INPROGRESS:
                    throw new Exception("You shouldn't be in {State} on Launch");
            }

            Task.Run(() =>
            {
                Start();

                int nbRetry = 0;

                do
                {
                    State = JobState.INPROGRESS;

                    if (Job.Execute(Log))
                    {
                        State = JobState.SUCCEED;
                    }
                    else
                    {
                        State = JobState.FAILED;
                    }

                    if (Canceled)
                    {
                        break;
                    }
                    else if (State == JobState.FAILED)
                    {
                        nbRetry++;
                        Log.Debug($"Retry {Job.Name} {nbRetry} time(s)");
                    }
                    else if (State == JobState.SUCCEED)
                    {
                        break;
                    }
                    else
                    {
                        throw new Exception($"We shouldn't be in state {State} after Execute");
                    }
                } while (nbRetry <= Nbretrymax);

                Finish();
            });
        }

        internal void Cancel()
        {
            Canceled = true;            
            Finish();
        }

        private void Start()
        {
            Log.Info($"Start {Job.Name}");
            OnLaunch?.Invoke(this);
            State = JobState.INPROGRESS;
        }

        private void Finish()
        {
            Log.Info($"{Job.Name} finished");
            OnFinished?.Invoke(this);
        }

        public IEnumerable<MyVertex> Duplicate(int nbtime)
        {
            var newjobs = new List<MyVertex>();

            Selected = false;

            for (var i = 0; i < nbtime; i++)
            {
                var newjob = Job.Clone() as IJob;
                newjob.Cut(i+1,nbtime+1);
                var newvertex = new MyVertex(newjob);
                newjobs.Add(newvertex);
            }
            Job.Cut(0, nbtime + 1);

            return newjobs;
        }
    }
};