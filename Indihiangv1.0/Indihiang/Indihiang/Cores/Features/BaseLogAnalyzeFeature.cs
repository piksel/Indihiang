using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indihiang.Cores.Features
{
    public abstract class BaseLogAnalyzeFeature
    {
        protected EnumLogFile _logFile = EnumLogFile.UNKNOWN;
        protected LogFeature _featureName;
        protected Dictionary<string, LogCollection> _logs = new Dictionary<string, LogCollection>();  

        public LogFeature FeatureName
        {
            get
            {
                return _featureName;
            }
            set
            {
                if (_featureName == value)
                    return;
                _featureName = value;
            }
        }
        public Dictionary<string, LogCollection> Items
        {           
            get
            {
                return _logs;
            }
        }

        public BaseLogAnalyzeFeature(EnumLogFile logFile)
        {
            _logFile = logFile;
        }

        public void Parse(List<string> header,string[] item)
        {
            RunFeature(header, item);
        }

        protected abstract bool RunFeature(List<string> header, string[] item);
    }
}
