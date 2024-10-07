using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI_Kasir_Toko
{
    internal class GlobalVariable
    {
        public static string fullName { get; set; }
        public static bool role { get; set; }

        public static DBContextDataContext Db = new DBContextDataContext();
        public static Form1 getStarted;
        public static LoginForm loginForm;
        public static SupplierForm supplierForm;
        public static RegisterForm registerForm;
        public static AdminDashboardForm dashboardAdmin;
        public static KasirDashboardForm dashboardKasir;
        public static FormBarang formBarang;
        public static FormPetugas formPetugas;
        public static FormRiwayat formRiwayat;
        public static FormTransaksi formTransaksi;
        public static StockBarangForm stockBarang;
    }
}
