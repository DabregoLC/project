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

//
// rdoHex and rdoBin are actually for the IEEE 754 formats.  These were part of the initial spec before other unencoded hex/bin were added.
// Going back and renaming control objects usually leads to problems, so regular unencoded hex/bin will be names rdoHexUnenc / rdoBinUnenc.  
//

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

        //public const string ADD = "+";
        //public const string SUBTRACT = "-";
        //public const string MULTIPLY = "X";

        public enum OperationIndex
        {
            MULT,
            SUBTR,
            ADD
        }

        public int gWhichOpHasFocus;

        public void appendOperand(string appendStr)
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



                if (appendStr == "." && txtOperand1.Text.Contains("."))  // only allow one "."
                { appendStr = ""; }

                else if ((txtOperand1.Text == "0") && (!rdoBin.Checked))
                {
                    if (appendStr == ".")
                    {
                        appendStr = "0.";
                    }

                    txtOperand1.Text = appendStr;
                }
                else if ((rdoBin.Checked))
                {
                    // have to do this to make it behave like a calculator display when first starting out, remember we start out with '0' in display so if we press '0' we want append
                    // but if we press '1' we want to turn '0' to '1'
                    if ((txtOperand1.Text == "0") && (txtOperand1.Text.Length == 1))
                    {
                        if (appendStr == "0")
                        {
                            txtOperand1.Text += appendStr;
                        }
                        else if (appendStr == "1")
                        {
                            txtOperand1.Text = appendStr;
                        }
                    }
                    else
                    {
                        txtOperand1.Text += appendStr;
                    }
                }
                else
                    txtOperand1.Text += appendStr;

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

                if (appendStr == "." && txtOperand2.Text.Contains("."))
                { appendStr = ""; }

                else if ((txtOperand2.Text == "0") && (!rdoBin.Checked))
                {
                    if (appendStr == ".")
                    {
                        appendStr = "0.";
                    }

                    txtOperand2.Text = appendStr;
                }
                else if ((txtOperand2.Text == "0") && (rdoBin.Checked))
                {
                    // have to do this to make it behave like a calculator display when first starting out, remember we start out with '0' in display so if we press '0' we want append
                    // but if we press '1' we want to turn '0' to '1'
                    if ((txtOperand1.Text == "0") && (txtOperand1.Text.Length == 1))
                    {
                        if (appendStr == "0")
                        {
                            txtOperand2.Text += appendStr;
                        }
                        else if (appendStr == "1")
                        {
                            txtOperand2.Text = appendStr;
                        }
                    }
                    else
                    {
                        txtOperand2.Text += appendStr;
                    }
                }
                else
                    txtOperand2.Text += appendStr;

            }

            //txtResult.Text = "";

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
            txtResult.Text = "0";
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

            btn00.Enabled = false;
            btn01.Enabled = false;
            btn000.Enabled = false;
            btn111.Enabled = false;

            lblOp1Len.Text = "";
            lblOp1Len.Visible = false;
            label6.Visible = false;
            lblOp1Limit.Text = "";
            lblOp1Limit.Visible = false;

            lblOp2Len.Text = "";
            lblOp2Len.Visible = false;
            label7.Visible = false;
            lblOp2Limit.Text = "";
            lblOp2Limit.Visible = false;
        }

        private void rdoHex_CheckedChanged(object sender, EventArgs e)
        {
            // **** this is IEEE 754 ENCODED hex ****

            txtOperand1.Text = "0";
            txtOperand2.Text = "0";
            txtResult.Text = "0";
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
            btnDot.Enabled = false;
            btnBackSpace.Enabled = true;
            btnClear.Enabled = true;

            btn00.Enabled = false;
            btn01.Enabled = false;
            btn000.Enabled = false;
            btn111.Enabled = false;

            lblOp1Len.Text = "";
            lblOp1Len.Visible = true;
            label6.Visible = true;
            lblOp1Limit.Text = "";
            lblOp1Limit.Visible = true;

            lblOp2Len.Text = "";
            lblOp2Len.Visible = true;
            label7.Visible = true;
            lblOp2Limit.Text = "";
            lblOp2Limit.Visible = true;

        }

        private void rdoBin_CheckedChanged(object sender, EventArgs e)
        {
            // **** this is IEEE 754 ENCODED bin ****

            txtOperand1.Text = "0";
            txtOperand2.Text = "0";
            txtResult.Text = "0";
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
            btnDot.Enabled = false;
            btnBackSpace.Enabled = true;
            btnClear.Enabled = true;
            btnNeg.Enabled = false;

            btn00.Enabled = false;
            btn01.Enabled = false;
            btn000.Enabled = false;
            btn111.Enabled = false;

            lblOp1Len.Text = "";
            lblOp1Len.Visible = true;
            label6.Visible = true;
            lblOp1Limit.Text = "";
            lblOp1Limit.Visible = true;

            lblOp2Len.Text = "";
            lblOp2Len.Visible = true;
            label7.Visible = true;
            lblOp2Limit.Text = "";
            lblOp2Limit.Visible = true;
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
            lblOp1Len.Text = "";
            lblOp1Limit.Text = "";
            lblOp2Len.Text = "";
            lblOp2Limit.Text = "";
            txtAllResults.Text = "";
        }

        private void txtOperand1_TextChanged(object sender, EventArgs e)
        {
            string s1 = txtOperand1.Text;
            if (s1 != ".")  // if number string is just starting with "." it will break Decimal.Parse
            {
            }

            if (rdoDec.Checked)
            {

            }
            else if (rdoHex.Checked)
            {
                lblOp1Len.Text = txtOperand1.Text.Length.ToString();
                lblOp1Limit.Text = fpOperations.Standard754FPNumber.HEX_LENGTH32.ToString();
            }
            else if (rdoBin.Checked)
            {
                lblOp1Len.Text = txtOperand1.Text.Length.ToString();
                lblOp1Limit.Text = (fpOperations.Standard754FPNumber.NUM_EXPONENT_BITS32 + fpOperations.Standard754FPNumber.NUM_MANTISSA_BITS32 + fpOperations.Standard754FPNumber.NUM_SIGN_BITS32).ToString();
            }
        }

        private void txtOperand2_TextChanged(object sender, EventArgs e)
        {
            string s1 = txtOperand2.Text;
            if (s1 != ".")  // if number string is just starting with "." it will break Decimal.Parse
            {
            }

            if (rdoDec.Checked)
            {

            }
            else if (rdoHex.Checked)
            {
                lblOp2Len.Text = txtOperand2.Text.Length.ToString();
                lblOp2Limit.Text = fpOperations.Standard754FPNumber.HEX_LENGTH32.ToString();
            }
            else if (rdoBin.Checked)
            {
                lblOp2Len.Text = txtOperand2.Text.Length.ToString();
                lblOp2Limit.Text = (fpOperations.Standard754FPNumber.NUM_EXPONENT_BITS32 + fpOperations.Standard754FPNumber.NUM_MANTISSA_BITS32 + fpOperations.Standard754FPNumber.NUM_SIGN_BITS32).ToString();
            }
        }

        private void txtOperand1_Enter(object sender, EventArgs e)
        {
            txtOperand1.BackColor = Color.Orange;
            txtOperand2.BackColor = Color.White;
        }

        private void txtOperand1_Leave(object sender, EventArgs e)
        {
            gWhichOpHasFocus = (int)OpFocus.OPERAND1;
        }

        private void txtOperand2_Enter(object sender, EventArgs e)
        {
            gWhichOpHasFocus = (int)OpFocus.OPERAND2;
            txtOperand2.BackColor = Color.Orange;
            txtOperand1.BackColor = Color.White;
        }

        private void txtOperand2_Leave(object sender, EventArgs e)
        {
        }

        private void btnMult_Click(object sender, EventArgs e)
        {
            //string s1 = txtOperand1.Text;
            //string s2 = txtOperand2.Text;

            //if ((s1 != ".") && (s1 != ".")) // if number string is just starting with "." it will break Decimal.Parse
            //{
            //    decimal d1 = Decimal.Parse(s1);

            //    fpOperations.Standard754FPNumber fpn1 = new fpOperations.Standard754FPNumber((float)d1);

            //    decimal d2 = Decimal.Parse(s2);

            //    fpOperations.Standard754FPNumber fpn2 = new fpOperations.Standard754FPNumber((float)d2);

            //    fpOperations.Standard754FPNumber fpn3 = fpn2 * fpn1;

            //    txtResult.Text = "";

            //}

        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            //string s1 = txtOperand1.Text;
            //string s2 = txtOperand2.Text;

            //if ((s1 != ".") && (s1 != ".")) // if number string is just starting with "." it will break Decimal.Parse
            //{
            //    decimal d1 = Decimal.Parse(s1);

            //    fpOperations.Standard754FPNumber fpn1 = new fpOperations.Standard754FPNumber((float)d1);

            //    decimal d2 = Decimal.Parse(s2);

            //    fpOperations.Standard754FPNumber fpn2 = new fpOperations.Standard754FPNumber((float)d2);
            //    fpOperations.Standard754FPNumber fpn3 = fpn1 - fpn2;

            //    txtResult.Text = "";

            //}

        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            //string s1 = txtOperand1.Text;
            //string s2 = txtOperand2.Text;

            //if ((s1 != ".") && (s1 != ".")) // if number string is just starting with "." it will break Decimal.Parse
            //{
            //    decimal d1 = Decimal.Parse(s1);

            //    fpOperations.Standard754FPNumber fpn1 = new fpOperations.Standard754FPNumber((float)d1);

            //    decimal d2 = Decimal.Parse(s2);

            //    fpOperations.Standard754FPNumber fpn2 = new fpOperations.Standard754FPNumber((float)d2);
            //    fpOperations.Standard754FPNumber fpn3 = fpn2 + fpn1;

            //    txtResult.Text = "";
            //}

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
            if (txtOperand1.Text.Length >= 1)
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

        // ---------------------------------------------------
        // selected unencoded hex 
        // add/subtract/multiply the unencoded numbers
        // turn result into IEEE 754 and exit
        // ---------------------------------------------------
        private void rdoHexUnencBody()
        {
            // convert unencoded hex string to dec string

            int ival1 = int.Parse(txtOperand1.Text, System.Globalization.NumberStyles.HexNumber);
            int ival2 = int.Parse(txtOperand2.Text, System.Globalization.NumberStyles.HexNumber);
            int ival3;

            fpOperations.Standard754FPNumber fpn1 = new fpOperations.Standard754FPNumber((float)ival1);
            fpOperations.Standard754FPNumber fpn2 = new fpOperations.Standard754FPNumber((float)ival2);

            if (cbOperation.SelectedIndex == (int)OperationIndex.ADD)  //+
            {
                ival3 = ival1 + ival2;
                fpOperations.Standard754FPNumber fpn3 = new fpOperations.Standard754FPNumber((float)ival3);

                txtResult.Text = ival3.ToString("X");
                txtAllResults.Text = fpn1.Dump2("operand 1") + "\n\n" + fpn2.Dump2("operand 2") + "\n\n" + fpn3.Dump2("addition result");
            }
            else if (cbOperation.SelectedIndex == (int)OperationIndex.SUBTR)  // -
            {
                ival3 = ival1 - ival2;
                fpOperations.Standard754FPNumber fpn3 = new fpOperations.Standard754FPNumber((float)ival3);

                txtResult.Text = ival3.ToString("X");
                txtAllResults.Text = fpn1.Dump2("operand 1") + "\n\n" + fpn2.Dump2("operand 2") + "\n\n" + fpn3.Dump2("subtraction result");
            }
            else if (cbOperation.SelectedIndex == (int)OperationIndex.MULT)  // *
            {
                ival3 = ival1 * ival2;
                fpOperations.Standard754FPNumber fpn3 = new fpOperations.Standard754FPNumber((float)ival3);

                txtResult.Text = ival3.ToString("X");
                txtAllResults.Text = fpn1.Dump2("operand 1") + "\n\n" + fpn2.Dump2("operand 2") + "\n\n" + fpn3.Dump2("multiplication result");
            }
        }

        // ---------------------------------------------------
        // selected unencoded bin 
        // add/subtract/multiply the unencoded numbers
        // turn result into IEEE 754 and exit
        // ---------------------------------------------------
        private void rdoBinUnencBody()
        {
            // convert unencoded bin string to dec string
            int ival1 = Convert.ToInt32(txtOperand1.Text, 2);
            int ival2 = Convert.ToInt32(txtOperand2.Text, 2);
            int ival3;

            fpOperations.Standard754FPNumber fpn1 = new fpOperations.Standard754FPNumber((float)ival1);
            fpOperations.Standard754FPNumber fpn2 = new fpOperations.Standard754FPNumber((float)ival2);

            if (cbOperation.SelectedIndex == (int)OperationIndex.ADD)  //+
            {
                ival3 = ival1 + ival2;
                fpOperations.Standard754FPNumber fpn3 = new fpOperations.Standard754FPNumber((float)ival3);

                txtResult.Text = Convert.ToString(ival3, 2);
                txtAllResults.Text = fpn1.Dump2("operand 1") + "\n\n" + fpn2.Dump2("operand 2") + "\n\n" + fpn3.Dump2("addition result");
            }
            else if (cbOperation.SelectedIndex == (int)OperationIndex.SUBTR)  // -
            {
                ival3 = ival1 - ival2;
                fpOperations.Standard754FPNumber fpn3 = new fpOperations.Standard754FPNumber((float)ival3);

                txtResult.Text = Convert.ToString(ival3, 2);
                txtAllResults.Text = fpn1.Dump2("operand 1") + "\n\n" + fpn2.Dump2("operand 2") + "\n\n" + fpn3.Dump2("subtraction result");
            }
            else if (cbOperation.SelectedIndex == (int)OperationIndex.MULT)  // *
            {
                ival3 = ival1 * ival2;
                fpOperations.Standard754FPNumber fpn3 = new fpOperations.Standard754FPNumber((float)ival3);

                txtResult.Text = Convert.ToString(ival3, 2);
                txtAllResults.Text = fpn1.Dump2("operand 1") + "\n\n" + fpn2.Dump2("operand 2") + "\n\n" + fpn3.Dump2("multiplication result");
            }
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            string errMsg1 = "";
            txtAllResults.Text = "";

            string numStr1 = "";
            string numStr2 = "";

            bool isNANtmp = false;
            bool isZerotmp = false;
            bool isDenormal = false;

            // ---------------------------------------------------
            // starting with unencoded hex 
            // ---------------------------------------------------
            if (rdoHexUnenc.Checked)
            {
                rdoHexUnencBody();
                return;
            }

            // ---------------------------------------------------
            // starting with unencoded bin 
            // ---------------------------------------------------
            if (rdoBinUnenc.Checked)
            {
                rdoBinUnencBody();
                return;
            }

            // ---------------------------------------------------
            // Following is the original processing before unenc hex 
            // and bin were added to requirements.
            // ---------------------------------------------------

            // ---------------------------------------------------
            // starting with a dec number 
            // ---------------------------------------------------
            if (rdoDec.Checked)
            {
                numStr1 = txtOperand1.Text;
                numStr2 = txtOperand2.Text;
            }

            // ---------------------------------------------------
            // starting with a IEEE 754 encoded hex humber 
            // ---------------------------------------------------
            if (rdoHex.Checked)
            {
                // check hex operand 1 length
                if (txtOperand1.Text.Length != fpOperations.Standard754FPNumber.HEX_LENGTH32)
                {
                    errMsg1 += "operand 1 hex input must be " + fpOperations.Standard754FPNumber.HEX_LENGTH32 + " bits, currently " + txtOperand1.Text.Length;
                }
                else
                {
                    //string bitString = fpOperations.Standard754FPNumber.HexStringToBinaryString(txtOperand1.Text);

                    string bitString = String.Join(String.Empty,
                                              txtOperand1.Text.Select(
                                                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                                              )
                                            );

                    string signStr = bitString.Substring(0, 1);
                    string expStr = bitString.Substring(1, 8);
                    string mantStr = bitString.Substring(9, 23);

                    fpOperations.Standard754FPNumber fp = new fpOperations.Standard754FPNumber(signStr, expStr, mantStr);

                    if (fp.isNAN == true)
                    {
                        isNANtmp = true;
                        txtAllResults.Text = fp.Dump2("operand 11");
                    }
                    if (fp.isZero == true)
                    {
                        isZerotmp = true;
                        txtAllResults.Text = fp.Dump2("operand 11");
                    }
                    if (fp.isDenormal == true)
                    {
                        isDenormal = true;
                        txtAllResults.Text = fp.Dump2("operand 11");
                    }
                    if (!(fp.isNAN || fp.isZero || fp.isDenormal))
                    {
                        numStr1 = fp.returnFloatVal().ToString();
                    }

                    //float tempFl = fpOperations.Standard754FPNumber.HexStringToFloat(txtOperand1.Text);
                    //numStr1 = tempFl.ToString();
                }

                // check hex operand 2 length
                if (txtOperand2.Text.Length != fpOperations.Standard754FPNumber.HEX_LENGTH32)
                {
                    errMsg1 += Environment.NewLine + "operand 2 hex input must be " + fpOperations.Standard754FPNumber.HEX_LENGTH32 + " bits, currently " + txtOperand2.Text.Length;
                }
                else
                {
                    //string bitString = fpOperations.Standard754FPNumber.HexStringToBinaryString(txtOperand2.Text);

                    string bitString = String.Join(String.Empty,
                          txtOperand2.Text.Select(
                            c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                          )
                        );

                    string signStr = bitString.Substring(0, 1);
                    string expStr = bitString.Substring(1, 8);
                    string mantStr = bitString.Substring(9, 23);

                    fpOperations.Standard754FPNumber fp = new fpOperations.Standard754FPNumber(signStr, expStr, mantStr);

                    if (fp.isNAN == true)
                    {
                        isNANtmp = true;
                        txtAllResults.Text = fp.Dump2("operand 22");
                    }
                    if (fp.isZero == true)
                    {
                        isZerotmp = true;
                        txtAllResults.Text = fp.Dump2("operand 22");
                    }
                    if (fp.isDenormal == true)
                    {
                        isDenormal = true;
                        txtAllResults.Text = fp.Dump2("operand 22");
                    }
                    if (!(fp.isNAN || fp.isZero || fp.isDenormal))
                    {
                        numStr2 = fp.returnFloatVal().ToString();
                    }
                    //float tempFl = fpOperations.Standard754FPNumber.HexStringToFloat(txtOperand2.Text);
                    //numStr2 = tempFl.ToString();
                }

                if (errMsg1.Length > 0)
                {
                    txtAllResults.Text += errMsg1;
                    return;
                }

                if (isNANtmp || isZerotmp || isDenormal)
                    return;
            }

            // ---------------------------------------------------
            // starting with a IEEE 754 encoded bin number 
            // ---------------------------------------------------
            if (rdoBin.Checked)
            {
                // check bin operand 1 length
                if (txtOperand1.Text.Length != fpOperations.Standard754FPNumber.NUM_EXPONENT_BITS32
                    + fpOperations.Standard754FPNumber.NUM_MANTISSA_BITS32
                    + fpOperations.Standard754FPNumber.NUM_SIGN_BITS32)
                {
                    errMsg1 += "operand 1 binary input must be " + fpOperations.Standard754FPNumber.HEX_LENGTH32 + " bits, currently " + txtOperand1.Text.Length;
                }
                else
                {
                    string signStr = txtOperand1.Text.Substring(0, 1);
                    string expStr = txtOperand1.Text.Substring(1, 8);
                    string mantStr = txtOperand1.Text.Substring(9, 23);

                    fpOperations.Standard754FPNumber fp = new fpOperations.Standard754FPNumber(signStr, expStr, mantStr);

                    if (fp.isNAN == true)
                    {
                        isNANtmp = true;
                        txtAllResults.Text = fp.Dump2("operand 11");
                    }
                    if (fp.isZero == true)
                    {
                        isZerotmp = true;
                        txtAllResults.Text = fp.Dump2("operand 11");
                    }
                    if (fp.isDenormal == true)
                    {
                        isDenormal = true;
                        txtAllResults.Text = fp.Dump2("operand 11");
                    }
                    if (!(fp.isNAN || fp.isZero || fp.isDenormal))
                        numStr1 = fp.returnFloatVal().ToString();
                }

                // check bin operand 2 length
                if (txtOperand2.Text.Length != fpOperations.Standard754FPNumber.NUM_EXPONENT_BITS32
                    + fpOperations.Standard754FPNumber.NUM_MANTISSA_BITS32
                    + fpOperations.Standard754FPNumber.NUM_SIGN_BITS32)
                {
                    errMsg1 += Environment.NewLine + "operand 2 binary input must be " + fpOperations.Standard754FPNumber.HEX_LENGTH32 + " bits, currently " + txtOperand2.Text.Length;
                }
                else
                {
                    string signStr = txtOperand2.Text.Substring(0, 1);
                    string expStr = txtOperand2.Text.Substring(1, 8);
                    string mantStr = txtOperand2.Text.Substring(9, 23);

                    fpOperations.Standard754FPNumber fp = new fpOperations.Standard754FPNumber(signStr, expStr, mantStr);

                    if (fp.isNAN == true)
                    {
                        isNANtmp = true;
                        txtAllResults.Text += fp.Dump2("operand 22");
                    }
                    if (fp.isZero == true)
                    {
                        isZerotmp = true;
                        txtAllResults.Text += fp.Dump2("operand 22");
                    }
                    if (fp.isDenormal == true)
                    {
                        isDenormal = true;
                        txtAllResults.Text += fp.Dump2("operand 22");
                    }
                    if (!(fp.isNAN || fp.isZero || fp.isDenormal))
                        numStr2 = fp.returnFloatVal().ToString();
                }

                if (errMsg1.Length > 0)
                {
                    txtAllResults.Text += errMsg1;
                    return;
                }

                if (isNANtmp || isZerotmp || isDenormal)
                    return;
            }

            // ---------------------------------------------------
            // finalize processing operation if not error or NAN
            //
            // this was original IEEE 754 calculation before new requirements
            // this accommodated dec, IEEE bin, IEEE hex, but not unencoded hex and bin
            // ---------------------------------------------------
            if ((numStr1 != ".") && (numStr2 != ".")) // if number string is just starting with "." it will break Decimal.Parse
            {
                decimal dec1 = Decimal.Parse(numStr1, NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);
                fpOperations.Standard754FPNumber fpn1 = new fpOperations.Standard754FPNumber((float)dec1);

                decimal dec2 = Decimal.Parse(numStr2, NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);
                fpOperations.Standard754FPNumber fpn2 = new fpOperations.Standard754FPNumber((float)dec2);

                fpOperations.Standard754FPNumber fpn3 = new fpOperations.Standard754FPNumber(fpOperations.Standard754FPNumber.EMPTYNUM);

                txtAllResults.Text += "";

                string operationStr = cbOperation.SelectedItem.ToString();

                if (cbOperation.SelectedIndex == (int)OperationIndex.ADD)  //+
                {
                    fpn3 = fpn1 + fpn2;
                    txtAllResults.Text = "Addition" + Environment.NewLine;
                }
                else if (cbOperation.SelectedIndex == (int)OperationIndex.SUBTR)  // -
                {
                    fpn3 = fpn1 - fpn2;
                    txtAllResults.Text = "Subtraction" + Environment.NewLine;
                }
                else if (cbOperation.SelectedIndex == (int)OperationIndex.MULT)  // *
                {
                    fpn3 = fpn1 * fpn2;
                    txtAllResults.Text = "Multiplication" + Environment.NewLine;
                }

                if (rdoDec.Checked)
                {
                    txtResult.Text = fpn3.returnDecimalVal().ToString();
                }
                else if (rdoHex.Checked)
                {
                    txtResult.Text = fpn3.returnEncodedHexString().ToString();
                }
                else if (rdoBin.Checked)
                {
                    txtResult.Text = fpn3.returnSignStr32().ToString() +
                                        fpn3.returnExponentStr32().ToString() +
                                        fpn3.returnMantissaStr32().ToString();
                }
                txtAllResults.Text = fpn1.Dump2("operand 1") + Environment.NewLine + fpn2.Dump2("operand 2") + Environment.NewLine + fpn3.Dump2("result"); ;
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
            string errStr1 = "";

            // ---------------------------------------------------
            // view decimal 
            // ---------------------------------------------------
            if (rdoDec.Checked)
            {
                string s1 = txtOperand1.Text;

                if ((s1 != ".") && (s1 != ".")) // if number string is just starting with "." it will break Decimal.Parse
                {
                    decimal d1 = Decimal.Parse(s1);
                    fpOperations.Standard754FPNumber fpn1 = new fpOperations.Standard754FPNumber((float)d1);
                    txtAllResults.Text += "";
                    txtAllResults.Text = fpn1.Dump2("operand 1");
                }
            }

            // ---------------------------------------------------
            // view encoded IEEE 754 hex 
            // ---------------------------------------------------
            else if (rdoHex.Checked)
            {
                // check hex operand 1 length
                if (txtOperand1.Text.Length != fpOperations.Standard754FPNumber.HEX_LENGTH32)
                {
                    errStr1 += "operand 1 hex input must be " + fpOperations.Standard754FPNumber.HEX_LENGTH32 + " bits, currently " + txtOperand1.Text.Length;
                }
                else
                {
                    float tempFl = fpOperations.Standard754FPNumber.HexStringToFloat(txtOperand1.Text);
                    fpOperations.Standard754FPNumber fpn1 = new fpOperations.Standard754FPNumber(tempFl);
                    txtAllResults.Text = fpn1.Dump2("operand 1");
                }

                if (errStr1.Length > 0)
                {
                    txtAllResults.Text += errStr1;
                    return;
                }
            }
            // ---------------------------------------------------
            // view encoded IEEE 754 bin 
            // ---------------------------------------------------
            else if (rdoBin.Checked)
            {
                // check bin operand 1 length
                if (txtOperand1.Text.Length != fpOperations.Standard754FPNumber.NUM_EXPONENT_BITS32
                    + fpOperations.Standard754FPNumber.NUM_MANTISSA_BITS32
                    + fpOperations.Standard754FPNumber.NUM_SIGN_BITS32)
                {
                    errStr1 += "operand 1 binary input must be " + fpOperations.Standard754FPNumber.HEX_LENGTH32 + " bits, currently " + txtOperand1.Text.Length;
                }
                else
                {
                    string signStr = txtOperand1.Text.Substring(0, 1);
                    string expStr = txtOperand1.Text.Substring(1, 8);
                    string mantStr = txtOperand1.Text.Substring(9, 23);

                    fpOperations.Standard754FPNumber fpn1 = new fpOperations.Standard754FPNumber(signStr, expStr, mantStr);
                    txtAllResults.Text = fpn1.Dump2("operand 1");

                }
                if (errStr1.Length > 0)
                {
                    txtAllResults.Text += errStr1;
                    return;
                }
            }

            // ---------------------------------------------------
            // view unencoded hex
            // ---------------------------------------------------
            else if (rdoHexUnenc.Checked)
            {
                int ival1 = int.Parse(txtOperand1.Text, System.Globalization.NumberStyles.HexNumber);
                fpOperations.Standard754FPNumber fpn1 = new fpOperations.Standard754FPNumber((float)ival1);
                txtAllResults.Text = fpn1.Dump2("operand 1");
            }
            // ---------------------------------------------------
            // view unencoded bin 
            // ---------------------------------------------------
            else if (rdoBinUnenc.Checked)
            {
                int ival1 = Convert.ToInt32(txtOperand1.Text, 2);
                fpOperations.Standard754FPNumber fpn1 = new fpOperations.Standard754FPNumber((float)ival1);
                txtAllResults.Text = fpn1.Dump2("operand 1");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rdoHexUnenc_CheckedChanged(object sender, EventArgs e)
        {
            // **** this is UNENCODED hex ****

            txtOperand1.Text = "0";
            txtOperand2.Text = "0";
            txtResult.Text = "0";
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
            btnDot.Enabled = false;
            btnBackSpace.Enabled = true;
            btnClear.Enabled = true;

            btn00.Enabled = false;
            btn01.Enabled = false;
            btn000.Enabled = false;
            btn111.Enabled = false;

            lblOp1Len.Text = "";
            lblOp1Len.Visible = false;
            label6.Visible = false;
            lblOp1Limit.Text = "";
            lblOp1Limit.Visible = false;

            lblOp2Len.Text = "";
            lblOp2Len.Visible = false;
            label7.Visible = false;
            lblOp2Limit.Text = "";
            lblOp2Limit.Visible = false;
        }

        private void rdoBinUnenc_CheckedChanged(object sender, EventArgs e)
        {
            // **** this is UNENCODED bin ****

            txtOperand1.Text = "0";
            txtOperand2.Text = "0";
            txtResult.Text = "0";
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
            btnDot.Enabled = false;
            btnBackSpace.Enabled = true;
            btnClear.Enabled = true;
            btnNeg.Enabled = false;

            btn00.Enabled = false;
            btn01.Enabled = false;
            btn000.Enabled = false;
            btn111.Enabled = false;

            lblOp1Len.Text = "";
            lblOp1Len.Visible = false;
            label6.Visible = false;
            lblOp1Limit.Text = "";
            lblOp1Limit.Visible = false;

            lblOp2Len.Text = "";
            lblOp2Len.Visible = false;
            label7.Visible = false;
            lblOp2Limit.Text = "";
            lblOp2Limit.Visible = false;
        }

        private void rdoTest4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnRunTest_Click(object sender, EventArgs e)
        {
            txtOperand1.Text = "0";
            txtOperand2.Text = "0";
            txtResult.Text = "";
            lblOp1Len.Text = "";
            lblOp1Limit.Text = "";
            lblOp2Len.Text = "";
            lblOp2Limit.Text = "";
            txtAllResults.Text = "";

            if (cbTest1.SelectedIndex == 1)
            {
                // dec
                rdoDec.Checked = true;
                txtOperand1.Text = "100";
                txtOperand2.Text = "20";
            }
            else if (cbTest1.SelectedIndex == 2)
            {
                // hex unenc
                rdoHexUnenc.Checked = true;
                txtOperand1.Text = "3E8";  // 1000
                txtOperand2.Text = "20";   // 14
            }
            else if (cbTest1.SelectedIndex == 3)
            {
                // bin unenc
                rdoBinUnenc.Checked = true;
                txtOperand1.Text = "11111010000"; // 2000
                txtOperand2.Text = "11001000";  // 200
            }
            else if (cbTest1.SelectedIndex == 4)
            {
                // IEEE hex
                rdoHex.Checked = true;
                txtOperand1.Text = "42c80000";  // 100
                txtOperand2.Text = "43480000";  // 200
            }
            else if (cbTest1.SelectedIndex == 5)
            {
                // IEEE 754 bin
                rdoBin.Checked = true;
                txtOperand1.Text = "01000011010010000000000000000000";  // 200
                txtOperand2.Text = "01000011100101100000000000000000";  // 300
            }
            else if (cbTest1.SelectedIndex == 6)
            {
                // dec unenc
                rdoDec.Checked = true;
                txtOperand1.Text = "4000";
                txtOperand2.Text = "2";
            }
            else if (cbTest1.SelectedIndex == 7)
            {
                // hex unenc
                rdoHexUnenc.Checked = true;
                txtOperand1.Text = "37";  //55 
                txtOperand2.Text = "3039"; // 12345
            }
            else if (cbTest1.SelectedIndex == 8)
            {
                // bin unenc
                rdoBinUnenc.Checked = true;
                txtOperand1.Text = "101100";  // 44
                txtOperand2.Text = "1001110001000";  // 5000
            }
            else if (cbTest1.SelectedIndex == 9)
            {
                // IEEE hex
                rdoHex.Checked = true;
                txtOperand1.Text = "4640e400";  // 12345
                txtOperand2.Text = "45d42800";  // 6789
            }
            else if (cbTest1.SelectedIndex == 10)
            {
                // IEEE 754 bin
                rdoBin.Checked = true;
                txtOperand1.Text = "01000110010000001110010000000000";  // 12345
                txtOperand2.Text = "01000101110101000010100000000000";  // 6789
            }

            else if (cbTest1.SelectedIndex == 11)
            {
                // IEEE 0
                rdoBin.Checked = true;
                txtOperand1.Text = "00000000000000000000000000000000";  // 0
                txtOperand2.Text = "00000000000000000000000000000000";  // 0
            }
            else if (cbTest1.SelectedIndex == 12)
            {
                // IEEE 0
                rdoBin.Checked = true;
                txtOperand1.Text = "10000000000000000000000000000000";  // 0
                txtOperand2.Text = "10000000000000000000000000000000";  // 0
            }
            else if (cbTest1.SelectedIndex == 13)
            {
                // IEEE 754 NAN
                rdoBin.Checked = true;
                txtOperand1.Text = "01111111100000000000000000000001";  // NAN
                txtOperand2.Text = "01111111100000000000000000000001";  // NAN
            }
            else if (cbTest1.SelectedIndex == 14)
            {
                // IEEE 754 NAN
                rdoBin.Checked = true;
                txtOperand1.Text = "01111111100000000000000000000001";  // NAN
                txtOperand2.Text = "01111111100000000000000000000010";  // NAN
            }
            else if (cbTest1.SelectedIndex == 15)
            {
                // IEEE 754 Denormal
                rdoBin.Checked = true;
                txtOperand1.Text = "00000000010000000000000000000000"; 
                txtOperand2.Text = "00000000010000000000000000000000"; 
            }
        }

        private void rdoTest1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button25_Click(object sender, EventArgs e)
        {
            //appendOperand("01");
        }

        private void btn00_Click(object sender, EventArgs e)
        {
            //appendOperand("00");
        }

        private void btn111_Click(object sender, EventArgs e)
        {
            //appendOperand("111");
        }

        private void btn000_Click(object sender, EventArgs e)
        {
            //appendOperand("000");
        }

        private void cbTest1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
