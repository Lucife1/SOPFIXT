namespace WindowsFormsApp1
{
    partial class PaymentSeller
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentSeller));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.cmbID = new System.Windows.Forms.ComboBox();
            this.cmbBoxPayType = new System.Windows.Forms.ComboBox();
            this.txtHarga = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblHarga = new System.Windows.Forms.Label();
            this.lblCard = new System.Windows.Forms.Label();
            this.lblBank = new System.Windows.Forms.Label();
            this.txtCard = new System.Windows.Forms.TextBox();
            this.cmbBank = new System.Windows.Forms.ComboBox();
            this.lblChange = new System.Windows.Forms.Label();
            this.dgvSellDetails = new System.Windows.Forms.DataGridView();
            this.lblch = new System.Windows.Forms.Label();
            this.lbltotalpay = new System.Windows.Forms.Label();
            this.btnEnd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSellDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Cyan;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBoxClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(798, 57);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Geometr212 BkCn BT", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 42);
            this.label2.TabIndex = 8;
            this.label2.Text = "Payment";
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxClose.Image")));
            this.pictureBoxClose.Location = new System.Drawing.Point(729, 0);
            this.pictureBoxClose.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(56, 46);
            this.pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxClose.TabIndex = 10;
            this.pictureBoxClose.TabStop = false;
            this.pictureBoxClose.Click += new System.EventHandler(this.pictureBoxClose_Click);
            // 
            // cmbID
            // 
            this.cmbID.FormattingEnabled = true;
            this.cmbID.Location = new System.Drawing.Point(214, 88);
            this.cmbID.Margin = new System.Windows.Forms.Padding(4);
            this.cmbID.Name = "cmbID";
            this.cmbID.Size = new System.Drawing.Size(460, 27);
            this.cmbID.TabIndex = 8;
            this.cmbID.Text = "Select Order ID";
            this.cmbID.SelectedIndexChanged += new System.EventHandler(this.cmbID_SelectedIndexChanged);
            // 
            // cmbBoxPayType
            // 
            this.cmbBoxPayType.FormattingEnabled = true;
            this.cmbBoxPayType.Location = new System.Drawing.Point(13, 332);
            this.cmbBoxPayType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBoxPayType.Name = "cmbBoxPayType";
            this.cmbBoxPayType.Size = new System.Drawing.Size(232, 27);
            this.cmbBoxPayType.TabIndex = 4;
            this.cmbBoxPayType.SelectedIndexChanged += new System.EventHandler(this.cmbBoxPayType_SelectedIndexChanged);
            // 
            // txtHarga
            // 
            this.txtHarga.Location = new System.Drawing.Point(284, 332);
            this.txtHarga.Margin = new System.Windows.Forms.Padding(4);
            this.txtHarga.Name = "txtHarga";
            this.txtHarga.Size = new System.Drawing.Size(232, 26);
            this.txtHarga.TabIndex = 5;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(13, 373);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(56, 19);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "Total :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 309);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "Payment Type";
            // 
            // lblHarga
            // 
            this.lblHarga.AutoSize = true;
            this.lblHarga.Location = new System.Drawing.Point(284, 309);
            this.lblHarga.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHarga.Name = "lblHarga";
            this.lblHarga.Size = new System.Drawing.Size(98, 19);
            this.lblHarga.TabIndex = 9;
            this.lblHarga.Text = "Input Harga";
            // 
            // lblCard
            // 
            this.lblCard.AutoSize = true;
            this.lblCard.Location = new System.Drawing.Point(284, 309);
            this.lblCard.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCard.Name = "lblCard";
            this.lblCard.Size = new System.Drawing.Size(104, 19);
            this.lblCard.TabIndex = 10;
            this.lblCard.Text = "Card Number";
            // 
            // lblBank
            // 
            this.lblBank.AutoSize = true;
            this.lblBank.Location = new System.Drawing.Point(557, 309);
            this.lblBank.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBank.Name = "lblBank";
            this.lblBank.Size = new System.Drawing.Size(46, 19);
            this.lblBank.TabIndex = 12;
            this.lblBank.Text = "Bank";
            // 
            // txtCard
            // 
            this.txtCard.Location = new System.Drawing.Point(284, 333);
            this.txtCard.Margin = new System.Windows.Forms.Padding(4);
            this.txtCard.Name = "txtCard";
            this.txtCard.Size = new System.Drawing.Size(232, 26);
            this.txtCard.TabIndex = 13;
            // 
            // cmbBank
            // 
            this.cmbBank.FormattingEnabled = true;
            this.cmbBank.Location = new System.Drawing.Point(553, 332);
            this.cmbBank.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBank.Name = "cmbBank";
            this.cmbBank.Size = new System.Drawing.Size(232, 27);
            this.cmbBank.TabIndex = 14;
            // 
            // lblChange
            // 
            this.lblChange.AutoSize = true;
            this.lblChange.Location = new System.Drawing.Point(13, 417);
            this.lblChange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(95, 19);
            this.lblChange.TabIndex = 15;
            this.lblChange.Text = "Kembalian :";
            // 
            // dgvSellDetails
            // 
            this.dgvSellDetails.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Geometr212 BkCn BT", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSellDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSellDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSellDetails.EnableHeadersVisualStyles = false;
            this.dgvSellDetails.Location = new System.Drawing.Point(214, 133);
            this.dgvSellDetails.Name = "dgvSellDetails";
            this.dgvSellDetails.RowHeadersWidth = 51;
            this.dgvSellDetails.RowTemplate.Height = 24;
            this.dgvSellDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSellDetails.Size = new System.Drawing.Size(342, 141);
            this.dgvSellDetails.TabIndex = 18;
            // 
            // lblch
            // 
            this.lblch.AutoSize = true;
            this.lblch.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblch.ForeColor = System.Drawing.Color.Black;
            this.lblch.Location = new System.Drawing.Point(130, 417);
            this.lblch.Name = "lblch";
            this.lblch.Size = new System.Drawing.Size(0, 19);
            this.lblch.TabIndex = 19;
            this.lblch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbltotalpay
            // 
            this.lbltotalpay.AutoSize = true;
            this.lbltotalpay.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalpay.ForeColor = System.Drawing.Color.Black;
            this.lbltotalpay.Location = new System.Drawing.Point(87, 373);
            this.lbltotalpay.Name = "lbltotalpay";
            this.lbltotalpay.Size = new System.Drawing.Size(0, 19);
            this.lbltotalpay.TabIndex = 18;
            this.lbltotalpay.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(408, 373);
            this.btnEnd.Margin = new System.Windows.Forms.Padding(4);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(377, 31);
            this.btnEnd.TabIndex = 20;
            this.btnEnd.Text = "Pay";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 91);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 19);
            this.label1.TabIndex = 21;
            this.label1.Text = "Select Order ID :";
            // 
            // PaymentSeller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 446);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.lblch);
            this.Controls.Add(this.lbltotalpay);
            this.Controls.Add(this.dgvSellDetails);
            this.Controls.Add(this.lblChange);
            this.Controls.Add(this.cmbBank);
            this.Controls.Add(this.txtCard);
            this.Controls.Add(this.lblBank);
            this.Controls.Add(this.lblCard);
            this.Controls.Add(this.lblHarga);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.txtHarga);
            this.Controls.Add(this.cmbBoxPayType);
            this.Controls.Add(this.cmbID);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Geometr212 BkCn BT", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PaymentSeller";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PaymentSeller";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSellDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbID;
        private System.Windows.Forms.ComboBox cmbBoxPayType;
        private System.Windows.Forms.TextBox txtHarga;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblHarga;
        private System.Windows.Forms.Label lblCard;
        private System.Windows.Forms.Label lblBank;
        private System.Windows.Forms.TextBox txtCard;
        private System.Windows.Forms.ComboBox cmbBank;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Label lblch;
        private System.Windows.Forms.Label lbltotalpay;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.DataGridView dgvSellDetails;
        private System.Windows.Forms.Label label1;
    }
}