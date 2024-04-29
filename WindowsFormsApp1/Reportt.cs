using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Reportt : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rasya\Documents\Works\Coding\SOP_fIX\dbMS1.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public Reportt()
        {
            InitializeComponent();
            dtPick1.Format = DateTimePickerFormat.Custom;
            dtPick1.CustomFormat = "MMMM";
            dtPick1.ShowUpDown = true;

            dtPick2.Format = DateTimePickerFormat.Custom;
            dtPick2.CustomFormat = "MMMM";
            dtPick2.ShowUpDown = true;
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            if (int.Parse(dtPick1.Value.ToString("MM")) <= int.Parse(dtPick2.Value.ToString("MM")))
            {
                LoadDGV();
            }
            else
            {
                MessageBox.Show("Dari Bulan tidak boleh lebih dari Tujuan Bulan");
            }
        }

        private void LoadDGV()
        {
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            dgvMonth.Refresh();
            dgvMonth.DataSource = dt;

            // Adjusted SQL command
            cm = new SqlCommand("SELECT MONTH(sl.sldate) AS Bulan, SUM(sl.sltotal) AS Total " +
                    "FROM tbSellList sl " +
                    "JOIN tbPay p ON sl.slid = p.slid " +
                    "WHERE MONTH(sl.sldate) BETWEEN @StartDate AND @EndDate " +
                    "GROUP BY MONTH(sl.sldate)", con);
            cm.Parameters.AddWithValue("@StartDate", dtPick1.Value.Month);
            cm.Parameters.AddWithValue("@EndDate", dtPick2.Value.Month);

            con.Open();

            // Execute command and fill DataTable
            SqlDataAdapter da = new SqlDataAdapter(cm);
            da.Fill(dt);

            // Close connection
            con.Close();

            dgvMonth.DataSource = dt;

            int i = 0;
            List<string> month = new List<string>();
            List<int> price = new List<int>();

            foreach (DataRow dtr in dt.Rows)
            {
                month.Add(dtr[0].ToString());
                price.Add(int.Parse(dtr[1].ToString()));
                i += 1;
            }

            var resMonth = month.ToArray();
            var resPrice = price.ToArray();

            chart1.Series.Clear();
            chart1.Titles.Clear();

            chart1.Series.Add("Income");
            chart1.Titles.Add("Income in Million");

            for (int j = 0; j < resMonth.Length; j++)
            {
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(resMonth[j]));
                chart1.Series["Income"].Points.AddXY(monthName, (float)resPrice[j] / 1000000); // dibagi 1 juta, karena ditampilinnya dalam satuan juta kalo disoal
            }
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btngenerate_Click(object sender, EventArgs e)
        {

        }

        private void btngenerate_Click_1(object sender, EventArgs e)
        {
            if (int.Parse(dtPick1.Value.ToString("MM")) <= int.Parse(dtPick2.Value.ToString("MM")))
            {
                LoadDGV();
            }
            else
            {
                MessageBox.Show("Dari Bulan tidak boleh lebih dari Tujuan Bulan");
            }
        }
    }
}

