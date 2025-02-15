﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    public partial class ProductModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rasya\Documents\Works\Coding\SOP_fIX\dbMS1.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public ProductModuleForm()
        {
            InitializeComponent();
            LoadCategory();
        }

        public void LoadCategory()
        {
            comboCat.Items.Clear();
            cm = new SqlCommand("SELECT catname FROM tbCategory", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read()) 
                {
                comboCat.Items.Add(dr[0 ].ToString());
                }
                dr.Close();
                con.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Apakah ingin Menyimpan Produk ini?", "Saving", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbProduct(pname, pqty, pprice, pdesc, pcategory)VALUES(@pname, @pqty, @pprice, @pdesc, @pcategory)", con);
                    cm.Parameters.AddWithValue("@pname", txtProductName.Text);
                    cm.Parameters.AddWithValue("@pqty",Convert.ToInt16(txtQty.Text));
                    cm.Parameters.AddWithValue("@pprice",Convert.ToInt16(txtPrice.Text));
                    cm.Parameters.AddWithValue("@pdesc", txtDesc.Text);
                    cm.Parameters.AddWithValue("@pcategory", comboCat.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Produk sudah berhasil disimpan");
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
            txtProductName.Clear();
            txtQty.Clear();
            txtPrice.Clear();
            txtDesc.Clear();
            comboCat.Text = "";
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
                if (MessageBox.Show("Apakah ingin Memperbarui Produk ini?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbProduct SET pname=@pname, pqty=@pqty, pprice=@pprice, pdesc=@pdesc, pcategory=@pcategory WHERE pid LIKE '" + lblPid.Text + "' ", con);
                    cm.Parameters.AddWithValue("@pname", txtProductName.Text);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(txtQty.Text));
                    cm.Parameters.AddWithValue("@pprice", Convert.ToInt16(txtPrice.Text));
                    cm.Parameters.AddWithValue("@pdesc", txtDesc.Text);
                    cm.Parameters.AddWithValue("@pcategory", comboCat.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Produk sudah berhasil diperbarui");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
