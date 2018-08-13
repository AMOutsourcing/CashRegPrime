namespace CashRegPrime
{
    partial class dlgOpenCashBox
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
            this.cmbCashBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dfOpeningBalance = new System.Windows.Forms.TextBox();
            this.dfQuantity = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pbOK
            // 
            this.pbOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.pbOK.Location = new System.Drawing.Point(407, 12);
            this.pbOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbOK.TabIndex = 2;
            this.pbOK.Click += new System.EventHandler(this.pbOK_Click);
            // 
            // pbCancel
            // 
            this.pbCancel.Location = new System.Drawing.Point(407, 46);
            this.pbCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbCancel.TabIndex = 3;
            this.pbCancel.Click += new System.EventHandler(this.pbCancel_Click);
            // 
            // pbHelp
            // 
            this.pbHelp.Location = new System.Drawing.Point(407, 80);
            this.pbHelp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbHelp.TabIndex = 4;
            // 
            // cmbCashBox
            // 
            this.cmbCashBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCashBox.FormattingEnabled = true;
            this.cmbCashBox.Location = new System.Drawing.Point(123, 17);
            this.cmbCashBox.Name = "cmbCashBox";
            this.cmbCashBox.Size = new System.Drawing.Size(278, 21);
            this.cmbCashBox.TabIndex = 0;
            this.cmbCashBox.SelectedIndexChanged += new System.EventHandler(this.cmbCashBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cashbox";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Amount to cashbox";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dfOpeningBalance
            // 
            this.dfOpeningBalance.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dfOpeningBalance.Location = new System.Drawing.Point(124, 54);
            this.dfOpeningBalance.Name = "dfOpeningBalance";
            this.dfOpeningBalance.Size = new System.Drawing.Size(163, 20);
            this.dfOpeningBalance.TabIndex = 1;
            this.dfOpeningBalance.WordWrap = false;
            this.dfOpeningBalance.Validated += new System.EventHandler(this.dfOpeningBalance_Validated);
            this.dfOpeningBalance.Validating += new System.ComponentModel.CancelEventHandler(this.dfOpeningBalance_Validating);
            // 
            // dfQuantity
            // 
            this.dfQuantity.Enabled = false;
            this.dfQuantity.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dfQuantity.Location = new System.Drawing.Point(123, 85);
            this.dfQuantity.Name = "dfQuantity";
            this.dfQuantity.Size = new System.Drawing.Size(163, 20);
            this.dfQuantity.TabIndex = 5;
            this.dfQuantity.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Quantity";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dlgOpenCashBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.pbCancel;
            this.ClientSize = new System.Drawing.Size(504, 147);
            this.Controls.Add(this.dfQuantity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dfOpeningBalance);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbCashBox);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "dlgOpenCashBox";
            this.Text = "Opening of cashbox";
            this.Load += new System.EventHandler(this.dlgOpenCashBox_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cmbCashBox, 0);
            this.Controls.SetChildIndex(this.pbOK, 0);
            this.Controls.SetChildIndex(this.pbCancel, 0);
            this.Controls.SetChildIndex(this.pbHelp, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.dfOpeningBalance, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.dfQuantity, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCashBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox dfOpeningBalance;
        private System.Windows.Forms.TextBox dfQuantity;
        private System.Windows.Forms.Label label3;
    }
}