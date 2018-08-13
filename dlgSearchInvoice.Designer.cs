namespace CashRegPrime
{
    partial class dlgSearchInvoice
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.chkboxCashreg = new System.Windows.Forms.CheckBox();
            this.chkboxVehichle = new System.Windows.Forms.CheckBox();
            this.chkboxWorkshop = new System.Windows.Forms.CheckBox();
            this.chkboxSpareparts = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtboxFilter = new System.Windows.Forms.TextBox();
            this.chkboxPaidInv = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrderNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInvoiceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReceiptNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInvoiceDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInvoiceSum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentSum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pbNewPayment = new System.Windows.Forms.Button();
            this.chkboxCreditInv = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // pbOK
            // 
            this.pbOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.pbOK.Location = new System.Drawing.Point(702, 19);
            this.pbOK.Margin = new System.Windows.Forms.Padding(4);
            this.pbOK.Size = new System.Drawing.Size(108, 28);
            this.pbOK.Text = "&Search";
            this.pbOK.Click += new System.EventHandler(this.pbOK_Click);
            // 
            // pbCancel
            // 
            this.pbCancel.Location = new System.Drawing.Point(702, 46);
            this.pbCancel.Margin = new System.Windows.Forms.Padding(4);
            this.pbCancel.Size = new System.Drawing.Size(108, 28);
            this.pbCancel.Click += new System.EventHandler(this.pbCancel_Click);
            // 
            // pbHelp
            // 
            this.pbHelp.Location = new System.Drawing.Point(702, 80);
            this.pbHelp.Margin = new System.Windows.Forms.Padding(4);
            this.pbHelp.Size = new System.Drawing.Size(108, 28);
            this.pbHelp.Visible = false;
            // 
            // chkboxCashreg
            // 
            this.chkboxCashreg.AutoSize = true;
            this.chkboxCashreg.Location = new System.Drawing.Point(24, 30);
            this.chkboxCashreg.Name = "chkboxCashreg";
            this.chkboxCashreg.Size = new System.Drawing.Size(87, 17);
            this.chkboxCashreg.TabIndex = 3;
            this.chkboxCashreg.Text = "Cash register";
            this.chkboxCashreg.UseVisualStyleBackColor = true;
            // 
            // chkboxVehichle
            // 
            this.chkboxVehichle.AutoSize = true;
            this.chkboxVehichle.Location = new System.Drawing.Point(24, 53);
            this.chkboxVehichle.Name = "chkboxVehichle";
            this.chkboxVehichle.Size = new System.Drawing.Size(94, 17);
            this.chkboxVehichle.TabIndex = 4;
            this.chkboxVehichle.Text = "Vehichle sales";
            this.chkboxVehichle.UseVisualStyleBackColor = true;
            // 
            // chkboxWorkshop
            // 
            this.chkboxWorkshop.AutoSize = true;
            this.chkboxWorkshop.Location = new System.Drawing.Point(147, 30);
            this.chkboxWorkshop.Name = "chkboxWorkshop";
            this.chkboxWorkshop.Size = new System.Drawing.Size(75, 17);
            this.chkboxWorkshop.TabIndex = 5;
            this.chkboxWorkshop.Text = "Workshop";
            this.chkboxWorkshop.UseVisualStyleBackColor = true;
            // 
            // chkboxSpareparts
            // 
            this.chkboxSpareparts.AutoSize = true;
            this.chkboxSpareparts.Location = new System.Drawing.Point(147, 53);
            this.chkboxSpareparts.Name = "chkboxSpareparts";
            this.chkboxSpareparts.Size = new System.Drawing.Size(102, 17);
            this.chkboxSpareparts.TabIndex = 6;
            this.chkboxSpareparts.Text = "Spare part sales";
            this.chkboxSpareparts.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Module";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Filter";
            // 
            // txtboxFilter
            // 
            this.txtboxFilter.Location = new System.Drawing.Point(294, 21);
            this.txtboxFilter.Name = "txtboxFilter";
            this.txtboxFilter.Size = new System.Drawing.Size(402, 20);
            this.txtboxFilter.TabIndex = 9;
            // 
            // chkboxPaidInv
            // 
            this.chkboxPaidInv.AutoSize = true;
            this.chkboxPaidInv.Location = new System.Drawing.Point(294, 50);
            this.chkboxPaidInv.Name = "chkboxPaidInv";
            this.chkboxPaidInv.Size = new System.Drawing.Size(126, 17);
            this.chkboxPaidInv.TabIndex = 10;
            this.chkboxPaidInv.Text = "Include paid invoices";
            this.chkboxPaidInv.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colType,
            this.colOrderNo,
            this.colCustNo,
            this.colCustomerName,
            this.colStatus,
            this.colInvoiceNo,
            this.colReceiptNo,
            this.colInvoiceDate,
            this.colPaymentDate,
            this.colInvoiceSum,
            this.colPaymentSum,
            this.colRemain,
            this.colRemark});
            this.dataGridView1.Location = new System.Drawing.Point(13, 115);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(797, 378);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // colType
            // 
            this.colType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colType.HeaderText = "Type";
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            this.colType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colType.Width = 37;
            // 
            // colOrderNo
            // 
            this.colOrderNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colOrderNo.HeaderText = "Order no";
            this.colOrderNo.Name = "colOrderNo";
            this.colOrderNo.ReadOnly = true;
            this.colOrderNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colOrderNo.Width = 49;
            // 
            // colCustNo
            // 
            this.colCustNo.HeaderText = "Cust. no";
            this.colCustNo.Name = "colCustNo";
            this.colCustNo.ReadOnly = true;
            this.colCustNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colCustNo.Width = 80;
            // 
            // colCustomerName
            // 
            this.colCustomerName.HeaderText = "Customer name";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.ReadOnly = true;
            this.colCustomerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colCustomerName.Width = 250;
            // 
            // colStatus
            // 
            this.colStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colStatus.Width = 43;
            // 
            // colInvoiceNo
            // 
            this.colInvoiceNo.HeaderText = "Invoice no";
            this.colInvoiceNo.Name = "colInvoiceNo";
            this.colInvoiceNo.ReadOnly = true;
            this.colInvoiceNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colReceiptNo
            // 
            this.colReceiptNo.HeaderText = "Receipt no";
            this.colReceiptNo.Name = "colReceiptNo";
            this.colReceiptNo.ReadOnly = true;
            this.colReceiptNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colInvoiceDate
            // 
            this.colInvoiceDate.HeaderText = "Invoice date";
            this.colInvoiceDate.Name = "colInvoiceDate";
            this.colInvoiceDate.ReadOnly = true;
            this.colInvoiceDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colPaymentDate
            // 
            this.colPaymentDate.HeaderText = "Payment date";
            this.colPaymentDate.Name = "colPaymentDate";
            this.colPaymentDate.ReadOnly = true;
            this.colPaymentDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colInvoiceSum
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colInvoiceSum.DefaultCellStyle = dataGridViewCellStyle1;
            this.colInvoiceSum.HeaderText = "Invoice sum";
            this.colInvoiceSum.Name = "colInvoiceSum";
            this.colInvoiceSum.ReadOnly = true;
            this.colInvoiceSum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colPaymentSum
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colPaymentSum.DefaultCellStyle = dataGridViewCellStyle2;
            this.colPaymentSum.HeaderText = "Payment sum";
            this.colPaymentSum.Name = "colPaymentSum";
            this.colPaymentSum.ReadOnly = true;
            this.colPaymentSum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colRemain
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colRemain.DefaultCellStyle = dataGridViewCellStyle3;
            this.colRemain.HeaderText = "Balance";
            this.colRemain.Name = "colRemain";
            this.colRemain.ReadOnly = true;
            this.colRemain.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colRemark
            // 
            this.colRemark.HeaderText = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.ReadOnly = true;
            this.colRemark.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colRemark.Width = 200;
            // 
            // pbNewPayment
            // 
            this.pbNewPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbNewPayment.Location = new System.Drawing.Point(702, 80);
            this.pbNewPayment.Name = "pbNewPayment";
            this.pbNewPayment.Size = new System.Drawing.Size(108, 28);
            this.pbNewPayment.TabIndex = 12;
            this.pbNewPayment.Text = "New payment";
            this.pbNewPayment.UseVisualStyleBackColor = true;
            this.pbNewPayment.Click += new System.EventHandler(this.pbNewPayment_Click);
            // 
            // chkboxCreditInv
            // 
            this.chkboxCreditInv.AutoSize = true;
            this.chkboxCreditInv.Location = new System.Drawing.Point(426, 50);
            this.chkboxCreditInv.Name = "chkboxCreditInv";
            this.chkboxCreditInv.Size = new System.Drawing.Size(132, 17);
            this.chkboxCreditInv.TabIndex = 13;
            this.chkboxCreditInv.Text = "Include credit invoices";
            this.chkboxCreditInv.UseVisualStyleBackColor = true;
            // 
            // dlgSearchInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.pbCancel;
            this.ClientSize = new System.Drawing.Size(822, 505);
            this.Controls.Add(this.chkboxCreditInv);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.pbNewPayment);
            this.Controls.Add(this.chkboxPaidInv);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkboxVehichle);
            this.Controls.Add(this.chkboxWorkshop);
            this.Controls.Add(this.chkboxSpareparts);
            this.Controls.Add(this.chkboxCashreg);
            this.Controls.Add(this.txtboxFilter);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "dlgSearchInvoice";
            this.Text = "Search invoice";
            this.Load += new System.EventHandler(this.dlgSearchInvoice_Load);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtboxFilter, 0);
            this.Controls.SetChildIndex(this.chkboxCashreg, 0);
            this.Controls.SetChildIndex(this.chkboxSpareparts, 0);
            this.Controls.SetChildIndex(this.chkboxWorkshop, 0);
            this.Controls.SetChildIndex(this.chkboxVehichle, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.chkboxPaidInv, 0);
            this.Controls.SetChildIndex(this.pbCancel, 0);
            this.Controls.SetChildIndex(this.pbHelp, 0);
            this.Controls.SetChildIndex(this.pbNewPayment, 0);
            this.Controls.SetChildIndex(this.dataGridView1, 0);
            this.Controls.SetChildIndex(this.pbOK, 0);
            this.Controls.SetChildIndex(this.chkboxCreditInv, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkboxCashreg;
        private System.Windows.Forms.CheckBox chkboxVehichle;
        private System.Windows.Forms.CheckBox chkboxWorkshop;
        private System.Windows.Forms.CheckBox chkboxSpareparts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtboxFilter;
        private System.Windows.Forms.CheckBox chkboxPaidInv;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button pbNewPayment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReceiptNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceSum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentSum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemain;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemark;
        private System.Windows.Forms.CheckBox chkboxCreditInv;
    }
}