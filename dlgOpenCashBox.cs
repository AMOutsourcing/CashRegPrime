using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;
using nsBaseClass;
using CashRegPrime.Model;

namespace CashRegPrime
{
    public partial class dlgOpenCashBox : nsBaseClass.clsBaseDialog
    {
        clsCashBox CashBox = new clsCashBox();
        decimal nQty=0;
        public dlgOpenCashBox()
        {
            InitializeComponent();
        }

        private void dlgOpenCashBox_Load(object sender, EventArgs e)
        {
            this.ContextMenuStrip.Items[this.ContextMenuStrip.Items.IndexOfKey("scheduleTaskToolStripMenuItem")].Enabled = false;

            CashBox.load();
            int i = 0;

            while (i < CashBox.CashBoxes.Rows.Count)
            {
                if (CashBox.CashBoxes.Rows[i]["ISOPEN"].ToString() == "0")
                {
                    cmbCashBox.Items.Add(CashBox.CashBoxes.Rows[i]["CASHBOXID"].ToString() + "=" + CashBox.CashBoxes.Rows[i]["NAME"].ToString());
                    
                }
                i++;
            }
            try
            {
                if (cmbCashBox.Items.Count > 0)
                    cmbCashBox.SelectedIndex = 0;
                else
                {
                    MessageBox.Show(objUtil.Localization.getMsgString("CASHREG_13"));//All cashboxes are opened. Please close at least one !
                    this.Close();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cmbCashBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String strCashBoxId = cmbCashBox.Text.Substring(0, cmbCashBox.Text.IndexOf("="));
            
            dfOpeningBalance.Text = string.Format("{0:#,0.00}", CashBox.getPreviousBalance(strCashBoxId,out nQty));
            dfQuantity.Text = string.Format("{0:#,0.00}", nQty);
        }


        private void pbOK_Click(object sender, EventArgs e)
        {
            try
            {
                String strCashBoxId = cmbCashBox.Text.Substring(0, cmbCashBox.Text.IndexOf("="));
                if (strCashBoxId != "")
                {
                    if (Decimal.Parse(dfOpeningBalance.Text.ToString()) == CashBox.getPreviousBalance(strCashBoxId, out nQty))
                    {
                        if (CashBox.openCashBox(strCashBoxId,Decimal.Parse(dfOpeningBalance.Text.ToString()),nQty) == true)
                        {
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                    else
                    {//Balance mismatched: 
                        MessageBox.Show(objUtil.Localization.getMsgString("CASHREG_04") + string.Format("{0:#,0.00}", CashBox.getPreviousBalance(strCashBoxId, out nQty)));
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(objUtil.Localization.getMsgString("CASHREG_03"));//Select a cashbox
               
            }


        }

        private void pbCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dfOpeningBalance_Validating(object sender, CancelEventArgs e)
        {
            Decimal nDecimal = 0;
            if (!Decimal.TryParse(((TextBox)sender).Text.ToString(),out nDecimal))
            {
                MessageBox.Show(objUtil.Localization.getMsgString("CASHREG_01"));//Invalid amount
                e.Cancel = true;
            }
        }

        private void dfOpeningBalance_Validated(object sender, EventArgs e)
        {
            dfOpeningBalance.Text = string.Format("{0:#,0.00}", Decimal.Parse(dfOpeningBalance.Text));
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
