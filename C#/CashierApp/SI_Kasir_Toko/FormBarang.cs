using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Org.BouncyCastle.Math;

namespace SI_Kasir_Toko
{
    using static GlobalVariable;
    public partial class FormBarang : Form
    {
        public string nameBarang;
        public FormBarang()
        {
            InitializeComponent();
            btnEdit.Enabled = false;
        }

        private void loadDataBarang()
        {
            var dataBarang = from barang in Db.Barcode2s
                             select new
                             {
                                 Kode = barang.BarcodeID,
                                 Nama = barang.Nama,
                                 Stok = barang.Stok,
                                 Harga = barang.Harga,
                             };
            dataGridView1.DataSource = dataBarang.ToList();
        }

        private void FormBarang_Load(object sender, EventArgs e)
        {
            loadDataBarang();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if(role == true)
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

        private bool isInputValid()
        {
            if (string.IsNullOrEmpty(fieldBarang.Text))
            {
                MessageBox.Show("Pastikan Nama Barang Sudah Terisi!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(fieldStock.Text))
            {
                MessageBox.Show("Pastikan Stock Barang Sudah Terisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(fieldHarga.Text))
            {
                MessageBox.Show("Pastikan Harga Barang Sudah Terisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(fieldDataMasuk.Text))
            {
                MessageBox.Show("Pastikan Tanggal Barang Masuk Sudah Terisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (isInputValid())
            {
                if (long.TryParse(txtBarang.Text, out long barcodeIDValue))
                {
                    Barcode2 newBarang = new Barcode2
                    {
                        BarcodeID = barcodeIDValue,
                        Nama = fieldBarang.Text,
                        Stok = Convert.ToInt32(fieldStock.Text),
                        Harga = Convert.ToInt32(fieldHarga.Text),
                    };

                    Db.Barcode2s.InsertOnSubmit(newBarang);
                    Db.SubmitChanges();

                    MessageBox.Show("Data Barang Berhasil Dimasukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDataBarang(); // Refresh data setelah insert
                }
                else
                {
                    MessageBox.Show("Barcode ID tidak valid. Pastikan hanya angka yang dimasukkan dan dalam rentang yang benar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Pastikan Semua Data Sudah Terisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            loadDataBarang();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            txtBarang.Text = "";
            fieldBarang.Text = "";
            fieldStock.Value = 0;
            fieldHarga.Value = 0;
            fieldDataMasuk.Value = DateTime.Now;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                string IDBarang = selectedRow.Cells["Kode"].Value != null ? selectedRow.Cells["Kode"].Value.ToString() : "";
                string namaBarang = selectedRow.Cells["Nama"].Value != null ? selectedRow.Cells["Nama"].Value.ToString() : "";
                int stok = selectedRow.Cells["Stok"].Value != null && int.TryParse(selectedRow.Cells["Stok"].Value.ToString(), out int stokValue) ? stokValue : 0;
                int harga = selectedRow.Cells["Harga"].Value != null && int.TryParse(selectedRow.Cells["Harga"].Value.ToString(), out int hargaValue) ? hargaValue : 0;

                // Mengisi field pada form dengan data yang diambil dari DataGridView
                txtBarang.Text = IDBarang;
                fieldBarang.Text = namaBarang;
                fieldStock.Value = stok;
                fieldHarga.Value = harga;

                btnEdit.Enabled = true;
            }
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (long.TryParse(txtBarang.Text, out long valueID)) // Memperbaiki penggunaan TryParse
            {
                if (fieldBarang.Text != "")
                {
                    var editListed = Db.Barcode2s.FirstOrDefault(i => i.BarcodeID == valueID); // Menggunakan fieldBarang.Text untuk pencarian
                    if (editListed != null)
                    {
                        editListed.Nama = fieldBarang.Text;
                        editListed.Stok = Convert.ToInt32(fieldStock.Value);
                        editListed.Harga = Convert.ToInt32(fieldHarga.Value);

                        Db.SubmitChanges();
                        MessageBox.Show("Data Berhasil Dirubah", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("pilih data yang ingin anda rubah lebih dahulu", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    MessageBox.Show("Silakan pilih data yang ingin anda rubah lebih dahulu", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("ID Barang tidak valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count >= 0)
            {
                var selectedBarang = Db.Barcode2s.FirstOrDefault(i => i.Nama == fieldBarang.Text);
                if (selectedBarang != null)
                {
                    selectedBarang.Stok = 0;
                    Db.SubmitChanges();
                    MessageBox.Show("Oke");
                }
            }
            else
            {
                MessageBox.Show("Silakan pilih barang yang ingin direset stoknya.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
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
                            PdfPTable pdfTable = new PdfPTable(dataGridView1.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dataGridView1.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dataGridView1.Rows)
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

        private void label7_Click(object sender, EventArgs e)
        {
            
        }

        private void txtBarang_TextChanged(object sender, EventArgs e)
        {
            var textSearched = txtBarang.Text;
            if (!string.IsNullOrEmpty(txtBarang.Text))
            {
                var dataTransaksi = from transaksi in Db.Barcode2s
                                    select new
                                    {
                                        Nama = transaksi.Nama,
                                        Kode = transaksi.BarcodeID,
                                        Stock = transaksi.Stok,
                                        Harga = transaksi.Harga,
                                    };

                // Terapkan filter dalam memori menggunakan LINQ to Objects
                var filteredDataTransaksi = dataTransaksi.Where(i => i.Nama.ToLower().Contains(textSearched) ||
                                                                     i.Kode.ToString().Contains(textSearched)).ToList();
                dataGridView1.DataSource = filteredDataTransaksi;

                if (dataTransaksi != null)
                {
                    dataGridView1.DataSource = filteredDataTransaksi.ToList();
                }
                else
                {
                    MessageBox.Show("Pastikan anda mencari berdasarkan nama barang", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("Silakan isi field search terlebih dahulu", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}
