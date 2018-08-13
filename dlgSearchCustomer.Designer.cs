namespace CashRegPrime
{
    partial class dlgSearchCustomer
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
            this.dfCustomerFilter = new System.Windows.Forms.TextBox();
            this.labCustFilter = new System.Windows.Forms.Label();
            this.gridCustomer = new System.Windows.Forms.DataGridView();
            this.colCustNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSpinnr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomer)).BeginInit();
            this.SuspendLayout();
            // 
            // pbOK
            // 
            this.pbOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.pbOK.Location = new System.Drawing.Point(1057, 12);
            this.pbOK.TabIndex = 1;
            this.pbOK.Text = "&Search";
            this.pbOK.Click += new System.EventHandler(this.pbSearchCust_Click);
            // 
            // pbCancel
            // 
            this.pbCancel.Location = new System.Drawing.Point(1057, 46);
            this.pbCancel.TabIndex = 2;
            // 
            // pbHelp
            // 
            this.pbHelp.Location = new System.Drawing.Point(966, 9);
            this.pbHelp.Visible = false;
            // 
            // dfCustomerFilter
            // 
            this.dfCustomerFilter.Location = new System.Drawing.Point(77, 17);
            this.dfCustomerFilter.Name = "dfCustomerFilter";
            this.dfCustomerFilter.Size = new System.Drawing.Size(402, 20);
            this.dfCustomerFilter.TabIndex = 0;
            // 
            // labCustFilter
            // 
            this.labCustFilter.AutoSize = true;
            this.labCustFilter.Location = new System.Drawing.Point(42, 20);
            this.labCustFilter.Name = "labCustFilter";
            this.labCustFilter.Size = new System.Drawing.Size(29, 13);
            this.labCustFilter.TabIndex = 10;
            this.labCustFilter.Text = "Filter";
            // 
            // gridCustomer
            // 
            this.gridCustomer.AllowUserToAddRows = false;
            this.gridCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCustomer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCustNo,
            this.colSpinnr,
            this.colLName,
            this.colFName,
            this.colTel,
            this.colAddress,
            this.colEmail});
            this.gridCustomer.Location = new System.Drawing.Point(10, 80);
            this.gridCustomer.MultiSelect = false;
            this.gridCustomer.Name = "gridCustomer";
            this.gridCustomer.ReadOnly = true;
            this.gridCustomer.RowHeadersWidth = 10;
            this.gridCustomer.RowTemplate.Height = 24;
            this.gridCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridCustomer.Size = new System.Drawing.Size(1132, 260);
            this.gridCustomer.TabIndex = 3;
            this.gridCustomer.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCustomer_CellDoubleClick);
            this.gridCustomer.DoubleClick += new System.EventHandler(this.gridCustomer_DoubleClick);
            // 
            // colCustNo
            // 
            this.colCustNo.DataPropertyName = "CUSTNO";
            this.colCustNo.HeaderText = "Cust. no";
            this.colCustNo.Name = "colCustNo";
            this.colCustNo.ReadOnly = true;
            this.colCustNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colCustNo.Width = 80;
            // 
            // colSpinnr
            // 
            this.colSpinnr.DataPropertyName = "SPINNR";
            this.colSpinnr.HeaderText = "Tax number";
            this.colSpinnr.Name = "colSpinnr";
            this.colSpinnr.ReadOnly = true;
            // 
            // colLName
            // 
            this.colLName.DataPropertyName = "LNAME";
            this.colLName.HeaderText = "Last name";
            this.colLName.Name = "colLName";
            this.colLName.ReadOnly = true;
            this.colLName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colLName.Width = 200;
            // 
            // colFName
            // 
            this.colFName.DataPropertyName = "FNAME";
            this.colFName.HeaderText = "First name";
            this.colFName.Name = "colFName";
            this.colFName.ReadOnly = true;
            // 
            // colTel
            // 
            this.colTel.DataPropertyName = "PHONES";
            this.colTel.HeaderText = "Phones";
            this.colTel.Name = "colTel";
            this.colTel.ReadOnly = true;
            this.colTel.Width = 200;
            // 
            // colAddress
            // 
            this.colAddress.DataPropertyName = "ADDRESS";
            this.colAddress.HeaderText = "Address";
            this.colAddress.Name = "colAddress";
            this.colAddress.ReadOnly = true;
            this.colAddress.Width = 250;
            // 
            // colEmail
            // 
            this.colEmail.DataPropertyName = "EMAIL";
            this.colEmail.HeaderText = "Email";
            this.colEmail.Name = "colEmail";
            this.colEmail.ReadOnly = true;
            this.colEmail.Width = 150;
            // 
            // dlgSearchCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1154, 353);
            this.Controls.Add(this.gridCustomer);
            this.Controls.Add(this.dfCustomerFilter);
            this.Controls.Add(this.labCustFilter);
            this.Name = "dlgSearchCustomer";
            this.Text = "Search customer";
            this.Load += new System.EventHandler(this.dlgSearchCustomer_Load);
            this.Controls.SetChildIndex(this.pbHelp, 0);
            this.Controls.SetChildIndex(this.pbOK, 0);
            this.Controls.SetChildIndex(this.pbCancel, 0);
            this.Controls.SetChildIndex(this.labCustFilter, 0);
            this.Controls.SetChildIndex(this.dfCustomerFilter, 0);
            this.Controls.SetChildIndex(this.gridCustomer, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox dfCustomerFilter;
        private System.Windows.Forms.Label labCustFilter;
        private System.Windows.Forms.DataGridView gridCustomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSpinnr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
    }
}
