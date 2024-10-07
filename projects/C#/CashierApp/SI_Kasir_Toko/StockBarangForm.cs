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
    public partial class StockBarangForm : Form
    {
        public StockBarangForm()
        {
            InitializeComponent();
        }

        private void StockBarangForm_Load(object sender, EventArgs e)
        {
            loadDataStock();
        }

        private void loadDataStock()
        {
            var dataStock = from barang in Db.Barangs
                            select new
                            {
                                Code_Barang = barang.ID,
                                Nama_Barang = barang.NamaBarang,
                                Harga = barang.HargaBarang,
                                Stock = barang.StokBarang,
                                Tanggal_Masuk = barang.DataMasuk
                            };

            dgvStock.DataSource = dataStock.ToList();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var textSearched = fieldSearched.Text;
            if (!string.IsNullOrEmpty(fieldSearched.Text))
            {
                var dataStock = (from barang in Db.Barangs
                                select new
                                {
                                    Code_Barang = barang.ID,
                                    Nama_Barang = barang.NamaBarang,
                                    Harga = barang.HargaBarang,
                                    Stock = barang.StokBarang,
                                    Tanggal_Masuk = barang.DataMasuk
                                }).Where(i => i.Nama_Barang.ToLower().Contains(textSearched) || i.Code_Barang.Equals(textSearched));

                if (dataStock != null)
                {
                    dgvStock.DataSource = dataStock.ToList();
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
            loadDataStock();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvStock.Rows.Count > 0)
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
                            PdfPTable pdfTable = new PdfPTable(dgvStock.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dgvStock.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dgvStock.Rows)
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

        private void button3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Yakin mau menghapus semua data transaksi ?", "Reminder", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                dgvStock.DataSource = null;
                MessageBox.Show("Berhasil mereset data dalam data grid view", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
    