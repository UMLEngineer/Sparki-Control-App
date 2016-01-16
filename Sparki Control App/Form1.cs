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


//Line follower fails if it reads a value less than 100, this is due to chartoint
//If sparki records a 92 and sends it back is that 092 or just 92? Probably 92
namespace Sparki_Control_App
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            cbPorts.Items.AddRange(serialPortc.getAvailablePorts());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //global variables  

        LogCreater logger = new LogCreater();
        Serial serialPortc = new Serial();
        
        //General Functions

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
            serialPortc.writeSerial(commOut);
        }

        void printDiag(String text, int data)
        {
            this.tbDiag.Text = text + data;
        }

        int charToInt(String returned)
        {
            char[] returnedc = returned.ToCharArray();
            int length = returnedc.Length;
            
            int converted = 0;
            int j = 0;
            int i = 0;
            int x = 0;
            switch (length)
            {
                case 4:
                    j = 3;
                    x = 100;
                    break;
                case 3:
                    j = 2;
                    x = 10;
                    break;
                case 2:
                    j = 1;
                    x = 1;
                    break;
                default:
                    logger.logWriter("Diag.txt", "Invalid Char to Int power/r/n", 0);
                    break;
            }
            
            while (i<j)
            {
                converted += x * (returnedc[i] - '0');
                x /= 10;
                i++;
            }
            return converted;

        }

        //Sparki Control Functions
            
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
            //if (bwLineFollower.WorkerSupportsCancellation == true)
            //{
                this.bwLineFollower.CancelAsync();
            //}
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
                    /*
                    serialPort1.PortName = cbPorts.Text;
                    serialPort1.BaudRate = Convert.ToInt32(cbBaud.Text);
                    */
                    serialPortc.operations(1, cbPorts.Text);
                    serialPortc.operations(2, cbBaud.Text);
                    serialPortc.operations(3,"");
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
            serialPortc.operations(4, "");
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
                tbDataReceived.Text = serialPortc.readBluetooth();
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
            int threshold = 500;  //below this value means the sensor is on the line
            String returned;  //value sent from sparki as a line of characters
            //char[] returnedc;  //character array of the returned string

            if ((bwLineFollower.CancellationPending == true))
            {
                e.Cancel = true;
                return;
            }
            else
            {
                while (true)
                {
                    commandCreater("000bll");  //ask Sparki for the left Line sensors data
                    returned = serialPortc.readBluetooth();  //read the returned value
                    if (returned == "TE")  //check for a transmission error
                        this.Invoke(new Action<string, int>(printDiag), "Transmission Error", 0);
                    else  //if no error convert the character array into an int and report the value for diag
                    {
                        lineLeft = charToInt(returned);
                        this.Invoke(new Action<string, int>(printDiag), "Line Left:  ", lineLeft);
                        logger.logWriter("bwLineFollower", "\r\n\r\nNew Iteration \r\nLine Left char: " + returned + " int: ", lineLeft);
                    }

                    commandCreater("000blr");  //ask Sparki for the right Line sensors data
                    returned = serialPortc.readBluetooth();
                    if (returned == "TE")
                        this.Invoke(new Action<string, int>(printDiag), "Transmission Error", 0);
                    else
                    {
                        lineRight = charToInt(returned);
                        this.Invoke(new Action<string, int>(printDiag), "Line Right:  " + returned + " int: ", lineRight);
                        logger.logWriter("bwLineFollower", "\r\nLine Right: " + returned + " int: ", lineRight);
                    }

                    commandCreater("000blc");  //ask Sparki for the Center Line sensors data
                    returned = serialPortc.readBluetooth();
                    if (returned == "TE")
                        this.Invoke(new Action<string, int>(printDiag), "Transmission Error", 0);
                    else
                    {
                        lineCenter = charToInt(returned);
                        this.Invoke(new Action<string, int>(printDiag), "Line Center:  " + returned + " int: ", lineCenter);
                        logger.logWriter("bwLineFollower", "\r\nLine Center: " + returned + " int: ", lineCenter);
                    }
                    //use the left, center and right data to make a decision about how to adjust to the line.
                    //need to calibrate, 001 may be to small a step, wht happens if the line is visible by both sensors?

                    if (lineRight < threshold && lineCenter > threshold && lineLeft > threshold)
                        commandCreater("001rig");
                    else if (lineLeft < threshold && lineCenter > threshold && lineRight > threshold)
                        commandCreater("001lef");
                    else if (lineCenter < threshold && lineLeft > threshold && lineRight > threshold)
                        commandCreater("001for");
                    else if (lineLeft < threshold && lineCenter < threshold && lineRight > threshold)
                        commandCreater("001for");
                    else if (lineRight < threshold && lineCenter < threshold && lineLeft > threshold)
                        commandCreater("001for");
                    else if (lineRight < threshold && lineCenter < threshold && lineLeft < threshold)
                    {
                        logger.logWriter("bwLineFollower", "\r\nIntersection found.", 0);
                        commandCreater("090rig");
                        if (lineRight > threshold && lineCenter > threshold && lineLeft > threshold)
                            commandCreater("180lef");
                    }

                    else
                    {
                        e.Cancel = true;
                        return;
                    }
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
                this.Invoke(new Action<string, int>(printDiag), "No line detected", 0);
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
