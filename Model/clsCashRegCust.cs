using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using nsBaseClass;
using System.Text.RegularExpressions;
using System.Configuration;
using log4net;
using System.Data;
using System.Data.Odbc;

namespace CashRegPrime.Model
{
    class CashRegCust
    {
        static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int Custno = 0;
        public string Fname = "";
        public string Lname = "";
        public string Spinnr = "";
        public string Tel1 ="";
        public string Tel2 = "";
        public string Tel3 = "";
        public string Tel4 = "";
        public string Address = "";
        public string Email = "";

        public static DataTable searchCustomers(string namephrase)
        {
            _log.Debug("searchCustomers >> " + namephrase);
            List<CashRegCust> Results = new List<CashRegCust>();
            clsBaseUtility locObjUtil = new clsBaseUtility();
            clsSqlFactory hSql = new clsSqlFactory();
            DataTable retDataTable = new DataTable();
            bool bRet = true;
            try
            {
                string searchString = namephrase;

                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex(@"[ ]{2,}", options);
                searchString = regex.Replace(searchString, @" ");

                string[] words = searchString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                int i = 0;
                string strSql = "";
                string strOrder = "";

                string strCUST = locObjUtil.getTable("CUST");
                string strFTVIEW_CUST =( strCUST=="CUST")?"FTVIEW_CUST":locObjUtil.getTable("FTVIEW_CUST") ;
                strSql = "select top " + ConfigurationManager.AppSettings["SearchResultNumber"].ToString() + " isnull(a.CUSTNO,'') as CUSTNO,isnull(a.LNAME,'') as LNAME,isnull(a.FNAME,'') as FNAME,isnull(a.SPINNR,'') as SPINNR, " +
                    " isnull(replace(a.WTEL,'/',''),'') +'/'+ isnull(replace(a.HTEL,'/',''),'') +'/'+ isnull(replace(a.Tel3,'/',''),'') as PHONES, " +
                    " isnull(a.EMAIL,'') as EMAIL, " +
                    " isnull(a.PO,'')+' '+ isnull(a.POSTCD,'')+' '+isnull(a.ADDR2,'')+' '+isnull(a.ADDR2E,'')+' '+isnull(a.ADDR1,'') as ADDRESS "+
                    " from " + strCUST + " a ";

                while (i < words.Length)
                {
                    if (words[i] != "")
                    {
                        strSql += " inner join containstable(" + strFTVIEW_CUST + ", *, '\"" + words[i] + "*\"' ) as T" + i.ToString() + " on a._OID = T" + i.ToString() + ".[KEY]  ";
                        if (i > 0)
                        {
                            strOrder += "+";
                        }
                        strOrder += "T" + i.ToString() + ".RANK";
                    }
                    i++;
                }
                strSql = strSql + " where a.CUSTNO >0 ";
                if (strOrder != "") strOrder = " order by " + strOrder + " desc ";
                if (words.Length > 0)
                    strSql += strOrder;
                _log.Debug(strSql);
                bRet = hSql.NewCommand(strSql);

                using (OdbcConnection connection =
                               new OdbcConnection(new clsGlobalVariable().getConnectionString()))
                {
                    OdbcDataAdapter adapter =
                        new OdbcDataAdapter(strSql, connection);

                    // Open the connection and fill the DataSet.
                    try
                    {
                        connection.Open();
                        adapter.Fill(retDataTable);
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex.ToString());
                        throw ex;
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
            _log.Debug("searchCustomers <<");
            return retDataTable;        
        }
    }
}
