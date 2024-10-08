using System;
using System.Linq;
using System.Windows.Forms;

namespace SI_Kasir_Toko
{
    using static GlobalVariable;
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }


        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loginForm.Show();
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            getStarted.Show();
            this.Hide();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (isInputValid())
            {
                Employee newEmployee = new Employee
                {
                    Username = fieldUsername.Text,
                    Fullname = fieldFullname.Text,
                    Password = fieldPassword.Text,
                    Email = fieldEmail.Text,
                    Alamat = fieldAlamat.Text,
                    MasukAt = DateTime.Now,
                    Role = true
                };

                Db.Employees.InsertOnSubmit(newEmployee);
                Db.SubmitChanges();

                MessageBox.Show("Berhasil menambahkan data, silakan login untuk menikmati fitur kami");
                loginForm.Show();
                this.Hide();
            }
        }
        private bool isInputValid()
        {
            var usernameExist = Db.Employees.FirstOrDefault(i => i.Username == fieldUsername.Text);
            if (usernameExist != null)
            {
                MessageBox.Show("Username Sudah digunakan, Coba yang lain", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrWhiteSpace(fieldUsername.Text))
            {
                MessageBox.Show("Username tidak boleh kosong dan hanya satu kata", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(fieldFullname.Text))
            {
                MessageBox.Show("Fullname tidak boleh kosong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(fieldPassword.Text))
            {
                MessageBox.Show("Password tidak boleh kosong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(fieldEmail.Text))
            {
                MessageBox.Show("Email tidak boleh kosong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(fieldAlamat.Text))
            {
                MessageBox.Show("Alamat tidak boleh kosong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }
    }
}
