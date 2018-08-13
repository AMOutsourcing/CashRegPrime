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
using System.Globalization;

namespace CashRegPrime
{
    public partial class dlgCloseCashBox : nsBaseClass.clsBaseDialog
    {
        static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        clsCashBox CashBox = new clsCashBox();
        System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
        Decimal nTotal = 0;
        public dlgCloseCashBox()
        {
            InitializeComponent();
        }

        private void dlgCloseCashBox_Load(object sender, EventArgs e)
        {
            this.ContextMenuStrip.Items[this.ContextMenuStrip.Items.IndexOfKey("scheduleTaskToolStripMenuItem")].Enabled = false;

            CashBox.load();
            gridDenomination.Columns["colCount"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridDenomination.Columns["colFaceValue"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridDenomination.Columns["colTotal"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dfTotal.Text = "0";
            this.dfTotalCoins.Text = "0";
            this.dfTotalNotes.Text = "0";
            gridDenomination.Columns["colCount"].ReadOnly = false;
            disableColumn(this.gridDenomination.Columns["colCaption"]);
            disableColumn(this.gridDenomination.Columns["colFaceValue"]);
            disableColumn(this.gridDenomination.Columns["colTotal"]);
            int i = 0;

            while (i < CashBox.CashBoxes.Rows.Count)
            {
                _log.Debug("CashBoxId = " + CashBox.CashBoxes.Rows[i]["CASHBOXID"].ToString() + ",IsOpen = " + CashBox.CashBoxes.Rows[i]["ISOPEN"].ToString() + ", Virtual = " + CashBox.CashBoxes.Rows[i]["VIRTUAL"].ToString());
                if 
                (
                    (CashBox.CashBoxes.Rows[i]["ISOPEN"].ToString() == "1") && 
                    (
                        objGlobal.ExtendedRights || CashBox.CashBoxes.Rows[i]["PC"].ToString() == CashBox.getClientPCName() || CashBox.CashBoxes.Rows[i]["VIRTUAL"].ToString() =="1"
                    )
                )
                {
                    clsBaseListItem objListItem = new clsBaseListItem();
                    objListItem.strText = CashBox.CashBoxes.Rows[i]["CASHBOXID"].ToString() + "=" + CashBox.CashBoxes.Rows[i]["NAME"].ToString() + "/" + CashBox.CashBoxes.Rows[i]["PC"].ToString();
                    objListItem.strValue1 = CashBox.CashBoxes.Rows[i]["CASHBOXID"].ToString();
                    objListItem.nValue1 = Int32.Parse(CashBox.CashBoxes.Rows[i]["NEED_DENOMINATION"].ToString());
                    cmbCashBox.Items.Add(objListItem);
                }
                i++;
            }
            try {
                
                //cmbCashBox_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            { }
        }

        private void pbOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCashBox.Text != "")
                {
                    String strCashBoxId = cmbCashBox.Text.Substring(0, cmbCashBox.Text.IndexOf("="));
                    if (strCashBoxId != "")
                    {

                        if (CashBox.closeCashBox(strCashBoxId) == true)
                        {
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                }
                else
                    MessageBox.Show(objUtil.Localization.getMsgString("CASHREG_03"));//Select a cashbox!
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void disableColumn(DataGridViewColumn c)
        {
            //toggle read-only state
            c.DefaultCellStyle.BackColor = Color.LightGray;
            c.DefaultCellStyle.ForeColor = Color.Black;
            c.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            c.DefaultCellStyle.SelectionForeColor = Color.Black;
        }
        private void cmbCashBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clsBaseListItem selectedListItem = (clsBaseListItem)cmbCashBox.SelectedItem;
                String strCashBoxId = selectedListItem.strValue1;//cmbCashBox.Text.Substring(0, cmbCashBox.Text.IndexOf("="));
                
                if (strCashBoxId != "")
                {
                    int CashBoxInd = CashBox.getIndexByCashBoxId(strCashBoxId);
                    String strCurCd = CashBox.CashBoxes.Rows[CashBoxInd]["CURCD"].ToString();
                    decimal nClosingQty = 0;
                    dfClosingBalance.Text = string.Format("{0:#,0.00}", CashBox.getClosingBalance(strCashBoxId, out nClosingQty));
                    dfClosingQty.Text = string.Format("{0:#,0.00}", nClosingQty);
                    this.gridDenomination.Rows.Clear();
                    //Fill denomination
                    if (selectedListItem.nValue1 == 1) //Need denomination
                    {
                        gridDenomination.Enabled = true;
                        clsSqlFactory hSql = new clsSqlFactory();
                        hSql.NewCommand("select V1,C2,C4 from " + objUtil.getTable("CORW") + " a where a.CODAID ='CASHDENOM' and  a.C3 = ? ");
                        hSql.Com.Parameters.AddWithValue("CURCD", strCurCd);
                        hSql.ExecuteReader();
                        int i = 0;
                        while (hSql.Read())
                        {
                            gridDenomination.Rows.Add();
                            gridDenomination.Rows[i].Cells["colIsCoin"].Value = hSql.Reader.GetInt32(0);
                            gridDenomination.Rows[i].Cells["colCaption"].Value = hSql.Reader.GetString(1);
                            gridDenomination.Rows[i].Cells["colFaceValue"].Value = hSql.Reader.GetString(2);
                            gridDenomination.Rows[i].Cells["colCount"].Value = string.Format("{0:#,0}", Int32.Parse("0"));
                            gridDenomination.Rows[i].Cells["colTotal"].Value = string.Format("{0:#,0.00}", Decimal.Parse("0"));

                            i++;
                        }
                        gridDenomination.CurrentCell = gridDenomination.Rows[0].Cells["colCount"];
                        gridDenomination.BeginEdit(true);
                    }
                    else
                        gridDenomination.Enabled = false;
                    //En_disable OK button
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(objUtil.Localization.getMsgString("CASHREG_03"));//Select Cashbox !

            }
            finally
            {
                disEnableButtons();
            }
        }
        private void disEnableButtons()
        {
            bool isOk = true;
            if (cmbCashBox.SelectedIndex < 0) 
            {
                isOk = false;
            } else
            {
                //if (CashBox.CashBoxes.Rows[cmbCashBox.SelectedIndex]["NEED_DENOMINATION"].ToString() == "1")
                //{
                //   if (Decimal.Parse(this.dfClosingBalance.Text.ToString()) != nTotal) isOk = false;
                //}
                clsBaseListItem selectedListItem = (clsBaseListItem)cmbCashBox.SelectedItem;
                if (selectedListItem.nValue1 == 1)
                {
                    if (Decimal.Parse(this.dfClosingBalance.Text.ToString()) != nTotal) isOk = false;
                }
            }
            if (isOk) this.pbOK.Enabled = true; else this.pbOK.Enabled = false;
        }
        private void pbCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }        

        private void gridDenomination_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == this.gridDenomination.Columns["colCount"].Index))
            {
                IFormatProvider provider = CultureInfo.CreateSpecificCulture(objGlobal.CultureInfo);

                String strCount = "";
                Int32 nCount = 0;
                Decimal nRowValue =0;
                Decimal nRowTotal =0;

                Decimal nCoinsTotal =0;
                Decimal nNotesTotal =0;
                //Use en-US format to convert decimal number in control data
                System.Globalization.CultureInfo usCultureInfo = new System.Globalization.CultureInfo("en-US");
                usCultureInfo.NumberFormat.NumberDecimalSeparator =".";
                //
                if (gridDenomination.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) gridDenomination.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                else
                {
                    strCount = this.gridDenomination.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    bool isValidAmount = true;
                    if (!Int32.TryParse(strCount,NumberStyles.Integer| NumberStyles.AllowThousands, provider,out nCount) ) 
                    {
                        isValidAmount = false;                    
                    }
                    isValidAmount = isValidAmount && (nCount >= 0);
                    if (!isValidAmount)
                    {
                        MessageBox.Show(objUtil.Localization.getMsgString("CASHREG_01"));//Invalid amount !
                        nCount = 0;
                    }
                }
                Decimal.TryParse(gridDenomination.Rows[e.RowIndex].Cells["colFaceValue"].Value.ToString(), NumberStyles.Number, usCultureInfo, out nRowValue);

                nRowTotal = nCount * nRowValue;
                gridDenomination.Rows[e.RowIndex].Cells["colCount"].Value = string.Format("{0:#,0}", nCount);
                gridDenomination.Rows[e.RowIndex].Cells["colTotal"].Value = string.Format("{0:#,0.00}", nRowTotal);

                //
                foreach (DataGridViewRow r in gridDenomination.Rows)
                {
                    strCount = r.Cells["colCount"].Value.ToString();
                    Int32.TryParse(strCount, NumberStyles.Integer | NumberStyles.AllowThousands, provider, out nCount);
                    Decimal.TryParse(r.Cells["colFaceValue"].Value.ToString(), NumberStyles.Number, usCultureInfo, out nRowValue);
                    //Recalculate the sub totals and total
                    nRowTotal = nCount * nRowValue;

                    if (r.Cells["colIsCoin"].Value.ToString() == "1")
                    {
                        nCoinsTotal = nCoinsTotal + nRowTotal;
                    }
                    else
                    {
                        nNotesTotal = nNotesTotal + nRowTotal;
                    }
                }
                //
                nTotal = nCoinsTotal+nNotesTotal;
                this.dfTotalCoins.Text = string.Format("{0:#,0.00}",nCoinsTotal);
                this.dfTotalNotes.Text = string.Format("{0:#,0.00}", nNotesTotal);
                this.dfTotal.Text = string.Format("{0:#,0.00}", nTotal);

                disEnableButtons();
            }
        }

        private void gridDenomination_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dfTotalNotes_TextChanged(object sender, EventArgs e)
        {

        }

        private void dfTotalCoins_TextChanged(object sender, EventArgs e)
        {

        }

        private void dfTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }                       
    }
}
