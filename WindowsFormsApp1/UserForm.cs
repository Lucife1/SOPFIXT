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
    public partial class UserForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rasya\Documents\Works\Coding\SOP_fIX\dbMS1.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public UserForm()
        {
            InitializeComponent();
            LoadUser();
        }

        public void LoadUser()
        {
            int i = 0;
            dgvUser.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbUser", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read()) 
            {
                i++;
                dgvUser.Rows.Add(i,dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());            
            }
            dr.Close();
            con.Close();
        }

        private void customer1_Click(object sender, EventArgs e)
        {
          UserModuleForm userModule = new UserModuleForm ();
            userModule.btnSave.Enabled = true;
            userModule.btnUpdate.Enabled = false;
            userModule.ShowDialog(); 
            LoadUser();
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvUser.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                UserModuleForm userModule = new UserModuleForm();
                userModule.txtUserName.Text = dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString();
                userModule.txtFullName.Text = dgvUser.Rows[e.RowIndex].Cells[2].Value.ToString();
                userModule.txtPass.Text = dgvUser.Rows[e.RowIndex].Cells[3].Value.ToString();
                userModule.txtPhone.Text = dgvUser.Rows[e.RowIndex].Cells[4].Value.ToString();

                userModule.btnSave.Enabled =false;
                userModule.btnUpdate.Enabled =true;
                userModule.txtUserName.Enabled =false;
                userModule.ShowDialog();
            }
            else if (colName =="Delete")
            {
                if (MessageBox.Show("Apakah kamu yakin ingin menghapus user tersebut?","Delete User",MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbUser WHERE Username LIKE '" + dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString() +"'", con );
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User berhasil dihapus");
                }
            }
            LoadUser();
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

            e.Graphics.DrawString("SELLER SUMMARY", new Font("Century", 25, FontStyle.Bold), Brushes.Red, new Point(marginLeft, OrderSummary + marginTop));

            int cellHeight = 40;
            int cellWidth = 300;
            int x = marginLeft;
            int y = PosRemainingContent + marginTop;

            while (rowIndex < dgvUser.Rows.Count)
            {
                DataGridViewRow row = dgvUser.Rows[rowIndex];

                // Print data for each row
                e.Graphics.DrawString("Seller Id: " + row.Cells[1].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
                y += cellHeight;
                e.Graphics.DrawString("Seller Name: " + row.Cells[2].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
                y += cellHeight;
                e.Graphics.DrawString("Seller Age : " + row.Cells[3].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
                y += cellHeight;
                e.Graphics.DrawString("Phone : " + row.Cells[4].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
                y += cellHeight;

                // Masking the password
                string password = row.Cells[5].Value.ToString();
                string maskedPassword = MaskPassword(password); // Call the function to mask the password
                e.Graphics.DrawString("Password : " + maskedPassword, new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
                y += cellHeight;

                // Add some spacing between rows
                y += cellHeight;

                rowIndex++;

                // Check if there are more rows to print or if the page boundary is reached
                if (rowIndex < dgvUser.Rows.Count && y + cellHeight >= e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true; // Set to true to indicate there are more pages
                    return; // Exit the method to print the current page and continue on the next page
                }
            }

            // If we reach here, it means all rows have been printed
            rowIndex = 0; // Reset the row index for the next printing
            e.HasMorePages = false; // No more pages to print
        }

        // Function to mask the password
        private string MaskPassword(string password)
        {
            const int charactersToShow = 3; // Number of characters to show before masking
            const char maskCharacter = '*'; // Placeholder character for masking

            if (password.Length <= charactersToShow)
            {
                return password; // If password length is less than or equal to the specified characters to show, return the actual password
            }
            else
            {
                // Create a masked string with the placeholder character for the remaining characters
                string maskedString = new string(maskCharacter, password.Length - charactersToShow);

                // Concatenate the first few characters of the actual password with the masked string
                return password.Substring(0, charactersToShow) + maskedString;
            }
        }

        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            PrintOrderSummary();
        }

    }
}
