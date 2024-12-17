namespace OutilsChat
{
    partial class IPAddressControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TextBox textBox4;
            System.Windows.Forms.TextBox textBox5;
            System.Windows.Forms.TextBox textBox6;
            this.field1 = new System.Windows.Forms.TextBox();
            this.field2 = new System.Windows.Forms.TextBox();
            this.field3 = new System.Windows.Forms.TextBox();
            this.field4 = new System.Windows.Forms.TextBox();
            textBox4 = new System.Windows.Forms.TextBox();
            textBox5 = new System.Windows.Forms.TextBox();
            textBox6 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // field1
            // 
            this.field1.AcceptsReturn = true;
            this.field1.AcceptsTab = true;
            this.field1.Location = new System.Drawing.Point(0, 0);
            this.field1.Name = "field1";
            this.field1.Size = new System.Drawing.Size(29, 20);
            this.field1.TabIndex = 0;
            this.field1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.field1.Enter += new System.EventHandler(this.field_Enter);
            this.field1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.field_KeyPress);
            this.field1.Validating += new System.ComponentModel.CancelEventHandler(this.field_Validating);
            // 
            // field2
            // 
            this.field2.AcceptsReturn = true;
            this.field2.AcceptsTab = true;
            this.field2.Location = new System.Drawing.Point(41, 0);
            this.field2.Name = "field2";
            this.field2.Size = new System.Drawing.Size(29, 20);
            this.field2.TabIndex = 1;
            this.field2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.field2.Enter += new System.EventHandler(this.field_Enter);
            this.field2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.field_KeyPress);
            this.field2.Validating += new System.ComponentModel.CancelEventHandler(this.field_Validating);
            // 
            // field3
            // 
            this.field3.AcceptsReturn = true;
            this.field3.AcceptsTab = true;
            this.field3.Location = new System.Drawing.Point(82, 0);
            this.field3.Name = "field3";
            this.field3.Size = new System.Drawing.Size(29, 20);
            this.field3.TabIndex = 2;
            this.field3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.field3.Enter += new System.EventHandler(this.field_Enter);
            this.field3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.field_KeyPress);
            this.field3.Validating += new System.ComponentModel.CancelEventHandler(this.field_Validating);
            // 
            // field4
            // 
            this.field4.AcceptsReturn = true;
            this.field4.AcceptsTab = true;
            this.field4.Location = new System.Drawing.Point(123, 0);
            this.field4.Name = "field4";
            this.field4.Size = new System.Drawing.Size(29, 20);
            this.field4.TabIndex = 3;
            this.field4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.field4.Enter += new System.EventHandler(this.field_Enter);
            this.field4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.field_KeyPress);
            this.field4.Validating += new System.ComponentModel.CancelEventHandler(this.field_Validating);
            // 
            // textBox4
            // 
            textBox4.AcceptsReturn = true;
            textBox4.AcceptsTab = true;
            textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox4.Location = new System.Drawing.Point(30, 0);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new System.Drawing.Size(10, 20);
            textBox4.TabIndex = 1;
            textBox4.TabStop = false;
            textBox4.Text = ".";
            textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox5
            // 
            textBox5.AcceptsReturn = true;
            textBox5.AcceptsTab = true;
            textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox5.Location = new System.Drawing.Point(71, 0);
            textBox5.Name = "textBox5";
            textBox5.ReadOnly = true;
            textBox5.Size = new System.Drawing.Size(10, 20);
            textBox5.TabIndex = 2;
            textBox5.TabStop = false;
            textBox5.Text = ".";
            textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox6
            // 
            textBox6.AcceptsReturn = true;
            textBox6.AcceptsTab = true;
            textBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox6.Location = new System.Drawing.Point(112, 0);
            textBox6.Name = "textBox6";
            textBox6.ReadOnly = true;
            textBox6.Size = new System.Drawing.Size(10, 20);
            textBox6.TabIndex = 3;
            textBox6.TabStop = false;
            textBox6.Text = ".";
            textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // IPAddressControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(textBox6);
            this.Controls.Add(textBox5);
            this.Controls.Add(textBox4);
            this.Controls.Add(this.field4);
            this.Controls.Add(this.field3);
            this.Controls.Add(this.field2);
            this.Controls.Add(this.field1);
            this.Name = "IPAddressControl";
            this.Size = new System.Drawing.Size(152, 20);
            this.Enter += new System.EventHandler(this.IPAddressControl_Enter);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.IPAddressControl_Validating);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox field1;
        private System.Windows.Forms.TextBox field2;
        private System.Windows.Forms.TextBox field3;
        private System.Windows.Forms.TextBox field4;
    }
}
