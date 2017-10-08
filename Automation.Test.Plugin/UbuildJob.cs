using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Automation.Core;
using log4net;

namespace Automation.Test.Plugin
{
    public abstract class UbuildJob : INode
    {
        private static Random _rand = new Random();

        public event PropertyChangedEventHandler PropertyChanged;

        private string _name;
        [Browsable(false)]
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                {
                    _name = GetType().Name;
                }

                return _name;
            }
            set
            {
                _name = value;
            }
        }
        
        public UbuildJob()
        {
        }

        public UbuildJob(string name)
        {
            Name = name;
        }

        public bool Execute(ILog log)
        {
            log.Info($"Execute {Name}");

            Thread.Sleep(_rand.Next(10000));
            
            if (_rand.Next(100) < 20)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public virtual void Cut(int _id, int _nbCut)
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
