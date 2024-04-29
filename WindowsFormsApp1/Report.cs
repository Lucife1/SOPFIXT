using Microsoft.Reporting.WinForms;
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
    public partial class Report : Form
    {
       
        
        
        public Report()
        {
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.tbOrder' table. You can move, or remove it, as needed.
             //this.tbOrderTableAdapter.Fill(this.dataSet1.tbOrder);
            this.reportViewer1.RefreshReport();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rasya\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");   
        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void loadprint_Click(object sender, EventArgs e)
        {
            SqlCommand cm = new SqlCommand("SELECT * FROM tbOrder", con);
            SqlDataAdapter d = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            d.Fill(dt);

            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource source = new ReportDataSource("DataSet1", dt);
            reportViewer1.LocalReport.ReportPath = "Report.rdlc";
            reportViewer1.LocalReport.DataSources.Add(source);
            reportViewer1.RefreshReport();
        }
    }
}
