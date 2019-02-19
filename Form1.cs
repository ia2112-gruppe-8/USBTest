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
        public Form1()
        {
            
            InitializeComponent();
            loadPorts();
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
            
        }
        
        void loadPorts()
        {
            string[] portNames = SerialPort.GetPortNames();
            foreach (string port in portNames)
            {
                comboBox1.Items.Add(port);
            }
            comboBox1.SelectedIndex = 0;
        }
        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(port != null && port.IsOpen)
            {
                port.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ask();
        }
        void ask()
        {
            string message;
            port.Write("1");
            message = port.ReadLine();
            values = message.Split(';');
            txtTemp.Text = $"{values[0]}°C";
            txtPot.Text = values[1];
            txtAlarm.Text = values[2];
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (port == null)
            {
                port = new SerialPort(comboBox1.Text, 9600);//BoardCom
                port.Open();
                timer1.Start();
            }
        }
    }
}
