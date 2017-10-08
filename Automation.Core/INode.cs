using log4net;
using System;
using System.ComponentModel;

namespace Automation.Core
{
    public interface INode : ICloneable, INotifyPropertyChanged
    {
        string Name { get; set; }

        bool Execute(ILog _log);

        void Cut(int _id, int _nbCut);
        void RaisePropertyChanged(string name);
    }
}