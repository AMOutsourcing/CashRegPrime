namespace CashRegPrime
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbOpenCashBox = new System.Windows.Forms.Button();
            this.pbCloseCashBox = new System.Windows.Forms.Button();
            this.pbInvoiceSearch = new System.Windows.Forms.Button();
            this.pbPrintClosingReport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pbOpenCashBox
            // 
            this.pbOpenCashBox.Location = new System.Drawing.Point(16, 15);
            this.pbOpenCashBox.Margin = new System.Windows.Forms.Padding(4);
            this.pbOpenCashBox.Name = "pbOpenCashBox";
            this.pbOpenCashBox.Size = new System.Drawing.Size(292, 50);
            this.pbOpenCashBox.TabIndex = 0;
            this.pbOpenCashBox.Text = "Open of cashbox";
            this.pbOpenCashBox.UseVisualStyleBackColor = true;
            this.pbOpenCashBox.Click += new System.EventHandler(this.pbOpenCashBox_Click);
            // 
            // pbCloseCashBox
            // 
            this.pbCloseCashBox.Location = new System.Drawing.Point(16, 73);
            this.pbCloseCashBox.Margin = new System.Windows.Forms.Padding(4);
            this.pbCloseCashBox.Name = "pbCloseCashBox";
            this.pbCloseCashBox.Size = new System.Drawing.Size(292, 50);
            this.pbCloseCashBox.TabIndex = 1;
            this.pbCloseCashBox.Text = "Close of cashbox";
            this.pbCloseCashBox.UseVisualStyleBackColor = true;
            this.pbCloseCashBox.Click += new System.EventHandler(this.pbCloseCashBox_Click);
            // 
            // pbInvoiceSearch
            // 
            this.pbInvoiceSearch.Location = new System.Drawing.Point(16, 130);
            this.pbInvoiceSearch.Margin = new System.Windows.Forms.Padding(4);
            this.pbInvoiceSearch.Name = "pbInvoiceSearch";
            this.pbInvoiceSearch.Size = new System.Drawing.Size(292, 50);
            this.pbInvoiceSearch.TabIndex = 2;
            this.pbInvoiceSearch.Text = "Invoice search and payment";
            this.pbInvoiceSearch.UseVisualStyleBackColor = true;
            this.pbInvoiceSearch.Click += new System.EventHandler(this.pbInvoiceSearch_Click);
            // 
            // pbPrintClosingReport
            // 
            this.pbPrintClosingReport.Location = new System.Drawing.Point(16, 221);
            this.pbPrintClosingReport.Margin = new System.Windows.Forms.Padding(4);
            this.pbPrintClosingReport.Name = "pbPrintClosingReport";
            this.pbPrintClosingReport.Size = new System.Drawing.Size(292, 50);
            this.pbPrintClosingReport.TabIndex = 3;
            this.pbPrintClosingReport.Text = "Print closing report";
            this.pbPrintClosingReport.UseVisualStyleBackColor = true;
            this.pbPrintClosingReport.Click += new System.EventHandler(this.pbPrintClosingReport_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 309);
            this.Controls.Add(this.pbPrintClosingReport);
            this.Controls.Add(this.pbInvoiceSearch);
            this.Controls.Add(this.pbCloseCashBox);
            this.Controls.Add(this.pbOpenCashBox);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Cash register";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button pbOpenCashBox;
        private System.Windows.Forms.Button pbCloseCashBox;
        private System.Windows.Forms.Button pbInvoiceSearch;
        private System.Windows.Forms.Button pbPrintClosingReport;
    }
}

