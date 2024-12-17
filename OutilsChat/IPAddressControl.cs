using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OutilsChat
{
    public partial class IPAddressControl : UserControl
    {
        public IPAddressControl()
        {
            InitializeComponent();
            this.IsSubnetMask = false;
            if (String.IsNullOrEmpty(this.field1.Text))
            {
                this.field1.Text = "0";
            }
            if (String.IsNullOrEmpty(this.field2.Text))
            {
                this.field2.Text = "0";
            }
            if (String.IsNullOrEmpty(this.field3.Text))
            {
                this.field3.Text = "0";
            }
            if (String.IsNullOrEmpty(this.field4.Text))
            {
                this.field4.Text = "0";
            }
        }

        public String IPAddress
        {
            get
            {
                String address;
                address = this.field1.Text + "." + this.field2.Text + "." + this.field3.Text + "." + this.field4.Text;
                return address;
            }

            set
            {
                String[] elt = value.Split('.');
                if (elt.Length == 4)
                {
                    this.field1.Text = elt[0];
                    this.field2.Text = elt[1];
                    this.field3.Text = elt[2];
                    this.field4.Text = elt[3];
                }
            }
        }

        public bool IsSubnetMask { get; set; }

        private void field_Validating(object sender, CancelEventArgs e)
        {
            //
            e.Cancel = this.CheckIPField((TextBox)sender);
            //
        }

        private bool CheckIPField(TextBox txtBox)
        {
            int f1 = 0;
            bool isOk = true;
            bool needCancel = true;
            //
            //
            if (String.IsNullOrEmpty(txtBox.Text))
            {
                txtBox.Text = "0";
            }
            //
            isOk = isOk && int.TryParse(txtBox.Text, out f1);
            if (!isOk)
            {
                return needCancel;
            }
            //
            if (!this.IsSubnetMask)
            {
                if (IsBetween(f1, 0, 254))
                {
                    // Ok !!
                    needCancel = false;
                }
            }
            else
            {
                if (IsMask(f1, true))
                {
                    // Mask Ok !!
                    needCancel = false;
                }
            }
            //
            return needCancel;
        }

        private bool IsBetween(int value, int min, int max)
        {
            return ((value >= min) && (value <= max));
        }

        private bool IsMask(int value, bool canBeNull)
        {
            if ((value == 0) && canBeNull)
                return true;
            //
            bool ok = false;
            int mask = 0;
            for (int i = 7; i <= 0; i = i - 1)
            {
                mask = mask + 2 ^ i;
                if (value == mask)
                {
                    ok = true;
                    break;
                }
            }
            return ok;
        }

        private void field_KeyPress(object sender, KeyPressEventArgs e)
        {
            //
            switch (e.KeyChar)
            {
                case (char)(8):
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    e.Handled = false;
                    break;
                case (char)(9):
                case (char)(13):
                case '.':
                    e.Handled = true;
                    if (sender == this.field1)
                    {
                        this.field2.Focus();
                    }
                    else if (sender == this.field2)
                    {
                        this.field3.Focus();
                    }
                    else if (sender == this.field3)
                    {
                        this.field4.Focus();
                    }
                    else if (sender == this.field4)
                    {
                        if (e.KeyChar == (char)(9))
                            e.Handled = false;
                    }
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        private void field_Enter(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            txtBox.SelectAll();
        }

        private void IPAddressControl_Enter(object sender, EventArgs e)
        {
            this.field1.Focus();
        }

        private void IPAddressControl_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = this.CheckIPField(this.field1) &&
                this.CheckIPField(this.field2) &&
                this.CheckIPField(this.field3) &&
                this.CheckIPField(this.field4);
        }
    }
}
