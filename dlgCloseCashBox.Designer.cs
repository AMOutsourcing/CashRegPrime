namespace CashRegPrime
{
    partial class dlgCloseCashBox
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
            this.cmbCashBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dfClosingBalance = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridDenomination = new System.Windows.Forms.DataGridView();
            this.colCaption = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFaceValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsCoin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dfTotalNotes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dfTotalCoins = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dfTotal = new System.Windows.Forms.TextBox();
            this.dfClosingQty = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDenomination)).BeginInit();
            this.SuspendLayout();
            // 
            // pbOK
            // 
            this.pbOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.pbOK.Location = new System.Drawing.Point(608, 12);
            this.pbOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbOK.TabIndex = 1;
            this.pbOK.Click += new System.EventHandler(this.pbOK_Click);
            // 
            // pbCancel
            // 
            this.pbCancel.Location = new System.Drawing.Point(608, 46);
            this.pbCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbCancel.TabIndex = 2;
            this.pbCancel.Click += new System.EventHandler(this.pbCancel_Click);
            // 
            // pbHelp
            // 
            this.pbHelp.Location = new System.Drawing.Point(608, 80);
            this.pbHelp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbHelp.TabIndex = 3;
            // 
            // cmbCashBox
            // 
            this.cmbCashBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCashBox.FormattingEnabled = true;
            this.cmbCashBox.Location = new System.Drawing.Point(86, 12);
            this.cmbCashBox.Name = "cmbCashBox";
            this.cmbCashBox.Size = new System.Drawing.Size(200, 21);
            this.cmbCashBox.TabIndex = 0;
            this.cmbCashBox.SelectedIndexChanged += new System.EventHandler(this.cmbCashBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cashbox";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dfClosingBalance
            // 
            this.dfClosingBalance.Enabled = false;
            this.dfClosingBalance.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dfClosingBalance.Location = new System.Drawing.Point(86, 39);
            this.dfClosingBalance.Name = "dfClosingBalance";
            this.dfClosingBalance.Size = new System.Drawing.Size(200, 20);
            this.dfClosingBalance.TabIndex = 3;
            this.dfClosingBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.dfClosingBalance.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Closing amount";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridDenomination);
            this.groupBox1.Location = new System.Drawing.Point(12, 115);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(582, 361);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Denomination";
            // 
            // gridDenomination
            // 
            this.gridDenomination.AllowUserToAddRows = false;
            this.gridDenomination.AllowUserToDeleteRows = false;
            this.gridDenomination.AllowUserToResizeRows = false;
            this.gridDenomination.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDenomination.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCaption,
            this.colFaceValue,
            this.colCount,
            this.colTotal,
            this.colIsCoin});
            this.gridDenomination.Location = new System.Drawing.Point(6, 19);
            this.gridDenomination.MultiSelect = false;
            this.gridDenomination.Name = "gridDenomination";
            this.gridDenomination.RowTemplate.Height = 24;
            this.gridDenomination.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridDenomination.Size = new System.Drawing.Size(674, 336);
            this.gridDenomination.TabIndex = 0;
            this.gridDenomination.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDenomination_CellValueChanged);
            this.gridDenomination.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDenomination_CellContentClick);
            // 
            // colCaption
            // 
            this.colCaption.HeaderText = "Caption";
            this.colCaption.MinimumWidth = 100;
            this.colCaption.Name = "colCaption";
            this.colCaption.ReadOnly = true;
            // 
            // colFaceValue
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colFaceValue.DefaultCellStyle = dataGridViewCellStyle1;
            this.colFaceValue.HeaderText = "Face value";
            this.colFaceValue.Name = "colFaceValue";
            this.colFaceValue.ReadOnly = true;
            // 
            // colCount
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "#,0";
            this.colCount.DefaultCellStyle = dataGridViewCellStyle2;
            this.colCount.HeaderText = "Count";
            this.colCount.MaxInputLength = 10;
            this.colCount.Name = "colCount";
            // 
            // colTotal
            // 
            this.colTotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colTotal.DefaultCellStyle = dataGridViewCellStyle3;
            this.colTotal.HeaderText = "Total";
            this.colTotal.MinimumWidth = 150;
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            this.colTotal.Width = 150;
            // 
            // colIsCoin
            // 
            this.colIsCoin.HeaderText = "IsCoin";
            this.colIsCoin.Name = "colIsCoin";
            this.colIsCoin.ReadOnly = true;
            this.colIsCoin.Visible = false;
            // 
            // dfTotalNotes
            // 
            this.dfTotalNotes.Enabled = false;
            this.dfTotalNotes.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dfTotalNotes.Location = new System.Drawing.Point(407, 15);
            this.dfTotalNotes.Name = "dfTotalNotes";
            this.dfTotalNotes.Size = new System.Drawing.Size(154, 20);
            this.dfTotalNotes.TabIndex = 10;
            this.dfTotalNotes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.dfTotalNotes.WordWrap = false;
            this.dfTotalNotes.TextChanged += new System.EventHandler(this.dfTotalNotes_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(306, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Total notes";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(306, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Total coins";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // dfTotalCoins
            // 
            this.dfTotalCoins.Enabled = false;
            this.dfTotalCoins.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dfTotalCoins.Location = new System.Drawing.Point(407, 41);
            this.dfTotalCoins.Name = "dfTotalCoins";
            this.dfTotalCoins.Size = new System.Drawing.Size(154, 20);
            this.dfTotalCoins.TabIndex = 12;
            this.dfTotalCoins.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.dfTotalCoins.WordWrap = false;
            this.dfTotalCoins.TextChanged += new System.EventHandler(this.dfTotalCoins_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(306, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Total";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // dfTotal
            // 
            this.dfTotal.Enabled = false;
            this.dfTotal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dfTotal.Location = new System.Drawing.Point(407, 64);
            this.dfTotal.Name = "dfTotal";
            this.dfTotal.Size = new System.Drawing.Size(154, 20);
            this.dfTotal.TabIndex = 14;
            this.dfTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.dfTotal.WordWrap = false;
            this.dfTotal.TextChanged += new System.EventHandler(this.dfTotal_TextChanged);
            // 
            // dfClosingQty
            // 
            this.dfClosingQty.Enabled = false;
            this.dfClosingQty.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dfClosingQty.Location = new System.Drawing.Point(86, 65);
            this.dfClosingQty.Name = "dfClosingQty";
            this.dfClosingQty.Size = new System.Drawing.Size(200, 20);
            this.dfClosingQty.TabIndex = 17;
            this.dfClosingQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.dfClosingQty.WordWrap = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Closing quantity";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dlgCloseCashBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.pbCancel;
            this.ClientSize = new System.Drawing.Size(704, 486);
            this.Controls.Add(this.dfClosingQty);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dfTotal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dfTotalCoins);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dfTotalNotes);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dfClosingBalance);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbCashBox);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "dlgCloseCashBox";
            this.Text = "Closing of cashbox";
            this.Load += new System.EventHandler(this.dlgCloseCashBox_Load);
            this.Controls.SetChildIndex(this.pbOK, 0);
            this.Controls.SetChildIndex(this.pbCancel, 0);
            this.Controls.SetChildIndex(this.pbHelp, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cmbCashBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.dfClosingBalance, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.dfTotalNotes, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.dfTotalCoins, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.dfTotal, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.dfClosingQty, 0);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDenomination)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCashBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox dfClosingBalance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView gridDenomination;
        private System.Windows.Forms.TextBox dfTotalNotes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox dfTotalCoins;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox dfTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCaption;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFaceValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsCoin;
        private System.Windows.Forms.TextBox dfClosingQty;
        private System.Windows.Forms.Label label6;
    }
}