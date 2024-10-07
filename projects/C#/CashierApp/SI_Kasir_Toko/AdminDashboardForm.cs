using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SI_Kasir_Toko
{
    using static GlobalVariable;
    public partial class AdminDashboardForm : Form
    {
        public AdminDashboardForm()
        {
            InitializeComponent();
        }

        private void btnPetugas_Click(object sender, EventArgs e)
        {
            formPetugas.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            loginForm.Show();
            this.Hide();
        }

        private void btnRiwayat_Click(object sender, EventArgs e)
        {
            formRiwayat.Show();
            this.Hide();
        }

        private void btnBarang_Click(object sender, EventArgs e)
        {
            formBarang.Show();
            this.Hide();
        }

        private void AdminDashboardForm_Load(object sender, EventArgs e)
        {
            txtFullname.Text = fullName;
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            supplierForm.Show();
        }
    }
}
