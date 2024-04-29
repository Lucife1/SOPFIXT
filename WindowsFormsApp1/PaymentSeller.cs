using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class PaymentSeller : Form
    {
        koneksi kon = new koneksi();
        int totalHarga ;
        SqlCommand cm = new SqlCommand();
        string SellerId;    
        public PaymentSeller(String SellerId)
        {
            InitializeComponent();
            this.SellerId = SellerId;

            setDefault();

        }

        private void loadDGOrder(String orderId)
        {

            DataTable dt = new DataTable();
            dt.Rows.Clear();
            dgvSellDetails.Refresh();
            dgvSellDetails.DataSource = dt;

            kon.select("select tbProduct.pname, tbSellList.slqty, tbProduct.pprice, (tbSellList.slqty * tbProduct.pprice) As Total from tbSellList left join tbProduct on tbSellList.pid = tbProduct.pid Where payid = '" + orderId + "'");
            kon.adp.Fill(dt);

            dgvSellDetails.Columns[0].HeaderText = "Product Name";
            dgvSellDetails.Columns[1].HeaderText = "Quantity";
            dgvSellDetails.Columns[2].HeaderText = "Price";
            dgvSellDetails.Columns[3].HeaderText = "Total";

            dgvSellDetails.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            dgvSellDetails.DataSource = dt;

            foreach (DataRow dtr in dt.Rows)
            {
                totalHarga += Int32.Parse(dtr[3].ToString());
            }

            refreshLabel();

        }

        private void cmbID_SelectedIndexChanged(object sender, EventArgs e)
        {
            totalHarga = 0;
            loadDGOrder(cmbID.Text);
        }
        private void cmbBoxPayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBoxPayType.Text != "")
            {
                if (cmbBoxPayType.Text == "Card")
                {
                    lblCard.Visible = true;
                    lblBank.Visible = true;
                    lblHarga.Visible = false;

                    lblCard.Enabled = true;
                    lblBank.Enabled = true;
                    lblHarga.Enabled = false;

                    txtCard.Enabled = true;
                    cmbBank.Enabled = true;
                    txtHarga.Enabled = false;

                    txtCard.Visible = true;
                    cmbBank.Visible = true;
                    txtHarga.Visible = false;
                }

                else if (cmbBoxPayType.Text == "Cash")
                {
                    lblCard.Visible = false;
                    lblBank.Visible = false;
                    lblHarga.Visible = true;

                    lblCard.Enabled = false;
                    lblBank.Enabled = false;
                    lblHarga.Enabled = true;

                    txtCard.Enabled = false;
                    cmbBank.Enabled = false;
                    txtHarga.Enabled = true;

                    txtCard.Visible = false;
                    cmbBank.Visible = false;
                    txtHarga.Visible = true;
                }
            }
        }
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void setDefault()
        {
            lblCard.Visible = false;
            lblBank.Visible = false;
            lblHarga.Visible = false;

            lblCard.Enabled = false;
            lblBank.Enabled = false;
            lblHarga.Enabled = false;

            txtCard.Enabled = false;
            cmbBank.Enabled = false;
            txtHarga.Enabled = false;

            txtCard.Visible = false;
            cmbBank.Visible = false;
            txtHarga.Visible = false;

            loadOrderID();
            loadCBPayment();
            refreshLabel();
            loadCBBank();
            totalHarga = 0;

            txtHarga.Text = "";
            txtCard.Text = "";
            cmbBoxPayType.Text = "";
            cmbBank.Text = "";
            cmbID.Text = "";

            dgvSellDetails.DataSource = null;

        }


        private void refreshLabel()
        {
            lblTotal.Text = "Total : " + totalHarga.ToString("C", CultureInfo.CreateSpecificCulture("id-ID")); ;
        }

        private void loadOrderID()
        {
            cmbID.Items.Clear();
            cmbID.Refresh();

            DataTable cb = new DataTable();
            cb.Clear();
            kon.select("SELECT payid FROM tbPay WHERE paytype IS NULL");
            kon.adp.Fill(cb);

            foreach (DataRow dtr in cb.Rows)
            {
                cmbID.Items.Add(dtr[0].ToString().TrimEnd());
            }
        }

        private void loadCBPayment()
        {
            cmbBoxPayType.Items.Clear();
            cmbBoxPayType.Refresh();

            cmbBoxPayType.Items.Add("Cash");
            cmbBoxPayType.Items.Add("Card");
        }

        private void loadCBBank()
        {
            cmbBank.Items.Clear();


            cmbBank.Items.Add("BCA");
            cmbBank.Items.Add("BNI");
            cmbBank.Items.Add("MANDIRI");
            cmbBank.Items.Add("BSI");
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            bool finish = false;
            try
            {
                if (cmbBoxPayType.Text == "")
                {
                    MessageBox.Show("Harap isi terlebih dahulu");
                    finish = false;
                }
                else if (cmbBoxPayType.Text == "Credit" && (txtCard.Text == "" || cmbBank.Text == ""))
                {
                    MessageBox.Show("Harap Isi Card Number & Nama Bank");
                    finish = false;
                }
                else if (cmbBoxPayType.Text == "Cash" && (txtHarga.Text == ""))
                {
                    MessageBox.Show("Harap Isi Input Bayar");
                    finish = false;
                }
                else
                {
                    if (cmbBoxPayType.Text == "Credit")
                    {
                        int valnum = 0;

                        if (int.TryParse(txtCard.Text.Trim(), out valnum))
                        {
                            kon.cud("UPDATE tbPay SET sid ='" + SellerId + "', pdate='" + DateTime.Now.ToString("yyyy-MM-dd") + "', paytype='" + cmbBoxPayType.Text + "', paycard='" + txtCard.Text + "', paybank='" + cmbBank.Text + "' WHERE payid='" + cmbID.Text + "'");
                            MessageBox.Show("Pembayaran Berhasil");
                            finish = true;
                        }
                        else
                        {
                            //not a number
                            MessageBox.Show("Harap Isikan Nilai Angka dengan benar");
                            finish = false;
                        }
                    }
                    else
                    {
                        int valnum = 0;

                        if (int.TryParse(txtHarga.Text.Trim(), out valnum))
                        {
                            if (Int32.Parse(txtHarga.Text) < totalHarga)
                            {
                                MessageBox.Show("Uang Anda Kurang");
                                finish = false;
                            }
                            else
                            {
                                kon.cud("UPDATE tbPay SET sid ='" + SellerId + "', pdate='" + DateTime.Now.ToString("yyyy-MM-dd") + "', paytype='" + cmbBoxPayType.Text + "' WHERE payid='" + cmbID.Text + "'");

                                MessageBox.Show("Pembayaran Berhasil");
                                MessageBox.Show("Kembalian Anda : " + (Int32.Parse(txtHarga.Text) - totalHarga).ToString("C", CultureInfo.CreateSpecificCulture("id-ID")));
                                finish = true;
                            }
                        }
                        else
                        {
                            //not a number
                            MessageBox.Show("Harap Isikan Nilai Angka dengan benar");
                            finish = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (finish)
                { setDefault(); }


            }
        }
    }
}

    


    


