using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace USBTest
{
    public partial class Form1 : Form
    {
        SerialPort port;
        string[] values;
        AlarmWatcher pot = new AlarmWatcher(500, 700,AlarmType.Temp);
        ArduinoCom Arduino; 
        bool potAlarm;

        public Form1()
        {
            InitializeComponent();
            Arduino = new ArduinoCom(/*comboBox1.Text,*/ 9600);
            Arduino.LoadPortsInComboBox(comboBox1);
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
        }
        
       /* void loadPorts()
        {
            string[] portNames = SerialPort.GetPortNames();
            foreach (string port in portNames)
            {
                comboBox1.Items.Add(port);
            }
            comboBox1.SelectedIndex = 0;
        }*/
        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(port != null && port.IsOpen)
            {
                port.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            values = Arduino.getValues();
            WriteValues();
            potAlarm = pot.updateAlarm(Convert.ToInt32(values[1]));
            if (potAlarm)
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;

            }
        }
        /*void ask()//sender 1 til arduino for å spørre om data, og mottar stringen og deler den opp ved skilletegn ';' og laster inn i array
        {
            string message;
            port.Write("1");
            message = port.ReadLine();
            values = message.Split(';');
            txtTemp.Text = $"{values[0]}°C";//Temp
            txtPot.Text = values[1];//Potmeter verdi
            txtAlarm.Text = values[2];//AlarmBit
        }*/
        void WriteValues()
        {
            txtTemp.Text = $"{values[0]}°C";//Temp
            txtPot.Text = values[1];//Potmeter verdi
            txtAlarm.Text = values[2];//AlarmBit
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Arduino = new ArduinoCom(com
            Arduino.ComPort = comboBox1.Text;
            Arduino.OpenCom();
                timer1.Start();
        }
    }
    enum AlarmType
    {
        Temp,
        Movement,
        Batteri
    }
}
