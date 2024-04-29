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
    public partial class Login : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rasya\Documents\Works\Coding\SOP_fIX\dbMS1.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        string SellerId, SellerName;
        DataTable dt = new DataTable();
        koneksi koneksi = new koneksi();

        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPass.Checked == false)
                txtPass.UseSystemPasswordChar = true;
            else
                txtPass.UseSystemPasswordChar = false;
        }

        private void lblClear_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtPass.Clear();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Exit","Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                    if(comboBox_role.SelectedIndex < 0)
                       {
                            MessageBox.Show("Please select a role", "Role not selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                       }
                
                    if (txtName.Text == String.Empty)
                    {
                        MessageBox.Show("masukan username yang valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtName.Focus();
                        return;
                    }

                    if (txtPass.Text == String.Empty)
                    {
                        MessageBox.Show("masukan password yang valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPass.Focus();
                        return;
                    }

                else if(comboBox_role.Text == "ADMIN")
                {
                    cm = new SqlCommand("SELECT * FROM tbUser WHERE Username=@Username AND password=@password", con);
                    cm.Parameters.AddWithValue("@Username", txtName.Text);
                    cm.Parameters.AddWithValue("@password", txtPass.Text);
                    con.Open();
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Welcome " + dr["fullname"].ToString() + " | ", "Akses diterima", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MainForm main = new MainForm();
                        this.Hide();
                        main.ShowDialog();

                    }
                    else
                    {
                        MessageBox.Show("Invalid Usern  ame or Password", "Access Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if(comboBox_role.Text == "SELLER")
                {
                    cm= new SqlCommand("SELECT * FROM tbSeller WHERE sname=@sname AND spassword=@spassword", con);
                    cm.Parameters.AddWithValue("@sname", txtName.Text);
                    cm.Parameters.AddWithValue("@spassword", txtPass.Text);
                    con.Open();
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        SellerId = dr["sid"].ToString();
                        SellerName = dr["sname"].ToString();

                        MessageBox.Show("Welcome " + dr["sname"].ToString() + " | ", "Akses diterima", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SellerForm1 main = new SellerForm1(SellerId);
                        this.Hide();
                        main.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Username or Password", "Access Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                
                con.Close();
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

        private void comboBox_role_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
