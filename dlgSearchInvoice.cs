using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using nsBaseClass;
using log4net;
using CashRegPrime.Model;

namespace CashRegPrime
{
    public partial class dlgSearchInvoice : nsBaseClass.clsBaseDialog
    {
        static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static int selectedRow = -1;
        List<Invoice> objInvoices;
        public dlgSearchInvoice()
        {
            InitializeComponent();
        }

        private void pbOK_Click(object sender, EventArgs e)
        {
            selectedRow = -1;
            dataGridView1.Rows.Clear();
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.Columns["colOrderNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["colCustNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["colInvoiceNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["colReceiptNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["colInvoiceSum"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["colPaymentSum"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["colInvoiceDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["colPaymentDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns["colPaymentDate"].DefaultCellStyle.
            try
            {
                objInvoices = new List<Invoice>();
                int flagNumber = 0;
                if (chkboxVehichle.Checked == true) flagNumber += InvoiceFlags.VehicleSalesFlag;
                if (chkboxWorkshop.Checked == true) flagNumber += InvoiceFlags.WorkshopFlag;
                if (chkboxSpareparts.Checked == true) flagNumber += InvoiceFlags.SparePartSalesFlag;
                if (chkboxCashreg.Checked == true) flagNumber += InvoiceFlags.CashRegisterFlag;
                objInvoices = Invoice.searchInvoices(txtboxFilter.Text.ToString(), chkboxPaidInv.Checked,chkboxCreditInv.Checked, flagNumber);


                for (int i = 0; i < objInvoices.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells["colType"].Value = objInvoices[i].strType;
                    dataGridView1.Rows[i].Cells["colOrderNo"].Value = objInvoices[i].strOrderNo;
                    dataGridView1.Rows[i].Cells["colCustNo"].Value = objInvoices[i].strCustNo;
                    dataGridView1.Rows[i].Cells["colCustomerName"].Value = objInvoices[i].strCustomerName;
                    dataGridView1.Rows[i].Cells["colStatus"].Value = objInvoices[i].strStatus;
                    dataGridView1.Rows[i].Cells["colInvoiceNo"].Value = objInvoices[i].strInvoiceNo;
                    dataGridView1.Rows[i].Cells["colReceiptNo"].Value = objInvoices[i].ReceiptNo.ToString();
                    if (objInvoices[i].dtInvoiceDate != null)
                        dataGridView1.Rows[i].Cells["colInvoiceDate"].Value = DateTime.Parse(objInvoices[i].dtInvoiceDate.ToString()).ToShortDateString();
                    if (objInvoices[i].dtPaymentDate != null && objInvoices[i].Paid == true)
                        dataGridView1.Rows[i].Cells["colPaymentDate"].Value = DateTime.Parse(objInvoices[i].dtPaymentDate.ToString()).ToShortDateString();
                    dataGridView1.Rows[i].Cells["colInvoiceSum"].Value = String.Format("{0:#,0.00}", objInvoices[i].decInvoiceSumOrig);
                    dataGridView1.Rows[i].Cells["colRemain"].Value = String.Format("{0:#,0.00}", objInvoices[i].decInvoiceSum);
                    if (objInvoices[i].Paid == true)
                        dataGridView1.Rows[i].Cells["colPaymentSum"].Value = String.Format("{0:#,0.00}", objInvoices[i].decPaymentSum);
                    dataGridView1.Rows[i].Cells["colRemark"].Value = objInvoices[i].strRemark;
                }
            }
            catch (Exception ex) { _log.Error(ex.ToString()); throw ex; }
            finally
            {
                dataGridView1.AllowUserToAddRows = false;
            }
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {   //... Not CellContentClick.....-.-
            selectedRow = e.RowIndex;                       
        }

        private void dlgSearchInvoice_Load(object sender, EventArgs e)
        {
            this.ContextMenuStrip.Items[this.ContextMenuStrip.Items.IndexOfKey("scheduleTaskToolStripMenuItem")].Enabled = false;

            chkboxCashreg.Checked = true;
            chkboxSpareparts.Checked = true;
            chkboxVehichle.Checked = true;
            chkboxWorkshop.Checked = true;
            chkboxCreditInv.Checked = false;
            pbOK_Click(this, new EventArgs());
            this.WindowState = FormWindowState.Maximized;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (selectedRow >= 0)
            {
                objInvoices[selectedRow].loadRows();
                dlgCashRegisterPayment dlg = new dlgCashRegisterPayment(true);
                dlg.objInvoice = objInvoices[selectedRow];
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    pbOK_Click(this, new EventArgs());
                }
            }
        }  

        private void pbNewPayment_Click(object sender, EventArgs e)
        {
            Invoice newInv = new Invoice();
            newInv.dtInvoiceDate = DateTime.Now;
            //if (selectedRow >= 0)
            //{
            //    newInv.strCustNo = objInvoices[selectedRow].strCustNo;
            //    newInv.strCustomerName = objInvoices[selectedRow].strCustomerName;
            //    newInv.strInvoiceNo = objInvoices[selectedRow].strInvoiceNo;
            //    newInv.strOrderNo = objInvoices[selectedRow].strOrderNo;
            //}
            newInv.strInvoiceModule = "CU";
            newInv.InvoiceFlag = InvoiceFlags.CashRegisterFlag;
            newInv.strType = "Cash Register";
            dlgCashRegisterPayment dlg = new dlgCashRegisterPayment(false);
            dlg.objInvoice = newInv;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pbOK_Click(this, new EventArgs());
            }
        }

        private void pbCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
