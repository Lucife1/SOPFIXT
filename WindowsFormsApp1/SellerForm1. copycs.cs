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
        koneksi con = new koneksi();
        DataTable dt = new DataTable(); // DG Menu
        DataTable dt2 = new DataTable(); // DG Order List
        DataTable dt3 = new DataTable(); // Temporary DT
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        string SellerId;
        int ProductId, rowIndexDeleted = 0;
        Int64 countID = 0;
        bool equalStatus = false;
        int totalSum = 0;
        private int selectedOrderId;
        int qty = 0;
        public SellerForm1(String SellerId)
        {
            this.SellerId = SellerId;


            InitializeComponent();
            LoadProduct();
            RefreshLabel();

            btnInsert.Enabled = false;
            button1.Enabled = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private string autoID()
        {
            DataTable dtID = new DataTable();
            con.select("SELECT count(*) FROM tbPay");
            con.adp.Fill(dtID);

            foreach (DataRow dtr in dtID.Rows)
            {
                countID = 1000 + Int64.Parse(dtr[0].ToString());
            }

            DateTime date = DateTime.UtcNow.Date;
            //countID = 1000 + dtID.Rows.Count;
            string generateId = date.ToString("yyyy") + date.ToString("MM") + date.ToString("dd") + countID.ToString();

            return generateId.Trim();
        }

        private void RefreshLabel()
        {
            txtPname.Clear();
            UDQty.Value = 0;

            btnInsert.Enabled = false;
            button1.Enabled = false;
        }

        private void RefreshAll()
        {
            totalSum  = 0;
            countID = 0;

            RefreshLabel();
            dgvSellList.Rows.Clear();
            dgvSellList.Refresh();
        }

        /*private void SellingForm_Load(object sender, EventArgs e)
        {
            SlrNameL.Text = Login.SellerName; 
            LoadSellList();
        }*/

        public void LoadProduct()
        {

            dt.Clear();
            dgvProduct.DataSource = dt;
            dgvProduct.Refresh();
            con.select("SELECT * FROM tbProduct");
            con.adp.Fill(dt);
            dgvProduct.DataSource = dt;

            dgvProduct.Columns[0].HeaderText = "Product ID";
            dgvProduct.Columns[1].HeaderText = "Product Name";
            dgvProduct.Columns[2].HeaderText = "QTY";
            dgvProduct.Columns[3].HeaderText = "Price";
            dgvProduct.Columns[4].HeaderText = "Desc";
            dgvProduct.Columns[5].HeaderText = "Category";
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
            try
            {
                con.con.Open();
                cm = new SqlCommand("SELECT pqty FROM tbProduct WHERE pid = @pid", con.con);
                cm.Parameters.AddWithValue("@pid", Convert.ToInt32(txtPid.Text)); // Pastikan untuk mengonversi nilai txtPid ke tipe data yang sesuai
                dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    qty = Convert.ToInt32(dr["pqty"]); // Ambil nilai QTY dari kolom pqty
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }
                if (con.con.State == ConnectionState.Open)
                {
                    con.con.Close();
                }
            }
        }


        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProductId != 0)
                {
                    dt3.Clear(); // Clear Datatable
                    con.select("SELECT * FROM tbProduct WHERE pid='" + ProductId + "'"); // Get Data

                    con.adp.Fill(dt3);

                    if (dt3.Rows.Count != 0) // Checking Not 0
                    {
                        DataRow dtr = dt3.Rows[0]; // Get Data in Row
                        int price = int.Parse(dtr[3].ToString()) * int.Parse(UDQty.Text); // Calculate Price

                        int maxCount = dgvSellList.RowCount; // Max Iteration in DataGridView
                        int count = 0;

                        foreach (DataGridViewRow row in dgvSellList.Rows)
                        {
                            count += 1;
                            if (count == maxCount) // This Line is Important, because Datagridview also read a last row, which is empty row
                            {
                                break;
                            }

                            // Check Equals Data For Update
                            if (row.Cells[0].Value.ToString() == dtr[0].ToString())
                            {
                                equalStatus = true;

                                totalSum -= int.Parse(row.Cells[4].Value.ToString());

                                row.Cells[2].Value = UDQty.Text;
                                row.Cells[3].Value = price;

                                totalSum += int.Parse(row.Cells[4].Value.ToString());

                                break;
                            } // Give status = false
                            else
                            {
                                equalStatus = false;
                            }

                        }
                        count = 0; // reset counter

                        if (equalStatus == false) // add new row, if not find equal data on order list
                        {
                            if ((dgvSellList.RowCount - 1) < dt.Rows.Count) // for restrict add new menu over sum of menu list
                            {
                                dgvSellList.Rows.Add(
                                    dtr[0].ToString(),
                                    dtr[1].ToString(),
                                    UDQty.Text,
                                    dtr[3].ToString(),
                                    price,
                                    dtr[4].ToString()
                                );
                                totalSum += price;
                            }
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
                RefreshLabel();
            }
        }


        /*if (MessageBox.Show("Apakah ingin Mengorder produk ini?", "Memesan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            cm = new SqlCommand("INSERT INTO tbSellList(slid, slname, sldate, prname, slqty, slprice, sltotal)VALUES(@slid, @slname, @sldate, @prname, @slqty, @slprice, @sltotal)", con.con);
            cm.Parameters.AddWithValue("@slid", SlrNameL.Text);
            cm.Parameters.AddWithValue("@slname", SellerName);
            cm.Parameters.AddWithValue("@sldate", dateOrder.Value);
            cm.Parameters.AddWithValue("@prname", txtPname.Text);
            cm.Parameters.AddWithValue("@slqty", UDQty.Value);
            cm.Parameters.AddWithValue("@slprice", txtPrice.Text);
            cm.Parameters.AddWithValue("@sltotal", txtTotal.Text);
            con.con.Open();
            cm.ExecuteNonQuery();
            con.con.Close();

            MessageBox.Show("Produk sudah berhasil Dipesan");*/

        // Update product quantity

        /*private void LoadSellList()
        {
            // Clear existing rows
            dgvSellList.Rows.Clear();

            try
            {
                con.con.Open();
                cm = new SqlCommand("SELECT * FROM tbSellList WHERE slname = @slname", con.con);
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
                if (con.con.State == ConnectionState.Open)
                {
                    con.con.Close();
                }
            }
        }*/
        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvProduct.Rows[e.RowIndex];

                if (row.Cells[0].Value.ToString() != "")
                {
                    txtPid.Text = dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtPname.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                    //qty = Convert.ToInt16(dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString());
                    ProductId = Int32.Parse(row.Cells[0].Value.ToString());
                    btnInsert.Enabled = true;
                    button1.Enabled = false;
                }
            }
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

        /*private void Slname(string sellerName)
        {
            dgvSellList.Rows.Clear();

            try
            {
                con.con.Open();
                cm = new SqlCommand("SELECT * FROM tbSeller WHERE sname = @sname", con.con);
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

            if (con.con.State == ConnectionState.Open)
            { 
                con.con.Close();
                
            }
        }*/

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvSellList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvSellList.Rows[e.RowIndex];
                if (row.Cells[0].Value.ToString() != "")
                {
                    ProductId = Int32.Parse(row.Cells[0].Value.ToString());
                    txtPname.Text = row.Cells[1].Value.ToString();
                    UDQty.Text = row.Cells[2].Value.ToString();
                   
                    btnInsert.Enabled = true;
                    button1.Enabled = true;
                }
            }
        }

        /*private void PrintOrderSummary()
        {
            printDocument1 = new PrintDocument();
            printDocument1.PrintPage += printDocument1_PrintPage;

            PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }*/

        /*public void SetSellerName(string sellerName)
        {
            this.SellerName = sellerName;
            // You can perform any additional actions related to setting the seller name here
        }*/

        private void btnPrint_Click(object sender, EventArgs e)
        {
            
        }



        /*private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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
        }*/

        private void btnPay_Click(object sender, EventArgs e)
        {
            //List<int> orderIDs = GetOrderIDsForSeller(SellerName);

            // Pass the order IDs to the PaymentSeller form
            PaymentSeller paymentForm = new PaymentSeller(SellerId);
            paymentForm.ShowDialog();
        }

        /*private List<int> GetOrderIDsForSeller(string sellerName)
        {
            List<int> orderIDs = new List<int>();
            try
            {
                con.con.Open();
                cm = new SqlCommand("SELECT slid FROM tbSellList WHERE slname = @SellerName", con.con);
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
                if (con.con.State == ConnectionState.Open)
                {
                    con.con .Close();
                }
            }
            return orderIDs;
        }*/

        private void btnOrder_Click(object sender, EventArgs e)
        {
                try
            {
                string orderID = autoID();

                if (dgvSellList.RowCount > 1)
                {
                    con.cud("INSERT INTO tbPay (payid) VALUES (" + orderID + ")");

                    int maxCount = dgvSellList.RowCount; // Max Iteration in DataGridView
                    int count = 0;

                    foreach (DataGridViewRow row in dgvSellList.Rows)
                    {
                        count += 1;
                        if (count == maxCount) // This Line is Important, because Datagridview also read a last row, which is empty row
                        {
                            break;
                        }

                        con.select("SELECT * FROM tbSellList");
                        // Ini ada Id karena kebetulan di db saya Id nya ngga auto increment, nanti kalo kamu hapus aja pake yang biasa.
                        con.cud("INSERT INTO tbSellList (payid, pid, slqty, slprice) VALUES('" + orderID + "',  '" + row.Cells[0].Value.ToString() + "', '" + row.Cells[2].Value.ToString() + "', '" + row.Cells[3].Value.ToString() + "')");

                        cm = new SqlCommand("UPDATE tbProduct SET pqty = (pqty - @pqty) WHERE pid = @pid", con.con);
                        cm.Parameters.AddWithValue("@pqty", Convert.ToInt32(row.Cells[2].Value)); // Ambil kuantitas dari DataGridView
                        cm.Parameters.AddWithValue("@pid", Convert.ToInt32(row.Cells[0].Value)); // Ambil ID produk dari DataGridView
                        con.con.Open();
                        cm.ExecuteNonQuery();
                        con.con.Close();
                    }

                    MessageBox.Show("Insert Order Successfully");
                }
                else
                {
                    MessageBox.Show("Harap Isi Order List");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                RefreshAll();
                LoadProduct();
            }
        }
        private void dgOrderList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvSellList.SelectedRows.Count > 0)
            {
                // Ambil baris yang dipilih
                DataGridViewRow row = dgvSellList.SelectedRows[0];

                // Ambil indeks baris yang dipilih untuk penggunaan selanjutnya
                rowIndexDeleted = row.Index;

                // Kurangi totalSum dengan harga total dari baris yang akan dihapus
                totalSum -= int.Parse(row.Cells[6].Value.ToString());

                // Hapus baris dari DataGridView    
                dgvSellList.Rows.RemoveAt(rowIndexDeleted);

                // Refresh label
                RefreshLabel();
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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


