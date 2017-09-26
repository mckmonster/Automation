using GraphX.PCL.Common.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Threading.Tasks;
using log4net;
using System.Xml;
using YAXLib;

namespace Automation.Core
{

    public abstract class Job : VertexBase, INotifyPropertyChanged
    {
        protected ILog _log = log4net.LogManager.GetLogger("Automation.Core");

        public event Action<Job> OnFinished;
        public event Action<Job> OnLaunch;
        public event PropertyChangedEventHandler PropertyChanged;

        private int _nbPreviousJob;
        private bool _PreviousStopped = false;
        internal bool Canceled { get; set; }

        private JobState _state = JobState.NONE;
        private readonly int NBRETRYMAX = 3;

        [YAXDontSerialize]
        public JobState State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("State"));
                }
            }
        }

        [YAXDontSerialize]
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

        public void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        internal void RegisterFinished(Job vertex)
        {
            Canceled = false;
            _nbPreviousJob++;
            _PreviousStopped = false;
            _log.Debug($"Register {_nbPreviousJob} {Name}, link on {vertex.Name}");
            vertex.OnFinished += PreviousJob_OnFinished;
        }

        private void PreviousJob_OnFinished(Job _job)
        {
            _nbPreviousJob--;
 
            _job.OnFinished -= PreviousJob_OnFinished;

            _log.Debug($"{Name} to finished {_nbPreviousJob}");
            if (_job.State != JobState.SUCCEED)
            {
                _log.Debug($"{_job.Name} is failed stop {Name}");
                _PreviousStopped = true;
            }
            if (_nbPreviousJob == 0)
            {
                if (!_PreviousStopped && !Canceled)
                {
                    Launch();
                }
                else
                {
                    _log.Debug($"Stop {Name} because previous are failed");
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

                int _nbRetry = 0;

                do
                {
                    State = JobState.INPROGRESS;

                    Execute();

                    if (Canceled)
                    {
                        break;
                    }
                    else if (State == JobState.FAILED)
                    {
                        _nbRetry++;
                        _log.Debug($"Retry {Name} {_nbRetry} time(s)");
                    }
                    else if (State == JobState.SUCCEED)
                    {
                        break;
                    }
                    else
                    {
                        throw new Exception($"We shouldn't be in state {State} after Execute");
                    }
                } while (_nbRetry <= NBRETRYMAX);

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