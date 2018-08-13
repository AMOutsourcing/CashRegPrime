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
using System.Configuration;
using CashRegPrime.Model;
namespace CashRegPrime
{
    public partial class frmMain : nsBaseClass.clsBaseForm
    {        
        static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public frmMain()
        {
            InitializeComponent();
            this.Visible = false;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _log.Info("Start CashRegPrime prgoram...");
            clsLoginDialog f = new clsLoginDialog();            
            DialogResult i = f.ShowDialog();
            if (i == DialogResult.OK)
            {
             //Check user rights
                if (objGlobal.DefaultSManID != "")
                {
                    // If user has default SmanId
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(objGlobal.CultureInfo);

                    objUtil.Localization.TranslateForm(this);
                    loadProfileData();
                    this.Text = objGlobal.DMSFirstUserName + "@" + objAppConfig.getSiteNameOnScreen();
                    this.Visible = true;
                    this.ContextMenuStrip.Items[this.ContextMenuStrip.Items.IndexOfKey("scheduleTaskToolStripMenuItem")].Visible = false;
                    _log.Info("CultureInfo = " + objGlobal.CultureInfo + ", ClientName = " + new clsCashBox().getClientPCName());
                }
                else
                {
                    MessageBox.Show("ERROR: User "+objGlobal.DMSFirstUserName+" must be assigned to a default SmanId !");
                    Application.Exit();
                }
            }
            else
            {
                Application.Exit();
            }
        }

        private void pbOpenCashBox_Click(object sender, EventArgs e)
        {
            dlgOpenCashBox dlg = new dlgOpenCashBox();
            dlg.ShowDialog();
        }

        private void pbCloseCashBox_Click(object sender, EventArgs e)
        {
            dlgCloseCashBox dlg = new dlgCloseCashBox();
            dlg.ShowDialog();
        }

        private void pbInvoiceSearch_Click(object sender, EventArgs e)
        {
            dlgSearchInvoice dlg = new dlgSearchInvoice();
            dlg.ShowDialog();
        }

        private void pbPrintClosingReport_Click(object sender, EventArgs e)
        {
            _log.Debug("pbPrintClosingReport_Click >>");
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
                String strPrintClosing_bat = ConfigurationSettings.AppSettings["PrintClosing_bat"];
                if (objAppConfig.getStringParam("CASHREG", "P_CLOSING", "C3", "") != "")
                    strPrintClosing_bat = objAppConfig.getStringParam("CASHREG", "P_CLOSING", "C3", "");
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = strPrintClosing_bat;
                process.StartInfo.Arguments = strLanguageCode;
                process.Start();
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
            _log.Debug("pbPrintClosingReport_Click <<");
        }        
      
    }
}
