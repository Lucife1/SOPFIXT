using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class OrdersDetails : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rasya\Documents\Works\Coding\SOP_fIX\dbMS1.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public OrdersDetails()
        {
            InitializeComponent();
        }



        public void LoadOrder()
        {
            double total = 0;
            int i = 0;
            dgvOrder.Rows.Clear();
            cm = new SqlCommand("SELECT orderid, odate, O.pid, P.pname, O.cid, C.cname, qty, price, total FROM tbOrder  AS O JOIN tbCustomer AS C ON O.cid=C.cid JOIN tbProduct AS P ON O.pid=P.pid WHERE CONCAT (orderid, odate, O.pid, P.pname, O.cid, C.cname, qty, price) LIKE '%" + txtSearch.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvOrder.Rows.Add(i, dr[0].ToString(), Convert.ToDateTime(dr[1].ToString()).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
                total += Convert.ToInt32(dr[8].ToString());
            }
            dr.Close();
            con.Close();

            lblQty.Text = i.ToString();
            lblTotal.Text = total.ToString();
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


            e.Graphics.DrawString("Order Id: " + dgvOrder.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
            y += cellHeight;
            e.Graphics.DrawString("Order Date: " + dgvOrder.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
            y += cellHeight;
            e.Graphics.DrawString("Product ID: " + dgvOrder.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
            y += cellHeight;
            e.Graphics.DrawString("Product Name : " + dgvOrder.SelectedRows[0].Cells[4].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
            y += cellHeight;
            e.Graphics.DrawString("Customer ID : " + dgvOrder.SelectedRows[0].Cells[5].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
            y += cellHeight;
            e.Graphics.DrawString("Customer Name : " + dgvOrder.SelectedRows[0].Cells[6].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
            y += cellHeight;
            e.Graphics.DrawString("QTY : " + dgvOrder.SelectedRows[0].Cells[7].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
            y += cellHeight;
            e.Graphics.DrawString("Price " + dgvOrder.SelectedRows[0].Cells[8].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));
            y += cellHeight;
            e.Graphics.DrawString("Total Amount : " + dgvOrder.SelectedRows[0].Cells[9].Value.ToString(), new Font("Century", 18, FontStyle.Regular), Brushes.Black, new Point(x, y));

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

        private void btnPrint1_Click(object sender, EventArgs e)
        {
            PrintOrderSummary();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {            
               LoadOrder();    
        }
    }
    
}
