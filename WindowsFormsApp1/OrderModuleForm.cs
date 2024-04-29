using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class OrderModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rasya\Documents\Works\Coding\SOP_fIX\dbMS1.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        int qty = 0;
        public OrderModuleForm()
        {
            InitializeComponent();
            LoadCustomer();
            LoadProduct();  
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cm = new SqlCommand("SELECT cid, cname FROM tbCustomer WHERE CONCAT(cid,cname) LIKE '%" + txtSeacrhCust.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pid, pname, pprice, pdesc, pcategory) LIKE '%" + txtSeacrhProd.Text+ "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtSeacrhCust_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void txtSeacrhProd_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtCid.Text == "")
                {
                    MessageBox.Show("Select Customer!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if(txtPid.Text == "")
                {
                    MessageBox.Show("Select Product!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     return;
                }
                if (MessageBox.Show("Apakah ingin Mengorder produk ini?", "Memesan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbOrder(odate,pid,cid,qty,price,total)VALUES(@odate,@pid,@cid,@qty,@price,@total)", con);
                    cm.Parameters.AddWithValue("@odate", dateOrder.Value);
                    cm.Parameters.AddWithValue("@pid",Convert.ToInt16(txtPid.Text));
                    cm.Parameters.AddWithValue("@cid",Convert.ToInt16(txtCid.Text));
                    cm.Parameters.AddWithValue("@qty",Convert.ToInt16(UDQty.Text));
                    cm.Parameters.AddWithValue("@price",Convert.ToInt16(txtPrice.Text));
                    cm.Parameters.AddWithValue("@total",Convert.ToInt16(txtTotal.Text));
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Produk sudah berhasil Dipesan");

                    cm = new SqlCommand("UPDATE tbProduct SET pqty = (pqty-@pqty) WHERE pid LIKE '" + txtPid.Text + "' ", con);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(UDQty.Text));
                    
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    LoadProduct();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
            {
                txtPid.Clear();
                txtCid.Clear();
                txtPrice.Clear();   
                txtTotal.Clear();
                dateOrder.Value = DateTime.Now;
                UDQty.Value = 0;
                txtCname.Clear();
                txtPname.Clear();
            }

        private void OrderModuleForm_Load(object sender, EventArgs e)
        {

        }
    
        private void UDQty_ValueChanged(object sender, EventArgs e)
        {
             GetQty();
            if (Convert.ToInt16(UDQty.Value) > qty)
            {
                MessageBox.Show("Ga ada Stok", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                UDQty.Value = UDQty.Value - 1;
                return;
            }
            if (Convert.ToInt16(UDQty.Value) > 0)
            {
                int total = Convert.ToInt16(txtPrice.Text) * Convert.ToInt16(UDQty.Value);
                txtTotal.Text = total.ToString();
            }
            
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCid.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCname.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPname.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
            //qty = Convert.ToInt16(dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnInsert.Enabled = true;
        }

        public void GetQty()
        {
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE pid='" + txtPid.Text + "'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                qty = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void PrintOrderSummary()
        {
            try
            {
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message); 
            }
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int OrderSummary = 50;
            int PosRemainingContent = OrderSummary + 100;

            int marginLeft = 50;
            int marginTop = 50;


            e.Graphics.DrawString("ORDER SUMMARY", new Font("Century", 25, FontStyle.Bold), Brushes.Red, new Point(marginLeft, OrderSummary + marginTop));

            int cellHeight = 40;
            int cellWidth = 300;
            int x = marginLeft;
            int y = PosRemainingContent + marginTop;


            e.Graphics.DrawString("Product Id: " + dgvProduct.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
            y += cellHeight;
            e.Graphics.DrawString("Product Name: " + dgvProduct.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
            y += cellHeight;
            e.Graphics.DrawString("QTY: " + dgvProduct.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
            y += cellHeight;
            e.Graphics.DrawString("Price : " + dgvProduct.SelectedRows[0].Cells[4].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));


        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintOrderSummary();
        }

        private void lblOid_Click(object sender, EventArgs e)
        {

        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
