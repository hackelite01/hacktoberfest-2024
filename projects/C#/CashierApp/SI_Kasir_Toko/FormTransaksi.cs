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
    public partial class FormTransaksi : Form
    {
        public FormTransaksi()
        {
            InitializeComponent();
        }

        private void FormTransaksi_Load(object sender, EventArgs e)
        {
            LoadDataTransaksi();
        }

        private void LoadDataTransaksi()
        {
            var dataTransaksi = from transaksi in Db.Transactions
                                join barang in Db.Barangs on transaksi.IDBarang equals barang.ID
                                select new
                                {
                                    Tanggal = transaksi.TransaksiAt,
                                    Code_transaksi = transaksi.ID,
                                    Code_Barang = barang.ID,
                                    Nama_Barang = barang.NamaBarang,
                                    Harga = barang.HargaBarang,
                                    Jumlah = transaksi.JumlahTransaksi,
                                    Total = transaksi.TotalHarga
                                };

            dgvTransaksi.DataSource = dataTransaksi.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDataTransaksi();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var textSearched = fieldSearched.Text.ToLower();
            if (!string.IsNullOrEmpty(fieldSearched.Text))
            {
                var dataTransaksi = (from transaksi in Db.Transactions
                                     join barang in Db.Barangs on transaksi.IDBarang equals barang.ID
                                     select new
                                     {
                                         Code_Transaksi = transaksi.ID,
                                         Code_Barang = transaksi.IDBarang,
                                         Nama_Barang = barang.NamaBarang,
                                         Harga = barang.HargaBarang,
                                         Jumlah = transaksi.JumlahTransaksi,
                                         Tanggal = transaksi.TransaksiAt,
                                         Total = transaksi.TotalHarga
                                     }).ToList(); // Panggil ToList() untuk memuat data ke dalam memori

                // Terapkan filter dalam memori menggunakan LINQ to Objects
                var filteredDataTransaksi = dataTransaksi.Where(i => i.Nama_Barang.ToLower().Contains(textSearched) ||
                                                                     i.Code_Transaksi.ToString().Equals(textSearched)).ToList();

                if (dataTransaksi != null)
                { 
                dgvTransaksi.DataSource = filteredDataTransaksi.ToList();
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

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (role == true)
            {
                dashboardKasir.Show();
            }
            else
            {
                dashboardAdmin.Show();
            }
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Yakin mau menghapus semua data transaksi ?", "Reminder", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                dgvTransaksi.DataSource = null;
                MessageBox.Show("Berhasil mereset data dalam data grid view", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvTransaksi.Rows.Count > 0)
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
                            PdfPTable pdfTable = new PdfPTable(dgvTransaksi.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dgvTransaksi.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dgvTransaksi.Rows)
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

        private void fieldSearched_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
