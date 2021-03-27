using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDRv
{
    class DevOptValue
    {
        private bool keyTriggerMode = false;
        private bool serialNubmerMode = false;
        private bool logFileMode = false;
        private int testStepMode = 0;
        private string currentSn = "";
        private string currentFilePath = "";
        private string historyFilePath = "";

        public DevOptValue(bool k, bool s, bool l, int t, string cs, string cf, string hf)
        {
            this.keyTriggerMode = k;
            this.serialNubmerMode = s;
            this.logFileMode = l;
            this.testStepMode = t;
            this.currentSn = cs;
            this.currentFilePath = cf;
            this.historyFilePath = hf;
        }

        public bool KeyTriggerMode
        {
            get
            {
                return keyTriggerMode;
            }
            set
            {
                keyTriggerMode = value;
            }
        }
        public bool SerialNubmerMode
        {
            get
            {
                return serialNubmerMode;
            }
            set
            {
                serialNubmerMode = value;
            }
        }
        public bool LogFileMode
        {
            get
            {
                return logFileMode;
            }
            set
            {
                logFileMode = value;
            }
        }
        public int TestStepMode
        {
            get
            {
                return testStepMode;
            }
            set
            {
                testStepMode = value;
            }
        }
        public string CurrentSn
        {
            get
            {
                return currentSn;
            }
            set
            {
                currentSn = value;
            }
        }
        public string CurrentFilePath
        {
            get
            {
                return currentFilePath;
            }
            set
            {
                currentFilePath = value;
            }
        }
        public string HistoryFilePath
        {
            get
            {
                return historyFilePath;
            }
            set
            {
                historyFilePath = value;
            }
        }
    }
}
