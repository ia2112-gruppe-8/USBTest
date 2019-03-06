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
    class ArduinoCom:SerialPort
    {
        public event EventHandler usbTimeout;
        //public string ComPort { get; set; }
        //int BaudRate { get; set; }
        //public SerialPort port;
        ComboBox cbo;
        public ArduinoCom(int baudRate, ComboBox comboBox)
        {
            cbo = comboBox;
            LoadPortsInComboBox(cbo);
            BaudRate = baudRate;
            WriteTimeout = 500;
        }
        
        //public bool IsOpen
        //{
          //  get { return port.IsOpen; }
        //}
       // public void Close()
        //{
            //port.Close();
        //}

        public string[] getValues()
        {
            string[] values = new string[3];
            string message;
            if (IsOpen)
            {
                try
                {
                    Write("1");
                    message = ReadLine();
                    values = message.Split(';');
                }
                catch (Exception ex)
                {
                    usbTimeout(this,new EventArgs());//Lager en event som brukes til å skru av timeren så ikke meldingsboksen kommer mange ganger
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                for (int i = 1; i <= values.Length; i++)
                {
                    values[i - 1] = null;
                }
            }
            return values;
        }

        public void LoadPortsInComboBox(ComboBox cbo)
        {
            string[] portNames = GetPortNames();
            cbo.Items.Clear();
            foreach (string port in portNames)
            {
                cbo.Items.Add(port);
            }
            cbo.SelectedIndex = 0;
        }

        public void OpenCom()
        {
            #region Gammel Kode
            /*if(port == null)// Hvis port ikke inneholder et objekt
            {
                try
                {
                port = new SerialPort(ComPort, BaudRate);
                    port.WriteTimeout = 500;
                    
                }
                catch (Exception ex)
                {
                   MessageBox.Show(ex.Message);
                }
            }
            else
            {
                port.Close();
                port.PortName = this.ComPort;
                
            }*/
            #endregion

            if (!IsOpen)//Hvis porten er lukket
            {
                try
                {
                    PortName = cbo.Text;
                Open();//Åpne porten
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                Close();
                PortName = cbo.Text;
                Open();
            }
        }
        
        
    }
}
