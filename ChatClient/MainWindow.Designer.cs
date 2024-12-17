namespace ChatClient
{
    partial class MainWindow
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
            System.Windows.Forms.Label label3;
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textAlias = new System.Windows.Forms.TextBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.ipAddressControl1 = new OutilsChat.IPAddressControl();
            this.label2 = new System.Windows.Forms.Label();
            this.numericPort = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.richMessages = new System.Windows.Forms.RichTextBox();
            this.textMessage = new System.Windows.Forms.TextBox();
            this.buttonEnvoi = new System.Windows.Forms.Button();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusBarInfo = new System.Windows.Forms.ToolStripStatusLabel();
            label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPort)).BeginInit();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(563, 484);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(65, 17);
            label3.TabIndex = 4;
            label3.Text = "Message";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textAlias);
            this.groupBox1.Controls.Add(this.buttonStop);
            this.groupBox1.Controls.Add(this.ipAddressControl1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonStart);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(613, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(249, 63);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Alias :";
            // 
            // textAlias
            // 
            this.textAlias.Location = new System.Drawing.Point(328, 59);
            this.textAlias.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textAlias.Name = "textAlias";
            this.textAlias.Size = new System.Drawing.Size(132, 22);
            this.textAlias.TabIndex = 4;
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(473, 26);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(100, 28);
            this.buttonStop.TabIndex = 3;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Visible = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // ipAddressControl1
            // 
            this.ipAddressControl1.BackColor = System.Drawing.SystemColors.Control;
            this.ipAddressControl1.IPAddress = "0.0.0.0";
            this.ipAddressControl1.IsSubnetMask = false;
            this.ipAddressControl1.Location = new System.Drawing.Point(75, 26);
            this.ipAddressControl1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ipAddressControl1.Name = "ipAddressControl1";
            this.ipAddressControl1.Size = new System.Drawing.Size(203, 25);
            this.ipAddressControl1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Serveur";
            // 
            // numericPort
            // 
            this.numericPort.Location = new System.Drawing.Point(357, 27);
            this.numericPort.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericPort.Name = "numericPort";
            this.numericPort.Size = new System.Drawing.Size(104, 22);
            this.numericPort.TabIndex = 1;
            this.numericPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericPort.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(303, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port : ";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(473, 26);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(100, 28);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // richMessages
            // 
            this.richMessages.Location = new System.Drawing.Point(16, 122);
            this.richMessages.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.richMessages.Name = "richMessages";
            this.richMessages.ReadOnly = true;
            this.richMessages.Size = new System.Drawing.Size(612, 354);
            this.richMessages.TabIndex = 1;
            this.richMessages.Text = "";
            // 
            // textMessage
            // 
            this.textMessage.Enabled = false;
            this.textMessage.Location = new System.Drawing.Point(16, 484);
            this.textMessage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textMessage.Multiline = true;
            this.textMessage.Name = "textMessage";
            this.textMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textMessage.Size = new System.Drawing.Size(529, 111);
            this.textMessage.TabIndex = 2;
            // 
            // buttonEnvoi
            // 
            this.buttonEnvoi.Enabled = false;
            this.buttonEnvoi.Location = new System.Drawing.Point(555, 503);
            this.buttonEnvoi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonEnvoi.Name = "buttonEnvoi";
            this.buttonEnvoi.Size = new System.Drawing.Size(75, 92);
            this.buttonEnvoi.TabIndex = 3;
            this.buttonEnvoi.Text = "Envoyer";
            this.buttonEnvoi.UseVisualStyleBackColor = true;
            this.buttonEnvoi.Click += new System.EventHandler(this.buttonEnvoi_Click);
            // 
            // statusBar
            // 
            this.statusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarInfo});
            this.statusBar.Location = new System.Drawing.Point(0, 614);
            this.statusBar.Name = "statusBar";
            this.statusBar.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusBar.Size = new System.Drawing.Size(645, 22);
            this.statusBar.TabIndex = 5;
            this.statusBar.Text = "statusStrip1";
            // 
            // statusBarInfo
            // 
            this.statusBarInfo.Name = "statusBarInfo";
            this.statusBarInfo.Size = new System.Drawing.Size(0, 16);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 636);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.buttonEnvoi);
            this.Controls.Add(label3);
            this.Controls.Add(this.textMessage);
            this.Controls.Add(this.richMessages);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Client de Tchatche";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPort)).EndInit();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label label2;
        private OutilsChat.IPAddressControl ipAddressControl1;
        private System.Windows.Forms.RichTextBox richMessages;
        private System.Windows.Forms.TextBox textMessage;
        private System.Windows.Forms.Button buttonEnvoi;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel statusBarInfo;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textAlias;
    }
}

