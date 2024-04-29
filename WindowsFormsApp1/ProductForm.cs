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
    public partial class ProductForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rasya\Documents\Works\Coding\SOP_fIX\dbMS1.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public ProductForm()
        {
            InitializeComponent();
            LoadProduct();
        }

        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pid, pname, pprice, pdesc, pcategory) LIKE '%"+txtSearch.Text+"%'" , con);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductModuleForm formModule = new ProductModuleForm();
            formModule.btnSave.Enabled = true;
            formModule.btnUpdate.Enabled = false;
            formModule.ShowDialog();
            LoadProduct();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                ProductModuleForm productModule = new ProductModuleForm();
                productModule.lblPid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                productModule.txtProductName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                productModule.txtQty.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                productModule.txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                productModule.txtDesc.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                productModule.comboCat.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();

                productModule.btnSave.Enabled = false;
                productModule.btnUpdate.Enabled = true;
                productModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Apakah kamu yakin ingin menghapus Produk tersebut?", "Delete Produk", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbProduct WHERE pid LIKE '" + dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Produk berhasil dihapus");
                }
            }
            LoadProduct();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void PrintOrderSummary()
        {
            printDocument1 = new PrintDocument();
            printDocument1.PrintPage += printDocument1_PrintPage;

            PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintOrderSummary();
        }

        private int rowIndex = 0; // Track the current row index for printing

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int OrderSummary = 50;
            int PosRemainingContent = OrderSummary + 100;

            int marginLeft = 50;
            int marginTop = 50;

            e.Graphics.DrawString("PRODUCT SUMMARY", new Font("Century", 25, FontStyle.Bold), Brushes.Red, new Point(marginLeft, OrderSummary + marginTop));

            int cellHeight = 40;
            int cellWidth = 300;
            int x = marginLeft;
            int y = PosRemainingContent + marginTop;

            while (rowIndex < dgvProduct.Rows.Count)
            {
                DataGridViewRow row = dgvProduct.Rows[rowIndex];

                // Print data for each row
                e.Graphics.DrawString("Product Id: " + row.Cells[1].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
                y += cellHeight;
                e.Graphics.DrawString("Product Name: " + row.Cells[2].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
                y += cellHeight;
                e.Graphics.DrawString("QTY : " + row.Cells[3].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
                y += cellHeight;
                e.Graphics.DrawString("Price : " + row.Cells[4].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
                y += cellHeight;
                e.Graphics.DrawString("Desc : " + row.Cells[5].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
                y += cellHeight;
                e.Graphics.DrawString("Category : " + row.Cells[6].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
                y += cellHeight;

                // Add some spacing between rows
                y += cellHeight;

                rowIndex++;

                // Check if there are more rows to print or if the page boundary is reached
                if (rowIndex < dgvProduct.Rows.Count && y + cellHeight >= e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true; // Set to true to indicate there are more pages
                    return; // Exit the method to print the current page and continue on the next page
                }
            }

            // If we reach here, it means all rows have been printed
            rowIndex = 0; // Reset the row index for the next printing
            e.HasMorePages = false; // No more pages to print
        }

    }
}
