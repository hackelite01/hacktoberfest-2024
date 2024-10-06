using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SI_Kasir_Toko
{
    using static GlobalVariable;
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            getStarted = new Form1();
            loginForm = new LoginForm();
            registerForm = new RegisterForm();
            dashboardAdmin = new AdminDashboardForm();
            dashboardKasir = new KasirDashboardForm();
            formBarang = new FormBarang();
            formPetugas = new FormPetugas();
            supplierForm = new SupplierForm();
            formRiwayat = new FormRiwayat();
            formTransaksi = new FormTransaksi();
            stockBarang = new StockBarangForm();
            Application.Run(new Form1());
        }
    }
}
