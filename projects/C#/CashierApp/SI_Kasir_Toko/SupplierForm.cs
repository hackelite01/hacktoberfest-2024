using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SI_Kasir_Toko
{
    using static GlobalVariable;
    public partial class SupplierForm : Form
    {
        public SupplierForm()
        {
            InitializeComponent();
            LoadDataSupplier();
        }

        private void LoadDataSupplier()
        {
            var data = Db.Suppliers.Select(i => i);
            dgvSupplier.DataSource = data;
        }

        private void SupplierForm_Load(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDataSupplier();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var getLastId = Db.Suppliers.OrderByDescending(i => i.CodeSupplier).FirstOrDefault();
            var newId = getLastId.CodeSupplier;
            var insertId = newId++;

            if (isInputValid())
            {
                Supplier newSupplier = new Supplier
                {
                    CodeSupplier = insertId,
                    NamaSupplier = fieldUsernameSupplier.Text,
                    Telephone = Convert.ToInt32(fieldNoHp.Text),
                    Alamat = fieldAlamat.Text,
                    NoRekening = Convert.ToInt32(fieldNoRekeningSupplier.Text),
                    NPWP = fieldNPWP.Text,
                    Kota = fieldKota.Text,
                    Negara = fieldNegara.Text,
                    Provinsi = fieldProvinsi.Text,
                    KodePos = Convert.ToInt32(fieldKodePos.Text),
                    Fax = Convert.ToInt32(fieldFAX.Text)
                };

                Db.Suppliers.InsertOnSubmit(newSupplier);
                Db.SubmitChanges();
                MessageBox.Show("Your data succesfully added", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lengkapi data diatas terlebih dahulu", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool isInputValid()
        {
            if (string.IsNullOrEmpty(fieldCodeSupplier.Text))
            {
                MessageBox.Show("Pastikan Code Sudah Terisi!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(fieldUsernameSupplier.Text))
            {
                MessageBox.Show("Pastikan Username Sudah Terisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(fieldEmailSupplier.Text))
            {
                MessageBox.Show("Pastikan Email Sudah Terisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(fieldNoRekeningSupplier.Text))
            {
                MessageBox.Show("Pastikan No Rekening Sudah Terisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(fieldNoHp.Text))
            {
                MessageBox.Show("Pastikan No HP Sudah Terisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(fieldAlamat.Text))
            {
                MessageBox.Show("Pastikan Alamat Sudah Terisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(fieldKota.Text))
            {
                MessageBox.Show("Pastikan Kota Sudah Terisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (fieldUsernameSupplier.Text != "")
            {
                var editListed = Db.Suppliers.FirstOrDefault(i => i.NamaSupplier == fieldUsernameSupplier.Text);
                if (editListed != null)
                {
                    editListed.CodeSupplier = editListed.CodeSupplier;
                    //editListed.Fullname = fieldUsernameSupplier.Text;
                    editListed.NamaSupplier = fieldUsernameSupplier.Text;
                    editListed.Telephone = Convert.ToInt32(fieldNoRekeningSupplier.Text);
                    //editListed.Email = fieldEmail.Text;
                    editListed.NoRekening = Convert.ToInt32(fieldNoRekeningSupplier.Text);
                    editListed.Alamat = fieldAlamat.Text;
                    editListed.Kota = fieldKota.Text;
                    editListed.NPWP = fieldNPWP.Text;
                    editListed.Negara = fieldNegara.Text;
                    editListed.Provinsi = fieldProvinsi.Text;
                    editListed.KodePos = Convert.ToInt32(fieldNoRekeningSupplier.Text);
                    editListed.Fax = Convert.ToInt32(fieldNoRekeningSupplier.Text);

                    Db.SubmitChanges();
                    MessageBox.Show("Data Berhasil Dirubah", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Silakan pilih data yang ingin anda rubah lebih dahulu", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvSupplier.Rows[e.RowIndex];
                string code = selectedRow.Cells[0].Value.ToString();
                string namaPetugas = selectedRow.Cells[1].Value != null ? selectedRow.Cells[1].Value.ToString() : "";
                string usernamePetugas = selectedRow.Cells[2].Value.ToString();
                string emailPetugas = selectedRow.Cells[3].Value.ToString();
                string alamatPetugas = selectedRow.Cells[4].Value.ToString();
                string passwordPetugas = selectedRow.Cells[5].Value.ToString();
                string daftarMasuk = selectedRow.Cells[6].Value.ToString();
                string negara = selectedRow.Cells[7].Value.ToString();
                string provinsi = selectedRow.Cells[8].Value.ToString();
                string kodePos = selectedRow.Cells[9].Value.ToString();
                string fax = selectedRow.Cells[10].Value.ToString();

                fieldCodeSupplier.Text = code;
                fieldEmailSupplier.Text = namaPetugas + "@gmail.com";
                fieldUsernameSupplier.Text = namaPetugas;
                fieldNoHp.Text = usernamePetugas;
                fieldAlamat.Text = emailPetugas;
                fieldNoRekeningSupplier.Text = alamatPetugas;
                fieldNPWP.Text = passwordPetugas;
                fieldKota.Text = daftarMasuk;
                fieldNegara.Text = negara;
                fieldProvinsi.Text = provinsi;
                fieldKodePos.Text = kodePos;
                fieldFAX.Text = fax;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            fieldUsernameSupplier.Text = "";
            fieldCodeSupplier.Text = "";
            //fieldTanggal.Value = DateTime.Now;
            fieldEmailSupplier.Text = "";
            fieldNoRekeningSupplier.Text = "";
            fieldAlamat.Text = "";
            fieldNoHp.Text = "";
            fieldKota.Text = "";
            fieldNPWP.Text = "";
            fieldNegara.Text = "";
            fieldProvinsi.Text = "";
            fieldKodePos.Text = "";
            fieldFAX.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvSupplier.SelectedRows.Count > 0 || fieldUsernameSupplier.Text != null)
            {
                var result = MessageBox.Show("Yakin ingin menghapus data ini?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var deleteListed = Db.Suppliers.FirstOrDefault(i => i.NamaSupplier == fieldUsernameSupplier.Text);
                    Db.Suppliers.DeleteOnSubmit(deleteListed);
                    Db.SubmitChanges();
                    MessageBox.Show("Data Berhasil Dihapus", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Silakan pilih data yang ingin anda hapus lebih dahulu", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvSupplier != null && dgvSupplier.Rows.Count > 0)
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
                            PdfPTable pdfTable = new PdfPTable(dgvSupplier.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            // Tambahkan header tabel
                            foreach (DataGridViewColumn column in dgvSupplier.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            // Tambahkan data dari DataGridView
                            decimal totalBelanjaan = 0; // Inisialisasi variabel untuk total belanjaan
                            foreach (DataGridViewRow row in dgvSupplier.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    // Periksa apakah cell.Value adalah null
                                    string cellValue = cell.Value?.ToString() ?? string.Empty;
                                    pdfTable.AddCell(cellValue);

                                    // Jika kolom ini mewakili belanjaan, tambahkan nilainya ke total
                                    if (dgvSupplier.Columns[cell.ColumnIndex].Name == "TotalBelanjaan") // Sesuaikan dengan nama kolom yang digunakan
                                    {
                                        decimal belanjaan;
                                        if (decimal.TryParse(cellValue, out belanjaan))
                                        {
                                            totalBelanjaan += belanjaan;
                                        }
                                    }
                                }
                            }

                            // Tambahkan baris untuk total belanjaan
                            PdfPCell totalCell = new PdfPCell(new Phrase("Total Belanjaan:"));
                            totalCell.Colspan = dgvSupplier.Columns.Count - 1; // Menggabungkan beberapa kolom
                            totalCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            pdfTable.AddCell(totalCell);

                            PdfPCell totalValueCell = new PdfPCell(new Phrase(totalBelanjaan.ToString("C"))); // Format sebagai mata uang
                            pdfTable.AddCell(totalValueCell);

                            // Membuat file PDF
                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Data berhasil dicetak.", "Info");
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
                MessageBox.Show("Tidak ada data yang dapat diekspor!", "Info");
            }
        }
    }
    }