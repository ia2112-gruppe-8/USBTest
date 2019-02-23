using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;
using System.Management;

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
            string[] values = new string[3];
            string message;
            if (port.IsOpen)
            {
            port.Write("1");
            message = port.ReadLine();
            values = message.Split(';');
            }
            else
            {
                for (int i = 1; i <= values.Length; i++)
                {
                    values[i - 1] = "9999";
                }
            }
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
            if(port == null)// Hvis port ikke inneholder et objekt
            {
                port = new SerialPort(ComPort, BaudRate);
            }
            if (!port.IsOpen)//Hvis porten er lukket
            {
                try
                {
                port.Open();//Åpne porten

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
