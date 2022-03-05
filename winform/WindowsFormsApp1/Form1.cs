using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace WindowsFormsApp1
{
    public partial class btnPosNeg : Form
    {
        public enum OpFocus
        {
            NONE,
            OPERAND1,
            OPERAND2
        }

        public const string ADD = "+";
        public const string SUBTRACT = "-";
        public const string MULTIPLY = "X";

        public int gWhichOpHasFocus;

        public void appendOperand(string s1)
        {

            if (gWhichOpHasFocus == (int)OpFocus.OPERAND1)
            {

                // prevent more than 8 bits for hex or 32 bits for bin
                if (rdoHex.Checked)
                {
                    if (txtOperand1.Text.Length == fpOperations.Standard754FPNumber.HEX_LENGTH32)
                    {
                        Debug.WriteLine("stop append op1 hex length is " + txtOperand1.Text.Length);
                        return;
                    }
                }
                if (rdoBin.Checked)
                {
                    if (txtOperand1.Text.Length == fpOperations.Standard754FPNumber.NUM_EXPONENT_BITS32 +
                            fpOperations.Standard754FPNumber.NUM_MANTISSA_BITS32 +
                            fpOperations.Standard754FPNumber.NUM_SIGN_BITS32)
                    {
                        Debug.WriteLine("stop append op2 bin length is " + txtOperand1.Text.Length);
                        return;
                    }
                }

                if (s1 == "." && txtOperand1.Text.Contains("."))  // only allow one "."
                { s1 = ""; }

                if (txtOperand1.Text == "0")
                {
                    if (s1 == ".")
                    {
                        s1 = "0.";
                    }

                    txtOperand1.Text = s1;
                }
                else
                    txtOperand1.Text += s1;

            }
            else if (gWhichOpHasFocus == (int)OpFocus.OPERAND2)
            {
                // prevent more than 8 bits for hex or 32 bits for bin
                if (rdoHex.Checked)
                {
                    if (txtOperand2.Text.Length == fpOperations.Standard754FPNumber.HEX_LENGTH32)
                    {
                        Debug.WriteLine("stop append op2 hex length is " + txtOperand2.Text.Length);
                        return;
                    }
                }
                if (rdoBin.Checked)
                {
                    if (txtOperand2.Text.Length == fpOperations.Standard754FPNumber.NUM_EXPONENT_BITS32 +
                            fpOperations.Standard754FPNumber.NUM_MANTISSA_BITS32 +
                            fpOperations.Standard754FPNumber.NUM_SIGN_BITS32)
                    {
                        Debug.WriteLine("stop append op2 bin length is " + txtOperand2.Text.Length);
                        return;
                    }
                }

                if (s1 == "." && txtOperand2.Text.Contains("."))
                { s1 = ""; }

                if (txtOperand2.Text == "0")
                {
                    if (s1 == ".")
                    {
                        s1 = "0.";
                    }

                    txtOperand2.Text = s1;
                }
                else
                    txtOperand2.Text += s1;

            }

            //lblOperation.Text = "";
            txtResult.Text = "";
            
        }
        public btnPosNeg()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rdoDec.Checked = true;
            //lblStatusOperand1.Visible = false;
            //lblStatusOperand2.Visible = false;

            //txtOperand2.Select();
            //txtOperand1.Select();

            txtOperand1.BackColor = Color.White;
            txtOperand2.BackColor = Color.White;

            // this label prevents the form from resizing too small on initialization
            // the form initializes with the desired spacing due to the label
            // now hide the label here

            cbOperation.SelectedIndex = 0;
            this.Text = "Floating Point Calculator";
        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {
                    
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void rdoDec_CheckedChanged(object sender, EventArgs e)
        {
            //lblStatusOperand1.Visible = false;

            txtOperand1.Text = "0";
            txtOperand2.Text = "0";
            txtAllResults.Text = "";

            btn0.Enabled = true;
            btn1.Enabled = true;
            btn2.Enabled = true;
            btn3.Enabled = true;
            btn4.Enabled = true;
            btn5.Enabled = true;
            btn6.Enabled = true;
            btn7.Enabled = true;
            btn8.Enabled = true;
            btn9.Enabled = true;
            btnA.Enabled = false;
            btnB.Enabled = false;
            btnC.Enabled = false;
            btnD.Enabled = false;
            btnE.Enabled = false;
            btnF.Enabled = false;
            btnDot.Enabled = true;
            btnBackSpace.Enabled = true;
            btnClear.Enabled = true;
        }

        private void rdoHex_CheckedChanged(object sender, EventArgs e)
        {
            txtOperand1.Text = "0";
            txtOperand2.Text = "0";
            txtAllResults.Text = "";
            //lblStatusOperand1.Visible = false;

            btn0.Enabled = true;
            btn1.Enabled = true;
            btn2.Enabled = true;
            btn3.Enabled = true;
            btn4.Enabled = true;
            btn5.Enabled = true;
            btn6.Enabled = true;
            btn7.Enabled = true;
            btn8.Enabled = true;
            btn9.Enabled = true;
            btnA.Enabled = true;
            btnB.Enabled = true;
            btnC.Enabled = true;
            btnD.Enabled = true;
            btnE.Enabled = true;
            btnF.Enabled = true;
            btnDot.Enabled = true;
            btnBackSpace.Enabled = true;
            btnClear.Enabled = true;
        }

        private void rdoBin_CheckedChanged(object sender, EventArgs e)
        {
            txtOperand1.Text = "0";
            txtOperand2.Text = "0";
            txtAllResults.Text = "";
            //lblStatusOperand1.Visible = true;

            btn0.Enabled = true;
            btn1.Enabled = true;
            btn2.Enabled = false;
            btn3.Enabled = false;
            btn4.Enabled = false;
            btn5.Enabled = false;
            btn6.Enabled = false;
            btn7.Enabled = false;
            btn8.Enabled = false;
            btn9.Enabled = false;
            btnA.Enabled = false;
            btnB.Enabled = false;
            btnC.Enabled = false;
            btnD.Enabled = false;
            btnE.Enabled = false;
            btnF.Enabled = false;
            btnDot.Enabled = true;
            btnBackSpace.Enabled = true;
            btnClear.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (gWhichOpHasFocus == (int)OpFocus.OPERAND1)
            {
                if (txtOperand1.Text.Length >= 1)
                    txtOperand1.Text = txtOperand1.Text.Remove(txtOperand1.Text.Length - 1, 1);
            }
            else if (gWhichOpHasFocus == (int)OpFocus.OPERAND2)
            {
                if (txtOperand2.Text.Length >= 1)
                    txtOperand2.Text = txtOperand2.Text.Remove(txtOperand2.Text.Length - 1, 1);
            }

            txtResult.Text = "";
           
        }

        private void button28_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(whichFieldHasFocus.ToString());
        }

        private void txtOperand1_GotFocus(object sender, EventArgs e)
        {
            //whichFieldHasFocus = 1;
            //txtOperand1.BackColor = Color.Red;
        }

        private void txtOperand2_GotFocus(object sender, EventArgs e)
        {
            //whichFieldHasFocus = 2;
            //txtOperand1.BackColor = Color.Red;
        }

        private void btnDiv_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtOperand1.Text = "0";
            txtOperand2.Text = "0";

            txtResult.Text = "";
        }

        private void txtOperand1_TextChanged(object sender, EventArgs e)
        {
            string s1 = txtOperand1.Text;
            if (s1 != ".")  // if number string is just starting with "." it will break Decimal.Parse
            {
                //decimal d1 = Decimal.Parse(s1);

                //fpOperations.Standard754FPNumber fpn = new fpOperations.Standard754FPNumber((float)d1);


            }
            lblOp1Len.Text = txtOperand1.Text.Length.ToString();
        }

        private void txtOperand2_TextChanged(object sender, EventArgs e)
        {
            string s1 = txtOperand2.Text;
            if (s1 != ".")  // if number string is just starting with "." it will break Decimal.Parse
            {
                //decimal d1 = Decimal.Parse(s1);

                //fpOperations.Standard754FPNumber fpn = new fpOperations.Standard754FPNumber((float)d1);

            }
            lblOp2Len.Text = txtOperand2.Text.Length.ToString();
        }

        private void txtOperand1_Enter(object sender, EventArgs e)
        {
            txtOperand1.BackColor = Color.Cornsilk;
            txtOperand2.BackColor = Color.White;
        }

        private void txtOperand1_Leave(object sender, EventArgs e)
        {
            gWhichOpHasFocus = (int)OpFocus.OPERAND1;
            //txtOperand1.BackColor = Color.White;
        }

        private void txtOperand2_Enter(object sender, EventArgs e)
        {
            gWhichOpHasFocus = (int)OpFocus.OPERAND2;
            txtOperand2.BackColor = Color.Cornsilk;
            txtOperand1.BackColor = Color.White;
        }

        private void txtOperand2_Leave(object sender, EventArgs e)
        {
            //txtOperand2.BackColor = Color.White;
        }

        private void btnMult_Click(object sender, EventArgs e)
        {
            string s1 = txtOperand1.Text;
            string s2 = txtOperand2.Text;

            if ((s1 != ".") && (s1 != ".")) // if number string is just starting with "." it will break Decimal.Parse
            {
                decimal d1 = Decimal.Parse(s1);

                fpOperations.Standard754FPNumber fpn1 = new fpOperations.Standard754FPNumber((float)d1);
            
                decimal d2 = Decimal.Parse(s2);

                fpOperations.Standard754FPNumber fpn2 = new fpOperations.Standard754FPNumber((float)d2);

                fpOperations.Standard754FPNumber fpn3 = fpn2 * fpn1;

                //txtResult.Text = fpn3.returnDecimalVal().ToString();
                //txtResultHex.Text = fpn3.returnEncodedHexString();
                //txtResultBin.Text = fpn3.returnSignStr32() + "  " + fpn3.returnExponentStr32() + "  " + fpn3.returnMantissaStr32();

                txtResult.Text = "";

            }

        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            string s1 = txtOperand1.Text;
            string s2 = txtOperand2.Text;

            if ((s1 != ".") && (s1 != ".")) // if number string is just starting with "." it will break Decimal.Parse
            {
                decimal d1 = Decimal.Parse(s1);

                fpOperations.Standard754FPNumber fpn1 = new fpOperations.Standard754FPNumber((float)d1);

                decimal d2 = Decimal.Parse(s2);

                fpOperations.Standard754FPNumber fpn2 = new fpOperations.Standard754FPNumber((float)d2);
                fpOperations.Standard754FPNumber fpn3 = fpn1 - fpn2;

                //txtResult.Text = fpn3.returnDecimalVal().ToString();
                //txtResultHex.Text = fpn3.returnEncodedHexString();
                //txtResultBin.Text = fpn3.returnSignStr32() + "  " + fpn3.returnExponentStr32() + "  " + fpn3.returnMantissaStr32();

                txtResult.Text = "";

            }

        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            string s1 = txtOperand1.Text;
            string s2 = txtOperand2.Text;

            if ((s1 != ".") && (s1 != ".")) // if number string is just starting with "." it will break Decimal.Parse
            {
                decimal d1 = Decimal.Parse(s1);

                fpOperations.Standard754FPNumber fpn1 = new fpOperations.Standard754FPNumber((float)d1);
                
                decimal d2 = Decimal.Parse(s2);

                fpOperations.Standard754FPNumber fpn2 = new fpOperations.Standard754FPNumber((float)d2);
                fpOperations.Standard754FPNumber fpn3 = fpn2 + fpn1;

                //txtResult.Text = fpn3.returnDecimalVal().ToString();
                //txtResultHex.Text = fpn3.returnEncodedHexString();
                //txtResultBin.Text = fpn3.returnSignStr32() + "  " + fpn3.returnExponentStr32() + "  " + fpn3.returnMantissaStr32();

                txtResult.Text = "";
            }

        }

        private void btnA_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(gWhichOpHasFocus);
            appendOperand("A");
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            appendOperand("B");
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            appendOperand("C");
        }

        private void btnD_Click(object sender, EventArgs e)
        {
            appendOperand("D");
        }

        private void btnE_Click(object sender, EventArgs e)
        {
            appendOperand("E");
        }

        private void btnF_Click(object sender, EventArgs e)
        {
            appendOperand("F");
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            appendOperand("7");
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            appendOperand("8");
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            appendOperand("9");
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            appendOperand("4");
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            appendOperand("5");
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            appendOperand("6");
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            appendOperand("1");
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            appendOperand("2");
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            appendOperand("3");
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            appendOperand("0");
        }

        private void btn1_Click_1(object sender, EventArgs e)
        {
            appendOperand("1");
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            appendOperand(".");
        }

        private void btnBackOp1_Click(object sender, EventArgs e)
        {
            if (txtOperand1.Text.Length >=1)
                txtOperand1.Text = txtOperand1.Text.Remove(txtOperand1.Text.Length - 1, 1);
        }

        private void btnBackOp2_Click(object sender, EventArgs e)
        {
            if (txtOperand2.Text.Length >= 1)
                txtOperand2.Text = txtOperand2.Text.Remove(txtOperand2.Text.Length - 1, 1);
        }

        private void txtOperand1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // should prevent entry from keyboard
            e.Handled = true;
        }

        private void txtOperand2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // should prevent entry from keyboard
            e.Handled = true;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnNeg_Click(object sender, EventArgs e)
        {
            if (gWhichOpHasFocus == (int)OpFocus.OPERAND1)
            {
                if (txtOperand1.Text.StartsWith("-"))
                    txtOperand1.Text = txtOperand1.Text.Remove(0, 1);
                else
                    txtOperand1.Text = "-" + txtOperand1.Text;
            }
            else if (gWhichOpHasFocus == (int)OpFocus.OPERAND2)
            {
                if (txtOperand2.Text.StartsWith("-"))
                    txtOperand2.Text = txtOperand2.Text.Remove(0, 1);
                else
                    txtOperand2.Text = "-" + txtOperand2.Text;
            }

            txtResult.Text = "";

        }

        private void txtResult_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("cb=" + cbOperation.SelectedIndex);
            string errStr1 = "";
            txtAllResults.Text = "";
            
            string str1 = "";
            string str2 = "";

            // starting with a dec number
            if (rdoDec.Checked)
            {
                str1 = txtOperand1.Text;
                str2 = txtOperand2.Text;
            }

            // starting with a hex humber
            if (rdoHex.Checked)
            {
                Debug.WriteLine("h:");
                Debug.WriteLine(txtOperand1.Text.Length);
                Debug.WriteLine(txtOperand2.Text.Length);
                
                // operand 1 length
                if (txtOperand1.Text.Length != fpOperations.Standard754FPNumber.HEX_LENGTH32)
                {
                    errStr1 += "operand 1 hex input must be " + fpOperations.Standard754FPNumber.HEX_LENGTH32 + " bits, currently " + txtOperand1.Text.Length;
                }
                else
                {
                    float tempFl = fpOperations.Standard754FPNumber.HexStringToFloat(txtOperand1.Text);
                    str1 = tempFl.ToString();
                }

                // operand 2 length
                if (txtOperand2.Text.Length != fpOperations.Standard754FPNumber.HEX_LENGTH32)
                {
                    errStr1 += Environment.NewLine + "operand 2 hex input must be " + fpOperations.Standard754FPNumber.HEX_LENGTH32 + " bits, currently " + txtOperand2.Text.Length; 
                }
                else
                {
                    float tempFl = fpOperations.Standard754FPNumber.HexStringToFloat(txtOperand2.Text);
                    str2 = tempFl.ToString();
                }

                if (errStr1.Length>0)
                { 
                    txtAllResults.Text += errStr1;
                    return;
                }
            }

            // starting with a bin number
            if (rdoBin.Checked)
            {
                Debug.WriteLine("b:");
                Debug.WriteLine(txtOperand1.Text.Length);
                Debug.WriteLine(txtOperand2.Text.Length);
                if (txtOperand1.Text.Length != fpOperations.Standard754FPNumber.NUM_EXPONENT_BITS32 
                    + fpOperations.Standard754FPNumber.NUM_MANTISSA_BITS32 
                    + fpOperations.Standard754FPNumber.NUM_SIGN_BITS32)
                {
                    errStr1 += "operand 1 binary input must be " + fpOperations.Standard754FPNumber.HEX_LENGTH32 + " bits, currently " + txtOperand1.Text.Length;
                }
 

                if (txtOperand2.Text.Length != fpOperations.Standard754FPNumber.NUM_EXPONENT_BITS32
                    + fpOperations.Standard754FPNumber.NUM_MANTISSA_BITS32
                    + fpOperations.Standard754FPNumber.NUM_SIGN_BITS32)
                {
                    errStr1 += Environment.NewLine + "operand 2 binary input must be " + fpOperations.Standard754FPNumber.HEX_LENGTH32 + " bits, currently " + txtOperand2.Text.Length;
                }

                if (errStr1.Length > 0)
                {
                    txtAllResults.Text += errStr1;
                }
            }

            //string s1 = txtOperand1.Text;
            //string s2 = txtOperand2.Text;

            if ((str1 != ".") && (str2 != ".")) // if number string is just starting with "." it will break Decimal.Parse
            {
                //decimal d1 = Decimal.Parse(str1);
                decimal dec1 = Decimal.Parse(str1, NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);
                fpOperations.Standard754FPNumber fpn1 = new fpOperations.Standard754FPNumber((float)dec1);

                //decimal dec2 = Decimal.Parse(str2);
                decimal dec2 = Decimal.Parse(str2, NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);
                fpOperations.Standard754FPNumber fpn2 = new fpOperations.Standard754FPNumber((float)dec2);

                fpOperations.Standard754FPNumber fpn3 = new fpOperations.Standard754FPNumber(fpOperations.Standard754FPNumber.EMPTYNUM);

                txtAllResults.Text += "";

                //fpn3 is initialized to a default
                if (cbOperation.SelectedIndex == 2)  // 2
                {
                    fpn3 = fpn1 + fpn2;

                    txtResult.Text = fpn3.returnDecimalVal().ToString();
                    txtAllResults.Text = fpn1.Dump2("operand 1:") + "\n\n" + fpn2.Dump2("operand 2") + "\n\n" + fpn3.Dump2("result");
                }
                else if (cbOperation.SelectedIndex == 1)  // 1
                {
                    fpn3 = fpn1 - fpn2;

                    txtResult.Text = fpn3.returnDecimalVal().ToString();
                    txtAllResults.Text = fpn1.Dump2("operand 1:") + "\n\n" + fpn2.Dump2("operand 2") + "\n\n" + fpn3.Dump2("result");
                }
                else if (cbOperation.SelectedIndex == 0)  // 0
                {
                    fpn3 = fpn1 * fpn2;

                    txtResult.Text = fpn3.returnDecimalVal().ToString();
                    txtAllResults.Text = fpn1.Dump2("operand 1:") + "\n\n" + fpn2.Dump2("operand 2") + "\n\n" + fpn3.Dump2("result");
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblOperation_Click(object sender, EventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string s1 = txtOperand1.Text;

            if ((s1 != ".") && (s1 != ".")) // if number string is just starting with "." it will break Decimal.Parse
            {
                decimal d1 = Decimal.Parse(s1);
                fpOperations.Standard754FPNumber fpn1 = new fpOperations.Standard754FPNumber((float)d1);
                txtAllResults.Text += "";
                txtAllResults.Text = fpn1.Dump2("viewing operand 1:");                
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
