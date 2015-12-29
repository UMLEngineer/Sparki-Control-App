using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;


namespace Sparki_Control_App
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            getAvailablePorts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void getAvailablePorts()
        {
            String[] ports = SerialPort.GetPortNames();
            cbPorts.Items.AddRange(ports);

        }

        private void buOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbPorts.Text == "" || cbBaud.Text == "")
                {
                    tbDataReceived.Text = "Please select port settings";
                }
                else
                {
                    serialPort1.PortName = cbPorts.Text;
                    serialPort1.BaudRate = Convert.ToInt32(cbBaud.Text);
                    serialPort1.Open();
                    pbStatus.Value = 100;
                    buClose.Enabled = true;
                    buOpen.Enabled = false;
                    buPing.Enabled = true;
                }
            }
            catch (UnauthorizedAccessException)
            {
                tbDataReceived.Text = "Unauthorized Access";
            }
        }

        private void buClose_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            pbStatus.Value = 0;
            buOpen.Enabled = true;
            buClose.Enabled = false;
            buPing.Enabled = false;
        }
        string commandOut;

        int checkSumF(char[] arr)
        {
            int sum = 0;
            for (int i=0; i < 5; i++)
                sum += arr[i];
            return sum;
        }

        void commandCreater(String commOut)
        {
            char[] commandArray = new char[9];
            commandArray = commOut.ToCharArray();
            int checkSumV = checkSumF(commandArray);
            commOut += checkSumV;
            commOut += 'z';
            serialPort1.Write(commOut);
            tbDiag.Text = "Data Sent: " + commOut;
        }

        private void buPing_Click(object sender, EventArgs e)
        {
            commandCreater("000pin");

            try
            {
                tbDataReceived.Text = serialPort1.ReadLine();
            }
            catch (TimeoutException)
            {
                tbDiag.Text = "Timeout Excetion";
            }
        }

         private void buDemo_Click(object sender, EventArgs e)
        {
            commandCreater("090lef");
        }
    }
    }
