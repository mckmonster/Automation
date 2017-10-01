using GraphX.PCL.Common.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Threading.Tasks;
using log4net;
using YAXLib;

namespace Automation.Core
{
    public abstract class Job : VertexBase, INotifyPropertyChanged
    {
        protected ILog Log = LogManager.GetLogger("Automation.Core");

        public event Action<Job> OnFinished;
        public event Action<Job> OnLaunch;
        public event PropertyChangedEventHandler PropertyChanged;

        private int _nbPreviousJob;
        private bool _previousStopped;
        internal bool Canceled { get; set; }

        private JobState _state = JobState.NONE;
        private const int Nbretrymax = 3;

        [YAXDontSerialize]
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

        [YAXDontSerialize]
        [Browsable(false)]
        public string Name
        {
            get;
        }

        [YAXDontSerialize]
        [Browsable(false)]
        public bool Selected
        {
            get;
            set;
        }

        private bool _isExtended = true;
        [YAXDontSerialize]
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

        protected Job(string name)
        {
            Name = name;
        }

        protected abstract void Execute();
        protected abstract void Cut(int _id, int _nbCut);

        public void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        internal void RegisterFinished(Job vertex)
        {
            Canceled = false;
            _nbPreviousJob++;
            _previousStopped = false;
            Log.Debug($"Register {_nbPreviousJob} {Name}, link on {vertex.Name}");
            vertex.OnFinished += PreviousJob_OnFinished;
        }

        private void PreviousJob_OnFinished(Job job)
        {
            _nbPreviousJob--;
 
            job.OnFinished -= PreviousJob_OnFinished;

            Log.Debug($"{Name} to finished {_nbPreviousJob}");
            if (job.State != JobState.SUCCEED)
            {
                Log.Debug($"{job.Name} is failed stop {Name}");
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
                    Log.Debug($"Stop {Name} because previous are failed");
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

                    Execute();

                    if (Canceled)
                    {
                        break;
                    }
                    else if (State == JobState.FAILED)
                    {
                        nbRetry++;
                        Log.Debug($"Retry {Name} {nbRetry} time(s)");
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
            Log.Info($"Start {Name}");
            OnLaunch?.Invoke(this);
            State = JobState.INPROGRESS;
        }

        private void Finish()
        {
            Log.Info($"{Name} finished");
            OnFinished?.Invoke(this);
        }

        public IEnumerable<Job> Duplicate(int nbtime)
        {
            var newjobs = new List<Job>();

            Selected = false;

            for (var i = 0; i < nbtime; i++)
            {
                var newjob = MemberwiseClone() as Job;
                newjob.Cut(i+1,nbtime+1);
                newjobs.Add(newjob);
            }
            Cut(0, nbtime + 1);

            return newjobs;
        }
    }
};