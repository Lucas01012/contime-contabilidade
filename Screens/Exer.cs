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
        private float[] resul = new float[8];
        private DataSet DS = new DataSet();
        private DataTable dados;

        public Exer()
        {
            InitializeComponent();
            this.Size = new Size(359, 200);
        }

        public void SalvarTudo()
        {
            string balanceteFilePath = null;
            string livroDiarioFilePath = null;
            string dreFilePath = null;
            string razoneteFilePath = null;
            string balancoFilePath = null;

            try
            {
                balanceteFilePath = GerarArquivoBalancete();
                livroDiarioFilePath = GerarArquivoLDia();
                dreFilePath = GerarArquivoDre();
                balancoFilePath = GerarArquivoBalancoPatrimonial();

                var razonete = new Razonete();
                razoneteFilePath = razonete.GerarArquivoRazonete();

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
                            // Adicionar arquivos ao ZIP
                            zipArchive.CreateEntryFromFile(balanceteFilePath, "Balancete.csv");
                            zipArchive.CreateEntryFromFile(livroDiarioFilePath, "LivroDiario.csv");
                            zipArchive.CreateEntryFromFile(razoneteFilePath, "Razonetes.csv");
                            zipArchive.CreateEntryFromFile(dreFilePath, "Dre.csv");
                            zipArchive.CreateEntryFromFile(balancoFilePath, "BalancoPatrimonial.csv");

                            MessageBox.Show("Arquivos salvos e compactados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar os módulos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (balanceteFilePath != null && File.Exists(balanceteFilePath))
                {
                    File.Delete(balanceteFilePath);
                }
                if (livroDiarioFilePath != null && File.Exists(livroDiarioFilePath))
                {
                    File.Delete(livroDiarioFilePath);
                }
                if (dreFilePath != null && File.Exists(dreFilePath))
                {
                    File.Delete(dreFilePath);
                }
                if (razoneteFilePath != null && File.Exists(razoneteFilePath))
                {
                    File.Delete(razoneteFilePath);
                }
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
        private string GerarArquivoDre()
        {
            string filePath = Path.Combine(Path.GetTempPath(), "Dre.csv");
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                string[] tables = { "RBruta", "Imposto", "Custos", "Despesas", "OutrasR", "Resultados Calculados" };

                foreach (string tableName in tables)
                {
                    if (DataStore.DreData.Tables.Contains(tableName))
                    {
                        writer.WriteLine($"Tabela: {tableName}");
                        writer.WriteLine("Receita,Valor");

                        foreach (DataRow row in DataStore.DreData.Tables[tableName].Rows)
                        {
                            string receita = row["Receita"]?.ToString() ?? "";
                            string valor = row["Valor"]?.ToString() ?? "";
                            writer.WriteLine($"{receita},{valor}");
                        }

                        writer.WriteLine();
                    }
                }
            }
            return filePath;
        }
        private string GerarArquivoBalancoPatrimonial()
        {
            string filePath = Path.Combine(Path.GetTempPath(), "BalancoPatrimonial.csv");
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                string[] tables = { "AtvCirculante", "AtvNCirculante", "PsvCirculante", "PsvNCirculante", "Patrimonio" };

                foreach (string tableName in tables)
                {
                    if (DataStore.BalancoPatrimonialData.Tables.Contains(tableName))
                    {
                        DataTable table = DataStore.BalancoPatrimonialData.Tables[tableName];
                        if (table.Rows.Count > 0)
                        {
                            writer.WriteLine($"Tabela: {tableName}");
                            writer.WriteLine("Nome,Valor");

                            foreach (DataRow row in table.Rows)
                            {
                                string nome = row[0]?.ToString() ?? "";
                                string valor = row[1]?.ToString() ?? "";

                                if (!string.IsNullOrWhiteSpace(nome) || !string.IsNullOrWhiteSpace(valor))
                                {
                                    writer.WriteLine($"{nome},{valor}");
                                }
                            }

                            writer.WriteLine();
                        }
                    }
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
            DataSet dreData = DataStore.DreData;

            if (dreData == null || dreData.Tables.Count == 0)
            {
                throw new InvalidOperationException("Não há dados para gerar o PDF da DRE.");
            }

            string[] requiredTables = { "RBruta", "Imposto", "Custos", "Despesas", "ROutras" };

            foreach (string tableName in requiredTables)
            {
                if (!dreData.Tables.Contains(tableName) || dreData.Tables[tableName].Rows.Count == 0)
                {
                    throw new InvalidOperationException($"A tabela {tableName} está faltando ou não possui dados.");
                }
            }

            // Cálculos das métricas financeiras
            float receitaLiquida = CalcularReceitaLiquida(dreData.Tables["RBruta"], dreData.Tables["Imposto"]);
            float lucroBruto = CalcularLucroBruto(receitaLiquida, dreData.Tables["Custos"]);
            float lucroLiquido = CalcularLucroLiquido(lucroBruto, dreData.Tables["Despesas"], dreData.Tables["ROutras"]);

            var dre = new Classes.Dre(
                dreData.Tables["RBruta"],
                dreData.Tables["Imposto"],
                dreData.Tables["Custos"],
                dreData.Tables["Despesas"],
                dreData.Tables["ROutras"],
                receitaLiquida, lucroBruto, lucroLiquido
            );

            // Preencher o DataStore
            dre.PreencherDataStore();

            try
            {
                using (var pdfStream = dre.PdfCreate())
                {
                    MemoryStream pdfMemoryStream = new MemoryStream();
                    pdfStream.CopyTo(pdfMemoryStream);
                    OpenPdfForViewing(pdfMemoryStream);
                    return pdfMemoryStream;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar o PDF: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private float CalcularReceitaLiquida(DataTable rBruta, DataTable impostos)
        {
            float receitaBruta = 0;
            float totalImpostos = 0;

            foreach (DataRow row in rBruta.Rows)
            {
                if (float.TryParse(row["Valor"].ToString(), out float valor))
                {
                    receitaBruta += valor;
                }
                else
                {
                    MessageBox.Show($"Formato inválido na tabela RBruta: {row["Valor"]}");
                }
            }

            foreach (DataRow row in impostos.Rows)
            {
                if (float.TryParse(row["Valor"].ToString(), out float valor))
                {
                    totalImpostos += valor;
                }
                else
                {
                    MessageBox.Show($"Formato inválido na tabela Imposto: {row["Valor"]}");
                }
            }

            return receitaBruta - totalImpostos;
        }

        private float CalcularLucroBruto(float receitaLiquida, DataTable custos)
        {
            float totalCustos = 0;

            foreach (DataRow row in custos.Rows)
            {
                if (float.TryParse(row["Valor"].ToString(), out float valor))
                {
                    totalCustos += valor;
                }

            }

            return receitaLiquida - totalCustos;
        }

        private float CalcularLucroLiquido(float lucroBruto, DataTable despesas, DataTable outrasReceitasDespesas)
        {
            float totalDespesas = 0;
            float totalOutras = 0;

            foreach (DataRow row in despesas.Rows)
            {
                if (float.TryParse(row["Valor"].ToString(), out float valor))
                {
                    totalDespesas += valor;
                }
                else
                {
                    MessageBox.Show($"Formato inválido na tabela Despesas: {row["Valor"]}");
                }
            }

            foreach (DataRow row in outrasReceitasDespesas.Rows)
            {
                if (float.TryParse(row["Valor"].ToString(), out float valor))
                {
                    totalOutras += valor;
                }

            }

            return lucroBruto - totalDespesas + totalOutras;
        }



        private MemoryStream PdfBPat()
        {
            DataSet bPat = DataStore.BalancoPatrimonialData;

            if (bPat == null || bPat.Tables.Count == 0)
            {
                throw new InvalidOperationException("Não há dados para gerar o PDF do balanço patrimonial.");
            }

            string[] requiredTables = { "AtvCirculante", "AtvNCirculante", "PsvCirculante", "PsvNCirculante", "Patrimonio"};

            foreach (string tableName in requiredTables)
            {
                if (!bPat.Tables.Contains(tableName))
                {
                    bPat.Tables.Add(new DataTable(tableName));
                }
            }

            var Balanco = new Classes.BPat(
                bPat.Tables["AtvCirculante"],
                bPat.Tables["AtvNCirculante"],
                bPat.Tables["PsvCirculante"],
                bPat.Tables["PsvNCirculante"],
                bPat.Tables["Patrimonio"]
            );

            try
            {
                return Balanco.PdfCreate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar o PDF: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
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
                MemoryStream BpatStream = PdfBPat();

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
                // Verifica se o arquivo já está aberto antes de criar um novo
                if (IsFileOpen(tempFilePath))
                {
                    MessageBox.Show("O arquivo PDF já está aberto. Feche o arquivo antes de tentar abri-lo novamente.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Salva o PDF no caminho temporário
                using (FileStream fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                {
                    pdfStream.WriteTo(fileStream);
                }

                // Abre o PDF no visualizador padrão
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

        /// <summary>
        /// </summary>
        /// <param name="filePath">O caminho do arquivo a verificar.</param>
        /// <returns>Retorna true se o arquivo está aberto; caso contrário, false.</returns>
        private bool IsFileOpen(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                return false;

            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }


        private void btn_Salvar_Click(object sender, EventArgs e)
        {
            SalvarTudo();
        }

    }
}