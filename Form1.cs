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
        string[] values;
        AlarmWatcher pot = new AlarmWatcher(500, 700,AlarmType.Temp);
        ArduinoCom Arduino; 
        bool potAlarm;
        int test = 1;
        
        public Form1()
        {
            InitializeComponent();
            Arduino = new ArduinoCom(/*comboBox1.Text,*/ 9600);
            Arduino.LoadPortsInComboBox(comboBox1);
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
            Arduino.usbTimeout += new EventHandler(usbTimeout);
            pot.alarmTriggered += new EventHandler(potTrigged);
            
        }
       private void usbTimeout(object kilde, EventArgs e)
        {
            timer1.Stop();
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
            if(Arduino != null && Arduino.IsOpen)
            {
                Arduino.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            values = Arduino.getValues();
            WriteValues();
            pot.updateAlarm(Convert.ToInt32(values[1]));
        }
        private void potTrigged(object kilde,EventArgs e)
        {

            //potAlarm = 

            //checkBox1.Checked = true;
            //checkBox1.Checked = false;
            textBox1.Text = pot.Type.ToString();/*$"Hei {test}";*/
                //test += 1;


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
        Batteri,
        Fire
    }
    
}
