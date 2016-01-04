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
using System.Threading;


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

        //global variables  
        
        //Functions

        void getAvailablePorts()  //poplate ports combo box
        {
            String[] ports = SerialPort.GetPortNames();
            cbPorts.Items.AddRange(ports);

        }

        String readBluetooth()  //read the data coming back from sparki as a line consider going to a character by character approach
        {
            try
            {
                return serialPort1.ReadLine();
            }
            catch (TimeoutException)
            {
                return "Timeout Exception";
            }
        }

        int checkSumF(char[] arr)
        {
            int sum = 0;
            for (int i = 0; i < 5; i++)
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
        }

        void printDiag(String text, int data)
        {
            this.tbDiag.Text = text + data;

        }

        int charToInt(int multiplier, String returned)
        {
            int x = 100;
            int converted = 0;
            char[] returnedc = returned.ToCharArray();
            for (int i = 0; i < 3; i++)
            {
                converted += x * (returnedc[i] - '0');
                x /= 10;
            }
            return converted;
        }

        //GUI elements

        private void buLineFollow_Click(object sender, EventArgs e)
        {
            if (bwLineFollower.IsBusy != true)
            {
                bwLineFollower.RunWorkerAsync();
            }
        }

        private void buStop_Click(object sender, EventArgs e)
        {
            if (bwLineFollower.WorkerSupportsCancellation == true)
            {
                bwLineFollower.CancelAsync();
            }
        }

        private void tbDiag_TextChanged(object sender, EventArgs e)
        {

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

        private void buPing_Click(object sender, EventArgs e)
        {
            commandCreater("000pin");

            try
            {
                tbDataReceived.Text = serialPort1.ReadLine();
            }
            catch (TimeoutException)
            {
                tbDiag.Text = "TE";
            }
        }

         private void buDemo_Click(object sender, EventArgs e)
        {
           
            commandCreater("090lef");
            System.Threading.Thread.Sleep(5000);
            commandCreater("090rig");
        }

        //Background Worker for the Line Following system
        private void bwLineFollower_DoWork(object sender, DoWorkEventArgs e)
        {

            int lineLeft = 0;
            int lineRight = 0;
            int lineCenter = 0;
            String returned;
            char[] returnedc;

            if ((bwLineFollower.CancellationPending == true))
            {
                e.Cancel = true;
                return;
            }
            else
            {
                commandCreater("000bll");
                returned = readBluetooth();
                if(returned == "TE")
                    this.Invoke(new Action<string, int>(printDiag), "Transmission Error", 0);
                else
                {
                    lineLeft = charToInt(100, returned);
                    this.Invoke(new Action<string, int>(printDiag), "Line Left:  ", lineLeft);
                }
                commandCreater("000blr");
                returned = readBluetooth();
                if (returned == "TE")
                    this.Invoke(new Action<string, int>(printDiag), "Transmission Error", 0);
                else
                {
                    lineRight = charToInt(100, returned);
                    this.Invoke(new Action<string, int>(printDiag), "Line Right:  ", lineRight);
                }
                commandCreater("000blc");
                returned = readBluetooth();
                if (returned == "TE")
                    this.Invoke(new Action<string, int>(printDiag), "Transmission Error", 0);
                else
                {
                    lineCenter = charToInt(100, returned);
                    this.Invoke(new Action<string, int>(printDiag), "Line Center:  ", lineCenter);
                }

            }
        }

        private void bwLineFollower_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void bwLineFollower_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                
            }

            else if (!(e.Error == null))
            {
                
            }

            else
            {
                
            }
        }
    }
}
