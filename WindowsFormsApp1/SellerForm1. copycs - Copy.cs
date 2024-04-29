using Microsoft.Reporting.Map.WebForms.BingMaps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class SellerForm1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rasya\Documents\Works\Coding\SOP_fIX\dbMS1.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        private string SellerName;
        private int selectedOrderId;
        int qty = 0;
        public SellerForm1(string SellerName)
        {
            this.SellerName = SellerName;
            InitializeComponent();
            LoadSellerData();
            LoadProduct();

        }
        private void LoadSellerData()
        {
            int sellerID = GetSellerID(SellerName);
            dgvCustomer.Rows.Add(sellerID, SellerName);
        }

        private int GetSellerID(string sellerName)
        {
            int sellerID = GetSellerIDFromDatabase(sellerName);
            return sellerID;    
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private int GetSellerIDFromDatabase(string sellerName)
        {
            int sellerID = 0;
            try
            {
                con.Open();
                cm = new SqlCommand("SELECT sid FROM tbSeller WHERE sname = @SellerName", con);
                cm.Parameters.AddWithValue("@SellerName", sellerName); // Add parameter for SellerName
                dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    sellerID = Convert.ToInt32(dr["sid"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return sellerID;
        }
        private void SellingForm_Load(object sender, EventArgs e)
        {
            SlrNameL.Text = Login.SellerName; 
            LoadSellList();
        }

        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pid, pname, pprice, pdesc, pcategory) LIKE '%" + txtSeacrhProd.Text + "%'", con);
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

        private void txtSeacrhProd_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        public void Clear()
        {
            txtPid.Clear();
            txtPname.Clear();
            txtPrice.Clear();
            txtTotal.Clear();
            txtSeacrhProd.Clear();
            UDQty.Value = 0;
            dateOrder.Value = DateTime.Now;
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

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPid.Text == "")
                {
                    MessageBox.Show("Select Product!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Apakah ingin Mengorder produk ini?", "Memesan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbSellList(slid, slname, sldate, prname, slqty, slprice, sltotal)VALUES(@slid, @slname, @sldate, @prname, @slqty, @slprice, @sltotal)", con);
                    cm.Parameters.AddWithValue("@slid", SlrNameL.Text);
                    cm.Parameters.AddWithValue("@slname", SellerName);
                    cm.Parameters.AddWithValue("@sldate", dateOrder.Value);
                    cm.Parameters.AddWithValue("@prname", txtPname.Text);
                    cm.Parameters.AddWithValue("@slqty", UDQty.Value);
                    cm.Parameters.AddWithValue("@slprice", txtPrice.Text);
                    cm.Parameters.AddWithValue("@sltotal", txtTotal.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Produk sudah berhasil Dipesan");

                    // Update product quantity
                    cm = new SqlCommand("UPDATE tbProduct SET pqty = (pqty-@pqty) WHERE pid = @pid", con);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(UDQty.Text));
                    cm.Parameters.AddWithValue("@pid", txtPid.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();

                    // Clear previous orders from dgvSellList
                    dgvSellList.Rows.Clear();

                    // Add the newly inserted order to dgvSellList
                    dgvSellList.Rows.Add(SlrNameL.Text, SellerName, dateOrder.Value.ToString(), txtPname.Text, UDQty.Value.ToString(), txtPrice.Text, txtTotal.Text);

                    Clear();
                    LoadProduct(); // Optional: Reload products if needed
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadSellList()
        {
            // Clear existing rows
            dgvSellList.Rows.Clear();

            try
            {
                con.Open();
                cm = new SqlCommand("SELECT * FROM tbSellList WHERE slname = @slname", con);
                cm.Parameters.AddWithValue("@slname", SellerName);
                dr = cm.ExecuteReader();

                int i = 0;
                while (dr.Read())
                {
                    dgvSellList.Rows.Add();
                    dgvSellList.Rows[i].Cells[0].Value = dr["slid"].ToString();
                    dgvSellList.Rows[i].Cells[1].Value = dr["slname"].ToString();
                    dgvSellList.Rows[i].Cells[2].Value = dr["sldate"].ToString();
                    dgvSellList.Rows[i].Cells[3].Value = dr["prname"].ToString();
                    dgvSellList.Rows[i].Cells[4].Value = dr["slqty"].ToString();
                    dgvSellList.Rows[i].Cells[5].Value = dr["slprice"].ToString();
                    dgvSellList.Rows[i].Cells[6].Value = dr["sltotal"].ToString();
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
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

        private void txtPid_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateOrder_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string selectSlName = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                Slname (selectSlName);
            }
        }

        private void Slname(string sellerName)
        {
            dgvSellList.Rows.Clear();

            try
            {
                con.Open();
                cm = new SqlCommand("SELECT * FROM tbSeller WHERE sname = @sname", con);
                cm.Parameters.AddWithValue("@sname", sellerName);
                dr = cm.ExecuteReader();

                    int i = 0;
                    while (dr.Read())
                    {
                        dgvSellList.Rows.Add();
                        dgvSellList.Rows[i].Cells[1].Value = dr["sname"].ToString();
                        i++;
                    }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
               if (dr != null) 
                { 
                    dr.Close(); 
                }
            }

            if (con.State == ConnectionState.Open)
            { 
                con.Close();
                
            }
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvSellList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Retrieve the selected order ID
               selectedOrderId = Convert.ToInt32(dgvSellList.Rows[e.RowIndex].Cells[0].Value);
            }
        }

        private void PrintOrderSummary()
        {
            printDocument1 = new PrintDocument();
            printDocument1.PrintPage += printDocument1_PrintPage;

            PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        public void SetSellerName(string sellerName)
        {
            this.SellerName = sellerName;
            // You can perform any additional actions related to setting the seller name here
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintOrderSummary();
        }

        private int rowIndex = 0;
        private double totalAmount = 0; 

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int OrderSummary = 50;
            int PosRemainingContent = OrderSummary + 100;

            int marginLeft = 50;
            int marginTop = 50;

            e.Graphics.DrawString("ORDER SUMMARY", new Font("Century", 25, FontStyle.Bold), Brushes.Red, new System.Drawing.Point(marginLeft, OrderSummary + marginTop));

            int cellHeight = 40;
            int cellWidth = 300;
            int x = marginLeft;
            int y = PosRemainingContent + marginTop;

            while (rowIndex < dgvSellList.SelectedRows.Count)
            {
                DataGridViewRow row = dgvSellList.SelectedRows[rowIndex];

                // Print data for each selected row
                e.Graphics.DrawString("Order Id: " + row.Cells[0].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new System.Drawing.Point(x, y));
                y += cellHeight;
                e.Graphics.DrawString("Seller Name: " + row.Cells[1].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new System.Drawing.Point(x, y));
                y += cellHeight;
                e.Graphics.DrawString("Date : " + row.Cells[2].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new System.Drawing.Point(x, y));
                y += cellHeight;
                double price = Convert.ToDouble(row.Cells[6].Value);
                e.Graphics.DrawString("Total Amount : " + price.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new System.Drawing.Point(x, y));
                y += cellHeight;

                totalAmount += price; // Add the current row's total amount to the overall total

                // Add some spacing between rows
                y += cellHeight;

                rowIndex++;

                // Check if there are more rows to print, if yes, add a new page
                if (y + cellHeight >= e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            // If we reach here, it means all selected rows have been printed
            e.Graphics.DrawString("Total Amount for All Orders: " + totalAmount.ToString(), new Font("Century", 18, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(x, y));
            y += cellHeight;

            // Reset the row index and total amount for next printing
            rowIndex = 0;
            totalAmount = 0;

            e.HasMorePages = false; // No more pages to print
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            List<int> orderIDs = GetOrderIDsForSeller(SellerName);

            // Pass the order IDs to the PaymentSeller form
            PaymentSeller paymentForm = new PaymentSeller(SellerName, orderIDs);
            paymentForm.ShowDialog();
        }

        private List<int> GetOrderIDsForSeller(string sellerName)
        {
            List<int> orderIDs = new List<int>();
            try
            {
                con.Open();
                cm = new SqlCommand("SELECT slid FROM tbSellList WHERE slname = @SellerName", con);
                cm.Parameters.AddWithValue("@SellerName", sellerName);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    orderIDs.Add(Convert.ToInt32(dr["slid"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return orderIDs;
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            Login logout = new Login();
            this.Hide();
            MessageBox.Show("You Has Been Logged Out");
            logout.ShowDialog();
            this.Close();
        }
    }
}


