using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null) 
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void customer4_Click(object sender, EventArgs e)
        {
           openChildForm(new UserForm());
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void customer1_Click(object sender, EventArgs e)
        {
            openChildForm(new ProductForm());
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            openChildForm(new SellerForm());
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            openChildForm(new CategoryForm()); 
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            openChildForm(new OrderForm());
        }

        private void MenuProduct_Click(object sender, EventArgs e)
        {
            openChildForm(new ProductForm());
        }

        private void MenuSeller_Click(object sender, EventArgs e)
        {
            openChildForm(new SellerForm());
        }

        private void MenuCategory_Click(object sender, EventArgs e)
        {
            openChildForm(new CategoryForm());
        }

        private void MenuAdmin_Click(object sender, EventArgs e)
        {
            openChildForm(new UserForm());
        }

        private void MenuOrders_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reportt report = new Reportt();
            report.ShowDialog();
        }
    }
}
