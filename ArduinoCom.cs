using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

namespace USBTest
{
    class ArduinoCom
    {
       public string ComPort { get; set; }
        int BaudRate { get; set; }
        SerialPort port;
        public ArduinoCom(/*string comPort,*/ int baudRate)
        {
            BaudRate = baudRate;
            //ComPort = comPort;
            
        }

        public string[] getValues()
        {
            string[] values;
            string message;
            port.Write("1");
            message = port.ReadLine();
            values = message.Split(';');
            return values;
        }
        public void LoadPortsInComboBox(ComboBox cbo)
        {
            string[] portNames = SerialPort.GetPortNames();
            foreach (string port in portNames)
            {
                cbo.Items.Add(port);
            }
            cbo.SelectedIndex = 0;
        }
        public void OpenCom()
        {
            if(port == null)
            {
                port = new SerialPort(ComPort, BaudRate);
                port.Open();
            }
        }
    }
}
