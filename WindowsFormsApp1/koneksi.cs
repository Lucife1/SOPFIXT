using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal class koneksi
    {
        public static string database = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rasya\Documents\Works\Coding\SOP_fIX\dbMS1.mdf;Integrated Security=True;Connect Timeout=30");
        public SqlConnection con = new SqlConnection(database);
        public SqlCommand cmd;
        public SqlDataAdapter adp = new SqlDataAdapter();
        public SqlDataReader dr;
        public DataTable dt = new DataTable();
        public DataSet ds;

        public void select(string query)
        {
            try
            {
                dt.Clear();
                con.Open();
                cmd = new SqlCommand(query, con);
                adp.SelectCommand = cmd;
                adp.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void cud(string query)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void chart(string query)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
        }
    }
}
