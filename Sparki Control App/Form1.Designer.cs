namespace Sparki_Control_App
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cbPorts = new System.Windows.Forms.ComboBox();
            this.cbBaud = new System.Windows.Forms.ComboBox();
            this.pbStatus = new System.Windows.Forms.ProgressBar();
            this.laPort = new System.Windows.Forms.Label();
            this.laBaud = new System.Windows.Forms.Label();
            this.laStatus = new System.Windows.Forms.Label();
            this.buPing = new System.Windows.Forms.Button();
            this.tbDataReceived = new System.Windows.Forms.TextBox();
            this.buOpen = new System.Windows.Forms.Button();
            this.buClose = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.laDataReceived = new System.Windows.Forms.Label();
            this.tbDiag = new System.Windows.Forms.TextBox();
            this.laDiag = new System.Windows.Forms.Label();
            this.buDemo = new System.Windows.Forms.Button();
            this.bwLineFollower = new System.ComponentModel.BackgroundWorker();
            this.buLineFollow = new System.Windows.Forms.Button();
            this.buStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbPorts
            // 
            this.cbPorts.FormattingEnabled = true;
            this.cbPorts.Location = new System.Drawing.Point(12, 56);
            this.cbPorts.Name = "cbPorts";
            this.cbPorts.Size = new System.Drawing.Size(121, 21);
            this.cbPorts.TabIndex = 0;
            // 
            // cbBaud
            // 
            this.cbBaud.FormattingEnabled = true;
            this.cbBaud.Items.AddRange(new object[] {
            "9600",
            "115200"});
            this.cbBaud.Location = new System.Drawing.Point(160, 56);
            this.cbBaud.Name = "cbBaud";
            this.cbBaud.Size = new System.Drawing.Size(121, 21);
            this.cbBaud.TabIndex = 1;
            // 
            // pbStatus
            // 
            this.pbStatus.Location = new System.Drawing.Point(320, 54);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(100, 23);
            this.pbStatus.TabIndex = 2;
            // 
            // laPort
            // 
            this.laPort.AutoSize = true;
            this.laPort.Location = new System.Drawing.Point(12, 37);
            this.laPort.Name = "laPort";
            this.laPort.Size = new System.Drawing.Size(26, 13);
            this.laPort.TabIndex = 3;
            this.laPort.Text = "Port";
            // 
            // laBaud
            // 
            this.laBaud.AutoSize = true;
            this.laBaud.Location = new System.Drawing.Point(160, 38);
            this.laBaud.Name = "laBaud";
            this.laBaud.Size = new System.Drawing.Size(32, 13);
            this.laBaud.TabIndex = 4;
            this.laBaud.Text = "Baud";
            // 
            // laStatus
            // 
            this.laStatus.AutoSize = true;
            this.laStatus.Location = new System.Drawing.Point(320, 36);
            this.laStatus.Name = "laStatus";
            this.laStatus.Size = new System.Drawing.Size(65, 13);
            this.laStatus.TabIndex = 5;
            this.laStatus.Text = "Connected?";
            // 
            // buPing
            // 
            this.buPing.Location = new System.Drawing.Point(15, 120);
            this.buPing.Name = "buPing";
            this.buPing.Size = new System.Drawing.Size(75, 23);
            this.buPing.TabIndex = 6;
            this.buPing.Text = "Ping Distance";
            this.buPing.UseVisualStyleBackColor = true;
            this.buPing.Click += new System.EventHandler(this.buPing_Click);
            // 
            // tbDataReceived
            // 
            this.tbDataReceived.Location = new System.Drawing.Point(160, 122);
            this.tbDataReceived.Multiline = true;
            this.tbDataReceived.Name = "tbDataReceived";
            this.tbDataReceived.Size = new System.Drawing.Size(260, 96);
            this.tbDataReceived.TabIndex = 7;
            // 
            // buOpen
            // 
            this.buOpen.Location = new System.Drawing.Point(475, 120);
            this.buOpen.Name = "buOpen";
            this.buOpen.Size = new System.Drawing.Size(75, 23);
            this.buOpen.TabIndex = 8;
            this.buOpen.Text = "Open";
            this.buOpen.UseVisualStyleBackColor = true;
            this.buOpen.Click += new System.EventHandler(this.buOpen_Click);
            // 
            // buClose
            // 
            this.buClose.Location = new System.Drawing.Point(475, 149);
            this.buClose.Name = "buClose";
            this.buClose.Size = new System.Drawing.Size(75, 23);
            this.buClose.TabIndex = 9;
            this.buClose.Text = "Close";
            this.buClose.UseVisualStyleBackColor = true;
            this.buClose.Click += new System.EventHandler(this.buClose_Click);
            // 
            // laDataReceived
            // 
            this.laDataReceived.AutoSize = true;
            this.laDataReceived.Location = new System.Drawing.Point(160, 103);
            this.laDataReceived.Name = "laDataReceived";
            this.laDataReceived.Size = new System.Drawing.Size(79, 13);
            this.laDataReceived.TabIndex = 10;
            this.laDataReceived.Text = "Data Received";
            // 
            // tbDiag
            // 
            this.tbDiag.Location = new System.Drawing.Point(160, 269);
            this.tbDiag.Multiline = true;
            this.tbDiag.Name = "tbDiag";
            this.tbDiag.Size = new System.Drawing.Size(260, 114);
            this.tbDiag.TabIndex = 11;
            this.tbDiag.TextChanged += new System.EventHandler(this.tbDiag_TextChanged);
            // 
            // laDiag
            // 
            this.laDiag.AutoSize = true;
            this.laDiag.Location = new System.Drawing.Point(160, 250);
            this.laDiag.Name = "laDiag";
            this.laDiag.Size = new System.Drawing.Size(107, 13);
            this.laDiag.TabIndex = 12;
            this.laDiag.Text = "Diagnotic Information";
            // 
            // buDemo
            // 
            this.buDemo.Location = new System.Drawing.Point(15, 148);
            this.buDemo.Name = "buDemo";
            this.buDemo.Size = new System.Drawing.Size(75, 23);
            this.buDemo.TabIndex = 13;
            this.buDemo.Text = "Demo";
            this.buDemo.UseVisualStyleBackColor = true;
            this.buDemo.Click += new System.EventHandler(this.buDemo_Click);
            // 
            // bwLineFollower
            // 
            this.bwLineFollower.WorkerReportsProgress = true;
            this.bwLineFollower.WorkerSupportsCancellation = true;
            this.bwLineFollower.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLineFollower_DoWork);
            this.bwLineFollower.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwLineFollower_ProgressChanged);
            this.bwLineFollower.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLineFollower_RunWorkerCompleted);
            // 
            // buLineFollow
            // 
            this.buLineFollow.Location = new System.Drawing.Point(15, 178);
            this.buLineFollow.Name = "buLineFollow";
            this.buLineFollow.Size = new System.Drawing.Size(75, 23);
            this.buLineFollow.TabIndex = 14;
            this.buLineFollow.Text = "Line Follow";
            this.buLineFollow.UseVisualStyleBackColor = true;
            this.buLineFollow.Click += new System.EventHandler(this.buLineFollow_Click);
            // 
            // buStop
            // 
            this.buStop.Location = new System.Drawing.Point(475, 178);
            this.buStop.Name = "buStop";
            this.buStop.Size = new System.Drawing.Size(75, 23);
            this.buStop.TabIndex = 15;
            this.buStop.Text = "Stop";
            this.buStop.UseVisualStyleBackColor = true;
            this.buStop.Click += new System.EventHandler(this.buStop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 408);
            this.Controls.Add(this.buStop);
            this.Controls.Add(this.buLineFollow);
            this.Controls.Add(this.buDemo);
            this.Controls.Add(this.laDiag);
            this.Controls.Add(this.tbDiag);
            this.Controls.Add(this.laDataReceived);
            this.Controls.Add(this.buClose);
            this.Controls.Add(this.buOpen);
            this.Controls.Add(this.tbDataReceived);
            this.Controls.Add(this.buPing);
            this.Controls.Add(this.laStatus);
            this.Controls.Add(this.laBaud);
            this.Controls.Add(this.laPort);
            this.Controls.Add(this.pbStatus);
            this.Controls.Add(this.cbBaud);
            this.Controls.Add(this.cbPorts);
            this.Name = "Form1";
            this.Text = "Sparki Control Center";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbPorts;
        private System.Windows.Forms.ComboBox cbBaud;
        private System.Windows.Forms.ProgressBar pbStatus;
        private System.Windows.Forms.Label laPort;
        private System.Windows.Forms.Label laBaud;
        private System.Windows.Forms.Label laStatus;
        private System.Windows.Forms.Button buPing;
        private System.Windows.Forms.TextBox tbDataReceived;
        private System.Windows.Forms.Button buOpen;
        private System.Windows.Forms.Button buClose;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label laDataReceived;
        private System.Windows.Forms.TextBox tbDiag;
        private System.Windows.Forms.Label laDiag;
        private System.Windows.Forms.Button buDemo;
        private System.ComponentModel.BackgroundWorker bwLineFollower;
        private System.Windows.Forms.Button buLineFollow;
        private System.Windows.Forms.Button buStop;
    }
}

