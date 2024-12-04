using ConTime.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace ConTime.Screens
{
    public partial class Bal : UserControl
    {
        public DataSet DS { get; private set; } = new DataSet();
        string tablename = "balancete";
        public Bal()
        {
            InitializeComponent();
            DataTable DT = DS.Tables.Add(tablename);
            dgv_bal.DataSource = DS.Tables[tablename];
            DT.Columns.Add("Código");
            DT.Columns.Add("Conta");
            DT.Columns.Add("Devedor");
            DT.Columns.Add("Credor");
            DT.Columns.Add("Saldo");
            dgv_bal.Columns["Conta"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv_bal.DataSource = DS.Tables[tablename];

            ModificarDataGrid();
        }
        // Em Bal, antes de passar para Exer
        public void PassarDadosParaExer()
        {
            // Verifique se os dados estão prontos
            if (DS.Tables.Contains(tablename))
            {
                // Armazenar no DataStore
                DataStore.BalanceteData = DS.Tables[tablename];
            }
            else
            {
                MessageBox.Show("Tabela não encontrada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(tb_saldo.Text, out decimal valorSaldo))
            {
                MessageBox.Show("Por favor, insira um valor válido no saldo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string codigo = cb_cod.Text;
            string conta = tb_Conta.Text;
            bool encontrouRegistroCompleto = false;
            bool encontrouParcial = false;

            DataRow linhaParcial = null;

            foreach (DataRow row in DS.Tables[tablename].Rows)
            {
                // Verifica se a linha foi excluída
                if (row.RowState == DataRowState.Deleted)
                {
                    continue; // Pula a linha excluída
                }

                // Verifica se já existe uma linha com o mesmo Código e Conta
                if (row["Código"].ToString() == codigo && row["Conta"].ToString() == conta)
                {
                    encontrouRegistroCompleto = true;

                    // Valida o tipo de lançamento para evitar duplicações
                    if (rb_c.Checked && row["Credor"] == DBNull.Value)
                    {
                        decimal saldoAtual = decimal.Parse(row["Saldo"].ToString());
                        saldoAtual += valorSaldo;
                        row["Credor"] = valorSaldo.ToString("#.00");
                        row["Saldo"] = saldoAtual.ToString("#.00");
                    }
                    else if (rb_d.Checked && row["Devedor"] == DBNull.Value)
                    {
                        decimal saldoAtual = decimal.Parse(row["Saldo"].ToString());
                        saldoAtual -= valorSaldo;
                        row["Devedor"] = valorSaldo.ToString("#.00");
                        row["Saldo"] = saldoAtual.ToString("#.00");
                    }
                    else
                    {
                        string tipo = rb_c.Checked ? "crédito" : "débito";
                        MessageBox.Show($"Já existe um lançamento de {tipo} para este código e conta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;
                }

                // Verifica se o Código OU a Conta já existem parcialmente
                if (row["Código"].ToString() == codigo || row["Conta"].ToString() == conta)
                {
                    encontrouParcial = true;
                    linhaParcial = row; // Armazena a linha parcial encontrada
                }
            }

            if (!encontrouRegistroCompleto && encontrouParcial)
            {
                // Pergunta ao usuário se deseja sobrescrever, adicionar ou cancelar
                string mensagem = $"Foi encontrado um registro parcial:\n\n" +
                                  $"Código: {linhaParcial["Código"]}\n" +
                                  $"Conta: {linhaParcial["Conta"]}\n\n" +
                                  $"Deseja:\n" +
                                  $"1. Sobrescrever o registro existente\n" +
                                  $"2. Adicionar como novo registro\n" +
                                  $"3. Cancelar a operação";

                DialogResult resultado = MessageBox.Show(mensagem, "Registro Encontrado", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    // Sobrescreve o registro parcial
                    if (rb_c.Checked)
                    {
                        linhaParcial["Credor"] = valorSaldo.ToString("#.00");
                        linhaParcial["Saldo"] = valorSaldo.ToString("#.00");
                    }
                    else if (rb_d.Checked)
                    {
                        linhaParcial["Devedor"] = valorSaldo.ToString("#.00");
                        linhaParcial["Saldo"] = (-valorSaldo).ToString("#.00");
                    }
                }
                else if (resultado == DialogResult.No)
                {
                    // Adiciona como um novo registro
                    AdicionarNovaLinha(codigo, conta, valorSaldo);
                }
            }
            else if (!encontrouRegistroCompleto)
            {
                // Se não encontrou nem parcialmente, adiciona uma nova linha
                AdicionarNovaLinha(codigo, conta, valorSaldo);
            }

            // Atualiza o DataStore com os dados mais recentes
            DataStore.BalanceteData.Clear(); // Limpa os dados antigos
            foreach (DataRow row in DS.Tables[tablename].Rows)
            {
                DataStore.BalanceteData.ImportRow(row); // Adiciona as linhas do DataSet no DataStore
            }

            // Salva as mudanças
            DS.Tables[tablename].AcceptChanges();

            // Limpa os campos
            LimparCampos();
        }

        private void AdicionarNovaLinha(string codigo, string conta, decimal valorSaldo)
        {
            DataRow novaLinha = DS.Tables[tablename].NewRow();
            novaLinha["Código"] = codigo;
            novaLinha["Conta"] = conta;

            if (rb_c.Checked)
            {
                novaLinha["Credor"] = valorSaldo.ToString("#.00");
                novaLinha["Saldo"] = valorSaldo.ToString("#.00");
            }
            else if (rb_d.Checked)
            {
                novaLinha["Devedor"] = valorSaldo.ToString("#.00");
                novaLinha["Saldo"] = (-valorSaldo).ToString("#.00");
            }

            DS.Tables[tablename].Rows.Add(novaLinha);
        }


        private void LimparCampos()
        {
            rb_c.Checked = false;
            rb_d.Checked = false;
            tb_Conta.Clear();
            tb_saldo.Clear();
            cb_cod.SelectedIndex = -1;

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (dgv_bal.SelectedRows.Count > 0)
                if (!dgv_bal.SelectedRows[0].IsNewRow)
                    dgv_bal.Rows.RemoveAt(dgv_bal.SelectedRows[0].Index);
                else
                    MessageBox.Show("Não se pode apagar linha vazia!");
        }

        public void GerarPdf(object sender, EventArgs e)
        {
            PdfBalancete();
        }

        public MemoryStream PdfBalancete()
        {
            // Certifique-se de que o DataTable existe
            DataTable dtBalancete = DS.Tables[tablename];
            MemoryStream pdfStream = null;

            if (dtBalancete != null && dtBalancete.Rows.Count > 0)
            {
                // Cria a instância de Balancete e gera o PDF em memória
                Classes.Balancete balancete = new Classes.Balancete(dtBalancete);
                pdfStream = balancete.CreatePDF(); // Aqui retornamos o MemoryStream de CreatePDF

                if (pdfStream != null)
                {
                    OpenPdfForViewing(pdfStream);  // Função que abre o PDF sem salvar fisicamente
                }
            }
            else
            {
                MessageBox.Show("Não há dados para gerar o PDF do balancete.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return pdfStream;
        }

        private void OpenPdfForViewing(MemoryStream pdfStream)
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), "Balancete.pdf");

            try
            {
                // Escrever o conteúdo do MemoryStream para o arquivo temporário
                using (FileStream fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                {
                    pdfStream.WriteTo(fileStream);
                }

                // Abrir o arquivo PDF com o programa associado no sistema
                Process.Start(new ProcessStartInfo(tempFilePath)
                {
                    UseShellExecute = true // Isso abre o PDF no programa padrão do sistema
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao tentar abrir o PDF: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_salvar_bal_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
                saveFileDialog.Title = "Salvar Balancete";
                saveFileDialog.DefaultExt = "csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SalvarBalancete();
                }
            }
        }

        public void SalvarBalancete()
        {
            DataTable balanceteData = DataStore.BalanceteData;

            if (balanceteData == null || balanceteData.Rows.Count == 0)
            {
                MessageBox.Show("Não há dados no balancete para salvar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Arquivo CSV (*.csv)|*.csv";
                saveFileDialog.Title = "Salvar Balancete";
                saveFileDialog.DefaultExt = "csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        using (StreamWriter writer = new StreamWriter(filePath))
                        {
                            writer.WriteLine("Código,Conta,Credor,Devedor,Saldo");

                            foreach (DataRow row in balanceteData.Rows)
                            {
                                string codigo = row["Código"]?.ToString() ?? "";
                                string conta = row["Conta"]?.ToString() ?? "";
                                string credor = row["Credor"] != DBNull.Value ? Convert.ToDecimal(row["Credor"]).ToString("F2", CultureInfo.InvariantCulture) : "";
                                string devedor = row["Devedor"] != DBNull.Value ? Convert.ToDecimal(row["Devedor"]).ToString("F2", CultureInfo.InvariantCulture) : "";
                                string saldo = row["Saldo"] != DBNull.Value ? Convert.ToDecimal(row["Saldo"]).ToString("F2", CultureInfo.InvariantCulture) : "";

                                writer.WriteLine($"{codigo},{conta},{credor},{devedor},{saldo}");
                            }

                            MessageBox.Show("Balancete salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao salvar o balancete: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



        private void ImportarBalancete(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line = reader.ReadLine(); 

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] data = line.Split(',');

                        if (data.Length == 5) 
                        {
                            string codigo = data[0];
                            string conta = data[1];
                            decimal? credor = string.IsNullOrEmpty(data[2]) ? (decimal?)null : Convert.ToDecimal(data[2], CultureInfo.InvariantCulture);
                            decimal? devedor = string.IsNullOrEmpty(data[3]) ? (decimal?)null : Convert.ToDecimal(data[3], CultureInfo.InvariantCulture);
                            decimal? saldo = string.IsNullOrEmpty(data[4]) ? (decimal?)null : Convert.ToDecimal(data[4], CultureInfo.InvariantCulture);

                            DataRow linhaExistente = DS.Tables[tablename].AsEnumerable()
                                .FirstOrDefault(row =>
                                    row.RowState != DataRowState.Deleted &&
                                    (row.Field<string>("Código") == codigo || row.Field<string>("Conta") == conta));

                            if (linhaExistente != null)
                            {
                                bool codigoIgual = linhaExistente.Field<string>("Código") == codigo;
                                bool contaIgual = linhaExistente.Field<string>("Conta") == conta;

                                string mensagem = $"Foi encontrado um registro parcialmente ou totalmente igual:\n\n" +
                                                  $"Código: {linhaExistente["Código"]}\n" +
                                                  $"Conta: {linhaExistente["Conta"]}\n\n" +
                                                  $"Deseja:\n" +
                                                  $"1. Sobrescrever o registro existente\n" +
                                                  $"2. Adicionar como novo registro\n" +
                                                  $"3. Cancelar a operação";

                                DialogResult resultado = MessageBox.Show(mensagem, "Registro Encontrado", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                                if (resultado == DialogResult.Yes)
                                {
                                    if (credor.HasValue) linhaExistente["Credor"] = credor.Value.ToString("#.00");
                                    if (devedor.HasValue) linhaExistente["Devedor"] = devedor.Value.ToString("#.00");
                                    if (saldo.HasValue) linhaExistente["Saldo"] = saldo.Value.ToString("#.00");
                                }
                                else if (resultado == DialogResult.No)
                                {
                                    AdicionarLinhaImportada(codigo, conta, credor, devedor, saldo);
                                }
                            }
                            else
                            {
                                AdicionarLinhaImportada(codigo, conta, credor, devedor, saldo);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Formato incorreto na linha: {line}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    MessageBox.Show("Balancete importado com sucesso! Linhas duplicadas foram tratadas.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao importar o balancete: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdicionarLinhaImportada(string codigo, string conta, decimal? credor, decimal? devedor, decimal? saldo)
        {
            DataRow novaLinha = DS.Tables[tablename].NewRow();
            novaLinha["Código"] = codigo;
            novaLinha["Conta"] = conta;
            novaLinha["Credor"] = credor.HasValue ? credor.Value.ToString("#.00") : (object)DBNull.Value;
            novaLinha["Devedor"] = devedor.HasValue ? devedor.Value.ToString("#.00") : (object)DBNull.Value;
            novaLinha["Saldo"] = saldo.HasValue ? saldo.Value.ToString("#.00") : (object)DBNull.Value;

            DS.Tables[tablename].Rows.Add(novaLinha);
        }




        private void ModificarDataGrid()
        {
            dgv_bal.ColumnHeadersDefaultCellStyle.Font = new Font("Yu Gothic UI", 12, FontStyle.Bold);
            dgv_bal.DefaultCellStyle.BackColor = Color.FromArgb(185, 220, 201);
            dgv_bal.DefaultCellStyle.Font = new Font("Yu Gothic UI", 12, FontStyle.Bold);
        }
        private void tb_Conta_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
                openFileDialog.Title = "Importar Balancete";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ImportarBalancete(openFileDialog.FileName);
                }
            }
        }

    }
}

