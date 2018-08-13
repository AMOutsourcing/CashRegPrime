using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using nsBaseClass;
using log4net;
using System.Data;
using System.Security.Principal;
using System.Data.Odbc;

using System.Configuration;
using System.IO;
using System.Windows.Forms;
namespace CashRegPrime.Model
{
    public class clsCashBox
    {
        public DataTable CashBoxes = new DataTable();
        public DataTable CashTypes = new DataTable();
        public DataTable CardTypes = new DataTable();

        clsGlobalVariable objGlobal = new clsGlobalVariable();
        clsBaseUtility objUtil = new clsBaseUtility();
        clsAppConfig objAppConfig = new clsAppConfig();
        static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int getIndexByCashBoxId(string strCashBoxId)
        {
            int nRet = -1;
            int i = 0;
            while (i<CashBoxes.Rows.Count)
                {
                if (CashBoxes.Rows[i]["CASHBOXID"].ToString()==strCashBoxId) return i;
                    i++;
                }
            return nRet;
        }
            public string getClientPCName()
        {
            string strRet = Environment.MachineName.ToString();
            if (Environment.GetEnvironmentVariable("CLIENTNAME") != null)
                strRet = Environment.GetEnvironmentVariable("CLIENTNAME").ToString();
            return strRet;
        }

        public decimal getClosingBalance(string strCashBoxId, out decimal nQty)
        {
            decimal nRet = 0;
            nQty = 0;
            clsSqlFactory hSql = new clsSqlFactory();
            hSql.NewCommand("select top 1 isnull(b.AMOUNT,0),a.UPDATED_DATE, isnull(b.QTY,0) from "+objUtil.getTable("CASHREG")+" a," +
                    objUtil.getTable("CASHREGROW")+" b where a.ROWID = b.CASHREGROWID and a.CASHBOXID = ? and a.EVENT = 'O' " +
                    " order by a.UPDATED_DATE desc");
            hSql.Com.Parameters.AddWithValue("CASHBOXID", strCashBoxId);
            hSql.ExecuteReader();
            hSql.Read();

            DateTime dtLastOpen = hSql.Reader.GetDateTime(1);
            decimal nOpeningBalance = hSql.Reader.GetDecimal(0);
            decimal nOpeningQty = hSql.Reader.GetDecimal(2);
            hSql.NewCommand("select isnull(sum(b.AMOUNT),0) as TOTAMOUNT, isnull(sum(b.QTY),0)  from "+objUtil.getTable("CASHTRANSH")+" a, "+objUtil.getTable("CASHTRANSR")+" b" +
                          " where a.CASHBOXID =? and a.RECEIPTNO = b.RECEIPTNO and a.UPDATED_DATE >=? ");
            hSql.Com.Parameters.AddWithValue("CASHBOXID", strCashBoxId);
            hSql.Com.Parameters.AddWithValue("LASTOPEN", dtLastOpen);
            hSql.ExecuteReader();
            hSql.Read();
            nQty = hSql.Reader.GetDecimal(1) + nOpeningQty;
            nRet = hSql.Reader.GetDecimal(0) + nOpeningBalance;
            return nRet;
        }
       
        public bool closeCashBox(string strCashBoxId)
        {
            _log.Debug("closeCashBox >> " + strCashBoxId);
            bool bRet = true;
            clsSqlFactory hSql = new clsSqlFactory();
            clsAppConfig objAppConfig = new clsAppConfig();

            try
            {
                int NUSEID = objAppConfig.getNumberParam("CASHREG", "DEPRECNO", "V1", "");
                DataRow cashbox = CashBoxes.Select(("CASHBOXID = '" + strCashBoxId+"'"))[0];
                int EOD_NUSEID = Int32.Parse(cashbox["EODNUSEID"].ToString());

                hSql.NewCommand("select ISOPEN from "+objUtil.getTable("CASHBOX")+" where CASHBOXID=? and ISOPEN=1");
                hSql.Com.Parameters.AddWithValue("CASHBOXID", strCashBoxId);
                bRet = bRet && hSql.ExecuteReader();
                if (hSql.Read())
                {
                    hSql.NewCommand("update "+objUtil.getTable("NUSE")+" set RECNO=RECNO+1 where NUSEID=?");
                    hSql.Com.Parameters.AddWithValue("NUSEID", NUSEID);
                    bRet = bRet && hSql.ExecuteNonQuery();

                    hSql.NewCommand("select RECNO from "+objUtil.getTable("NUSE")+" where NUSEID=?");
                    hSql.Com.Parameters.AddWithValue("NUSEID", NUSEID);
                    bRet = bRet && hSql.ExecuteReader();
                    if (hSql.Read())
                    {
                        int RECEIPTNO = hSql.Reader.GetInt32(0);

                        //EOD_NUSEID
                        hSql.NewCommand("update " + objUtil.getTable("NUSE") + " set RECNO=RECNO+1 where NUSEID=?");
                        hSql.Com.Parameters.AddWithValue("NUSEID", EOD_NUSEID);
                        hSql.ExecuteNonQuery();

                        hSql.NewCommand("select RECNO from " + objUtil.getTable("NUSE") + " where NUSEID=?");
                        hSql.Com.Parameters.AddWithValue("NUSEID", EOD_NUSEID);
                        bRet = bRet && hSql.ExecuteReader();

                        if (hSql.Read())
                        {

                            int EOD_RECEIPTNO = hSql.Reader.GetInt32(0);
                            //
                            hSql.NewCommand("update " + objUtil.getTable("CASHBOX") + " set ISOPEN=0 where CASHBOXID=?");
                            hSql.Com.Parameters.AddWithValue("CASHBOXID", strCashBoxId);
                            hSql.ExecuteNonQuery();
                            hSql.NewCommand("insert into " + objUtil.getTable("CASHREG") + "(CASHBOXID,SMANID,EVENT,UPDATED_DATE,UPDATED_BY,RECEIPTNO,EOD_RECEIPTNO) values(?,?,'C',getdate(),?,?,?)");
                            hSql.Com.Parameters.AddWithValue("CASHBOXID", strCashBoxId);
                            hSql.Com.Parameters.AddWithValue("SMANID", objGlobal.DefaultSManID);
                            hSql.Com.Parameters.AddWithValue("UPDATED_BY", objGlobal.DMSFirstUserName);
                            hSql.Com.Parameters.AddWithValue("RECEIPTNO", RECEIPTNO);
                            hSql.Com.Parameters.AddWithValue("EOD_RECEIPTNO", EOD_RECEIPTNO);
                            hSql.ExecuteNonQuery();
                            hSql.Com.Parameters.Clear();
                            hSql.NewCommand("SELECT @@IDENTITY");
                            bRet = bRet && hSql.ExecuteReader();
                            hSql.Read();
                            int nCashRegRowId = hSql.Reader.GetInt32(0);

                            hSql.NewCommand("select top 1 isnull(b.AMOUNT,0),a.UPDATED_DATE, isnull(b.QTY,0) from " + objUtil.getTable("CASHREG") + " a," +
                            " CASHREGROW b where a.ROWID = b.CASHREGROWID and a.CASHBOXID = ? and a.EVENT = 'O' " +
                            " order by a.UPDATED_DATE desc");
                            hSql.Com.Parameters.AddWithValue("CASHBOXID", strCashBoxId);
                            bRet = bRet && hSql.ExecuteReader();
                            hSql.Read();

                            DateTime dtLastOpen = hSql.Reader.GetDateTime(1);
                            decimal nOpeningBalance = hSql.Reader.GetDecimal(0);

                            decimal nOpeningQty = hSql.Reader.GetDecimal(2);

                            hSql.NewCommand("insert into " + objUtil.getTable("CASHREGROW") + "(CASHREGROWID,ROWNO,CASHFORM,CASHTYPE,AMOUNT,QTY,UPDATED_DATE,UPDATED_BY) " +
                                " select ?, row_number() over (partition by getdate() order by getdate()),CASHFORM,CASHTYPE,isnull(TOTAMOUNT,0),isnull(TOTQTY,0),getdate(),? from " +
                                "(select b.CASHFORM,case b.CASHFORM when 'CASH' then '" + ConfigurationSettings.AppSettings["DefaultCashType"] + "' else b.CASHTYPE end as CASHTYPE,sum(b.AMOUNT) as TOTAMOUNT,sum(b.QTY) as TOTQTY from " + objUtil.getTable("CASHTRANSH") + " a, " + objUtil.getTable("CASHTRANSR") + " b" +
                                   " where a.CASHBOXID =? and a.RECEIPTNO = b.RECEIPTNO and a.UPDATED_DATE >=? " +
                                " group by b.CASHFORM,case b.CASHFORM when 'CASH' then '" + ConfigurationSettings.AppSettings["DefaultCashType"] + "' else b.CASHTYPE end ) x");
                            hSql.Com.Parameters.AddWithValue("CASHREGROWID", nCashRegRowId);
                            hSql.Com.Parameters.AddWithValue("UPDATED_BY", objGlobal.DMSFirstUserName);

                            hSql.Com.Parameters.AddWithValue("CASHBOXID", strCashBoxId);
                            hSql.Com.Parameters.AddWithValue("LASTOPEN", dtLastOpen);

                            bRet = bRet && hSql.ExecuteNonQuery();

                            hSql.NewCommand("select 1 from " + objUtil.getTable("CASHREGROW") + " where CASHREGROWID=? and CASHFORM in ('CASH','CURRENCY')");
                            hSql.Com.Parameters.AddWithValue("CASHREGROWID", nCashRegRowId);
                            bRet = bRet && hSql.ExecuteReader();
                            if (hSql.Read())
                            {
                                hSql.NewCommand("update " + objUtil.getTable("CASHREGROW") + " set AMOUNT=AMOUNT+?, QTY=QTY+? where CASHREGROWID=? and CASHFORM in ('CASH','CURRENCY') ");
                                hSql.Com.Parameters.AddWithValue("OPENBALANCE", nOpeningBalance);
                                hSql.Com.Parameters.AddWithValue("OPENQTY", nOpeningQty);
                                hSql.Com.Parameters.AddWithValue("CASHREGROWID", nCashRegRowId);
                                bRet = bRet && hSql.ExecuteNonQuery();
                            }
                            else
                            {
                                hSql.NewCommand("insert into " + objUtil.getTable("CASHREGROW") + "(CASHREGROWID,ROWNO,CASHFORM,CASHTYPE,AMOUNT,QTY,UPDATED_DATE,UPDATED_BY) values(" +
                                                       "  ?, (select isnull(max(ROWNO),0)+1 from CASHREGROW where CASHREGROWID=? ),'CASH',?,?,?,getdate(),? )");
                                hSql.Com.Parameters.AddWithValue("CASHREGROWID", nCashRegRowId);
                                hSql.Com.Parameters.AddWithValue("CASHREGROWID2", nCashRegRowId);
                                hSql.Com.Parameters.AddWithValue("CASHTYPE", ConfigurationSettings.AppSettings["DefaultCashType"]);

                                hSql.Com.Parameters.AddWithValue("AMOUNT", nOpeningBalance);
                                hSql.Com.Parameters.AddWithValue("QTY", nOpeningQty);

                                hSql.Com.Parameters.AddWithValue("UPDATED_BY", objGlobal.DMSFirstUserName);

                                hSql.Com.Parameters.AddWithValue("CASHBOXID", strCashBoxId);
                                bRet = bRet && hSql.ExecuteNonQuery();

                            }
                            //Update CASHTRANSH.EOD_RECEIPTNO

                            hSql.NewCommand("update " + objUtil.getTable("CASHTRANSH") + " set EOD_RECEIPTNO =? where CASHBOXID =? and EOD_RECEIPTNO is null and UPDATED_DATE >=?");
                            hSql.Com.Parameters.AddWithValue("EOD_RECEIPTNO", EOD_RECEIPTNO);
                            hSql.Com.Parameters.AddWithValue("CASHBOXID", strCashBoxId);
                            hSql.Com.Parameters.AddWithValue("LASTOPEN", dtLastOpen);
                            bRet = bRet && hSql.ExecuteNonQuery();
                            //
                            hSql.NewCommand("delete from " + objUtil.getTable("CASHBOXPC") + " where CASHBOXID = ?");
                            hSql.Com.Parameters.AddWithValue("CASHBOXID", strCashBoxId);
                            bRet = bRet && hSql.ExecuteNonQuery();
                        }
                        else
                        {
                            bRet = false;
                            throw new Exception("Invalid EODNUSEID (CASHBOXID.V3) ): " + NUSEID);
                        }
                    }
                    else
                    {

                        bRet = false;
                        throw new Exception("Invalid NUSEID (CASHREG.DEPRECNO): " + NUSEID);
                    }
                        
                }
                else
                {
                    bRet = false;
                }
                if (bRet)
                    hSql.Commit();
                else
                    hSql.Rollback();
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                hSql.Rollback();
                bRet = false;
                throw ex;
            }
            finally
            {
                hSql.Close();
            }
            _log.Debug("closeCashBox >> " + bRet.ToString());
            return bRet;
        }
        public bool openCashBox(string strCashBoxId,decimal nOpeningBalance, decimal nQty)
        {
            bool bRet = true;
            _log.Debug("openCashBox >> SmanId = "+objGlobal.DefaultSManID+", UserName  = " +objGlobal.DMSFirstUserName);
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {

                hSql.NewCommand("select ISOPEN from "+objUtil.getTable("CASHBOX")+" where CASHBOXID=?");
                hSql.Com.Parameters.AddWithValue("CASHBOXID", strCashBoxId);
                hSql.ExecuteReader();
                if (hSql.Read())
                {
                    if (hSql.Reader.GetInt16(0) == 1)
                    {
                        bRet = false;
                    }
                    else
                    {
                        hSql.NewCommand("update "+objUtil.getTable("CASHBOX")+" set ISOPEN=1 where CASHBOXID=?");
                        hSql.Com.Parameters.AddWithValue("CASHBOXID", strCashBoxId);
                        bRet = hSql.ExecuteNonQuery();
                    }
                }
                else
                {
                    hSql.NewCommand("insert into "+objUtil.getTable("CASHBOX")+"(CASHBOXID,ISOPEN) values(?,1)");
                    hSql.Com.Parameters.AddWithValue("CASHBOXID", strCashBoxId);
                    bRet = hSql.ExecuteNonQuery();
                }
                string strComputerName = getClientPCName();
                hSql.NewCommand("select 1 from "+objUtil.getTable("CASHBOXPC")+" where CASHBOXID=? and PC=?");
                hSql.Com.Parameters.AddWithValue("CASHBOXID", strCashBoxId);
                hSql.Com.Parameters.AddWithValue("PC", strComputerName);
                bRet =bRet && hSql.ExecuteReader();
                //clsGlobalVariable objGlobal = new clsGlobalVariable();
                if (bRet)
                {
                    if (hSql.Read())
                    {
                        hSql.NewCommand("update "+objUtil.getTable("CASHBOXPC")+" set UPDATED_DATE=getdate(),UPDATED_BY=? where CASHBOXID=? and PC=?");
                        hSql.Com.Parameters.AddWithValue("UPDATED_BY",objGlobal.DMSFirstUserName );
                        hSql.Com.Parameters.AddWithValue("CASHBOXID", strCashBoxId);
                        hSql.Com.Parameters.AddWithValue("PC", strComputerName);
                        bRet = bRet && hSql.ExecuteNonQuery();
                    }
                    else
                    {
                        hSql.NewCommand("insert into "+objUtil.getTable("CASHBOXPC")+"(CASHBOXID,PC,UPDATED_DATE,UPDATED_BY) values(?,?,getdate(),?)");
                        hSql.Com.Parameters.AddWithValue("CASHBOXID", strCashBoxId);
                        hSql.Com.Parameters.AddWithValue("PC", strComputerName);
                        hSql.Com.Parameters.AddWithValue("UPDATED_BY", objGlobal.DMSFirstUserName);
                       
                        bRet = bRet && hSql.ExecuteNonQuery();
                    }
                }
                if (bRet)
                {
                    hSql.NewCommand("insert into "+objUtil.getTable("CASHREG")+"(CASHBOXID,SMANID,EVENT,UPDATED_DATE,UPDATED_BY) values(?,?,'O',getdate(),?)");
                    hSql.Com.Parameters.AddWithValue("CASHBOXID", strCashBoxId);
                    hSql.Com.Parameters.AddWithValue("SMANID", objGlobal.DefaultSManID);
                    hSql.Com.Parameters.AddWithValue("UPDATED_BY", objGlobal.DMSFirstUserName);
                    hSql.ExecuteNonQuery();
                    hSql.Com.Parameters.Clear();
                    hSql.NewCommand("SELECT @@IDENTITY");
                    hSql.ExecuteReader();
                    hSql.Read();
                   
                        int nCashRegRowId = hSql.Reader.GetInt32(0);
                        hSql.NewCommand("insert into "+objUtil.getTable("CASHREGROW")+"(CASHREGROWID,ROWNO,CASHFORM,CASHTYPE,AMOUNT,UPDATED_DATE,UPDATED_BY,QTY) values(?,1,'CASH',?,?,getdate(),?,?)");
                        hSql.Com.Parameters.AddWithValue("CASHREGROWID", nCashRegRowId);
                        hSql.Com.Parameters.AddWithValue("CASHTYPE", ConfigurationSettings.AppSettings["DefaultCashType"]);
                        hSql.Com.Parameters.AddWithValue("AMOUNT", nOpeningBalance);
                        hSql.Com.Parameters.AddWithValue("UPDATED_BY", objGlobal.DMSFirstUserName);
                        hSql.Com.Parameters.AddWithValue("QTY", nQty);
                        hSql.ExecuteNonQuery();
                    
                   
                }
                if (bRet)
                    hSql.Commit();
                else
                    hSql.Rollback();
            }
            catch (Exception ex)
            {
                hSql.Rollback();
                _log.Error(ex.ToString());
                bRet = false;
            }
            finally
            {
                hSql.Close();
            }
            return bRet;
        }
        public decimal getPreviousBalance(string strCashBoxId, out decimal nQty)
        {
            decimal nRet = 0;
            nQty = 0;
            clsSqlFactory hSql = new clsSqlFactory();
            hSql.NewCommand("select top 1 isnull(b.AMOUNT,0), isnull(b.QTY,0) from "+objUtil.getTable("CASHREG")+" a left join "+objUtil.getTable("CASHREGROW")+" b on a.ROWID = b.CASHREGROWID "+
            " and  b.CASHFORM in ('CASH','CURRENCY') where  a.EVENT = 'C' and  a.CASHBOXID =? order by a.UPDATED_DATE desc");
            hSql.Com.Parameters.AddWithValue("CASHBOXID", strCashBoxId);
            hSql.ExecuteReader();
            if (hSql.Read())
            {
                nRet = hSql.Reader.GetDecimal(0);
                nQty = hSql.Reader.GetDecimal(1);
            }
            return nRet;
        }
        public bool load()
        {
            
            bool bRet = true;
            CashBoxes.Reset();
            using (OdbcDataAdapter adapter = new OdbcDataAdapter("", objGlobal.getConnectionString()))
            {
                adapter.SelectCommand.CommandText = "select a.C1 as CASHBOXID,a.c2 as NAME,ISNULL(b.ISOPEN,0)  as ISOPEN,isnull((select top 1 PC from " + objUtil.getTable("CASHBOXPC") + " c where c.CASHBOXID = " +
            " b.CASHBOXID),'')  as PC, isnull(a.V3,0) as EODNUSEID, isnull(a.V2,0) as NEED_DENOMINATION,isnull(a.C3,'') as CURCD, isnull(a.V4,0) as PRINT_RECEIPT,isnull(a.C4,'') as FP_PATH, isnull(a.C5,'') as FP_CODAID, "+
            " isnull(a.V6,0) as VIRTUAL, isnull(a.V10,0) as ENABLE_FEE "+
            " from " + objUtil.getTable("CORW") + " a left join " + objUtil.getTable("CASHBOX") + " b on a.C1 = b.CASHBOXID where a.CODAID = 'CASHBOXID'";
                adapter.Fill(CashBoxes);
            }            
            return bRet;
        }

        public string getDefaultOpenCashBoxOfType(string strCashType)
        {
            string strRet = "";
            string strAvailableCashBoxes = getCashBoxesOfType(strCashType);
            int i = 0;
            while (i < CashBoxes.Rows.Count)
            {
                if ((CashBoxes.Rows[i]["ISOPEN"].ToString() == "1") &&
                    (CashBoxes.Rows[i]["PC"].ToString() == getClientPCName()) &&
                    (strAvailableCashBoxes.IndexOf(CashBoxes.Rows[i]["CASHBOXID"].ToString()) >= 0))
                    return CashBoxes.Rows[i]["CASHBOXID"].ToString();
                i++;
            }
            return strRet;
        }
        public string getCashBoxesOfType(string strCashType)
        {
            string strRet = "";
            int i = 0;
            while (i < CashTypes.Rows.Count)
            {
                if (CashTypes.Rows[i]["TYPE"].ToString() == strCashType)
                    return CashTypes.Rows[i]["CASHBOXES"].ToString();
                i++;
            }
            return strRet;
        }
        public int getVoucherIdOfType(string strCashType, bool isWithDrawVoucher)
        {
            _log.Debug("getVoucherIdOfType >> CashType = " + strCashType + ", isWithDrawVoucher " + isWithDrawVoucher.ToString());
            int nRet = 0;
            int i = 0;
            while (i < CashTypes.Rows.Count)
            {
                if (CashTypes.Rows[i]["TYPE"].ToString() == strCashType)
                { if (!isWithDrawVoucher)
                    return int.Parse(CashTypes.Rows[i]["VOUCHERID"].ToString());
                else
                    return int.Parse(CashTypes.Rows[i]["WITHDRAW_VOUCHERID"].ToString());
                }
                i++;
            }
            _log.Debug("getVoucherIdOfType  << nRet = " + nRet.ToString());
            return nRet;
        }
        public int getSpecRound(string strCashType)
        {
            int nRet = 0;
            int i = 0;
            while (i < CashTypes.Rows.Count)
            {
                if (CashTypes.Rows[i]["TYPE"].ToString() == strCashType)
                    return int.Parse(CashTypes.Rows[i]["SPECROUND"].ToString());
                i++;
            }
            return nRet;
        }
        public decimal roundToLocalCurrency(decimal dec, string strCashType)
        {
            //In Hungary there was a need to round to 5 or 0 Ft
            int rnd = getSpecRound(strCashType);
            if (rnd > 0)
            {
                int i = 0;
                if (rnd > 1)
                {
                    i = (Int32)Decimal.Round(dec, 0, MidpointRounding.AwayFromZero);
                    if ((i % 5) <= 2)
                    { i = i - i % 5; }
                    else
                    { i = i + (5 - i % 5); }
                }
                else
                {
                    //rnd =1
                    i = (Int32)Decimal.Round(dec);
                }
                return i;
            }
            return dec;
        }
        public string getSaleTypeOfType(string strCashType)
        {
            string strRet = "";
            int i = 0;
            while (i < CashTypes.Rows.Count)
            {
                if (CashTypes.Rows[i]["TYPE"].ToString() == strCashType)
                    return CashTypes.Rows[i]["SALETYPE"].ToString();
                i++;
            }
            return strRet;
        }
      
        public bool loadCashType()
        {
            _log.Debug("loadCashType >> ");
            bool bRet = true;
            CashTypes.Reset();
            using (OdbcDataAdapter adapter = new OdbcDataAdapter("", objGlobal.getConnectionString()))
            {
                adapter.SelectCommand.CommandText = 
                @" select a.C1 as FORM,a.C2 as FORMNAME,b.TYPE,b.DESCRIPTION as TYPENAME,b.SALETYPE,c.V1 as VOUCHERID,c.C3 as CASHBOXES,isnull(c.V2,0) as SPECROUND, 
                isnull(c.V3,0) as IS_AMINVOICE,isnull(c.V4,0) as WITHDRAW_VOUCHERID,isnull(c.V5,0) as DEPOSITWITHDRAW  from " + objUtil.getTable("CORW") + " a, " + objUtil.getTable("CASHTYPE") + " b, " + objUtil.getTable("CORW") + " c "
                + " where a.CODAID = 'CASHFORM' and a.V1=1 and a.C1 = b.FORM and b.INUSE = 1 and b.TYPE = c.C1 and c.CODAID = 'CASHTYPE' order by a.V2 desc, a.C2 ";
                adapter.Fill(CashTypes);
            }
            foreach (DataRow row in CashTypes.Rows)
            {
                _log.Debug("FORM = " + row["FORM"].ToString() + ", TYPE = " + row["TYPE"].ToString() + ",NAME = " + row["TYPENAME"].ToString() + ",IS_AMINVOICE = " + row["IS_AMINVOICE"].ToString());
            }
             _log.Debug("loadCashType << ");
            return bRet;
        }
        public bool loadCardType()
        {
            _log.Debug("loadCardType >> ");
            bool bRet = true;
            CardTypes.Reset();
            using (OdbcDataAdapter adapter = new OdbcDataAdapter("", objGlobal.getConnectionString()))
            {
                adapter.SelectCommand.CommandText =
                @" select isnull(a.V1,0) as ISDEFAULT,a.C1 as TYPEID,isnull(a.C2,'') as NAME  from " + objUtil.getTable("CORW") + " a " 
                + " where a.CODAID = 'CASHCCTYPE' order by a.V1 desc ";
                adapter.Fill(CardTypes);
            }
            foreach (DataRow row in CardTypes.Rows)
            {
                _log.Debug("CARDTYPE = " + row["TYPEID"].ToString() + ", NAME = " + row["NAME"].ToString());
            }
            _log.Debug("loadCardType << ");
            return bRet;
        }
   
    }
}

