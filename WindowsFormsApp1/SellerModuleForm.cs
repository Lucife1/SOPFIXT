using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class SellerModuleForm : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rasya\Documents\Works\Coding\SOP_fIX\dbMS1.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        public SellerModuleForm()
        {
            InitializeComponent();
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtRePass_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSPass.Text != txtSRePass.Text)
                {
                    MessageBox.Show("Password tidak cocok", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Apakah ingin Memperbarui Seller ini?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbSeller SET sname=@sname, sage=@sage, sphone=@sphone, spassword=@spassword WHERE sid LIKE '" + lblCid.Text + "' ", con);
                    cm.Parameters.AddWithValue("@sname", txtSname.Text);
                    cm.Parameters.AddWithValue("@sage", txtSage.Text);
                    cm.Parameters.AddWithValue("@sphone", txtSPhone.Text);
                    cm.Parameters.AddWithValue("@spassword", txtSPass.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Seller sudah berhasil diperbarui");
                    this.Dispose();
                }
            }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtSPass.Text != txtSRePass.Text)
            {
                MessageBox.Show("Password tidak cocok", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                try
                {
                    if (MessageBox.Show("Apakah ingin Menyimpan Seller ini?", "Saving", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO tbSeller(sname, sage, sphone, spassword)VALUES(@sname,@sage,@sphone,@spassword)", con);
                        cm.Parameters.AddWithValue("@sname", txtSname.Text);
                        cm.Parameters.AddWithValue("@sage", txtSage.Text);
                        cm.Parameters.AddWithValue("@sphone", txtSPhone.Text);
                        cm.Parameters.AddWithValue("@spassword", txtSPass.Text);
                        con.Open();
                        cm.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Seller sudah berhasil disimpan");
                        Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        public void Clear()
        {
            txtSname.Clear();
            txtSPhone.Clear();
            txtSPass.Clear();
            txtSage.Clear();
            txtSRePass.Clear();

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }

    }

