namespace CashRegPrime
{
    partial class dlgCashRegisterPayment
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labType = new System.Windows.Forms.Label();
            this.labOrderNo = new System.Windows.Forms.Label();
            this.labLinceceNo = new System.Windows.Forms.Label();
            this.labCustomer = new System.Windows.Forms.Label();
            this.labRemark = new System.Windows.Forms.Label();
            this.labInvoiceNo = new System.Windows.Forms.Label();
            this.labInvoiceDate = new System.Windows.Forms.Label();
            this.richTextBoxRemark = new System.Windows.Forms.RichTextBox();
            this.textBoxType = new System.Windows.Forms.TextBox();
            this.textBoxOrderNo = new System.Windows.Forms.TextBox();
            this.textBoxLicNo = new System.Windows.Forms.TextBox();
            this.textBoxCustNo = new System.Windows.Forms.TextBox();
            this.textBoxCustName = new System.Windows.Forms.TextBox();
            this.textBoxInvoiceNo = new System.Windows.Forms.TextBox();
            this.textBoxInvoiceDate = new System.Windows.Forms.TextBox();
            this.rbDepositToCashBox = new System.Windows.Forms.RadioButton();
            this.rbWithdrawFromCashBox = new System.Windows.Forms.RadioButton();
            this.labAmountToPay = new System.Windows.Forms.Label();
            this.textBoxAmountToPay = new System.Windows.Forms.TextBox();
            this.labCashReturn = new System.Windows.Forms.Label();
            this.textBoxTotalPaid = new System.Windows.Forms.TextBox();
            this.pbUndo = new System.Windows.Forms.Button();
            this.pbReprint = new System.Windows.Forms.Button();
            this.butDeleteRow = new System.Windows.Forms.Button();
            this.butAddRow = new System.Windows.Forms.Button();
            this.gridPayment = new System.Windows.Forms.DataGridView();
            this.colPaymentForm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCashBox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbPaymentForm = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPaymentType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCashBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxQuantity = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxReceiptNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pbSearchCustomer = new System.Windows.Forms.Button();
            this.textBoxFee = new System.Windows.Forms.TextBox();
            this.lbFee = new System.Windows.Forms.Label();
            this.cmbCardTypes = new System.Windows.Forms.ComboBox();
            this.lbCardTypes = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridPayment)).BeginInit();
            this.SuspendLayout();
            // 
            // pbOK
            // 
            this.pbOK.Location = new System.Drawing.Point(723, 11);
            this.pbOK.Margin = new System.Windows.Forms.Padding(4);
            this.pbOK.Size = new System.Drawing.Size(113, 28);
            this.pbOK.TabIndex = 28;
            this.pbOK.Click += new System.EventHandler(this.pbOK_Click);
            // 
            // pbCancel
            // 
            this.pbCancel.Location = new System.Drawing.Point(723, 45);
            this.pbCancel.Margin = new System.Windows.Forms.Padding(4);
            this.pbCancel.Size = new System.Drawing.Size(113, 28);
            this.pbCancel.TabIndex = 29;
            this.pbCancel.Click += new System.EventHandler(this.pbCancel_Click);
            // 
            // pbHelp
            // 
            this.pbHelp.Location = new System.Drawing.Point(723, 79);
            this.pbHelp.Margin = new System.Windows.Forms.Padding(4);
            this.pbHelp.Size = new System.Drawing.Size(113, 28);
            this.pbHelp.TabIndex = 30;
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(10, 16);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(105, 14);
            this.labType.TabIndex = 0;
            this.labType.Text = "Type";
            this.labType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labOrderNo
            // 
            this.labOrderNo.Location = new System.Drawing.Point(10, 43);
            this.labOrderNo.Name = "labOrderNo";
            this.labOrderNo.Size = new System.Drawing.Size(105, 14);
            this.labOrderNo.TabIndex = 2;
            this.labOrderNo.Text = "Order no";
            this.labOrderNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labLinceceNo
            // 
            this.labLinceceNo.Location = new System.Drawing.Point(10, 69);
            this.labLinceceNo.Name = "labLinceceNo";
            this.labLinceceNo.Size = new System.Drawing.Size(105, 14);
            this.labLinceceNo.TabIndex = 4;
            this.labLinceceNo.Text = "Licence no";
            this.labLinceceNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(10, 92);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(105, 14);
            this.labCustomer.TabIndex = 6;
            this.labCustomer.Text = "Customer";
            this.labCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labRemark
            // 
            this.labRemark.Location = new System.Drawing.Point(10, 111);
            this.labRemark.Name = "labRemark";
            this.labRemark.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labRemark.Size = new System.Drawing.Size(105, 14);
            this.labRemark.TabIndex = 9;
            this.labRemark.Text = "Remark";
            this.labRemark.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labInvoiceNo
            // 
            this.labInvoiceNo.Location = new System.Drawing.Point(268, 43);
            this.labInvoiceNo.Name = "labInvoiceNo";
            this.labInvoiceNo.Size = new System.Drawing.Size(105, 14);
            this.labInvoiceNo.TabIndex = 5;
            this.labInvoiceNo.Text = "Invoice no";
            this.labInvoiceNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labInvoiceDate
            // 
            this.labInvoiceDate.Location = new System.Drawing.Point(268, 69);
            this.labInvoiceDate.Name = "labInvoiceDate";
            this.labInvoiceDate.Size = new System.Drawing.Size(105, 14);
            this.labInvoiceDate.TabIndex = 6;
            this.labInvoiceDate.Text = "Invoice date";
            this.labInvoiceDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // richTextBoxRemark
            // 
            this.richTextBoxRemark.Location = new System.Drawing.Point(118, 111);
            this.richTextBoxRemark.Name = "richTextBoxRemark";
            this.richTextBoxRemark.Size = new System.Drawing.Size(463, 71);
            this.richTextBoxRemark.TabIndex = 9;
            this.richTextBoxRemark.Text = "";
            this.richTextBoxRemark.Validated += new System.EventHandler(this.richTextBoxRemark_Validated);
            // 
            // textBoxType
            // 
            this.textBoxType.Enabled = false;
            this.textBoxType.Location = new System.Drawing.Point(118, 14);
            this.textBoxType.Name = "textBoxType";
            this.textBoxType.ReadOnly = true;
            this.textBoxType.Size = new System.Drawing.Size(150, 20);
            this.textBoxType.TabIndex = 1;
            // 
            // textBoxOrderNo
            // 
            this.textBoxOrderNo.Location = new System.Drawing.Point(118, 39);
            this.textBoxOrderNo.MaxLength = 10;
            this.textBoxOrderNo.Name = "textBoxOrderNo";
            this.textBoxOrderNo.Size = new System.Drawing.Size(150, 20);
            this.textBoxOrderNo.TabIndex = 2;
            // 
            // textBoxLicNo
            // 
            this.textBoxLicNo.Location = new System.Drawing.Point(118, 65);
            this.textBoxLicNo.MaxLength = 15;
            this.textBoxLicNo.Name = "textBoxLicNo";
            this.textBoxLicNo.Size = new System.Drawing.Size(150, 20);
            this.textBoxLicNo.TabIndex = 3;
            // 
            // textBoxCustNo
            // 
            this.textBoxCustNo.Location = new System.Drawing.Point(118, 88);
            this.textBoxCustNo.MaxLength = 10;
            this.textBoxCustNo.Name = "textBoxCustNo";
            this.textBoxCustNo.Size = new System.Drawing.Size(81, 20);
            this.textBoxCustNo.TabIndex = 7;
            this.textBoxCustNo.TextChanged += new System.EventHandler(this.textBoxCustNo_TextChanged);
            this.textBoxCustNo.Validated += new System.EventHandler(this.textBoxCustNo_Validated);
            this.textBoxCustNo.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxCustNo_Validating);
            // 
            // textBoxCustName
            // 
            this.textBoxCustName.Enabled = false;
            this.textBoxCustName.Location = new System.Drawing.Point(207, 88);
            this.textBoxCustName.Name = "textBoxCustName";
            this.textBoxCustName.ReadOnly = true;
            this.textBoxCustName.Size = new System.Drawing.Size(374, 20);
            this.textBoxCustName.TabIndex = 8;
            // 
            // textBoxInvoiceNo
            // 
            this.textBoxInvoiceNo.Location = new System.Drawing.Point(375, 39);
            this.textBoxInvoiceNo.MaxLength = 50;
            this.textBoxInvoiceNo.Name = "textBoxInvoiceNo";
            this.textBoxInvoiceNo.Size = new System.Drawing.Size(206, 20);
            this.textBoxInvoiceNo.TabIndex = 5;
            // 
            // textBoxInvoiceDate
            // 
            this.textBoxInvoiceDate.Location = new System.Drawing.Point(375, 65);
            this.textBoxInvoiceDate.Name = "textBoxInvoiceDate";
            this.textBoxInvoiceDate.Size = new System.Drawing.Size(206, 20);
            this.textBoxInvoiceDate.TabIndex = 6;
            this.textBoxInvoiceDate.Validated += new System.EventHandler(this.textBoxInvoiceDate_Validated);
            this.textBoxInvoiceDate.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxInvoiceDate_Validating);
            // 
            // rbDepositToCashBox
            // 
            this.rbDepositToCashBox.AutoSize = true;
            this.rbDepositToCashBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rbDepositToCashBox.Location = new System.Drawing.Point(592, 193);
            this.rbDepositToCashBox.Name = "rbDepositToCashBox";
            this.rbDepositToCashBox.Size = new System.Drawing.Size(138, 17);
            this.rbDepositToCashBox.TabIndex = 26;
            this.rbDepositToCashBox.Text = "Deposit to cash box";
            this.rbDepositToCashBox.UseVisualStyleBackColor = true;
            this.rbDepositToCashBox.CheckedChanged += new System.EventHandler(this.rbDepositToCashBox_CheckedChanged);
            // 
            // rbWithdrawFromCashBox
            // 
            this.rbWithdrawFromCashBox.AutoSize = true;
            this.rbWithdrawFromCashBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rbWithdrawFromCashBox.Location = new System.Drawing.Point(592, 220);
            this.rbWithdrawFromCashBox.Name = "rbWithdrawFromCashBox";
            this.rbWithdrawFromCashBox.Size = new System.Drawing.Size(161, 17);
            this.rbWithdrawFromCashBox.TabIndex = 27;
            this.rbWithdrawFromCashBox.Text = "Withdraw from cash box";
            this.rbWithdrawFromCashBox.UseVisualStyleBackColor = true;
            this.rbWithdrawFromCashBox.CheckedChanged += new System.EventHandler(this.rbWithdrawFromCashBox_CheckedChanged);
            // 
            // labAmountToPay
            // 
            this.labAmountToPay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labAmountToPay.Location = new System.Drawing.Point(10, 192);
            this.labAmountToPay.Name = "labAmountToPay";
            this.labAmountToPay.Size = new System.Drawing.Size(105, 14);
            this.labAmountToPay.TabIndex = 10;
            this.labAmountToPay.Text = "Amount to pay";
            this.labAmountToPay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxAmountToPay
            // 
            this.textBoxAmountToPay.Location = new System.Drawing.Point(118, 191);
            this.textBoxAmountToPay.MaxLength = 24;
            this.textBoxAmountToPay.Name = "textBoxAmountToPay";
            this.textBoxAmountToPay.Size = new System.Drawing.Size(150, 20);
            this.textBoxAmountToPay.TabIndex = 11;
            this.textBoxAmountToPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxAmountToPay.TextChanged += new System.EventHandler(this.textBoxAmountToPay_TextChanged);
            this.textBoxAmountToPay.Validated += new System.EventHandler(this.textBoxAmountToPay_Validated);
            this.textBoxAmountToPay.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAmountToPay_Validating);
            // 
            // labCashReturn
            // 
            this.labCashReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labCashReturn.Location = new System.Drawing.Point(296, 192);
            this.labCashReturn.Name = "labCashReturn";
            this.labCashReturn.Size = new System.Drawing.Size(120, 14);
            this.labCashReturn.TabIndex = 12;
            this.labCashReturn.Text = "Total paid";
            this.labCashReturn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxTotalPaid
            // 
            this.textBoxTotalPaid.Enabled = false;
            this.textBoxTotalPaid.Location = new System.Drawing.Point(417, 191);
            this.textBoxTotalPaid.Name = "textBoxTotalPaid";
            this.textBoxTotalPaid.ReadOnly = true;
            this.textBoxTotalPaid.Size = new System.Drawing.Size(164, 20);
            this.textBoxTotalPaid.TabIndex = 13;
            this.textBoxTotalPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pbUndo
            // 
            this.pbUndo.Location = new System.Drawing.Point(724, 113);
            this.pbUndo.Name = "pbUndo";
            this.pbUndo.Size = new System.Drawing.Size(113, 28);
            this.pbUndo.TabIndex = 31;
            this.pbUndo.Text = "Undo";
            this.pbUndo.UseVisualStyleBackColor = true;
            this.pbUndo.Click += new System.EventHandler(this.pbUndo_Click);
            // 
            // pbReprint
            // 
            this.pbReprint.Location = new System.Drawing.Point(724, 153);
            this.pbReprint.Name = "pbReprint";
            this.pbReprint.Size = new System.Drawing.Size(113, 28);
            this.pbReprint.TabIndex = 32;
            this.pbReprint.Text = "Reprint";
            this.pbReprint.UseVisualStyleBackColor = true;
            this.pbReprint.Click += new System.EventHandler(this.pbReprint_Click);
            // 
            // butDeleteRow
            // 
            this.butDeleteRow.Location = new System.Drawing.Point(724, 294);
            this.butDeleteRow.Name = "butDeleteRow";
            this.butDeleteRow.Size = new System.Drawing.Size(112, 28);
            this.butDeleteRow.TabIndex = 25;
            this.butDeleteRow.Text = "Delete row";
            this.butDeleteRow.UseVisualStyleBackColor = true;
            this.butDeleteRow.Click += new System.EventHandler(this.butDeleteRow_Click);
            // 
            // butAddRow
            // 
            this.butAddRow.Location = new System.Drawing.Point(606, 294);
            this.butAddRow.Name = "butAddRow";
            this.butAddRow.Size = new System.Drawing.Size(112, 28);
            this.butAddRow.TabIndex = 24;
            this.butAddRow.Text = "Add row";
            this.butAddRow.UseVisualStyleBackColor = true;
            this.butAddRow.Click += new System.EventHandler(this.butAddRow_Click);
            // 
            // gridPayment
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPayment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridPayment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPayment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPaymentForm,
            this.colPaymentType,
            this.colCardType,
            this.colCashBox,
            this.colQuantity,
            this.colPaid,
            this.colChange,
            this.colTotal,
            this.colFee});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridPayment.DefaultCellStyle = dataGridViewCellStyle6;
            this.gridPayment.Location = new System.Drawing.Point(12, 327);
            this.gridPayment.Name = "gridPayment";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPayment.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gridPayment.RowTemplate.Height = 24;
            this.gridPayment.Size = new System.Drawing.Size(824, 209);
            this.gridPayment.TabIndex = 33;
            this.gridPayment.Tag = "";
            this.gridPayment.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPayment_CellClick);
            this.gridPayment.SelectionChanged += new System.EventHandler(this.gridPayment_SelectionChanged);
            // 
            // colPaymentForm
            // 
            this.colPaymentForm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colPaymentForm.HeaderText = "Payment form";
            this.colPaymentForm.Name = "colPaymentForm";
            this.colPaymentForm.ReadOnly = true;
            this.colPaymentForm.Width = 96;
            // 
            // colPaymentType
            // 
            this.colPaymentType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colPaymentType.HeaderText = "Payment type";
            this.colPaymentType.Name = "colPaymentType";
            this.colPaymentType.ReadOnly = true;
            this.colPaymentType.Width = 96;
            // 
            // colCardType
            // 
            this.colCardType.HeaderText = "CardType";
            this.colCardType.Name = "colCardType";
            this.colCardType.ReadOnly = true;
            // 
            // colCashBox
            // 
            this.colCashBox.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colCashBox.HeaderText = "Cash box";
            this.colCashBox.Name = "colCashBox";
            this.colCashBox.ReadOnly = true;
            this.colCashBox.Width = 76;
            // 
            // colQuantity
            // 
            this.colQuantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colQuantity.DefaultCellStyle = dataGridViewCellStyle2;
            this.colQuantity.HeaderText = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.ReadOnly = true;
            this.colQuantity.Width = 71;
            // 
            // colPaid
            // 
            this.colPaid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colPaid.DefaultCellStyle = dataGridViewCellStyle3;
            this.colPaid.HeaderText = "Paid";
            this.colPaid.Name = "colPaid";
            this.colPaid.ReadOnly = true;
            this.colPaid.Width = 53;
            // 
            // colChange
            // 
            this.colChange.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colChange.DefaultCellStyle = dataGridViewCellStyle4;
            this.colChange.HeaderText = "Change";
            this.colChange.Name = "colChange";
            this.colChange.ReadOnly = true;
            this.colChange.Width = 69;
            // 
            // colTotal
            // 
            this.colTotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colTotal.DefaultCellStyle = dataGridViewCellStyle5;
            this.colTotal.HeaderText = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            this.colTotal.Width = 56;
            // 
            // colFee
            // 
            this.colFee.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colFee.HeaderText = "Fee";
            this.colFee.Name = "colFee";
            this.colFee.ReadOnly = true;
            this.colFee.Width = 50;
            // 
            // cmbPaymentForm
            // 
            this.cmbPaymentForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentForm.FormattingEnabled = true;
            this.cmbPaymentForm.Location = new System.Drawing.Point(118, 217);
            this.cmbPaymentForm.Name = "cmbPaymentForm";
            this.cmbPaymentForm.Size = new System.Drawing.Size(206, 21);
            this.cmbPaymentForm.TabIndex = 15;
            this.cmbPaymentForm.SelectedIndexChanged += new System.EventHandler(this.cmbPaymentForm_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 14);
            this.label1.TabIndex = 14;
            this.label1.Text = "Form";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPaymentType
            // 
            this.cmbPaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentType.FormattingEnabled = true;
            this.cmbPaymentType.Location = new System.Drawing.Point(118, 244);
            this.cmbPaymentType.Name = "cmbPaymentType";
            this.cmbPaymentType.Size = new System.Drawing.Size(463, 21);
            this.cmbPaymentType.TabIndex = 17;
            this.cmbPaymentType.SelectedIndexChanged += new System.EventHandler(this.cmbPaymentType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 247);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 14);
            this.label2.TabIndex = 16;
            this.label2.Text = "Type";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCashBox
            // 
            this.cmbCashBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCashBox.FormattingEnabled = true;
            this.cmbCashBox.Location = new System.Drawing.Point(118, 271);
            this.cmbCashBox.Name = "cmbCashBox";
            this.cmbCashBox.Size = new System.Drawing.Size(463, 21);
            this.cmbCashBox.TabIndex = 19;
            this.cmbCashBox.SelectedIndexChanged += new System.EventHandler(this.cmbCashBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 274);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 14);
            this.label3.TabIndex = 18;
            this.label3.Text = "Cashbox";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxAmount
            // 
            this.textBoxAmount.Location = new System.Drawing.Point(118, 298);
            this.textBoxAmount.MaxLength = 24;
            this.textBoxAmount.Name = "textBoxAmount";
            this.textBoxAmount.Size = new System.Drawing.Size(150, 20);
            this.textBoxAmount.TabIndex = 21;
            this.textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxAmount.Validated += new System.EventHandler(this.textBoxAmount_Validated);
            this.textBoxAmount.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAmount_Validating);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(10, 301);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 14);
            this.label4.TabIndex = 20;
            this.label4.Text = "Amount";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxQuantity
            // 
            this.textBoxQuantity.Location = new System.Drawing.Point(517, 298);
            this.textBoxQuantity.MaxLength = 24;
            this.textBoxQuantity.Name = "textBoxQuantity";
            this.textBoxQuantity.Size = new System.Drawing.Size(64, 20);
            this.textBoxQuantity.TabIndex = 23;
            this.textBoxQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxQuantity.Validated += new System.EventHandler(this.textBoxQuantity_Validated);
            this.textBoxQuantity.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxQuantity_Validating);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(440, 301);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 14);
            this.label5.TabIndex = 22;
            this.label5.Text = "Quantity";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxReceiptNo
            // 
            this.textBoxReceiptNo.Enabled = false;
            this.textBoxReceiptNo.Location = new System.Drawing.Point(375, 14);
            this.textBoxReceiptNo.Name = "textBoxReceiptNo";
            this.textBoxReceiptNo.ReadOnly = true;
            this.textBoxReceiptNo.Size = new System.Drawing.Size(206, 20);
            this.textBoxReceiptNo.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(268, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 14);
            this.label6.TabIndex = 40;
            this.label6.Text = "Receipt no";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pbSearchCustomer
            // 
            this.pbSearchCustomer.Location = new System.Drawing.Point(587, 88);
            this.pbSearchCustomer.Name = "pbSearchCustomer";
            this.pbSearchCustomer.Size = new System.Drawing.Size(35, 20);
            this.pbSearchCustomer.TabIndex = 41;
            this.pbSearchCustomer.Text = "...";
            this.pbSearchCustomer.UseVisualStyleBackColor = true;
            this.pbSearchCustomer.Click += new System.EventHandler(this.pbSearchCustomer_Click);
            // 
            // textBoxFee
            // 
            this.textBoxFee.Location = new System.Drawing.Point(381, 298);
            this.textBoxFee.MaxLength = 24;
            this.textBoxFee.Name = "textBoxFee";
            this.textBoxFee.Size = new System.Drawing.Size(67, 20);
            this.textBoxFee.TabIndex = 42;
            this.textBoxFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxFee.Validated += new System.EventHandler(this.textBoxFee_Validated);
            this.textBoxFee.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxFee_Validating);
            // 
            // lbFee
            // 
            this.lbFee.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbFee.Location = new System.Drawing.Point(275, 301);
            this.lbFee.Name = "lbFee";
            this.lbFee.Size = new System.Drawing.Size(104, 14);
            this.lbFee.TabIndex = 43;
            this.lbFee.Text = "Fee";
            this.lbFee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCardTypes
            // 
            this.cmbCardTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCardTypes.Enabled = false;
            this.cmbCardTypes.FormattingEnabled = true;
            this.cmbCardTypes.Location = new System.Drawing.Point(417, 216);
            this.cmbCardTypes.Name = "cmbCardTypes";
            this.cmbCardTypes.Size = new System.Drawing.Size(164, 21);
            this.cmbCardTypes.TabIndex = 45;
            // 
            // lbCardTypes
            // 
            this.lbCardTypes.Enabled = false;
            this.lbCardTypes.Location = new System.Drawing.Point(330, 219);
            this.lbCardTypes.Name = "lbCardTypes";
            this.lbCardTypes.Size = new System.Drawing.Size(84, 14);
            this.lbCardTypes.TabIndex = 44;
            this.lbCardTypes.Text = "Card type";
            this.lbCardTypes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dlgCashRegisterPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.pbCancel;
            this.ClientSize = new System.Drawing.Size(848, 548);
            this.Controls.Add(this.cmbCardTypes);
            this.Controls.Add(this.lbCardTypes);
            this.Controls.Add(this.lbFee);
            this.Controls.Add(this.textBoxFee);
            this.Controls.Add(this.pbSearchCustomer);
            this.Controls.Add(this.textBoxReceiptNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxQuantity);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxAmount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbCashBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbPaymentType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbPaymentForm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridPayment);
            this.Controls.Add(this.butAddRow);
            this.Controls.Add(this.butDeleteRow);
            this.Controls.Add(this.pbReprint);
            this.Controls.Add(this.pbUndo);
            this.Controls.Add(this.textBoxTotalPaid);
            this.Controls.Add(this.labCashReturn);
            this.Controls.Add(this.textBoxAmountToPay);
            this.Controls.Add(this.labAmountToPay);
            this.Controls.Add(this.rbWithdrawFromCashBox);
            this.Controls.Add(this.rbDepositToCashBox);
            this.Controls.Add(this.textBoxInvoiceDate);
            this.Controls.Add(this.textBoxInvoiceNo);
            this.Controls.Add(this.textBoxCustName);
            this.Controls.Add(this.textBoxCustNo);
            this.Controls.Add(this.textBoxLicNo);
            this.Controls.Add(this.textBoxOrderNo);
            this.Controls.Add(this.textBoxType);
            this.Controls.Add(this.richTextBoxRemark);
            this.Controls.Add(this.labInvoiceDate);
            this.Controls.Add(this.labInvoiceNo);
            this.Controls.Add(this.labRemark);
            this.Controls.Add(this.labCustomer);
            this.Controls.Add(this.labLinceceNo);
            this.Controls.Add(this.labOrderNo);
            this.Controls.Add(this.labType);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "dlgCashRegisterPayment";
            this.Text = "Cash Register Payment";
            this.Load += new System.EventHandler(this.dlgCashRegisterPayment_Load);
            this.Controls.SetChildIndex(this.labType, 0);
            this.Controls.SetChildIndex(this.labOrderNo, 0);
            this.Controls.SetChildIndex(this.labLinceceNo, 0);
            this.Controls.SetChildIndex(this.labCustomer, 0);
            this.Controls.SetChildIndex(this.labRemark, 0);
            this.Controls.SetChildIndex(this.labInvoiceNo, 0);
            this.Controls.SetChildIndex(this.labInvoiceDate, 0);
            this.Controls.SetChildIndex(this.richTextBoxRemark, 0);
            this.Controls.SetChildIndex(this.textBoxType, 0);
            this.Controls.SetChildIndex(this.textBoxOrderNo, 0);
            this.Controls.SetChildIndex(this.textBoxLicNo, 0);
            this.Controls.SetChildIndex(this.textBoxCustNo, 0);
            this.Controls.SetChildIndex(this.textBoxCustName, 0);
            this.Controls.SetChildIndex(this.textBoxInvoiceNo, 0);
            this.Controls.SetChildIndex(this.textBoxInvoiceDate, 0);
            this.Controls.SetChildIndex(this.rbDepositToCashBox, 0);
            this.Controls.SetChildIndex(this.rbWithdrawFromCashBox, 0);
            this.Controls.SetChildIndex(this.labAmountToPay, 0);
            this.Controls.SetChildIndex(this.textBoxAmountToPay, 0);
            this.Controls.SetChildIndex(this.labCashReturn, 0);
            this.Controls.SetChildIndex(this.textBoxTotalPaid, 0);
            this.Controls.SetChildIndex(this.pbUndo, 0);
            this.Controls.SetChildIndex(this.pbReprint, 0);
            this.Controls.SetChildIndex(this.butDeleteRow, 0);
            this.Controls.SetChildIndex(this.pbOK, 0);
            this.Controls.SetChildIndex(this.pbCancel, 0);
            this.Controls.SetChildIndex(this.pbHelp, 0);
            this.Controls.SetChildIndex(this.butAddRow, 0);
            this.Controls.SetChildIndex(this.gridPayment, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cmbPaymentForm, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.cmbPaymentType, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cmbCashBox, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.textBoxAmount, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.textBoxQuantity, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.textBoxReceiptNo, 0);
            this.Controls.SetChildIndex(this.pbSearchCustomer, 0);
            this.Controls.SetChildIndex(this.textBoxFee, 0);
            this.Controls.SetChildIndex(this.lbFee, 0);
            this.Controls.SetChildIndex(this.lbCardTypes, 0);
            this.Controls.SetChildIndex(this.cmbCardTypes, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gridPayment)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labType;
        private System.Windows.Forms.Label labOrderNo;
        private System.Windows.Forms.Label labLinceceNo;
        private System.Windows.Forms.Label labCustomer;
        private System.Windows.Forms.Label labRemark;
        private System.Windows.Forms.Label labInvoiceNo;
        private System.Windows.Forms.Label labInvoiceDate;
        private System.Windows.Forms.RichTextBox richTextBoxRemark;
        private System.Windows.Forms.TextBox textBoxType;
        private System.Windows.Forms.TextBox textBoxOrderNo;
        private System.Windows.Forms.TextBox textBoxLicNo;
        private System.Windows.Forms.TextBox textBoxCustNo;
        private System.Windows.Forms.TextBox textBoxCustName;
        private System.Windows.Forms.TextBox textBoxInvoiceNo;
        private System.Windows.Forms.TextBox textBoxInvoiceDate;
        private System.Windows.Forms.RadioButton rbDepositToCashBox;
        private System.Windows.Forms.RadioButton rbWithdrawFromCashBox;
        private System.Windows.Forms.Label labAmountToPay;
        private System.Windows.Forms.TextBox textBoxAmountToPay;
        private System.Windows.Forms.Label labCashReturn;
        private System.Windows.Forms.TextBox textBoxTotalPaid;
        private System.Windows.Forms.Button pbUndo;
        private System.Windows.Forms.Button pbReprint;
        private System.Windows.Forms.Button butDeleteRow;
        private System.Windows.Forms.Button butAddRow;
        private System.Windows.Forms.DataGridView gridPayment;
        private System.Windows.Forms.ComboBox cmbPaymentForm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPaymentType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCashBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxQuantity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxReceiptNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button pbSearchCustomer;
        private System.Windows.Forms.TextBox textBoxFee;
        private System.Windows.Forms.Label lbFee;
        private System.Windows.Forms.ComboBox cmbCardTypes;
        private System.Windows.Forms.Label lbCardTypes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentForm;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCashBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChange;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFee;
    }
}