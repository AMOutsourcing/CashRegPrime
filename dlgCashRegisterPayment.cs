using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Configuration;
using nsBaseClass;
using log4net;
using CashRegPrime.Model;
using System.IO;
using System.Data.Odbc;
using ReportExecution2005;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
namespace CashRegPrime
{
    public partial class dlgCashRegisterPayment : nsBaseClass.clsBaseDialog
    {
        public Invoice objInvoice = new Invoice() ;
        clsCashBox CashBox = new clsCashBox();
        decimal decAmountForCashType = 0;
        clsCurrExchange objCurr = new clsCurrExchange();
        int selectedRow = -1;
        bool isAmInvoice = true;
        static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string strFiscalFileCodePage = ConfigurationManager.AppSettings["WinCodepage"];
        string strRoundingCashTypePositive = ConfigurationManager.AppSettings["RoundingCashTypePositive"];
        string strRoundingCashTypeNegative = ConfigurationManager.AppSettings["RoundingCashTypeNegative"];
        public dlgCashRegisterPayment(bool pIsAmInvoice)
        {
            InitializeComponent();
            isAmInvoice = pIsAmInvoice;
        }

        private void fillCashFormCOmbobox()
        {
            int i = 0;
            cmbPaymentForm.Items.Clear();
            cmbPaymentType.Items.Clear();
            cmbCashBox.Items.Clear();
            List<string> addedList = new List<string>();
            
            while (i < CashBox.CashTypes.Rows.Count)
            {
                if (addedList.IndexOf(CashBox.CashTypes.Rows[i]["FORM"].ToString())<0)               
                {
                    clsBaseListItem objItem = new clsBaseListItem();
                    objItem.strText = CashBox.CashTypes.Rows[i]["FORM"].ToString() + "=" + CashBox.CashTypes.Rows[i]["FORMNAME"].ToString();
                    objItem.strValue1 = CashBox.CashTypes.Rows[i]["FORM"].ToString();
                    cmbPaymentForm.Items.Add(objItem);
                    addedList.Add(CashBox.CashTypes.Rows[i]["FORM"].ToString());
                }
                i++;
            }
            if (cmbPaymentForm.Items.Count>0)
                cmbPaymentForm.SelectedIndex = 0;
            
        }
        private void fillCashBoxCombobox()
        {
            int i = 0;
            cmbCashBox.Items.Clear();
            string availableCashBoxes = CashBox.getCashBoxesOfType(cmbPaymentType.Text.Substring(0, cmbPaymentType.Text.IndexOf("=")));
            while (i < CashBox.CashBoxes.Rows.Count)
            {
                if ((availableCashBoxes.IndexOf(CashBox.CashBoxes.Rows[i]["CASHBOXID"].ToString())>=0) &&
                    (CashBox.CashBoxes.Rows[i]["PC"].ToString() == CashBox.getClientPCName() || CashBox.CashBoxes.Rows[i]["VIRTUAL"].ToString() == "1") &&
                   CashBox.CashBoxes.Rows[i]["ISOPEN"].ToString()=="1")
                {
                    clsBaseListItem objCashBoxItem = new clsBaseListItem();
                    objCashBoxItem.strText = CashBox.CashBoxes.Rows[i]["CASHBOXID"].ToString() + "=" + CashBox.CashBoxes.Rows[i]["NAME"].ToString();
                    objCashBoxItem.strValue1 = CashBox.CashBoxes.Rows[i]["CASHBOXID"].ToString();
                    cmbCashBox.Items.Add(objCashBoxItem);
                    //cmbCashBox.Items.Add(CashBox.CashBoxes.Rows[i]["CASHBOXID"].ToString() + "=" + CashBox.CashBoxes.Rows[i]["NAME"].ToString());
                }
                i++;
            }
            try
            {
                cmbCashBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(objUtil.Localization.getMsgString("CASHREG_02"));//Cash box is closed or not defined for this payment type!!
                this.textBoxAmount.Text = "0";
            }
            finally
            {
                disEnableButtons();
                cmbCashBox_SelectedIndexChanged(null, null);
            }
        }
        private void fillCashTypeCombobox()
        {
            _log.Debug("fillCashType >>");
            int i = 0;
            cmbPaymentType.Items.Clear();
            cmbCashBox.Items.Clear();
            if (cmbPaymentForm.Text != "")
            {
                string selectedForm = cmbPaymentForm.Text.Substring(0, cmbPaymentForm.Text.IndexOf("="));
                while (i < CashBox.CashTypes.Rows.Count)
                {
                    if (CashBox.CashTypes.Rows[i]["FORM"].ToString() == selectedForm)
                    {
                        //Load all cashtypes except Rounding type
                        if ((CashBox.CashTypes.Rows[i]["IS_AMINVOICE"].ToString() == "2" ||(isAmInvoice && CashBox.CashTypes.Rows[i]["IS_AMINVOICE"].ToString() == "1") || (!isAmInvoice && CashBox.CashTypes.Rows[i]["IS_AMINVOICE"].ToString() == "0")) &&
                            //(isAmInvoice || !isAmInvoice && (CashBox.CashTypes.Rows[i]["DEPOSITWITHDRAW"].ToString() == "0" || (this.rbDepositToCashBox.Checked && CashBox.CashTypes.Rows[i]["DEPOSITWITHDRAW"].ToString() == "1") || (this.rbWithdrawFromCashBox.Checked && CashBox.CashTypes.Rows[i]["DEPOSITWITHDRAW"].ToString() != "1")))
                            ((CashBox.CashTypes.Rows[i]["DEPOSITWITHDRAW"].ToString() == "0" || (this.rbDepositToCashBox.Checked && CashBox.CashTypes.Rows[i]["DEPOSITWITHDRAW"].ToString() == "1") || (this.rbWithdrawFromCashBox.Checked && CashBox.CashTypes.Rows[i]["DEPOSITWITHDRAW"].ToString() != "1")))
                            )                        
                        {
                            clsBaseListItem objItem = new clsBaseListItem();
                            objItem.strText = CashBox.CashTypes.Rows[i]["TYPE"].ToString() + "=" + CashBox.CashTypes.Rows[i]["TYPENAME"].ToString();
                            objItem.strValue1 = CashBox.CashTypes.Rows[i]["TYPE"].ToString();
                            cmbPaymentType.Items.Add(objItem);
                        }
                    }
                    i++;
                }
            }
            if (cmbPaymentType.Items.Count>0)
                cmbPaymentType.SelectedIndex = 0;
            _log.Debug("fillCashType << ");
            
        }
        private void fillCardType()
        {
            _log.Debug("fillCardType >>");
            int i = 0;
            cmbCardTypes.Items.Clear();
            if (cmbPaymentForm.Text != "")
            {
                string selectedForm = cmbPaymentForm.Text.Substring(0, cmbPaymentForm.Text.IndexOf("="));
                if (selectedForm == "CREDITCARD")
                {
                    while (i < CashBox.CardTypes.Rows.Count)
                    {
                        clsBaseListItem objItem = new clsBaseListItem();
                        objItem.strText = CashBox.CardTypes.Rows[i]["TYPEID"].ToString() + "=" + CashBox.CardTypes.Rows[i]["NAME"].ToString();
                        objItem.strValue1 = CashBox.CardTypes.Rows[i]["TYPEID"].ToString();
                        this.cmbCardTypes.Items.Add(objItem);
                        i++;
                    }
                    clsBaseListItem objEmptyItem = new clsBaseListItem();
                    objEmptyItem.strText = "" + "=" + "";
                    objEmptyItem.strValue1 = "";
                    this.cmbCardTypes.Items.Add(objEmptyItem);
                }
                
            }
            if (cmbCardTypes.Items.Count > 0)
                cmbCardTypes.SelectedIndex = 0;
            _log.Debug("fillCardType << ");

        }
        private void dlgCashRegisterPayment_Load(object sender, EventArgs e)
        {
            _log.Debug("dlgCashRegisterPayment_Load >>");
            try
            {
                this.ContextMenuStrip.Items[this.ContextMenuStrip.Items.IndexOfKey("scheduleTaskToolStripMenuItem")].Enabled = false;
                
                gridPayment.AllowUserToAddRows = false;
                textBoxFee.Visible = false;
                this.lbFee.Visible = false;
                objCurr.Init();
                CashBox.load();
                CashBox.loadCashType();
                CashBox.loadCardType();
                //if (objInvoice.Paid == false)
                //    fillCashFormCOmbobox();

                textBoxType.Text = objInvoice.strType;
                textBoxOrderNo.Text = objInvoice.strOrderNo;
                textBoxInvoiceNo.Text = objInvoice.strInvoiceNo;
                textBoxAmountToPay.Text = formatDecimal(Math.Abs(objInvoice.decInvoiceSumOrig));
                textBoxCustNo.Text = objInvoice.strCustNo;
                textBoxCustName.Text = objInvoice.strCustomerName;
                textBoxInvoiceDate.Text = objInvoice.dtInvoiceDate.ToShortDateString();//("yyyy.MM.dd");
                textBoxReceiptNo.Text = objInvoice.ReceiptNo.ToString();
                textBoxLicNo.Text = objInvoice.strLicno;
                richTextBoxRemark.Text = objInvoice.strRemark;
                if (objInvoice.Paid == false)
                {
                    rbDepositToCashBox.Checked = true;
                    rbWithdrawFromCashBox.Checked = false;

                    if (objInvoice.InvoiceFlag != InvoiceFlags.CashRegisterFlag)
                    {
                        textBoxOrderNo.Enabled = false;
                        textBoxInvoiceNo.Enabled = false;
                        //textBoxInvoiceDate.Enabled = false;
                        textBoxAmountToPay.Enabled = false;
                        if (objInvoice.decInvoiceSumOrig < 0)
                        {
                            rbDepositToCashBox.Checked = false;
                            rbWithdrawFromCashBox.Checked = true;
                        }
                        textBoxCustNo.Enabled = false;
                        pbSearchCustomer.Enabled = false;
                        textBoxCustName.Enabled = false;
                        textBoxCustName.Enabled = false;
                        rbDepositToCashBox.Enabled = false;
                        rbWithdrawFromCashBox.Enabled = false;
                        textBoxLicNo.Enabled = false;
                    }
                    if (isAmInvoice) textBoxInvoiceDate.Enabled = false;
                    fillCashFormCOmbobox();

                }
                else
                {
                    textBoxOrderNo.Enabled = false;
                    textBoxInvoiceNo.Enabled = false;
                    textBoxInvoiceDate.Enabled = false;
                    textBoxLicNo.Enabled = false;
                    textBoxCustNo.Enabled = false;
                    pbSearchCustomer.Enabled = false;
                    richTextBoxRemark.Enabled = false;
                    textBoxAmountToPay.Enabled = false;
                    textBoxAmount.Enabled = false;
                    textBoxQuantity.Enabled = false;
                    cmbPaymentForm.Enabled = false;
                    cmbPaymentType.Enabled = false;
                    cmbCashBox.Enabled = false;
                    rbDepositToCashBox.Enabled = false;
                    rbWithdrawFromCashBox.Enabled = false;
                    if (objInvoice.getRowsTotal() >= 0)
                    {
                        rbDepositToCashBox.Checked = true;
                        rbWithdrawFromCashBox.Checked = false;
                    }

                    else
                    {
                        rbDepositToCashBox.Checked = false;
                        rbWithdrawFromCashBox.Checked = true;
                    }
                    textBoxFee.Enabled = false;
                    
                    displayRows();
                }
                //CashBox.loadCashType();
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                throw ex;
            }
            finally
            {
                disEnableButtons();

            }
        }

        private void cmbPaymentForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            _log.Debug("cmbPaymentForm_SelectedIndexChanged >> ");
            try
            {
                string selectedForm = cmbPaymentForm.Text.Substring(0, cmbPaymentForm.Text.IndexOf("="));
                this.textBoxFee.Text = "0";
                if ((selectedForm == "CASH") || (selectedForm == "CREDITCARD"))
                {
                    textBoxQuantity.Enabled = false;
                    textBoxAmount.Enabled = true;
                    textBoxQuantity.Text = "0";                    
                }
                else
                {
                    textBoxQuantity.Enabled = true;
                    textBoxAmount.Enabled = false;
                    textBoxAmount.Text = "0";
                }
                if (selectedForm == "CREDITCARD")
                {
                    //Show card types combobox
                    this.lbCardTypes.Enabled = true;
                    this.cmbCardTypes.Enabled = true;
                }
                else
                {
                    this.lbCardTypes.Enabled = false;
                    this.cmbCardTypes.Enabled = false;
                }
                fillCashTypeCombobox();
                fillCardType();
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                throw ex;
            }
            finally
            {
                disEnableButtons();
            }
            _log.Debug("cmbPaymentForm_SelectedIndexChanged << ");
        }

        private void cmbPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _log.Debug("cmbPaymentType_SelectedIndexChanged >> ");
            try
            {
                fillCashBoxCombobox();
                getAmountForCashType();
                disEnableButtons();
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                throw ex;
            }
            _log.Debug("cmbPaymentType_SelectedIndexChanged << ");
        }

        private void getAmountForCashType()
        {
            _log.Debug("getAmountForCashType");
            if (cmbPaymentType.Text!="")
            {
                _log.Debug("objInvoice.decInvoiceSum=" + objInvoice.decInvoiceSum.ToString());
                _log.Debug("objInvoice.getRowsTotal()=" + objInvoice.getRowsTotal().ToString());
                decimal nAmountToPay = ((decimal)Math.Abs(objInvoice.decInvoiceSum) - (decimal)Math.Abs(objInvoice.getRowsTotal()));
                
                //decAmountForCashType = CashBox.roundToInt(nAmountToPay, cmbPaymentType.Text.Substring(0, cmbPaymentType.Text.IndexOf("=")));
                decAmountForCashType = nAmountToPay;
                _log.Debug("nAmountToPay=" + nAmountToPay.ToString());
                _log.Debug("decAmountForCashType=" + decAmountForCashType.ToString());
                textBoxAmount.Text = formatDecimal(Math.Abs(decAmountForCashType));
                if (cmbPaymentForm.Text.Substring(0, cmbPaymentForm.Text.IndexOf("=")) == "CURRENCY")
                {
                    decimal nCurr = Decimal.Round((Decimal)objCurr.Curr_ExchangeSumToRateFind(decAmountForCashType,
                            cmbPaymentType.Text.Substring(0, cmbPaymentType.Text.IndexOf("=")), DateTime.Now, true, true),2,MidpointRounding.AwayFromZero);
                    textBoxQuantity.Text = formatDecimal(nCurr);
                    decimal nAmount = (Decimal)objCurr.Curr_ExchangeSumToRateFind(nCurr,
                           cmbPaymentType.Text.Substring(0, cmbPaymentType.Text.IndexOf("=")), DateTime.Now, true, false);
                    textBoxAmount.Text = formatDecimal(Math.Abs(nAmount));                    
                }                
            }
            else
                textBoxAmount.Text = "0";

        }
        private void textBoxAmount_Validated(object sender, EventArgs e)
        {
            try
            {
                decimal nAmount = Decimal.Parse(textBoxAmount.Text);
                nAmount = CashBox.roundToLocalCurrency(nAmount, cmbPaymentType.Text.Substring(0, cmbPaymentType.Text.IndexOf("=")));
                textBoxAmount.Text = formatDecimal(nAmount);
            }
            catch (Exception ex)
            {
                textBoxAmount.Text = "0";
            }
            finally
            {
                disEnableButtons();
            }
        }

        private void butAddRow_Click(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(textBoxAmount.Text) > 0)
                {
                    addPaymentRow();
                    getAmountForCashType();
                    displayRows();
                    //Not allow to change Radio selection if has rows
                    rbDepositToCashBox.Enabled = false;
                    rbWithdrawFromCashBox.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                throw ex;
            }
        }

        private void addPaymentRow()
        {
            //try
            _log.Debug("addPaymentRow >> ");
            if (cmbPaymentForm.Text != "" && cmbPaymentType.Text != "" && cmbCashBox.Text !="")
            {
                InvoiceRow aRow = new InvoiceRow();
                aRow.PaymentForm.Code = ((clsBaseListItem)cmbPaymentForm.SelectedItem).strValue1;
                aRow.PaymentForm.Name = ((clsBaseListItem)cmbPaymentForm.SelectedItem).strText;
                if (aRow.PaymentForm.Code == "CREDITCARD")
                {
                    aRow.CardType.Code = ((clsBaseListItem)cmbCardTypes.SelectedItem).strValue1;
                    aRow.CardType.Name = ((clsBaseListItem)cmbCardTypes.SelectedItem).strText;
                }
                aRow.PaymentType.Code = ((clsBaseListItem)cmbPaymentType.SelectedItem).strValue1;
                aRow.PaymentType.Name = ((clsBaseListItem)cmbPaymentType.SelectedItem).strText;
                aRow.CashBoxId = cmbCashBox.Text.Substring(0, cmbCashBox.Text.IndexOf("="));
                decimal nAmount = decimal.Parse(textBoxAmount.Text);

                decimal AmountPaid = nAmount;
                decimal AmountChange = 0;
                if (nAmount > decAmountForCashType)
                {
                    if (aRow.PaymentForm.Code == "CASH")
                    {
                        AmountChange = nAmount - decAmountForCashType;
                        nAmount = decAmountForCashType;
                    }
                    else if (aRow.PaymentForm.Code == "CURRENCY" ||aRow.PaymentForm.Code == "CREDITCARD")
                    {
                        decimal nChange = CashBox.roundToLocalCurrency(nAmount - decAmountForCashType, ConfigurationSettings.AppSettings["DefaultCashType"]);
                        _log.Debug("nAmount = " + nAmount.ToString());
                        _log.Debug("decAmountForCashType = " + decAmountForCashType.ToString());
                        _log.Debug("nChange = " + nChange.ToString());

                        objInvoice.addReturnRow(-nChange,
                            ConfigurationSettings.AppSettings["DefaultCashType"],
                            CashBox.getDefaultOpenCashBoxOfType(ConfigurationSettings.AppSettings["DefaultCashType"]),
                            CashBox.getSaleTypeOfType(ConfigurationSettings.AppSettings["DefaultCashType"]));
                    }
                    else
                    {
                        nAmount = decAmountForCashType;
                        AmountPaid = decAmountForCashType;
                    }
                }
                if (nAmount > 0)
                {
                    aRow.Amount = nAmount;
                    aRow.AmountPaid = AmountPaid;
                    aRow.AmountChange = AmountChange;
                    aRow.Quantity = decimal.Parse(textBoxQuantity.Text);
                    aRow.SaleType = CashBox.getSaleTypeOfType(aRow.PaymentType.Code);
                    aRow.AmountFee = decimal.Parse(this.textBoxFee.Text);                    
                    objInvoice.Rows.Add(aRow);
                    //
                    if (aRow.PaymentForm.Code == "CASH")
                    {                        
                        //addRoundingPaymentRow(ref aRow);  
                        objInvoice.addRoundingRow(CashBox,((clsBaseListItem)cmbPaymentForm.SelectedItem).strText,getDescriptionOfCashtypeByCode(CashBox.CashTypes, strRoundingCashTypeNegative)
                            ,getDescriptionOfCashtypeByCode(CashBox.CashTypes, strRoundingCashTypePositive),cmbCashBox.Text.Substring(0, cmbCashBox.Text.IndexOf("=")));
                    }   
                }
            }
            _log.Debug("addPaymentRow << ");
        }
        
        private string getDescriptionOfCashtypeByCode(DataTable cashTypes, string strCode)
        {
            string strRetVal = "";

            for (int i = 0; i < cashTypes.Rows.Count; i++)
            {
                _log.Debug(cashTypes.Rows[i]["TYPE"].ToString());
                if (cashTypes.Rows[i]["TYPE"].ToString() == strCode)
                {
                    strRetVal = cashTypes.Rows[i]["TYPENAME"].ToString();
                    break;
                }               
            }
            return strRetVal;
        }
        private void displayRows()
        {
            _log.Debug("displayRows >> ");
            int i = 0;
            gridPayment.Rows.Clear();
            while (i < objInvoice.Rows.Count)
            {
                gridPayment.Rows.Add();
                gridPayment.Rows[i].Cells["colPaymentForm"].Value = objInvoice.Rows[i].PaymentForm.Name;
                gridPayment.Rows[i].Cells["colPaymentType"].Value = objInvoice.Rows[i].PaymentType.Name;
                gridPayment.Rows[i].Cells["colCashBox"].Value = objInvoice.Rows[i].CashBoxId;
                gridPayment.Rows[i].Cells["colQuantity"].Value = formatDecimal(objInvoice.Rows[i].Quantity);
                gridPayment.Rows[i].Cells["colPaid"].Value = formatDecimal(objInvoice.Rows[i].AmountPaid);
                gridPayment.Rows[i].Cells["colChange"].Value = formatDecimal(objInvoice.Rows[i].AmountChange);
                gridPayment.Rows[i].Cells["colTotal"].Value = formatDecimal(objInvoice.Rows[i].Amount);
                gridPayment.Rows[i].Cells["colFee"].Value = formatDecimal(objInvoice.Rows[i].AmountFee);
                gridPayment.Rows[i].Cells["colCardType"].Value = objInvoice.Rows[i].CardType.Name;
                i++;
            }
            textBoxTotalPaid.Text = formatDecimal(Math.Abs(objInvoice.getRowsTotal()));
            disEnableButtons();
           
            _log.Debug("displayRows << ");
        }
        private void disEnableButtons()
        {
            _log.Debug("disEnableButtons >> ");
            try
            {
                if (objInvoice.Paid)
                {
                    pbOK.Enabled = false;
                    butAddRow.Enabled = false;
                    butDeleteRow.Enabled = false;
                    pbUndo.Enabled = true;
                    pbReprint.Enabled = true;
                    DataRow[] cashboxesByCahboxId = CashBox.CashBoxes.Select(("CASHBOXID = '" + objInvoice.strCashBoxId + "'"));
                    _log.Debug("CashBoxId = " + cashboxesByCahboxId);
                    if (cashboxesByCahboxId.Length > 0)
                    {
                        DataRow invoiceCashbox = CashBox.CashBoxes.Select(("CASHBOXID = '" + objInvoice.strCashBoxId + "'"))[0];
                        _log.Debug(" invoiceCashbox[PC] = "+invoiceCashbox["PC"].ToString()+", getClientPCName = "+CashBox.getClientPCName()+", Virtual cashbox = "+invoiceCashbox["VIRTUAL"].ToString());
                        if (invoiceCashbox["ISOPEN"].ToString() == "1" && (invoiceCashbox["PC"].ToString() == CashBox.getClientPCName() || invoiceCashbox["VIRTUAL"].ToString() == "1") && objInvoice.nCreditNewNo == 0 && objInvoice.nCreditNote == 0 && objInvoice.nCrediOfNo == 0 && objInvoice.Rows.Count > 0)
                            pbUndo.Enabled = true;
                        else
                            pbUndo.Enabled = false;
                    }
                    else
                        pbUndo.Enabled = false;                                
                }                                
                else
                {
                    pbUndo.Enabled = false;
                    pbReprint.Enabled = false;
                    if ( cmbPaymentType.Text != "" && cmbPaymentForm.Text != "" && cmbCashBox.Text != "")
                        butAddRow.Enabled = true;
                    else
                        butAddRow.Enabled = false;

                    if (selectedRow >= 0 ) 
                    { butDeleteRow.Enabled = true; }
                    else
                    { butDeleteRow.Enabled = false; }
                    if ((objInvoice.decPaymentSum > 0 ||objInvoice.Rows.Count>0) && this.textBoxCustNo.Text != "" && this.textBoxInvoiceDate.Text != "") pbOK.Enabled = true;
                    else pbOK.Enabled = false;
                }                
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                throw ex;
            }

        }
        private string formatDecimal(decimal dec)
        {
            return String.Format("{0:#,0.00}", dec);
            
        }

        private void textBoxQuantity_Validated(object sender, EventArgs e)
        {
            try
            {
                textBoxQuantity.Text = formatDecimal(Decimal.Parse(textBoxQuantity.Text));
                if (cmbPaymentForm.Text.Substring(0, cmbPaymentForm.Text.IndexOf("=")) == "CURRENCY")
                {
                    decimal nAmount = (Decimal)objCurr.Curr_ExchangeSumToRateFind(decimal.Parse(textBoxQuantity.Text),
                        cmbPaymentType.Text.Substring(0, cmbPaymentType.Text.IndexOf("=")), DateTime.Now, true, false);
                    textBoxAmount.Text = formatDecimal(nAmount);
                }
                //else

            }
            catch (Exception ex)
            {
                textBoxQuantity.Text = "0";
            }
        }

        private void butDeleteRow_Click(object sender, EventArgs e)
        {
            _log.Debug("DeleteRow >> ");
            try
            {
                if (selectedRow >= 0 && objInvoice.Rows[selectedRow].PaymentType.Code != strRoundingCashTypeNegative && objInvoice.Rows[selectedRow].PaymentType.Code != strRoundingCashTypePositive) //Not allow to delete rounding row manually
                {
                    //Delete row, not rounding row
                    objInvoice.Rows.RemoveAt(selectedRow);
                    //Reinsert rounding row
                    objInvoice.addRoundingRow(CashBox, ((clsBaseListItem)cmbPaymentForm.SelectedItem).strText, getDescriptionOfCashtypeByCode(CashBox.CashTypes, strRoundingCashTypeNegative)
                            , getDescriptionOfCashtypeByCode(CashBox.CashTypes, strRoundingCashTypePositive), cmbCashBox.Text.Substring(0, cmbCashBox.Text.IndexOf("=")));
                    getAmountForCashType();
                    displayRows();
                    if (objInvoice.Rows.Count == 0)
                    {
                        selectedRow = -1;
                        if (objInvoice.InvoiceFlag == InvoiceFlags.CashRegisterFlag)
                        {
                            rbDepositToCashBox.Enabled = true;
                            rbWithdrawFromCashBox.Enabled = true;
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                throw ex;
            }
            finally
            {
                disEnableButtons();

            }
            _log.Debug("DeleteRow << ");
        }

        private void gridPayment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            disEnableButtons();
        }

        private void pbOK_Click(object sender, EventArgs e)
        {
            _log.Debug("Payment OK button >>");
            this.pbOK.Enabled = false;
            if (this.checkProfileData())
            {
                if (objInvoice.InvoiceFlag == InvoiceFlags.CashRegisterFlag)
                {
                    objInvoice.strInvoiceModule = "CU";
                }
                if (rbWithdrawFromCashBox.Checked)
                {
                    if (objInvoice.InvoiceFlag == InvoiceFlags.CashRegisterFlag)
                    {
                        objInvoice.strInvoiceModule = "SU";
                    }

                    int i = 0;
                    if (objInvoice.decInvoiceSum>0)
                        objInvoice.decInvoiceSum = -objInvoice.decInvoiceSum;
                    while (i < objInvoice.Rows.Count)
                    {
                        objInvoice.Rows[i].Amount = -objInvoice.Rows[i].Amount;
                        objInvoice.Rows[i].Quantity = -objInvoice.Rows[i].Quantity;

                        i++;
                    }
                }
                objInvoice.strType = textBoxType.Text ;
                objInvoice.strOrderNo = textBoxOrderNo.Text ;
                objInvoice.strInvoiceNo = textBoxInvoiceNo.Text;
                objInvoice.decInvoiceSumOrig = Decimal.Parse( textBoxAmountToPay.Text);
                objInvoice.strCustNo = textBoxCustNo.Text;
                objInvoice.strCustomerName = textBoxCustName.Text ;
                objInvoice.dtInvoiceDate = DateTime.Parse(textBoxInvoiceDate.Text);
                objInvoice.strLicno = textBoxLicNo.Text ;
                objInvoice.strRemark = this.richTextBoxRemark.Text;
                if (objInvoice.saveReceipt(CashBox) == false)
                {
                    //disEnableButtons();
                    return;
                }
                else
                {
                    //Print invoice
                    printInvoice(true);
                };
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
            //disEnableButtons();
            _log.Debug("Payment OK button <<");
        }
        private void printInvoice(bool printOriginal)
        {
            try
            {
                _log.Debug("printInvoice >> " + objInvoice.strInvoiceNo);
               
                List<int> printedReceiptNos = new List<int>();
                foreach (InvoiceRow row in objInvoice.Rows)
                {
                    int receiptno = printOriginal ? row.Receiptno : row.CreditNewNo;
                    
                    if (!printedReceiptNos.Contains(receiptno))
                    {
                        _log.Debug("Printing receiptno " + receiptno.ToString());
                        DataRow invoiceCashbox = CashBox.CashBoxes.Select(("CASHBOXID = '" + row.CashBoxId + "'"))[0];                        
                        printedReceiptNos.Add(receiptno);
                        
                        if (invoiceCashbox["PRINT_RECEIPT"] != null && invoiceCashbox["PRINT_RECEIPT"].ToString() == "1")
                        {
                            printSSRS_Receipt(receiptno);
                        }
                        else
                        {
                            if (invoiceCashbox["FP_PATH"].ToString() != "" && invoiceCashbox["FP_CODAID"].ToString() != "" && Directory.Exists(invoiceCashbox["FP_PATH"].ToString()))
                            {
                                bool isDepositWithraw = (objInvoice.InvoiceFlag == InvoiceFlags.CashRegisterFlag);
                                ExportToFiscalPrinter(row.CashBoxId, invoiceCashbox["FP_PATH"].ToString(), invoiceCashbox["FP_CODAID"].ToString(), receiptno, isDepositWithraw, 0);
                            }
                            else
                            {                               
                                _log.Warn("The Fiscal printer path (CASHBOX.C4) or Codaid (CASHBOX.C5) are missing or path does not exist !");
                                //throw new Exception("The Fiscal printer path (CASHBOX.C4) or Codaid (CASHBOX.C5) are missing or path does not exist !");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                throw ex;
            }

            _log.Debug("printInvoice << ");
        }
        public void ExportToFiscalPrinter(String strCashBoxId,String strFiscalPrinterPath, String strCodaLayout, Int32 nReceiptno, bool isOpenOrDepositReceipt, int nType)
        {
            _log.Debug("ExportToFiscalPrinter>> "+strFiscalPrinterPath);          
            bool bRet = true;
            bool bReceiptFound = false;
            //Read settings from controldata
            string strPath = strFiscalPrinterPath;//Path of fiscal printer 
            string strFilename = objAppConfig.getStringParam(strCodaLayout, "FILENAME", "C6", "");
            int nAppend = objAppConfig.getNumberParam(strCodaLayout, "APPEND", "V1", "");
            String strSeparator = objAppConfig.getStringParam(strCodaLayout, "SEPARATOR", "C6", "");
            String strVehiSaleText = objAppConfig.getStringParam(strCodaLayout, "VEHISALE", "C6", "");
            int nPrintTot = objAppConfig.getNumberParam(strCodaLayout, "PRINTTOT", "V1", "");
            String strSalesText = objAppConfig.getStringParam(strCodaLayout, "PRINTTOT", "C6", "");
            int nExcludeCreditNote = objAppConfig.getNumberParam(strCodaLayout, "EXCL_CRED", "V1", "");
            String strBackupPath = ConfigurationManager.AppSettings["FiscalPrinterBackup"];
            String strCardTypeCheque = ConfigurationManager.AppSettings["CardtypeCheque"];
            if (strCardTypeCheque == null) strCardTypeCheque = "";
            clsSqlFactory hSql = new clsSqlFactory();
            String strSql = "";
            System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            _log.Debug("Default Code page = "+Encoding.Default.EncodingName+", "+Encoding.Default.CodePage);
            try
            {
                _log.Debug(" Starting export fiscal receipt to file... CODAID = " + strCodaLayout);
                _log.Debug(" RECEIPTNO = " + nReceiptno.ToString() + ", TYPE = " + nType.ToString());
                String strModule = "";
                String strInvoiceNo = "";
                int nCreditNote = 0;
                String strSite = objGlobal.CurrentSiteId;
                {
                    if (nType == 0)
                    {
                        // For Module CU we export only payment rows
                        strSql = "select top 1 isnull(INVOICENO,0), isnull(MODULE,''), isnull(CREDITNOTE,0) as CREDITNOTE from ALL_CASHTRANSH where RECEIPTNO = '" + nReceiptno.ToString() + "' and _UNITID = '" + strSite + "'";
                        if (nExcludeCreditNote == 1) strSql = strSql + " and isnull(CREDITNOTE,0)!=1 ";

                        bRet = hSql.ExecuteReader(strSql);                     
                        if (hSql.Read())
                        {
                            strInvoiceNo = (hSql.Reader.GetInt32(0)).ToString();
                            strModule = hSql.Reader.GetString(1);
                            nCreditNote = hSql.Reader.GetInt32(hSql.Reader.GetOrdinal("CREDITNOTE"));
                            bReceiptFound = true;
                        }
                    }
                    else
                    {
                        bRet = hSql.ExecuteReader("select top 1 1 from ALL_CASHREG where RECEIPTNO = '" + nReceiptno.ToString() + "' and _UNITID = '" + strSite + "'");
                        if (hSql.Read())
                        {
                            bReceiptFound = true;
                        }
                    }

                    if (bReceiptFound)
                    {
                        OdbcDataAdapter adapter = new OdbcDataAdapter("", objGlobal.getConnectionString());
                        DataTable tbSales = new DataTable();
                        DataTable tbPayments = new DataTable();
                        DataTable tbOpenOrDeposits = new DataTable();
                        //bRet = hSql.ExecuteReader("select top 1 isnull(INVOICENO,0), isnull(MODULE,'') from ALL_CASHTRANSH where RECEIPTNO = '" + dfReceiptNo.Text + "' and _UNITID = '" + strSite + "'");
                        if (strInvoiceNo != "" && strInvoiceNo != "0")
                        {
                            _log.Debug("INVOICENO = " + strInvoiceNo.ToString());
                            if (isOpenOrDepositReceipt)
                            {
                                _log.Debug(" isOpenOrDepositReceipt = TRUE");
                                //adapter.SelectCommand.CommandText = " select isnull(sum(b.AMOUNT),0) as AMOUNT, case a.EVENT when 'D' then  '1' else  '0' end  as TYPE from ALL_CASHREG a,    ALL_CASHREGROW b where a.ROWID = b.CASHREGROWID and a._UNITID = b._UNITID and a.RECEIPTNO ='" + nReceiptno.ToString() + "' and a.EVENT in ('C','D') and b.CASHFORM in ('CASH') group by a.EVENT ";
                                //adapter.SelectCommand.CommandText = " select case a.MODULE when 'CU' then 1 else 0 end as TYPE, isnull(sum(b.AMOUNT),0) as AMOUNT, b.CASHFORM from ALL_CASHTRANSH a, ALL_CASHTRANSR b where a.MODULE in ('CU','SU') and a.RECEIPTNO = b.RECEIPTNO and a._UNITID = b._UNITID and a.RECEIPTNO ='" + nReceiptno.ToString() + "' and a._UNITID ='" + strSite + "' group by b.CASHFORM, a.MODULE ";
                                adapter.SelectCommand.CommandText = " select case a.MODULE when 'CU' then 0 else 1 end as TYPE, isnull(sum(b.AMOUNT),0) as AMOUNT, b.CASHFORM from ALL_CASHTRANSH a, ALL_CASHTRANSR b where a.MODULE in ('CU','SU') and a.RECEIPTNO = b.RECEIPTNO and a._UNITID = b._UNITID and a.RECEIPTNO ='" + nReceiptno.ToString() + "' and a._UNITID ='" + strSite + "' group by b.CASHFORM, a.MODULE ";
                                adapter.Fill(tbOpenOrDeposits);
                            }
                            else
                            {
                                //adapter.SelectCommand.CommandText = "select sum(b.AMOUNT) as AMOUNT, b.CASHFORM from ALL_CASHTRANSH a, ALL_CASHTRANSR b where a.RECEIPTNO = b.RECEIPTNO and a._UNITID = b._UNITID and a.MODULE !='CU' and a.RECEIPTNO ='" + dfReceiptNo.Text + "' group by b.CASHFORM";
                                // For Module SU/CU we export only payment rows
                                //adapter.SelectCommand.CommandText = " select isnull(sum(b.AMOUNT),0) as AMOUNT, b.CASHFORM from ALL_CASHTRANSH a, ALL_CASHTRANSR b where a.RECEIPTNO = b.RECEIPTNO and a._UNITID = b._UNITID and a.RECEIPTNO ='" + nReceiptno.ToString() +  "' and a._UNITID ='" + strSite + "' group by b.CASHFORM ";
                                adapter.SelectCommand.CommandText =
    @" select isnull(sum(b.AMOUNT),0) as AMOUNT, b.CASHFORM from ALL_CASHTRANSH a, ALL_CASHTRANSR b where a.RECEIPTNO = b.RECEIPTNO and a._UNITID = b._UNITID and a.RECEIPTNO =? and a._UNITID =? 
and  (b.CC_TERMINALID !=? or isnull(b.CC_TERMINALID,'' ) = '' )
group by b.CASHFORM 
union
 select isnull(sum(b.AMOUNT),0) as AMOUNT, 'CHEQUE' as CASHFORM from ALL_CASHTRANSH a, ALL_CASHTRANSR b where a.RECEIPTNO = b.RECEIPTNO and a._UNITID = b._UNITID and a.RECEIPTNO =? and a._UNITID =? 
and  (? !='' and b.CC_TERMINALID =?  )
group by b.CASHFORM 
    ";
                                adapter.SelectCommand.Parameters.AddWithValue("RECEIPTNO", nReceiptno);
                                adapter.SelectCommand.Parameters.AddWithValue("UNITID", strSite);
                                adapter.SelectCommand.Parameters.AddWithValue("CHEQUEID", strCardTypeCheque);
                                adapter.SelectCommand.Parameters.AddWithValue("RECEIPTNO1", nReceiptno);
                                adapter.SelectCommand.Parameters.AddWithValue("UNITID1", strSite);
                                adapter.SelectCommand.Parameters.AddWithValue("CHEQUEID1", strCardTypeCheque);
                                adapter.SelectCommand.Parameters.AddWithValue("CHEQUEID2", strCardTypeCheque);
                                _log.Debug(adapter.SelectCommand.CommandText);
                                adapter.Fill(tbPayments);
                            }
                            decimal nTotalPayment =0;
                            foreach (DataRow r in tbPayments.Rows)                               
                            {
                                nTotalPayment = nTotalPayment + Decimal.Parse(r["AMOUNT"].ToString());
                            }
                            // In GROW there are lines with RSUM<0 and NUM<0 !

                            // If NUM=0 we set NUM=1 and the sign of the amount is the sign of RSUM
                            strSql = "select isnull(a.SUPL,'X') as SUPL,isnull(a.ITEM,'X') as ITEM, replace(a.NAME,',',' ') as NAME,case when isnull(NUM,0) = 0 then sign(RSUM)*1	else NUM end as NUM, RSUM,case when isnull(NUM,0) = 0 then RSUM else RSUM/(sign(NUM)*NUM) end as UNITPR,VATCD,isnull(b.CREDIT,0) as CREDIT from ";
                            if (strModule == "WO")
                            {
                                strSql += " ALL_GROW a, ALL_GSAL b where a._UNITID = b._UNITID and a.GSALID = b.GSALID and a.RSUM !=0 and a.GRECNO = '" + strInvoiceNo + "' and a._UNITID = '" + strSite + "'";
                            }
                            else if (strModule == "SP")
                            {
                                strSql += " ALL_SROW a, ALL_SSAL b where a._UNITID = b._UNITID and a.SSALID = b.SSALID and a.RSUM !=0 and a.SRECNO = '" + strInvoiceNo + "' and a._UNITID = '" + strSite + "'";
                            }
                            else if (strModule == "VA")
                            {
                                strSql = "select top 1  'X' as SUPL, 'X' as ITEM, '" + strVehiSaleText + "' as NAME, 1 as NUM,isnull(a.CREDIT,0) as CREDIT, a.INVSUM as RSUM, a.INVSUM as UNITPR, a.VATCD1 as VATCD from TEMPINV a where a.RECNO = '" + strInvoiceNo + "' and a.UNITID ='" + strSite + "'";
                            }

                            //20140515 LHD

                            if ((nPrintTot == 1) && ((strModule == "WO") || (strModule == "SP")))
                            {
                                strSql = "select 'X' as SUPL,'X' as ITEM, '" + strSalesText + "' as NAME,1 as NUM, sum(isnull(RSUM,0)),sum(isnull(RSUM,0)) as UNITPR,a.VATCD,isnull(b.CREDIT,0) as CREDIT from ";
                                if (strModule == "WO")
                                {
                                    strSql += " ALL_GROW a, ALL_GSAL b where a._UNITID = b._UNITID and a.GSALID = b.GSALID and a.RSUM !=0 and a.GRECNO = '" + strInvoiceNo + "' and a._UNITID = '" + strSite + "'";
                                }
                                else if (strModule == "SP")
                                {
                                    strSql += " ALL_SROW a, ALL_SSAL b where a._UNITID = b._UNITID and a.SSALID = b.SSALID and a.RSUM !=0 and a.SRECNO = '" + strInvoiceNo + "' and a._UNITID = '" + strSite + "'";
                                }
                                strSql += " group by a.VATCD, isnull(b.CREDIT,0) ";
                                //In some cases the sum from CASHTRANS not equal the sum from GROW !
                                //strSql = "select 'X' as ITEM, '" + strSalesText + "' as NAME,1 as NUM, isnull(sum(b.AMOUNT),0) , isnull(sum(b.AMOUNT),0) as UNITPR from ALL_CASHTRANSH a, ALL_CASHTRANSR b where a.RECEIPTNO = b.RECEIPTNO and a._UNITID = b._UNITID and a.RECEIPTNO ='" + nReceiptno.ToString() + "' group by b.CASHFORM ";                           
                            }
                            adapter.SelectCommand.CommandText = strSql;

                            if (strModule == "WO" || strModule == "SP" || strModule == "VA")
                            {
                                adapter.Fill(tbSales);
                                if (nPrintTot == 1 && tbSales.Rows.Count == 1) tbSales.Rows[0]["UNITPR"] = nTotalPayment;
                                _log.Debug(strSql);
                            }
                        }
                        
                        //
                        //Dispose adapter
                        adapter.Dispose();
                        //
                        if (strCodaLayout == "ZMP55")
                        {
                            if (nAppend == 1)
                                ExportMP55B(strInvoiceNo, tbSales, tbPayments, tbOpenOrDeposits, strPath, strFilename, strSeparator, strModule, FileMode.Append, nCreditNote);
                            else
                                ExportMP55B(strInvoiceNo, tbSales, tbPayments, tbOpenOrDeposits, strPath, strFilename, strSeparator, strModule, FileMode.Create, nCreditNote);
                        }
                        if (strCodaLayout == "ZFP550")
                        {
                            ExportFP550(strCashBoxId, tbSales, tbPayments, tbOpenOrDeposits, strPath, strFilename, strSeparator, strModule, FileMode.Create, nCreditNote);
                        }
                        //Save to backup folder
                        //strBackupPath
                        String strFullFileName = strPath + strFilename;
                        if (strBackupPath != "" && Directory.Exists(strBackupPath) && File.Exists(strFullFileName))
                        {
                            _log.Debug("Save to backup folder");
                            try
                            {
                                File.Copy(strFullFileName, strBackupPath + strFilename + "_" + nType.ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + nReceiptno.ToString());
                            }
                            catch (Exception ex)
                            {
                                _log.Error(ex.ToString());
                            }
                        }
                        //Insert into [Z_FISCALPRINTER] ?
                    }
                    else
                    {
                        _log.Error("Receipno not found in this site/or creditnote printing is disabled !");
                        MessageBox.Show("Receipno not found in this site/or creditnote printing is disabled !");
                    }
                }
                if (bRet)
                    hSql.Commit();
                else
                    hSql.Rollback();
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                throw ex;
            }
            finally
            {
                hSql.Close();
                System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
            }
            _log.Debug("ExportToFiscalPrinter<<");
        }
        private void ExportFP550(String strCashBoxId, DataTable tbSales, DataTable tbPayments, DataTable tbOpenOrDeposits, string strPath, string strFilename, string strSeparator, string strModule, FileMode fileMode, int nCreditReceipt)
        {
            _log.Debug("ExportFP550 >> ");
            bool bRet = true;
            string strWrkFileName = strPath + strFilename;
            if (tbSales.Rows.Count > 0 || tbPayments.Rows.Count > 0 || tbOpenOrDeposits.Rows.Count > 0)
            {
                String strLine = "";
                clsConvertValue objConv = new clsConvertValue();
                objConv.Init("ZFP550CONV");
                //Get record types and create records to be exported
                clsFlatWriter objRecord = new clsFlatWriter();
                clsFlatWriter objWriter = new clsFlatWriter();
                String strItemName ="";
                bRet = objRecord.initLayout("XFPABC", clsFlatWriter.MODE_FLAT_WRITE);

                bRet = objWriter.initLayout("XFPABC", clsFlatWriter.MODE_FLAT_WRITE);
                if (strFiscalFileCodePage !=null&& strFiscalFileCodePage !="")
                    bRet = objWriter.openFile(strWrkFileName, Encoding.GetEncoding(Int32.Parse(strFiscalFileCodePage)), fileMode);
                else
                    bRet = objWriter.openFile(strWrkFileName, Encoding.Default, fileMode);

                clsSqlFactory hSql = new clsSqlFactory();
                try
                {
                    //if (tbOpenOrDeposits.Rows.Count > 0)
                    //{
                    //}
                    //else
                    {
                        //Get the daily sequence number of the receipt
                        int nSequence = objAppConfig.getNumberParam("ZFP550", "FILESEQ", "V1", "")+1;
                        DateTime dtLastReceiptDate = objAppConfig.getDateTimeParam("ZFP550", "FILESEQ", "D1", "");
                        if (dtLastReceiptDate.Date != DateTime.Now.Date) nSequence = 1;
                        int SKIPSUMLIN = objAppConfig.getNumberParam("ZFP550", "SKIPSUMLIN", "V1", "");
                        int SKIPUSER = objAppConfig.getNumberParam("ZFP550", "SKIPUSER", "V1", "");
                        //S Sales
                        foreach (DataRow r in tbSales.Rows)
                        {
                            objRecord.clear();
                            objRecord.setField("01", r["ITEM"].ToString());// ITEM
                            //Convert VATCD
                            objRecord.setField("02", objConv.getValue("FPVAT", r["VATCD"].ToString(), true));//VAT
                            //Item desc must be unique ??
                            strItemName = ((r["NAME"].ToString()+'_'+r["ITEM"].ToString() + '_' + r["SUPL"].ToString()).Replace(",", " ")).Replace("\"", " ") ;

                            if (strItemName.Length>30) strItemName = strItemName.Substring(0,30);
                            strItemName = "\""+strItemName+"\"";
                            objRecord.setField("03", strItemName);//NAME
                            //
                            objRecord.setField("04", r["UNITPR"].ToString());
                            if (nCreditReceipt ==1)
                                r["NUM"]=(-1)*Decimal.Parse(r["NUM"].ToString());
                                //objRecord.setField("05", "-"+r["NUM"].ToString());
                            //else
                                objRecord.setField("05", r["NUM"].ToString());
                            //
                            strLine = objRecord.getRecordData();
                            objWriter.writeLine(strLine);
                            _log.Debug(strLine);
                            _log.Debug(r["ITEM"].ToString());
                        }
                        //Write END OF SALES 
                        if (SKIPSUMLIN != 1)
                        {
                            objRecord.clear();
                            objRecord.setField("01", "END_OF_SALE");// ITEM
                            objRecord.setField("02", "0");//VAT
                            objRecord.setField("03", "0");//NAME
                            //objRecord.setField("04", "0");
                            //objRecord.setField("05", "0");
                            //
                            strLine = objRecord.getRecordData();
                            objWriter.writeLine(strLine);
                            _log.Debug(strLine);
                        }
                        //Payments
                        if (tbPayments.Rows.Count > 0)
                        {
                            objRecord.clear();
                            foreach (DataRow r in tbPayments.Rows)
                            {
                                if (objConv.getValue("FPPAYFORM", r["CASHFORM"].ToString(), true) != "")
                                {
                                    objRecord.setField("01", objConv.getValue("FPPAYFORM", r["CASHFORM"].ToString(), true));
                                    objRecord.setField("02", "0");
                                    objRecord.setField("03", "0");                                    
                                    //objRecord.setField(r, "03", objAppConfig.getStringParam("CASHFORM", r["CASHFORM"].ToString(), "C2", "").Replace(","," "));
                                    objRecord.setField("04", r["AMOUNT"].ToString());
                                    objRecord.setField("05", "0");
                                    strLine = objRecord.getRecordData();
                                    objWriter.writeLine(strLine);
                                    _log.Debug(strLine);
                                }
                            }
                            //Write END OF PAY 
                            if (SKIPSUMLIN != 1)
                            {
                                objRecord.clear();
                                objRecord.setField("01", "END_OF_PAY");// ITEM
                                objRecord.setField("02", "0");//VAT
                                objRecord.setField("03", "0");//NAME
                                //objRecord.setField("04", "0");
                                //objRecord.setField("05", "0");
                                //
                                strLine = objRecord.getRecordData();
                                objWriter.writeLine(strLine);
                            }
                            //USER_ID,0,0999,0,0,0
                            if (SKIPUSER != 1)
                            {
                                hSql.NewCommand("select replace(left(EXPL,22),',',' ') from EUSR where USRSID = ?");
                                hSql.Com.Parameters.AddWithValue("USRSID", objGlobal.DMSFirstUserName);
                                hSql.ExecuteReader();
                                if (hSql.Read())
                                {
                                    objWriter.writeLine("USER_NAME,0," + "\"" + hSql.Reader.GetString(0).Replace("\"", " ") + "\"" + ",0,0,0");
                                }
                            }
                            
                            //nSequence.ToString();
                            //objWriter.writeLine("FOOTER_1,0," + "\"" + DateTime.Now.ToString("yyyyMMdd") + strCashBoxId + nSequence.ToString().PadLeft(6, '0') + "\"" + ",0,0");
                            //objWriter.writeLine("FOOTER_1,0," + "" + ",0,0,0");
                            _log.Debug(strLine);
                            hSql.NewCommand("update " + objUtil.getTable("CORW") + " set V1 = ?, D1 = getdate() where CODAID ='ZFP550' and C1 = 'FILESEQ'");
                            hSql.Com.Parameters.AddWithValue("SEQUENCE",nSequence);
                            hSql.ExecuteNonQuery();

                            hSql.Commit();
                        }
                    }
                }
                finally
                {
                    objWriter.closeFile();
                    hSql.Close();
                }
            }
            else
                _log.Debug("No records found!");
        }
        private void ExportMP55B(String strInvoiceNo, DataTable tbSales, DataTable tbPayments, DataTable tbOpenOrDeposits, string strPath, string strFilename, string strSeparator, string strModule, FileMode fileMode, int nCreditReceipt)
        {
            bool bRet = true;
            _log.Debug("ExportMP55B >>");
            int nPrintDuplicate = objAppConfig.getNumberParam("ZMP55", "PRINTDUP", "V1", ""); // D type line
            string strPrintDuplicate = objAppConfig.getStringParam("ZMP55", "PRINTDUP", "C6", ""); // D type line
            int nExcludeF = objAppConfig.getNumberParam("ZMP55", "EXCL_F", "V1", ""); // F type line
            if (tbSales.Rows.Count > 0 || tbPayments.Rows.Count > 0 || tbOpenOrDeposits.Rows.Count > 0)
            {

                String strLine = "";
                clsConvertValue objConv = new clsConvertValue();
                bRet = objConv.Init("ZMP55CONV");
                //Get record types and create records to be exported
                clsFlatWriter objRecordH = new clsFlatWriter();
                clsFlatWriter objRecordP = new clsFlatWriter();
                clsFlatWriter objRecordS = new clsFlatWriter();
                clsFlatWriter objRecordT = new clsFlatWriter();
                clsFlatWriter objRecordI = new clsFlatWriter();
                clsFlatWriter objRecordTE = new clsFlatWriter();
                clsFlatWriter objRecordF = new clsFlatWriter();
                clsFlatWriter objWriter = new clsFlatWriter();

                bRet = objWriter.initLayout("XMP55H", clsFlatWriter.MODE_FLAT_WRITE);

                //bRet = objWriter.openFile(strPath + strFilename, Encoding.Default, fileMode);
                if (strFiscalFileCodePage != null && strFiscalFileCodePage != "")
                    bRet = objWriter.openFile(strPath + strFilename, Encoding.GetEncoding(Int32.Parse(strFiscalFileCodePage)), fileMode);
                else
                    bRet = objWriter.openFile(strPath + strFilename, Encoding.Default, fileMode);
                try
                {
                    if (tbOpenOrDeposits.Rows.Count > 0)
                    {
                        bRet = objRecordI.initLayout("XMP55I", clsFlatWriter.MODE_FLAT_WRITE);
                        foreach (DataRow r in tbOpenOrDeposits.Rows)
                        {
                            objRecordI.setField(r, "01", "");
                            objRecordI.setField(r, "02", "");
                            objRecordI.setField(r, "03", "");                            
                            objRecordI.setField("03", Math.Abs(Decimal.Parse(r["AMOUNT"].ToString())).ToString());
                            objRecordI.setField(r, "04", "");
                            strLine = objRecordI.getRecordData() + strSeparator;
                            objWriter.writeLine(strLine);
                            _log.Debug(strLine);
                        }
                    }
                    else
                    {
                        //H 
                        bRet = objRecordH.initLayout("XMP55H", clsFlatWriter.MODE_FLAT_WRITE);
                        objRecordH.setField("01", "");
                        strLine = objRecordH.getRecordData() + strSeparator;
                        objWriter.writeLine(strLine);
                        //P text

                        bRet = objRecordP.initLayout("XMP55P", clsFlatWriter.MODE_FLAT_WRITE);
                        objRecordP.setField("01", "");
                        //Put the invoice number here 
                        objRecordP.setField("02", "F.: " + strInvoiceNo);
                        strLine = objRecordP.getRecordData() + strSeparator;
                        objWriter.writeLine(strLine);
                        //
                        //S Sales
                        bRet = objRecordS.initLayout("XMP55S", clsFlatWriter.MODE_FLAT_WRITE);
                        foreach (DataRow r in tbSales.Rows)
                        {
                            objRecordS.setField(r, "01", "");
                            objRecordS.setField(r, "02", "");
                            objRecordS.setField(r, "03", "");                            
                            //objRecordS.setField(r, "04", "");
                            objRecordS.setField(r, "05", "");
                            objRecordS.setField(r, "06", "");
                            objRecordS.setField(r, "07", "");
                            if (nCreditReceipt==1)
                                r["NUM"]=(-1)*Decimal.Parse(r["NUM"].ToString());
                                //objRecordS.setField("04", "-" + r["NUM"].ToString());
                            //else
                                objRecordS.setField("04",r["NUM"].ToString());
                            //Convert VAT code
                            objRecordS.setField("07", objConv.getValue("FPVAT", objRecordS.getField("07").Trim(), true));
                            //
                            objRecordS.setField(r, "08", "");
                            objRecordS.setField(r, "09", "");
                            strLine = objRecordS.getRecordData() + strSeparator;
                            objWriter.writeLine(strLine);
                            _log.Debug(strLine);
                        }
                        //T Payments
                        if (tbPayments.Rows.Count > 0)
                        {
                            _log.Debug("Number of payment T rows " + tbPayments.Rows.Count.ToString());
                            bRet = objRecordT.initLayout("XMP55T", clsFlatWriter.MODE_FLAT_WRITE);
                            foreach (DataRow r in tbPayments.Rows)
                            {

                                objRecordT.setField(r, "01", "");
                                objRecordT.setField(r, "02", "");
                                //Convert PAYFORM FPPAYFORM
                                _log.Debug("CASHFORM = " + objRecordT.getField("02").Trim());
                                objRecordT.setField("02", objConv.getValue("FPPAYFORM", objRecordT.getField("02").Trim(), true));
                                //
                                objRecordT.setField(r, "03", "");
                                strLine = objRecordT.getRecordData() + strSeparator;
                                objWriter.writeLine(strLine);

                                _log.Debug(strLine);
                            }
                            if (nPrintDuplicate != 1)
                            {
                                if (nExcludeF != 1)
                                {
                                    _log.Debug("TE Payment end ");
                                    bRet = objRecordTE.initLayout("XMP55TE", clsFlatWriter.MODE_FLAT_WRITE);
                                    objRecordTE.setField("01", "");
                                    strLine = objRecordTE.getRecordData() + strSeparator;
                                    objWriter.writeLine(strLine);
                                }
                            }
                        }
                        //F or D
                        if (nPrintDuplicate != 1)
                        {
                            if (nExcludeF != 1)
                            {
                                // Print F
                                bRet = objRecordF.initLayout("XMP55F", clsFlatWriter.MODE_FLAT_WRITE);
                                objRecordF.setField("01", "");
                                strLine = objRecordF.getRecordData() + strSeparator;
                                objWriter.writeLine(strLine);
                            }
                        }
                        else
                        {
                            //Print D
                            strLine = strPrintDuplicate;// D,1,______,_,__;                            
                            objWriter.writeLine(strLine);
                        }                        
                    }
                }
                finally
                {
                    objWriter.closeFile();
                }
            }
            else
                _log.Debug("No records found!");
        }

        private void richTextBoxRemark_Validated(object sender, EventArgs e)
        {
            objInvoice.strRemark = richTextBoxRemark.Text;
        }

        private void textBoxAmountToPay_Validated(object sender, EventArgs e)
        {
            _log.Debug("textBoxAmountToPay_Validated >> ");
            try
            {
                decimal nAmount = Math.Abs(Decimal.Parse(textBoxAmountToPay.Text));
                nAmount =    CashBox.roundToLocalCurrency(nAmount, cmbPaymentType.Text.Substring(0, cmbPaymentType.Text.IndexOf("=")));
                
                textBoxAmountToPay.Text = formatDecimal(nAmount);
                objInvoice.decInvoiceSum = nAmount;
                getAmountForCashType();
                displayRows();
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                throw ex;
            }
            _log.Debug("textBoxAmountToPay_Validated << ");
        }

        private void pbCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void pbUndo_Click(object sender, EventArgs e)
        {
            _log.Debug("pbUndo_Click >> "+objInvoice.strInvoiceNo);
            try
            {
                DialogResult result =
                    MessageBox.Show(objUtil.Localization.getMsgString("CASHREG_12") + " " + objInvoice .ReceiptNo+ " ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    objInvoice.undoPayment(CashBox);
                    printInvoice(false);
                    //objInvoice.
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    this.Dispose();
                }
                else if (result == DialogResult.Cancel)
                    return;                
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                throw ex;
            }
            _log.Debug("pbUndo_Click << ");
        }
        private static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
        {
            bool result = true;
            //if (cert.Subject.ToUpper().Contains("YourServerName"))
            //{
            //  result = true;
            //}
            return result;
        }
        private void printSSRS_Receipt(int nReceiptNo)
        {
            _log.Debug("printSSRS_Receipt >> " + nReceiptNo.ToString());
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {
                String strLanguageCode = "ENG";

                hSql.NewCommand("select C1 from " + objUtil.getTable("CORW") + " where CODAID ='KIELIKOODI' and C8 = ?");
                hSql.Com.Parameters.AddWithValue("C8", objGlobal.CultureInfo);
                hSql.ExecuteReader();

                if (hSql.Read())
                    strLanguageCode = hSql.Reader.GetString(0);
                else
                    if (ConfigurationManager.AppSettings["LangCodeAM"] != null && ConfigurationManager.AppSettings["LangCodeAM"] != "")
                        strLanguageCode = ConfigurationManager.AppSettings["LangCodeAM"];
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                if (ConfigurationManager.AppSettings["rsExecUrl"] == null || ConfigurationManager.AppSettings["rsExecUrl"] == "")
                {

                    String strReprint_bat = ConfigurationSettings.AppSettings["Reprint_bat"];
                    if (objAppConfig.getStringParam("CASHREG", "P_RECEIPT", "C3", "") != "")
                        strReprint_bat = objAppConfig.getStringParam("CASHREG", "P_RECEIPT", "C3", "");
                    _log.Debug("Reprint file : " + strReprint_bat);

                    process.StartInfo.FileName = strReprint_bat;
                    process.StartInfo.Arguments = strLanguageCode + " " + objGlobal.CurrentSiteId + " " + nReceiptNo;
                    process.Start();
                }
                else
                {
                    ReportExecutionService rsExec = new ReportExecutionService();
                    if (objGlobal.WinAuth && ConfigurationManager.AppSettings["rsUserName"] !="" && ConfigurationManager.AppSettings["rsPassWord"] !="")
                        rsExec.Credentials = CredentialCache.DefaultNetworkCredentials;
                    else
                        rsExec.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["rsUserName"], ConfigurationManager.AppSettings["rsPassWord"], ConfigurationManager.AppSettings["rsDomain"]);
                    rsExec.Url = ConfigurationManager.AppSettings["rsExecUrl"] + ConfigurationManager.AppSettings["rsExecService"];
                    _log.Debug("DefaultNetworkCredentials.UserName = " + CredentialCache.DefaultNetworkCredentials.UserName);
                    _log.Debug("Windows User  = "+WindowsIdentity.GetCurrent().Name);
                    ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);
                    string historyID = null;
                    string reportPath = ConfigurationManager.AppSettings["rsCashReceiptReport"];
                    rsExec.LoadReport(reportPath, historyID);

                    ReportExecution2005.ParameterValue[] executionParams;
                    executionParams = new ReportExecution2005.ParameterValue[3];
                    executionParams[0] = new ReportExecution2005.ParameterValue();
                    executionParams[0].Name = "Languages";
                    executionParams[0].Value = strLanguageCode;
                    executionParams[1] = new ReportExecution2005.ParameterValue();
                    executionParams[1].Name = "Site";
                    executionParams[1].Value = objGlobal.CurrentSiteId;
                    executionParams[2] = new ReportExecution2005.ParameterValue();
                    executionParams[2].Name = "iReceiptNo";
                    executionParams[2].Value = nReceiptNo.ToString();

                    rsExec.SetExecutionParameters(executionParams, "en-us");

                    string deviceInfo = null;
                    string extension;
                    string encoding;
                    string mimeType;
                    ReportExecution2005.Warning[] warnings = null;
                    string[] streamIDs = null;
                    string format = ConfigurationManager.AppSettings["rsFormat"];
                    if (format == null || format == "") format = "PDF"; // Default
                    //Render file and save
                    Byte[] results = rsExec.Render(format, deviceInfo, out extension, out mimeType, out encoding, out warnings, out streamIDs);
                    string rsFileName = ConfigurationManager.AppSettings["rsFilePath"] + "Cashreceipt" + nReceiptNo.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + format;
                    FileStream stream = File.OpenWrite(rsFileName);
                    stream.Write(results, 0, results.Length);
                    stream.Close();
                    //Open the PDF file
                    if (File.Exists(rsFileName))
                    {
                        process = new System.Diagnostics.Process();
                        process.StartInfo.FileName = rsFileName;
                        process.Start();
                    }
                    //Delete temporary files older than 1 day
                    string[] files = Directory.GetFiles(ConfigurationManager.AppSettings["rsFilePath"]);

                    foreach (string file in files)
                    {
                        FileInfo fi = new FileInfo(file);
                        if (fi.LastAccessTime < DateTime.Now.AddDays(-1))
                            fi.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                throw ex;
            }
            finally
            {
                hSql.Close();
            }
            _log.Debug("printSSRS_Receipt << " + nReceiptNo.ToString());
        }
        private void pbReprint_Click(object sender, EventArgs e)
        {
            _log.Debug("pbReprint_Click >>");
            this.pbReprint.Enabled = false;
            printInvoice(true);
            this.Close();
            this.Dispose();
             _log.Debug("pbReprint_Click <<");
        }        
        private void textBoxAmountToPay_Validating(object sender, CancelEventArgs e)
        {
            Decimal nDecimal = 0;
            if (!Decimal.TryParse(((TextBox)sender).Text.ToString(), out nDecimal) || nDecimal < 0)
            {
                MessageBox.Show(objUtil.Localization.getMsgString("CASHREG_01"));
                e.Cancel = true;
            }
        }

        private void textBoxAmount_Validating(object sender, CancelEventArgs e)
        {
            Decimal nDecimal = 0;
            if (!Decimal.TryParse(((TextBox)sender).Text.ToString(), out nDecimal) || nDecimal < 0)
            {
                MessageBox.Show(objUtil.Localization.getMsgString("CASHREG_01"));
                e.Cancel = true;
            }
        }

        private void textBoxQuantity_Validating(object sender, CancelEventArgs e)
        {
            Decimal nDecimal = 0;
            if (!Decimal.TryParse(((TextBox)sender).Text.ToString(), out nDecimal) || nDecimal<0)
            {
                MessageBox.Show(objUtil.Localization.getMsgString("CASHREG_01"));
                e.Cancel = true;
            }
            
        }

        private void textBoxCustNo_Validating(object sender, CancelEventArgs e)
        {
            //Check if is valid number
            Int32 nInteger = 0;
            if (!Int32.TryParse(((TextBox)sender).Text.ToString(), out nInteger))
            {
                ((TextBox)sender).Text = "";
                this.textBoxCustName.Text = "";
            }
        }

        private void textBoxCustNo_Validated(object sender, EventArgs e)
        {
            _log.Debug("Get customer name from CUSTNO >>");
            clsSqlFactory hSql = new clsSqlFactory();
            clsBaseUtility objLocUtil = new clsBaseUtility();
            try
            {
                hSql.NewCommand("Select isnull(a.LNAME,''),isnull(a.FNAME,'') from "+objLocUtil.getTable("CUST") +" a where a.CUSTNO =? ");
                hSql.Com.Parameters.AddWithValue("CUSTNO", this.textBoxCustNo.Text);
                hSql.ExecuteReader();
                if (hSql.Read())
                {
                    this.textBoxCustName.Text = hSql.Reader.GetString(0) + " " + hSql.Reader.GetString(1);
                    objInvoice.strCustNo = this.textBoxCustNo.Text;
                }
                else
                {
                    ((TextBox)sender).Text = "";
                    textBoxCustName.Text = "";
                    objInvoice.strCustNo = "";
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                throw ex;
            }
            finally
            {
                hSql.Close();
                disEnableButtons();
            }
            _log.Debug("Get customer name from CUSTNO <<");
        }

        private void gridPayment_SelectionChanged(object sender, EventArgs e)
        {
            selectedRow = gridPayment.CurrentCell.RowIndex;
            disEnableButtons();
        }

        private void textBoxInvoiceDate_Validating(object sender, CancelEventArgs e)
        {
            //Check if is valid date
            DateTime dtInvoiceDate;
            if (((TextBox)sender).Text.ToString() !="" && !DateTime.TryParse(((TextBox)sender).Text.ToString(), out dtInvoiceDate))
            {
                MessageBox.Show(objUtil.Localization.getMsgString("CASHREG_05"));//Invalid date!);
                e.Cancel =true;
            }
        }

        private void textBoxInvoiceDate_Validated(object sender, EventArgs e)
        {
            disEnableButtons();
        }
        
        private void pbSearchCustomer_Click(object sender, EventArgs e)
        {
            dlgSearchCustomer searhCustomer = new dlgSearchCustomer();
           searhCustomer.Owner = this;
           searhCustomer.ShowDialog();
           if (searhCustomer.Custno   != "")
           {
               this.textBoxCustNo.Text = searhCustomer.Custno;
               this.textBoxCustName.Text = searhCustomer.CustName;
               disEnableButtons();
           }
        }

        private void textBoxCustNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxFee_Validated(object sender, EventArgs e)
        {
            try
            {
                decimal nAmount = Decimal.Parse(textBoxFee.Text);
                //nAmount = CashBox.roundToInt(nAmount, cmbPaymentType.Text.Substring(0, cmbPaymentType.Text.IndexOf("=")));
                this.textBoxFee.Text = formatDecimal(nAmount);
            }
            catch (Exception ex)
            {
                textBoxFee.Text = "0";
            }
            finally
            {
                //disEnableButtons();
            }
        }

        private void textBoxFee_Validating(object sender, CancelEventArgs e)
        {
            Decimal nDecimal = 0;
            if (!Decimal.TryParse(((TextBox)sender).Text.ToString(), out nDecimal) || nDecimal < 0)
            {
                MessageBox.Show(objUtil.Localization.getMsgString("CASHREG_01"));
                e.Cancel = true;
            }
        }

        private void cmbCashBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _log.Debug("cmbCashBox_SelectedIndexChanged >> ");
            try
            {
                if (this.cmbCashBox.Text != "")
                {
                    string selectedCashBoxId = this.cmbCashBox.Text.Substring(0, cmbCashBox.Text.IndexOf("="));
                    DataRow invoiceCashbox = CashBox.CashBoxes.Select(("CASHBOXID = '" + selectedCashBoxId + "'"))[0];


                    if (invoiceCashbox["ENABLE_FEE"] != null && invoiceCashbox["ENABLE_FEE"].ToString() == "1")
                    {
                        this.textBoxFee.Visible = true;
                        this.lbFee.Visible = true;
                    }
                    else
                    {
                        this.textBoxFee.Visible = false;
                        this.lbFee.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                throw ex;
            }
            _log.Debug("cmbCashBox_SelectedIndexChanged << ");
        }

        private void rbDepositToCashBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isAmInvoice) fillCashTypeCombobox();
        }

        private void rbWithdrawFromCashBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isAmInvoice) fillCashTypeCombobox();
        }

        private void textBoxAmountToPay_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
