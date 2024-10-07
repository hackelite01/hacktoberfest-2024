using System;
using System.Linq;
using System.Windows.Forms;

namespace SI_Kasir_Toko
{
    using static GlobalVariable;
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            var id = Db.Employees.FirstOrDefault(e => e.Role);
            //role = id.ID;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            registerForm.Show();
            this.Hide();
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            registerForm.Show();
            this.Hide();
        }

        private void checkBoxPw_CheckedChanged(object sender, EventArgs e)
        {
            fieldPassword.PasswordChar = checkBoxPw.Checked ? '\0' : '*';
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            getStarted.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var usernameNow = fieldUsername.Text;
            var passwordNow = fieldPassword.Text;
            var usernameLogin = Db.Employees.FirstOrDefault(i => i.Username == usernameNow && i.Password == passwordNow);
            if (isInputValid())
            {
                if (usernameLogin != null)
                {
                    fullName = usernameLogin.Username.ToString();
                    if (usernameLogin.Role == false)
                    {
                        dashboardAdmin.Show();
                        this.Hide();
                        MessageBox.Show($"Login Berhasil, selamat datang Admin {usernameNow}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        role = usernameLogin.Role;
                    }
                    else
                    {
                        dashboardKasir.Show();
                        this.Hide();
                        MessageBox.Show($"Login Berhasil, selamat datang Kasir {usernameNow}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        role = usernameLogin.Role;
                    }
                }
                else
                {
                    MessageBox.Show("Data Admin Maupun Kasir tidak ditemukan, Silakan Coba lagi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private bool isInputValid()
        {
            if (string.IsNullOrEmpty(fieldUsername.Text))
            {
                MessageBox.Show("Username tidak boleh kosong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }  
            if (string.IsNullOrEmpty(fieldPassword.Text))
            {
                MessageBox.Show("Password tidak boleh kosong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }
    }
}