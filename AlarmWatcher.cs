using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBTest
{
    class AlarmWatcher
    {
        int LowLimit, HighLimit;
        private bool AlarmLastScan;
        bool AlarmBit, limitBit;
        AlarmType Type { get; set; }
        public AlarmWatcher(int lowLimit, int highLimit,AlarmType type)
        {
            LowLimit = lowLimit;
            HighLimit = highLimit;
            Type = type;
        }
        public bool updateAlarm(int value)
        {
            if (checkLimits(value))
            {
                limitBit = true;
                AlarmBit = sendAlarm();
            }
            else
            {
                AlarmBit = false;
                limitBit = false;
            }
            AlarmLastScan = limitBit;
            return AlarmBit;
        }
        private bool checkLimits(int value)
        {
            return (value <= LowLimit || value >= HighLimit);
        }
        private bool sendAlarm()
        {
            return (limitBit == true && AlarmLastScan == false);
        }

    }
}
