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

//sparki is over turning

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
        GeneralCommands generalC = new GeneralCommands();
                
        //General Functions

        void printDiag(String text, int data)
        {
            if (data == -111)
                this.tbDiag.Text = text;
            else
                this.tbDiag.Text = text + data;
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
            bwLineFollower.CancelAsync();
            this.Invoke(new Action<string, int>(printDiag), "Action stopped by user", -111);
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
                    serialPortc.operations(1, cbPorts.Text);
                    serialPortc.operations(2, cbBaud.Text);
                    serialPortc.operations(3,"");
                    pbStatus.Value = 100;
                    buClose.Enabled = true;
                    buOpen.Enabled = false;
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
        }

         private void buDemo_Click(object sender, EventArgs e)
        {
            generalC.commandCreater("090lef");
            System.Threading.Thread.Sleep(5000);
            generalC.commandCreater("090rig");
        }

        //Background Worker for the Line Following system
        private void bwLineFollower_DoWork(object sender, DoWorkEventArgs e)
        {
            int threshold = 500;  //below this value means the sensor is on the line
            String returned;  //value sent from sparki as a line of characters

            if ((bwLineFollower.CancellationPending == true))
            {
                e.Cancel = true;
                return;
            }
            else
            {
                while (true)
                {
                    var result = generalC.lineValues();
                    if (result.lineRight < threshold && result.lineCenter > threshold && result.lineLeft > threshold)
                        generalC.commandCreater("001rig");
                    else if (result.lineLeft < threshold && result.lineCenter > threshold && result.lineRight > threshold)
                        generalC.commandCreater("001lef");
                    else if (result.lineCenter < threshold && result.lineLeft > threshold && result.lineRight > threshold)
                        generalC.commandCreater("005for");
                    else if (result.lineLeft < threshold && result.lineCenter < threshold && result.lineRight > threshold)
                        generalC.commandCreater("001for");
                    else if (result.lineRight < threshold && result.lineCenter < threshold && result.lineLeft > threshold)
                        generalC.commandCreater("001for");
                    else if (result.lineRight < threshold && result.lineCenter < threshold && result.lineLeft < threshold)
                    {
                        logger.logWriter("bwLineFollower", "\r\n\r\nIntersection found.", -111);
                        generalC.commandCreater("090rig");
                        Thread.Sleep(2000);
                        result = generalC.lineValues();
                        if (result.lineRight > threshold && result.lineCenter > threshold && result.lineLeft > threshold)
                        {
                            generalC.commandCreater("180lef");
                            Thread.Sleep(3000);
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                        this.Invoke(new Action<string, int>(printDiag), "No Line Found", -111);
                        return;
                    }
                    if ((bwLineFollower.CancellationPending == true))
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private void bwLineFollower_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {}

        private void bwLineFollower_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {}
            else if (!(e.Error == null))
            {}
            else
            {}
        }
    }
}
