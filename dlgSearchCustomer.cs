using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CashRegPrime.Model;

namespace CashRegPrime
{
    public partial class dlgSearchCustomer : nsBaseClass.clsBaseDialog
    {
        public string Custno = "";
        public string CustName = "";
        public string CustAddress = "";
        public string CustEmail = "";
        public string CustPhone = "";
        public dlgSearchCustomer()
        {
            InitializeComponent();
        }

        private void gridCustomer_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void gridCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRow = e.RowIndex;
            if (selectedRow >= 0)
            {
                Custno = gridCustomer.Rows[selectedRow].Cells["colCustNo"].Value.ToString();
                CustName = gridCustomer.Rows[selectedRow].Cells["colLName"].Value.ToString() + " " + gridCustomer.Rows[selectedRow].Cells["colFName"].Value.ToString();
                CustAddress = gridCustomer.Rows[selectedRow].Cells["colAddress"].Value.ToString();
                CustEmail = gridCustomer.Rows[selectedRow].Cells["colEmail"].Value.ToString();
                CustPhone = gridCustomer.Rows[selectedRow].Cells["colTel"].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void pbSearchCust_Click(object sender, EventArgs e)
        {
            CashRegCust cashregCust = new CashRegCust();
            DataTable dataTable = CashRegCust.searchCustomers(dfCustomerFilter.Text);
            gridCustomer.DataSource = dataTable;
        }

        private void dlgSearchCustomer_Load(object sender, EventArgs e)
        {
            this.ContextMenuStrip.Items[this.ContextMenuStrip.Items.IndexOfKey("scheduleTaskToolStripMenuItem")].Visible = false;
        }
    }
}
