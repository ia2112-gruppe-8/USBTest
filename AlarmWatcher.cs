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
        public AlarmType Type { get; set; }
        public event EventHandler alarmTriggered;
        
        public AlarmWatcher(int lowLimit, int highLimit,AlarmType type)
        {
            LowLimit = lowLimit;
            HighLimit = highLimit;
            Type = type;
        }
        public void updateAlarm(int value)
        {
            if (checkLimits(value))
            {
                limitBit = true;
                AlarmBit = sendAlarm();
                if (AlarmBit)
                {
                    alarmTriggered(this, new EventArgs());
                }
            }
            else
            {
                AlarmBit = false;
                limitBit = false;
            }
            AlarmLastScan = limitBit;
            
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
