using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using log4net;
using nsBaseClass;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Data.Odbc;
namespace CashRegPrime.Model
{

     class InvoiceFlags
    {
        public static int VehicleSalesFlag = 1;
        public static int WorkshopFlag = 2;
        public static int SparePartSalesFlag = 4;
        public static int CashRegisterFlag = 8;
       
    }
     public class PaymentForm
     {
         public string Code = "";
         public string Name = "";
     }
     public class PaymentType
     {
         public string Code = "";
         public string Name = "";
     }
     public class CardType
     {
         public string Code = "";
         public string Name = "";
     }
    public class InvoiceRow
     {
         //public string PaymentForm;
         //public string PaymentType;
        public PaymentForm PaymentForm = new PaymentForm();
        public PaymentType PaymentType = new PaymentType();
         public decimal Quantity;
         public decimal Amount;
         public string SaleType;
         public string CashBoxId;
         public decimal AmountPaid;
         public decimal AmountChange;
         public int Rowno;
         public int Receiptno = 0;
         public int CreditNewNo = 0;
         public decimal AmountFee = 0;
         public CardType CardType = new CardType();//Saved to CC_TERMINALID
         //public int OrigRowno;//the original Rowno of the rounding row

     }
    public class Invoice
    {
       
        static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string strType;
        public string strOrderNo;
        public string strCustNo;
        public string strCustomerName;
        public string strStatus;
        public string strInvoiceNo;
        public int ReceiptNo;
        public DateTime dtInvoiceDate;
        public DateTime dtPaymentDate;
        public decimal decInvoiceSum;
        public decimal decInvoiceSumOrig;
        public decimal decPaymentSum;
        public string strInvoiceModule;
        public string strDeptId;
        public bool Paid;
        public int InvoiceFlag;
        public string strCashBoxId;
        public string strRemark;
        public string strLicno;
        //
        public int nCreditNewNo = 0;
        public int nCreditNote = 0;
        public int nCrediOfNo = 0;

        //
        public List<InvoiceRow> Rows=new List<InvoiceRow>();
        clsBaseUtility objUtil = new clsBaseUtility();
        clsAppConfig objConfig = new clsAppConfig();
        string strRoundingCashTypePositive = ConfigurationManager.AppSettings["RoundingCashTypePositive"];
        string strRoundingCashTypeNegative = ConfigurationManager.AppSettings["RoundingCashTypeNegative"];
        public int nRowNoIndex = 1;

        public Invoice()
        {
            
            strType = ""; strOrderNo = ""; strCustNo = ""; strCustomerName = ""; strStatus = ""; strInvoiceNo = ""; ReceiptNo = 0;
            strCashBoxId = "";
            strRemark = "";
            strDeptId = "";
            strLicno = "";
            decInvoiceSum = 0; decPaymentSum = 0; Paid = false;
            InvoiceFlag = InvoiceFlags.CashRegisterFlag;
        }
     
        public int getReceiptNUSE(clsSqlFactory hSql, int nNuseId)
        {
            _log.Debug("getReceiptNUSE >> nUseId = "+nNuseId.ToString());
            int nRet = 0;
            hSql.NewCommand("update "+objUtil.getTable("NUSE")+" set RECNO=RECNO+1 where NUSEID = ?");
            hSql.Com.Parameters.AddWithValue("NUSEID", nNuseId);
            hSql.ExecuteNonQuery();
            hSql.NewCommand("select RECNO from "+objUtil.getTable("NUSE")+" where NUSEID = ?");
            hSql.Com.Parameters.AddWithValue("NUSEID", nNuseId);
            hSql.ExecuteReader();
            if (hSql.Read())
                nRet = hSql.Reader.GetInt32(0);
            _log.Debug("getReceiptNUSE << ReceipNo = " + nRet.ToString());
            return nRet;
        }
        public bool undoPayment(clsCashBox CashBox)
        {
            _log.Debug("undoPayment >> "+ReceiptNo.ToString());
            bool bRet = true;
            clsSqlFactory hSql = new clsSqlFactory();
            clsGlobalVariable objGlobal = new clsGlobalVariable();
            try
            {
                 
                DataRow invoiceCashbox = CashBox.CashBoxes.Select(("CASHBOXID = '" + strCashBoxId+"'"))[0];
               
                if (invoiceCashbox["ISOPEN"].ToString() == "1" && invoiceCashbox["PC"].ToString() == CashBox.getClientPCName() && nCreditNewNo == 0 && nCreditNote == 0 && nCrediOfNo == 0 && Rows.Count > 0)
                {                   
                    //Get new Receiptno
                    int nNuseId = CashBox.getVoucherIdOfType(Rows[0].PaymentType.Code, Rows[0].Amount > 0 ? true : false);
                    
                    //Insert new TRANSH row
                    if (nNuseId > 0)
                    {
                        int nNewReceiptNo = getReceiptNUSE(hSql, nNuseId);

                        _log.Debug("New credit receiptno: " + nNewReceiptNo.ToString());
                        hSql.NewCommand("insert into " + objUtil.getTable("CASHTRANSH") + "(CASHBOXID,DEPARTMENT,SMANID,UNIT,MODULE," +
                            "INVOICECATEGORY,CUSTNO,AMOUNTTOPAY,TOTPAID,CASHRETURNED," +
                            "EXT_BILLD,EXT_ORDERNO,EXT_INVOICENO,INVOICENO,ORDERNO,LICNO, " +
                            "UPDATED_DATE,TEXT,INSERTPC,UPDATED_BY, RECEIPTNO,CREDITNOTE,CREDITOFNO )" +
                            " select a.CASHBOXID,a.DEPARTMENT,a.SMANID,a.UNIT,a.MODULE," +
                            "a.INVOICECATEGORY,a.CUSTNO,(-1)*a.AMOUNTTOPAY,(-1)*a.TOTPAID,(-1)*a.CASHRETURNED," +
                            "a.EXT_BILLD,a.EXT_ORDERNO,a.EXT_INVOICENO,a.INVOICENO,a.ORDERNO,a.LICNO," +
                            "getdate(),?,?,?,?,?,? " +
                            " from " + objUtil.getTable("CASHTRANSH") + " a where a.RECEIPTNO = ?");

                        hSql.Com.Parameters.AddWithValue("TEXT", "");
                        hSql.Com.Parameters.AddWithValue("INSERTEDPC", CashBox.getClientPCName());
                        hSql.Com.Parameters.AddWithValue("UPDATED_BY", objGlobal.DMSFirstUserName);
                        hSql.Com.Parameters.AddWithValue("RECEIPTNO", nNewReceiptNo);
                        hSql.Com.Parameters.AddWithValue("CREDITNOTE", 1);
                        hSql.Com.Parameters.AddWithValue("CREDITOFNO", ReceiptNo);

                        hSql.Com.Parameters.AddWithValue("RECEIPTNO", ReceiptNo);
                        bRet = bRet && hSql.ExecuteNonQuery();
                        //Update current TRANSH row
                        hSql.NewCommand("update " + objUtil.getTable("CASHTRANSH") + " set CREDITNEWNO = ? where RECEIPTNO =? ");
                        hSql.Com.Parameters.AddWithValue("CREDITNEWNO", nNewReceiptNo);
                        hSql.Com.Parameters.AddWithValue("RECEIPTNO", ReceiptNo);
                        bRet = bRet && hSql.ExecuteNonQuery();

                        nCreditNewNo = nNewReceiptNo;
                        //Get the credited amount 
                        Decimal nCreditedPaidAmount = 0;
                        hSql.NewCommand("select a.TOTPAID from " + objUtil.getTable("CASHTRANSH") + " a where a.RECEIPTNO = ?");
                        hSql.Com.Parameters.AddWithValue("RECEIPTNO", nNewReceiptNo);
                        hSql.ExecuteReader();
                        if (hSql.Read()) nCreditedPaidAmount = hSql.Reader.GetDecimal(0);
                        //Insert new TRANSR row
                        int i = 0;
                        bool bNeedRounding = false;
                        while (i < Rows.Count)
                        {
                            hSql.NewCommand("insert into " + objUtil.getTable("CASHTRANSR") + "(RECEIPTNO,ROWNO,CASHFORM,CASHTYPE,QTY,AMOUNT,SALETYPE,FEE, CC_TERMINALID) " +
                                        " values(?,?,?,?,?,?,?,?,?)");

                            hSql.Com.Parameters.AddWithValue("RECEIPTNO", nNewReceiptNo);
                            hSql.Com.Parameters.AddWithValue("ROWNO", Rows[i].Rowno);
                            hSql.Com.Parameters.AddWithValue("CASHFORM", Rows[i].PaymentForm.Code);
                            hSql.Com.Parameters.AddWithValue("CASHTYPE", Rows[i].PaymentType.Code);
                            hSql.Com.Parameters.AddWithValue("QTY", Rows[i].Quantity*(-1));
                            hSql.Com.Parameters.AddWithValue("AMOUNT", Rows[i].Amount * (-1));
                            hSql.Com.Parameters.AddWithValue("SALETYPE", Rows[i].SaleType);
                            hSql.Com.Parameters.AddWithValue("FEE", Rows[i].AmountFee*(-1));
                            hSql.Com.Parameters.AddWithValue("CC_TERMINALID", Rows[i].CardType.Code);
                            bRet = bRet && hSql.ExecuteNonQuery();
                            Rows[i].CreditNewNo = nNewReceiptNo;
                            i++;
                        }
                        _log.Debug(strInvoiceModule);
                        if ((strInvoiceModule == "VA") || (strInvoiceModule == "SP") || (strInvoiceModule == "WO"))
                        {
                            hSql.NewCommand("update TEMPINV set PAYSUM=isnull(PAYSUM,0) + ? where RECNO=?  and UNITID = ?");
                            //hSql.NewCommand("update TEMPINV set PAYSUM= ? where RECNO=?  and UNITID = ?"); //and PAYDATE is not null
                            hSql.Com.Parameters.AddWithValue("PAYSUM", nCreditedPaidAmount);
                            hSql.Com.Parameters.AddWithValue("RECNO", strInvoiceNo);
                            hSql.Com.Parameters.AddWithValue("UNITID", objGlobal.CurrentSiteId);
                            hSql.ExecuteNonQuery();

                            hSql.NewCommand("update TEMPINV set PAYDATE=null where RECNO=? and PAYDATE is not null and UNITID = ?");                                                        
                            hSql.Com.Parameters.AddWithValue("RECNO", strInvoiceNo);
                            hSql.Com.Parameters.AddWithValue("UNITID", objGlobal.CurrentSiteId);
                            hSql.ExecuteNonQuery();

                            string strSql = "";
                            if (strInvoiceModule == "VA")
                                strSql = "update a set a.PAYD=b.PAYDATE,a.PAIDSUM=b.PAYSUM from " + objUtil.getTable("CBIL") + " a, TEMPINV b where a.CRECNO=b.RECNO and a.PAYD is not null and b.PAYDATE is null and b.RECNO=? and b.UNITID = ?";
                            if (strInvoiceModule == "SP")
                                strSql = "update a set a.PAIDDATE=b.PAYDATE,a.PAIDSUM=b.PAYSUM from " + objUtil.getTable("SBIL") + " a, TEMPINV b where a.SRECNO=b.RECNO and a.PAIDDATE is not null and b.PAYDATE is null and b.RECNO=? and b.UNITID = ?";
                            if (strInvoiceModule == "WO")
                                strSql = "update a set a.PAIDDATE=b.PAYDATE,a.PAIDSUM=b.PAYSUM from " + objUtil.getTable("GBIL") + " a, TEMPINV b where a.GRECNO=b.RECNO and a.PAIDDATE is not null and b.PAYDATE is null and b.RECNO=? and b.UNITID = ?";
                            hSql.NewCommand(strSql);
                            hSql.Com.Parameters.AddWithValue("RECNO", strInvoiceNo);
                            hSql.Com.Parameters.AddWithValue("UNITID", objGlobal.CurrentSiteId);
                            hSql.ExecuteNonQuery();
                        }
                    }
                    else
                        _log.Error("Invalid NUSEID assigned  to the CASHTYPE : " + Rows[0].PaymentType);
                    //
                    if (bRet)
                        hSql.Commit();
                    else
                        hSql.Rollback();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                bRet = false;
                hSql.Rollback();
                throw ex;
            }
            finally
            {
                hSql.Close();                           
            }
            _log.Debug("undoPayment << ");
            return bRet;

        }
        public bool saveReceipt(clsCashBox CashBox)
        {
            bool bRet = true;
            clsSqlFactory hSql = new clsSqlFactory();
            clsGlobalVariable objGlobal = new clsGlobalVariable();
            
            try
            {
                _log.Debug("saveReceipt >> SmanId = "+objGlobal.DefaultSManID);
                List<int> nVoucherIds = new List<int>();
                List<int> nReceiptNos = new List<int>();
                List<decimal> nReceiptTotals = new List<decimal>();
                int i = 0;
                int nNuseId=0;
                int nReceiptNo=0;
                bool bNeedRounding = false;
                decPaymentSum = getRowsTotal();
                while (i < Rows.Count)
                {
                    DataRow rowPaymentType = CashBox.CashTypes.Select("TYPE = '" + Rows[i].PaymentType.Code+"'")[0];
                    if (rowPaymentType != null)
                    {
                        if (CashBox.getSpecRound(rowPaymentType["TYPE"].ToString()) > 0) bNeedRounding = true;

                        nNuseId = CashBox.getVoucherIdOfType(Rows[i].PaymentType.Code, Rows[0].Amount > 0 ? false : true);
                        if (nVoucherIds.IndexOf(nNuseId) < 0)
                        {
                            nVoucherIds.Add(nNuseId);
                            nReceiptNo = getReceiptNUSE(hSql, nNuseId);
                            nReceiptNos.Add(nReceiptNo);
                            nReceiptTotals.Add(0);
                            // insert header

                            _log.Debug("receiptno: " + nReceiptNo.ToString());
                            hSql.NewCommand("insert into " + objUtil.getTable("CASHTRANSH") + "(RECEIPTNO,CASHBOXID,DEPARTMENT,SMANID,UNIT,MODULE," +
                                "INVOICECATEGORY,CUSTNO,LICNO,AMOUNTTOPAY,TOTPAID,CASHRETURNED," +
                                "TEXT,INSERTPC,UPDATED_DATE,UPDATED_BY,EXT_BILLD,EXT_ORDERNO,EXT_INVOICENO,INVOICENO,ORDERNO) " +
                                " values(?,?,?,?,?,?," +
                                "'1',?,?,?,?,0," +
                                "?,?,getdate(),?,?,?,?,?,?)");
                            hSql.Com.Parameters.AddWithValue("RECEIPTNO", nReceiptNo);
                            hSql.Com.Parameters.AddWithValue("CASHBOXID", Rows[i].CashBoxId);
                            if (strDeptId != "")
                            {
                                _log.Debug("Department = "+strDeptId);
                                hSql.Com.Parameters.AddWithValue("DEPARTMENT", strDeptId);
                            }
                            else
                            {
                               
                                String strDefDeptId = objConfig.getStringParam("CASHREG", "DEFDEPTID", "C3", "").Trim();
                                if (strDefDeptId == "") strDefDeptId = ConfigurationManager.AppSettings["DefDeptId"].ToString();
                                hSql.Com.Parameters.AddWithValue("DEPARTMENT", strDefDeptId);
                                _log.Debug("Nor department found from invoices, use default department " + strDeptId);
                            }
                            hSql.Com.Parameters.AddWithValue("SMANID", objGlobal.DefaultSManID);
                            hSql.Com.Parameters.AddWithValue("UNITID", objGlobal.CurrentSiteId);
                            hSql.Com.Parameters.AddWithValue("MODULE", strInvoiceModule);
                            hSql.Com.Parameters.AddWithValue("CUSTNO", strCustNo);
                            hSql.Com.Parameters.AddWithValue("LICNO", strLicno);
                            hSql.Com.Parameters.AddWithValue("AMOUNTTOPAY", decInvoiceSum);
                            hSql.Com.Parameters.AddWithValue("TOTPAID", 0);
                            hSql.Com.Parameters.AddWithValue("TEXT", strRemark);
                            hSql.Com.Parameters.AddWithValue("INSERTEDPC", CashBox.getClientPCName());
                            hSql.Com.Parameters.AddWithValue("UPDATED_BY", objGlobal.DMSFirstUserName);
                            hSql.Com.Parameters.AddWithValue("EXT_BILLD", dtInvoiceDate);
                            hSql.Com.Parameters.AddWithValue("EXT_ORDNO", strOrderNo);
                            hSql.Com.Parameters.AddWithValue("EXT_INVOICENO", strInvoiceNo);
                            if (InvoiceFlag != InvoiceFlags.CashRegisterFlag)
                            {
                                hSql.Com.Parameters.AddWithValue("INVOICENO", strInvoiceNo);
                                hSql.Com.Parameters.AddWithValue("ORDERNO", strOrderNo);
                            }
                            else
                            {
                                hSql.Com.Parameters.AddWithValue("INVOICENO", DBNull.Value);
                                hSql.Com.Parameters.AddWithValue("ORDERNO", DBNull.Value);
                            }
                            hSql.ExecuteNonQuery();

                        }
                        else
                        {
                            nReceiptNo = nReceiptNos[nVoucherIds.IndexOf(nNuseId)];
                        }
                        if (nReceiptNo > 0)
                        {
                            hSql.NewCommand("insert into " + objUtil.getTable("CASHTRANSR") + "(RECEIPTNO,ROWNO,CASHFORM,CASHTYPE,QTY,AMOUNT,SALETYPE,FEE,CC_TERMINALID) " +
                                " values(?,?,?,?,?,?,?,?,?)");
                            hSql.Com.Parameters.AddWithValue("RECEIPTNO", nReceiptNo);
                            hSql.Com.Parameters.AddWithValue("ROWNO", i + 1);
                            hSql.Com.Parameters.AddWithValue("CASHFORM", Rows[i].PaymentForm.Code);
                            hSql.Com.Parameters.AddWithValue("CASHTYPE", Rows[i].PaymentType.Code);
                            hSql.Com.Parameters.AddWithValue("QTY", Rows[i].Quantity);
                            hSql.Com.Parameters.AddWithValue("AMOUNT", Rows[i].Amount);
                            hSql.Com.Parameters.AddWithValue("SALETYPE", Rows[i].SaleType);
                            hSql.Com.Parameters.AddWithValue("FEE", Rows[i].AmountFee);
                            hSql.Com.Parameters.AddWithValue("CC_TERMINALID", Rows[i].CardType.Code);
                            hSql.ExecuteNonQuery();
                            Rows[i].Receiptno = nReceiptNo;
                            nReceiptTotals[nReceiptNos.IndexOf(nReceiptNo)] += Rows[i].Amount;
                        }
                        else
                        {
                            _log.Error("Fail to generate receipt number");
                            Exception ex = new Exception("Fail to generate receipt number, check the voucher id settings !");
                            throw ex;
                        }
                    }
                    else
                    {
                        _log.Error("PaymentType " + Rows[i].PaymentType.Code+" not found in the valid CashTypes !");
                    }
                    i++;
                }
                i = 0;
                while (i < nReceiptNos.Count)
                {
                    hSql.NewCommand("update "+objUtil.getTable("CASHTRANSH")+" set TOTPAID =? where RECEIPTNO=? ");
                    hSql.Com.Parameters.AddWithValue("TOTPAID", nReceiptTotals[i]);
                    hSql.Com.Parameters.AddWithValue("RECEIPTNO", nReceiptNos[i]);
                    hSql.ExecuteNonQuery();
                    i++;
                }
                _log.Debug(strInvoiceModule);
                if ((strInvoiceModule == "VA") || (strInvoiceModule == "SP") || (strInvoiceModule == "WO"))
                {
                   
                    hSql.NewCommand("update TEMPINV set PAYSUM=isnull(PAYSUM,0) + ? where RECNO=?  and UNITID = ?");
                    hSql.Com.Parameters.AddWithValue("PAYSUM", decPaymentSum);
                    hSql.Com.Parameters.AddWithValue("RECNO", strInvoiceNo);
                    hSql.Com.Parameters.AddWithValue("UNITID", objGlobal.CurrentSiteId);
                    hSql.ExecuteNonQuery();
                    
                    //
                    if (bNeedRounding)
                    {
                        hSql.NewCommand("update TEMPINV set PAYDATE=getdate() where RECNO=? and PAYDATE is null and abs(PAYSUM-INVSUM) < 3   and UNITID = ?");
                    }
                    else
                        hSql.NewCommand("update TEMPINV set PAYDATE=getdate() where RECNO=? and PAYDATE is null and abs(PAYSUM-INVSUM) = 0   and UNITID = ?");
                    hSql.Com.Parameters.AddWithValue("RECNO", strInvoiceNo);
                    hSql.Com.Parameters.AddWithValue("UNITID", objGlobal.CurrentSiteId);
                    hSql.ExecuteNonQuery();

                    string strSql = "";
                    if (strInvoiceModule == "VA")
                        strSql = "update a set a.PAYD=b.PAYDATE,    a.PAIDSUM=b.PAYSUM from " + objUtil.getTable("CBIL") + " a, TEMPINV b where a.CRECNO=b.RECNO "+
                    //" and a.PAYD is null and b.PAYDATE is not null "+
                    " and b.RECNO=? and b.UNITID = ?";
                    if (strInvoiceModule == "SP")
                        strSql = "update a set a.PAIDDATE=b.PAYDATE,a.PAIDSUM=b.PAYSUM from " + objUtil.getTable("SBIL") + " a, TEMPINV b where a.SRECNO=b.RECNO "+
                    //"and a.PAIDDATE is null and b.PAYDATE is not null "+
                    " and b.RECNO=? and b.UNITID = ?";
                    if (strInvoiceModule == "WO")
                        strSql = "update a set a.PAIDDATE=b.PAYDATE,a.PAIDSUM=b.PAYSUM from " + objUtil.getTable("GBIL") + " a, TEMPINV b where a.GRECNO=b.RECNO "+
                    //" and a.PAIDDATE is null and b.PAYDATE is not null"+
                    " and b.RECNO=? and b.UNITID = ?";
                    hSql.NewCommand(strSql);
                        hSql.Com.Parameters.AddWithValue("RECNO", strInvoiceNo);
                        hSql.Com.Parameters.AddWithValue("UNITID", objGlobal.CurrentSiteId);
                    hSql.ExecuteNonQuery();                
                }
                
                hSql.Commit();
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                bRet = false;
                hSql.Rollback();
                throw ex;
            }
            finally {
                hSql.Close();
            }
            return bRet;
        }
        public decimal getRowsTotal()
        {
            decimal nRet = 0;
            int i = 0;
            while (i < Rows.Count)
            {                
                    nRet+= Rows[i].Amount;
                i++;
            }
            decPaymentSum = nRet;
            return nRet;
        }
        public void addReturnRow(decimal dec, string strCashType, string strCashBoxId,string strSaleType)
        {
            int i = 0;
            while (i < Rows.Count)
            {
                if (Rows[i].PaymentForm.Code == "CASH" && Rows[i].PaymentForm.Code != strRoundingCashTypeNegative && Rows[i].PaymentForm.Code != strRoundingCashTypePositive)
                {
                    //found an existing cash line
                    Rows[i].Amount += dec;
                    Rows[i].AmountPaid += dec;
                }
                i++;
            }
            //not found, add a new cash line
            if (i == Rows.Count)
            {
               
                InvoiceRow aRow = new InvoiceRow();
                aRow.PaymentForm.Code = "CASH";
                aRow.PaymentType.Code = strCashType;
                aRow.CashBoxId = strCashBoxId;
                aRow.AmountPaid = dec;
                aRow.AmountChange = 0;
                aRow.Amount = dec;
                aRow.Quantity = 0;
                aRow.SaleType = strSaleType;
                Rows.Add(aRow);
            }
        }
        public void addRoundingRow(clsCashBox CashBox,string strCashFormName, string strNegativeRoundingCashTypeName,string strPositiveRoundingCashTypeName, string strCashBoxId)
        {
            int i = 0;
            int nIndexRounding = -1;
            decimal nAmount = 0;
            while (i < Rows.Count)
            {
                if (Rows[i].PaymentForm.Code == "CASH" )
                {
                    if (Rows[i].PaymentType.Code == strRoundingCashTypePositive || Rows[i].PaymentType.Code == strRoundingCashTypeNegative)
                        nIndexRounding = i;
                    else
                        nAmount = nAmount + Rows[i].Amount;
                }
                i++;
            }
            //Remove the existing rounding line
             if (nIndexRounding >=0) Rows.RemoveAt(nIndexRounding);
            //Add a new rounding line
            decimal remainAmount = CashBox.roundToLocalCurrency(nAmount, strRoundingCashTypePositive) - nAmount;
            if (remainAmount != 0)
            {
                InvoiceRow roundingRow = new InvoiceRow();
                roundingRow.PaymentForm.Code = "CASH";
                roundingRow.PaymentForm.Name = strCashFormName;
                string strSaleType = "";
                if (remainAmount > 0)
                {
                    roundingRow.PaymentType.Code = strRoundingCashTypePositive;
                    roundingRow.PaymentType.Name = strPositiveRoundingCashTypeName;
                    strSaleType = CashBox.getSaleTypeOfType(strRoundingCashTypePositive);
                }
                else
                {
                    roundingRow.PaymentType.Code = strRoundingCashTypeNegative;
                    roundingRow.PaymentType.Name = strNegativeRoundingCashTypeName;
                    strSaleType = CashBox.getSaleTypeOfType(strRoundingCashTypeNegative);
                }
                roundingRow.CashBoxId = strCashBoxId;
                roundingRow.AmountPaid = remainAmount;
                roundingRow.AmountChange = 0;
                roundingRow.Amount = remainAmount;
                roundingRow.Quantity = 0;
                roundingRow.SaleType = strSaleType;
                Rows.Add(roundingRow);
            }
        }
        public bool loadRows()
        {
            Rows.Clear();
            if ((ReceiptNo > 0) && (Paid==true))
            {
               
                clsSqlFactory hSql = new clsSqlFactory();
                hSql.NewCommand("select CASHFORM,CASHTYPE,isnull(QTY,0),isnull(AMOUNT,0),SALETYPE,ROWNO, isnull(b.C2,'') as CASHFORM_NAME,"
                    + " isnull(c.C2,'') as CASHTYPE_NAME, isnull(FEE,0) as FEE, isnull(CC_TERMINALID,'') as CARDTYPE, isnull(d.C2,'') as CARDNAME from " + objUtil.getTable("CASHTRANSR")
                    + " left join " + objUtil.getTable("CORW") + " b on b.CODAID = 'CASHFORM' and b.C1=CASHFORM "
                    + " left join " + objUtil.getTable("CORW") + " c on c.CODAID = 'CASHTYPE' and c.C1=CASHTYPE "
                    + " left join " + objUtil.getTable("CORW") + " d on d.CODAID = 'CASHCCTYPE' and d.C1=CC_TERMINALID "
                    + " where RECEIPTNO=?");
                hSql.Com.Parameters.AddWithValue("RECEIPTNO", ReceiptNo);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    InvoiceRow aRow = new InvoiceRow();
                    aRow.CashBoxId = strCashBoxId;
                    aRow.Receiptno = ReceiptNo;
                    aRow.PaymentForm.Code = hSql.Reader.GetString(0);
                    aRow.PaymentType.Code = hSql.Reader.GetString(1);
                    aRow.Quantity = hSql.Reader.GetDecimal(2);
                    aRow.Amount = hSql.Reader.GetDecimal(3);
                    aRow.AmountPaid = aRow.Amount;
                    aRow.AmountChange = 0;
                    aRow.SaleType = hSql.Reader.GetString(4);
                    aRow.Rowno = hSql.Reader.GetInt32(5);
                    aRow.PaymentForm.Name = aRow.PaymentForm.Code+ "="+hSql.Reader.GetString(6);
                    aRow.PaymentType.Name = aRow.PaymentType.Code+ "="+hSql.Reader.GetString(7);
                    aRow.AmountFee = hSql.Reader.GetDecimal(hSql.Reader.GetOrdinal("FEE"));
                    aRow.CardType.Code = hSql.Reader.GetString(hSql.Reader.GetOrdinal("CARDTYPE"));
                    aRow.CardType.Name = aRow.CardType.Code + "=" + hSql.Reader.GetString(hSql.Reader.GetOrdinal("CARDNAME"));
                    Rows.Add(aRow);
                }
                hSql.Close();
            }
            return true;
        }
        public static List<Invoice> searchInvoices(string namephrase, bool isPaid,bool isIncludeCredit, int SearchFlags)
        {
            List<Invoice> Results = new List<Invoice>();
            clsBaseUtility locObjUtil = new clsBaseUtility();
            clsGlobalVariable locObjGlobal = new clsGlobalVariable();
            clsSqlFactory hSql = new clsSqlFactory();

            try
            {
                string searchString = namephrase;

                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex(@"[ ]{2,}", options);
                searchString = regex.Replace(searchString, @" ");

                string[] words = searchString.Split(new char[] { ' ' });
              
                int i = 0;
                string strOrder = "";
                string strSql = "";
                string strCUST = locObjUtil.getTable("CUST");
                string strASVIEW_CAREG_UNPAID_INVOICE = strCUST == "CUST" ? "ASVIEW_CAREG_UNPAID_INVOICE" : "ASVIEW_CAREG_UNPAID_INVOICE" + locObjGlobal.CurrentSiteId;
                if (SearchFlags >0)
                {
                strOrder = "";
                strSql = "select top " + ConfigurationManager.AppSettings["SearchResultNumber"].ToString() + " a.ORDNO,isnull(a.CUSTNO,''),isnull(a.LNAME,''),isnull(a.FNAME,''),a.BILLD,isnull(a.RECNO,''),a.INVSUM,a.APPAREA, isnull(a.DEPT,'') as DEPT,isnull(a.PAYSUM,0) from " + strASVIEW_CAREG_UNPAID_INVOICE + " a ";
                
                while (i < words.Length)
                {
                    if (words[i] != "")
                    {
                        strSql += " inner join containstable(" + strASVIEW_CAREG_UNPAID_INVOICE + ", *, '\"" + words[i] + "*\"' ) as T" + i.ToString() + " on a.INVID = T" + i.ToString() + ".[KEY]  ";
                        if (i > 0)
                        {
                            strOrder += "+";
                        }
                        strOrder += "T" + i.ToString() + ".RANK";
                    }
                    i++;
                }

                //if (strOrder!="") strOrder = " order by " + strOrder + " desc, a.BILLD desc, a.RECNO desc ";
                strOrder = " order by a.BILLD desc, a.RECNO desc ";
                
                strSql = strSql + " where a.UNITID =? and (1=0 ";

                if ((SearchFlags & InvoiceFlags.VehicleSalesFlag) == InvoiceFlags.VehicleSalesFlag) strSql = strSql + " or APPAREA = 'C' ";
                if ((SearchFlags & InvoiceFlags.SparePartSalesFlag) == InvoiceFlags.SparePartSalesFlag) strSql = strSql + " or APPAREA = 'S' ";
                if ((SearchFlags & InvoiceFlags.WorkshopFlag) == InvoiceFlags.WorkshopFlag) strSql = strSql + " or APPAREA = 'G' ";
                  
                 //  strSql += ") and not exists( select 1 from TEMPINV x where x.UNITID = a.UNITID and a.RECNO = x.CRERECNO) " + strOrder;
                 strSql += ") and ( exists (select 1 from ALL_CBIL bil where bil.CRECNO = a.RECNO ) or exists (select 1 from ALL_GBIL bil where bil.GRECNO = a.RECNO ) or exists (select 1 from ALL_SBIL bil where bil.SRECNO = a.RECNO ) ) ";
                 if (!isIncludeCredit)
                 {
                     //Exclude credited 
                     strSql += " and not exists( select 1 from TEMPINV x where x.UNITID = a.UNITID and a.RECNO = x.CRERECNO) ";
                     //Exclude credit 
                     strSql += " and not exists( select 1 from TEMPINV x where x.UNITID = a.UNITID and a.RECNO = x.RECNO and x.CREDIT = 1 ) ";
                 }
                    //
                 strSql  += strOrder;
                
                _log.Debug(strSql);

                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("UNITID",locObjGlobal.CurrentSiteId);
                if (hSql.ExecuteReader())
                {
                    while (hSql.Read())
                    {
                        Invoice Inv = new Invoice();
                        Inv.strOrderNo = hSql.GetString(0).ToString();
                        Inv.strCustNo = hSql.GetString(1).ToString();
                        Inv.strCustomerName = hSql.GetString(2).ToString() + " " + hSql.GetString(3).ToString();
                        Inv.strStatus = locObjUtil.Localization.getMsgString("CASHREG_10");//Unpaid
                        Inv.strInvoiceNo = hSql.GetString(5);
                        Inv.dtInvoiceDate = hSql.GetDateTime(4);//.ToString("yyyy.MM.dd"));
                        Inv.decInvoiceSum = hSql.Reader.GetDecimal(6) - hSql.Reader.GetDecimal(9);//.ToString("N6").TrimEnd('0').Trim(',');
                        Inv.Paid = false;
                        Inv.strDeptId = hSql.GetString(8);
                        Inv.decInvoiceSumOrig = hSql.Reader.GetDecimal(6);
                        switch (hSql.GetString(7))
                        {
                            case "C":
                                {
                                    Inv.InvoiceFlag = InvoiceFlags.VehicleSalesFlag;
                                    Inv.strType = locObjUtil.Localization.getMsgString("CASHREG_07");//"Vehicle sales"
                                    Inv.strInvoiceModule = "VA";
                                    break;
                                }
                            case "G":
                                {
                                    Inv.InvoiceFlag = InvoiceFlags.WorkshopFlag;
                                    Inv.strType = locObjUtil.Localization.getMsgString("CASHREG_09");//"Workshop"
                                    Inv.strInvoiceModule = "WO"; 
                                    break;
                                }
                            case "S":
                                {
                                    Inv.InvoiceFlag = InvoiceFlags.SparePartSalesFlag;
                                    Inv.strType = locObjUtil.Localization.getMsgString("CASHREG_06");//"Spare part sales"
                                    Inv.strInvoiceModule = "SP"; 
                                    break;
                                }
                        }
                        Results.Add(Inv);
                    }
                }
                }
                    //--------------------------------------------------------------------Paid part------------------------------------------------
                    if (isPaid == true )
                    {
                        //                          0           1                   2                   3            4            5                 6          7                8                9       10
                        strSql = "select top " + ConfigurationManager.AppSettings["SearchResultNumber"].ToString() + " a.ORDNO,isnull(a.CUSTNO,''),isnull(a.LNAME,'') as LNAME,isnull(a.FNAME,''),a.BILLD,isnull(a.RECNO,''),isnull(a.AMOUNTTOPAY,0),isnull(a.TOTPAID,0),isnull(a.RECEIPTNO,''),a.PAYDATE " +
                            " , a.CASHBOXID,a.MODULE,a.DEPARTMENT,b.TEXT,isnull(a.LICNO,'') as LICNO,isnull(b.CREDITNEWNO,0) as CREDITNEWNO,ISNULL(b.CREDITNOTE,0) as CREDITNOTE, ISNULL(b.CREDITOFNO,0) as CREDITOFNO from " + locObjUtil.getTable("ASVIEW_CAREG_PAID_INVOICE") + " a inner join " + locObjUtil.getTable("CASHTRANSH") + " b on a._OID=b._OID ";

                 i = 0;
                        strOrder = "";
                        while (i < words.Length)
                        {
                            if (words[i] != "")
                            {
                                strSql += " inner join containstable(ASVIEW_CAREG_PAID_INVOICE"+locObjGlobal.CurrentSiteId+", *, '\"" + words[i] + "*\"' ) as T" + i.ToString() + " on a._OID = T" + i.ToString() + ".[KEY]  ";
                                if (i > 0)
                                {
                                    strOrder += "+";
                                }
                                strOrder += "T" + i.ToString() + ".RANK";
                            }
                            i++;
                        }
                        //if (strOrder!="") strOrder = " order by " + strOrder + " desc, a.PAYDATE desc";
                        strOrder = " order by a.BILLD desc, a.RECNO desc,a.PAYDATE desc ";
                        strSql = strSql + " where (1=0  ";
                        if ((SearchFlags & InvoiceFlags.VehicleSalesFlag) == InvoiceFlags.VehicleSalesFlag) strSql = strSql + " or a.MODULE = 'VA' ";
                        if ((SearchFlags & InvoiceFlags.SparePartSalesFlag) == InvoiceFlags.SparePartSalesFlag) strSql = strSql + " or a.MODULE = 'SP' ";
                        if ((SearchFlags & InvoiceFlags.WorkshopFlag) == InvoiceFlags.WorkshopFlag) strSql = strSql + " or a.MODULE = 'WO' ";
                        if ((SearchFlags & InvoiceFlags.CashRegisterFlag) == InvoiceFlags.CashRegisterFlag) strSql = strSql + " or a.MODULE in ('CU','SU','CR') ";

                        strSql += ") " + strOrder;
                        _log.Debug(strSql);

                        hSql.NewCommand(strSql);

                        if (hSql.ExecuteReader())
                        {
                            while (hSql.Read())
                            {
                                //try
                                {

                                    Invoice Inv = new Invoice();
                                    Inv.strOrderNo = hSql.GetString(0).ToString();
                                    Inv.strCustNo = hSql.GetString(1).ToString();
                                    Inv.strCustomerName = hSql.GetString(2).ToString() + " " + hSql.GetString(3).ToString();
                                    Inv.strStatus = locObjUtil.Localization.getMsgString("CASHREG_11");//Paid
                                    Inv.strInvoiceNo = hSql.GetString(5);
                                    Inv.ReceiptNo = hSql.Reader.GetInt32(8);
                                    Inv.dtInvoiceDate = hSql.GetDateTime(4);//.ToString("yyyy.MM.dd"));
                                    Inv.dtPaymentDate = hSql.GetDateTime(9);//.ToString("yyyy.MM.dd"));
                                    Inv.decInvoiceSumOrig = hSql.Reader.GetDecimal(6);//.ToString("N6").TrimEnd('0').Trim(',');
                                    Inv.decInvoiceSum = 0;
                                    Inv.decPaymentSum = hSql.Reader.GetDecimal(7);//.ToString("N6").TrimEnd('0').Trim(',');
                                    Inv.strCashBoxId = hSql.Reader.GetString(10);
                                    Inv.strInvoiceModule = hSql.GetString(11);

                                    Inv.strDeptId = hSql.GetString(12);
                                    Inv.strRemark = hSql.GetString(13);
                                    Inv.strLicno = hSql.GetString(14);
                                    Inv.nCreditNewNo = hSql.Reader.GetInt32(15);
                                    Inv.nCreditNote = hSql.Reader.GetInt32(16);
                                    Inv.nCrediOfNo = hSql.Reader.GetInt32(17);
                                    switch (Inv.strInvoiceModule)
                                    {
                                        case "VA":
                                            {
                                                Inv.InvoiceFlag = InvoiceFlags.VehicleSalesFlag;
                                                Inv.strType = locObjUtil.Localization.getMsgString("CASHREG_11"); break;//"Vehicle sales"
                                            }
                                        case "WO":
                                            {
                                                Inv.InvoiceFlag = InvoiceFlags.WorkshopFlag;
                                                Inv.strType = locObjUtil.Localization.getMsgString("CASHREG_09"); break;//"Workshop"
                                            }
                                        case "SP":
                                            {
                                                Inv.InvoiceFlag = InvoiceFlags.SparePartSalesFlag;
                                                Inv.strType = locObjUtil.Localization.getMsgString("CASHREG_06"); break;//"Spare part sales"
                                            }
                                        default:
                                            {
                                                Inv.InvoiceFlag = InvoiceFlags.CashRegisterFlag;
                                                Inv.strType = locObjUtil.Localization.getMsgString("CASHREG_08"); break;//"Cash Register"
                                            }
                                    }
                                    Inv.Paid = true;
                                    Results.Add(Inv);
                                }                               
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
               
                throw ex;
            }
            finally
            {
                hSql.Close();
            }
            return Results;
        }
    }
}
