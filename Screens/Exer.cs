using ConTime.Classes;
using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConTime.Classes;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using System.Diagnostics;
using System.IO.Compression;
using System.Globalization;
using Org.BouncyCastle.Crypto.Agreement.Srp;


namespace ConTime.Screens
{
    public partial class Exer : UserControl
    {
        private DataTable dados;

        public Exer()
        {
            InitializeComponent();
            this.Size = new Size(300, 200);
        }

        public void SalvarTudo()
        {
            try
            {
                string balanceteFilePath = GerarArquivoBalancete();

                string livroDiarioFilePath = GerarArquivoLDia();

                var razonete = new Razonete();

              string razoneteFilePath =  razonete.GerarArquivoRazonete();  // Agora não esperamos o retorno do caminho

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Arquivo ZIP (*.zip)|*.zip";
                    saveFileDialog.Title = "Salvar Arquivos Compactados";
                    saveFileDialog.DefaultExt = "zip";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string zipFilePath = saveFileDialog.FileName;

                        using (var zipFileStream = new FileStream(zipFilePath, FileMode.Create))
                        using (var zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
                        {
                            AddFileToZip(zipArchive, "Balancete.csv", balanceteFilePath);
                            AddFileToZip(zipArchive, "LivroDiario.csv", livroDiarioFilePath);
                            AddFileToZip(zipArchive, "Razonetes.csv", razoneteFilePath);

                            MessageBox.Show("Arquivos salvos e compactados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar os módulos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string GerarArquivoBalancete()
        {
            string filePath = Path.Combine(Path.GetTempPath(), "Balancete.csv");
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Código,Conta,Credor,Devedor,Saldo");
                foreach (DataRow row in DataStore.BalanceteData.Rows)
                {
                    string codigo = row["Código"].ToString();
                    string conta = row["Conta"].ToString();
                    string credor = row["Credor"] != DBNull.Value ? Convert.ToDecimal(row["Credor"]).ToString("F2", CultureInfo.InvariantCulture) : "";
                    string devedor = row["Devedor"] != DBNull.Value ? Convert.ToDecimal(row["Devedor"]).ToString("F2", CultureInfo.InvariantCulture) : "";
                    string saldo = row["Saldo"] != DBNull.Value ? Convert.ToDecimal(row["Saldo"]).ToString("F2", CultureInfo.InvariantCulture) : "";

                    writer.WriteLine($"{codigo},{conta},{credor},{devedor},{saldo}");
                }
            }
            return filePath; 
        }

        private string GerarArquivoLDia()
        {
            string filePath = Path.Combine(Path.GetTempPath(), "LivroDiario.csv");
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Código,Data,Conta,Histórico,Débito,Crédito,Saldo");
                foreach (DataRow row in DataStore.LdiaData.Rows)
                {
                    string codigo = row["Código"].ToString();
                    string data = Convert.ToDateTime(row["Data"]).ToString("dd/MM/yyyy");
                    string conta = row["Conta"].ToString();
                    string historico = row["Histórico"].ToString();
                    string debito = row["Débito"] != DBNull.Value ? Convert.ToDecimal(row["Débito"]).ToString("F2", CultureInfo.InvariantCulture) : "";
                    string credito = row["Crédito"] != DBNull.Value ? Convert.ToDecimal(row["Crédito"]).ToString("F2", CultureInfo.InvariantCulture) : "";
                    string saldo = row["Saldo"] != DBNull.Value ? Convert.ToDecimal(row["Saldo"]).ToString("F2", CultureInfo.InvariantCulture) : "";

                    writer.WriteLine($"{codigo},{data},{conta},{historico},{debito},{credito},{saldo}");
                }
            }
            return filePath;
        }


        private void AddFileToZip(ZipArchive zipArchive, string fileName, string filePath)
        {
            if (File.Exists(filePath))
            {
                var zipEntry = zipArchive.CreateEntry(fileName);
                using (var entryStream = zipEntry.Open())
                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    fileStream.CopyTo(entryStream);
                }
            }
            else
            {
                MessageBox.Show($"O arquivo {fileName} não foi encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private MemoryStream PdfBalancete()
        {
            DataTable dtBalancete = DataStore.BalanceteData;

            if (dtBalancete == null || dtBalancete.Rows.Count == 0)
            {
                throw new InvalidOperationException("Não há dados para gerar o PDF do balancete.");
            }

            var balancete = new Balancete(dtBalancete);
            return balancete.PdfCreate();
        }

        private MemoryStream PdfRazonete()
        {
            var razonetes = DataStore.ObterRazonetes();
            var outputStream = new MemoryStream();

            foreach (var razonete in razonetes)
            {
                var pdfStream = razonete.PdfRazoGeral();

                if (pdfStream != null)
                {
                    pdfStream.Position = 0;

                    pdfStream.CopyTo(outputStream);
                }
                else if (pdfStream == null)
                {
                    MessageBox.Show("Não foram encontrados razonetes!");
                }
            }
            outputStream.Position = 0;

            return outputStream;
        }
        private MemoryStream DRE()
        {
            var dre = new Dre();

            foreach (var painel in DataStore.DrePanéisData)
            {
                DataTable painelData = painel.Value;

                if (painelData != null && painelData.Rows.Count > 0)
                {
                    foreach (DataRow row in painelData.Rows)
                    {
                        string conta = row["Conta"].ToString();
                        string valor = row["Valor"].ToString();

                    }
                }
            }

            return dre.GerarPdf();
        }



        private MemoryStream BPat()
        {
            var balancoPatrimonial = DataStore.BalancoPatrimoniais.FirstOrDefault();

            if (balancoPatrimonial == null ||
                (balancoPatrimonial.Ativos.Count == 0 && balancoPatrimonial.Passivos.Count == 0 && balancoPatrimonial.Patrimonio.Count == 0))
            {
                throw new InvalidOperationException("Não há dados suficientes para gerar o PDF do Balanço Patrimonial.");
            }

            var Bpat = new BPat();

            return Bpat.PdfGeral(balancoPatrimonial.Ativos, balancoPatrimonial.Passivos, balancoPatrimonial.Patrimonio);
        }


        private MemoryStream PdfLdia()
        {
            DataTable dtLdia = DataStore.LdiaData;

            if (dtLdia == null || dtLdia.Rows.Count == 0)
            {
                throw new InvalidOperationException("Não há dados para gerar o PDF do Livro Diário.");
            }

            var ldia = new LDia(dtLdia);
            return ldia.PdfGerar();
        }

        private void btn_Pdf_Click(object sender, EventArgs e)
        {
            String painel = "Ativo Circulante";
            try
            {
                MemoryStream ldiaStream = PdfLdia();
                MemoryStream RazoneteStream = PdfRazonete();
                MemoryStream balanceteStream = PdfBalancete();
                MemoryStream DreStream = DRE();
                MemoryStream BpatStream = BPat();

                MemoryStream combinedPdfStream = CombinePDFs(ldiaStream, RazoneteStream, balanceteStream, DreStream, BpatStream);

                OpenPdfForViewing(combinedPdfStream);

                MessageBox.Show("PDF combinado gerado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar PDF: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private MemoryStream CombinePDFs(MemoryStream pdf1, MemoryStream pdf2, MemoryStream pdf3, MemoryStream pdf4, MemoryStream pdf5)
        {
            if (pdf1 == null || pdf2 == null || pdf3 == null || pdf4 == null || pdf5 == null)
            {
                throw new ArgumentNullException("Os streams de PDF fornecidos não podem ser nulos.");
            }

            var combinedStream = new MemoryStream();

            try
            {
                using (PdfDocument outputDocument = new PdfDocument())
                {
                    AddPagesToDocument(outputDocument, pdf1);
                    AddPagesToDocument(outputDocument, pdf2);
                    AddPagesToDocument(outputDocument, pdf3);
                    AddPagesToDocument(outputDocument, pdf4);
                    AddPagesToDocument(outputDocument, pdf5);

                    outputDocument.Save(combinedStream, false);
                }

                combinedStream.Position = 0;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao combinar PDFs.", ex);
            }

            return combinedStream;
        }

        private void AddPagesToDocument(PdfDocument outputDocument, MemoryStream pdfStream)
        {
            pdfStream.Position = 0;

            using (PdfDocument inputDocument = PdfReader.Open(pdfStream, PdfDocumentOpenMode.Import))
            {
                for (int i = 0; i < inputDocument.PageCount; i++)
                {
                    outputDocument.AddPage(inputDocument.Pages[i]);
                }
            }
        }


        private void OpenPdfForViewing(MemoryStream pdfStream)
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), "documento_unificado.pdf");

            try
            {
                using (FileStream fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                {
                    pdfStream.WriteTo(fileStream);
                }

                var processInfo = new ProcessStartInfo(tempFilePath)
                {
                    UseShellExecute = true
                };
                Process.Start(processInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao abrir o PDF: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Salvar_Click(object sender, EventArgs e)
        {
            SalvarTudo();
        }
    }
}