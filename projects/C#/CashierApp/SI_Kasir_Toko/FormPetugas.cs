using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SI_Kasir_Toko
{
    using static GlobalVariable;
    using static iTextSharp.text.pdf.security.SignaturePermissions;

    public partial class FormPetugas : Form
    {
        public FormPetugas()
        {
            InitializeComponent();
        }

        private void FormPetugas_Load(object sender, EventArgs e)
        {
            loadDataPetugas();
        }

        private void loadDataPetugas()
        {
            var dataPetugas = from petugas in Db.Employees
                              select new
                              {
                                  ID = petugas.ID,
                                  Nama = petugas.Fullname,
                                  Username = petugas.Username,
                                  Email = petugas.Email,
                                  Alamat = petugas.Alamat,
                                  Password = petugas.Password,
                                  Daftar = petugas.MasukAt
                              };

            dgvPetugas.DataSource = dataPetugas.ToList();
        }

        private bool isInputValid()
        {
            if (string.IsNullOrEmpty(fieldPetugas.Text))
            {
                MessageBox.Show("Pastikan Nama Petugas Sudah Terisi!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(fieldUsername.Text))
            {
                MessageBox.Show("Pastikan Username Sudah Terisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(fieldEmail.Text))
            {
                MessageBox.Show("Pastikan Email Sudah Terisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(fieldPassword.Text))
            {
                MessageBox.Show("Pastikan Password Masuk Sudah Terisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(fieldAlamat.Text))
            {
                MessageBox.Show("Pastikan Alamat Sudah Terisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvPetugas.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Output.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(dgvPetugas.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dgvPetugas.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dgvPetugas.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(cell.Value.ToString());
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Data Success di Print", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Info");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadDataPetugas();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (role == true)
            {
                dashboardKasir.Show();
                this.Hide();
            }
            else
            {
                dashboardAdmin.Show();
                this.Hide();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            fieldUsername.Text = "";
            fieldPetugas.Text = "";
            fieldTanggal.Value = DateTime.Now;
            fieldEmail.Text = "";
            fieldPassword.Text = "";
            fieldAlamat.Text = "";
        }

        private void dgvPetugas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvPetugas.Rows[e.RowIndex];
                string namaPetugas = selectedRow.Cells[1].Value != null ? selectedRow.Cells[1].Value.ToString() : "";
                string usernamePetugas = selectedRow.Cells[2].Value.ToString();
                string emailPetugas = selectedRow.Cells[3].Value.ToString();
                string alamatPetugas = selectedRow.Cells[4].Value.ToString();
                string passwordPetugas = selectedRow.Cells[5].Value.ToString();
                DateTime daftarMasuk = (DateTime)selectedRow.Cells[6].Value;

                fieldPetugas.Text = namaPetugas;
                fieldUsername.Text = usernamePetugas;
                fieldEmail.Text = emailPetugas;
                fieldAlamat.Text = alamatPetugas;
                fieldPassword.Text = passwordPetugas;
                fieldTanggal.Value = daftarMasuk;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var latestId = Db.Employees.OrderByDescending(i => i.ID).FirstOrDefault();
            var IdNow = latestId.ID;
            var newId = IdNow++;

            if (isInputValid())
            {
                Employee newEmployee = new Employee
                {
                    ID = newId,
                    Username = fieldUsername.Text,
                    Fullname = fieldPetugas.Text,
                    Email = fieldEmail.Text,
                    Password = fieldPassword.Text,
                    Alamat = fieldAlamat.Text,
                    MasukAt = fieldTanggal.Value
                };
                Db.Employees.InsertOnSubmit(newEmployee);
                Db.SubmitChanges();
                MessageBox.Show("Data Petugas Berhasil Ditambahkan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Pastikan Semua Data Sudah Terisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (fieldUsername.Text != "")
            {
                var editListed = Db.Employees.FirstOrDefault(i => i.Fullname == fieldPetugas.Text);
                if (editListed != null)
                {
                    editListed.ID = editListed.ID;
                    editListed.Fullname = fieldPetugas.Text;
                    editListed.Username = fieldUsername.Text;
                    editListed.Email = fieldEmail.Text;
                    editListed.Password = fieldPassword.Text;
                    editListed.Alamat = fieldAlamat.Text;
                    editListed.MasukAt = fieldTanggal.Value;
                    
                    Db.SubmitChanges();
                    MessageBox.Show("Data Berhasil Dirubah", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Silakan pilih data yang ingin anda rubah lebih dahulu", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvPetugas.SelectedRows.Count > 0 || fieldPetugas.Text != null)
            {
                var result = MessageBox.Show("Yakin ingin menghapus data ini?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var deleteListed = Db.Employees.FirstOrDefault(i => i.Fullname == fieldPetugas.Text);
                    Db.Employees.DeleteOnSubmit(deleteListed);
                    Db.SubmitChanges();
                    MessageBox.Show("Data Berhasil Dihapus", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Silakan pilih data yang ingin anda hapus lebih dahulu", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}
