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
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConTime.Screens
{
    public partial class LDia : UserControl
    {
        public DataSet DS { get; private set; } = new DataSet();
        string tablename = "livro_diario";

        public LDia(DataTable dt = null)
        {
            InitializeComponent();

            DataTable DT;

            if (dt != null)
            {
                if (dt.DataSet != null)
                {
                    dt.DataSet.Tables.Remove(dt); 
                }

                DT = dt;

                if (DS.Tables.Contains(tablename))
                {
                    DS.Tables[tablename].Clear();
                }
                else
                {
                    DT.TableName = tablename;
                    DS.Tables.Add(DT);
                }
            }
            else
            {
                DT = DS.Tables.Add(tablename);
                DT.Columns.Add("Data");
                DT.Columns.Add("Código");
                DT.Columns.Add("Conta");
                DT.Columns.Add("Histórico");
                DT.Columns.Add("Débito");
                DT.Columns.Add("Crédito");
                DT.Columns.Add("Saldo");
            }

            dgv.DataSource = DS.Tables[tablename];

            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv.Columns["Código"].Width = 65;
            dgv.Columns["Histórico"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(29, 61, 48);
            dgv.DefaultCellStyle.SelectionBackColor = Color.White;
            dgv.DefaultCellStyle.Font = new Font("Yu Gothic UI", 12, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Yu Gothic UI", 12, FontStyle.Bold);

        }

        private void PassarDadosParaExer()
        {
            if (DS.Tables.Contains(tablename))
            {
                DataStore.LdiaData = DS.Tables[tablename];
            }
            else
            {
                MessageBox.Show("Tabela não encontrada.","Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btn_insert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tb_saldo.Text) ||
                string.IsNullOrWhiteSpace(tb_valor.Text) ||
                string.IsNullOrWhiteSpace(cb_cod.Text) ||
                string.IsNullOrWhiteSpace(tb_Conta.Text) ||
                string.IsNullOrWhiteSpace(tb_historico.Text))
            {
                MessageBox.Show("Por favor, preencha todos os campos obrigatórios.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string codigo = cb_cod.Text;
            string conta = tb_Conta.Text;
            bool isCredito = rb_c.Checked;

            DataRow? registroExistente = DS.Tables[tablename].Rows
                .Cast<DataRow>()
                .Where(row => row.RowState != DataRowState.Deleted)
                .FirstOrDefault(row =>
                    row["Código"].ToString() == codigo &&
                    row["Conta"].ToString() == conta &&
                    ((isCredito && !string.IsNullOrEmpty(row["Crédito"].ToString()) && row["Crédito"].ToString() != "0.00") ||
                     (!isCredito && !string.IsNullOrEmpty(row["Débito"].ToString()) && row["Débito"].ToString() != "0.00"))
                );

            if (registroExistente != null)
            {
                DialogResult resultado = MessageBox.Show(
                    $"Já existe um registro do tipo {(isCredito ? "Crédito" : "Débito")} para esta Conta e Código. Deseja:\n\n" +
                    "Sim: Substituir o registro existente\n" +
                    "Não: Inserir um novo registro\n" +
                    "Cancelar: Não fazer nada",
                    "Registro Duplicado",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question
                );

                if (resultado == DialogResult.Cancel) return;

                if (resultado == DialogResult.Yes)
                {
                    AtualizarRegistro(registroExistente);
                    return;
                }
            }

            DS.Tables[tablename].AcceptChanges();
            InserirNovoRegistro();
        }



        private void InserirNovoRegistro()
        {
            DataRow dr = DS.Tables[tablename].NewRow();
            decimal.TryParse(tb_valor.Text, out decimal valor);
            decimal.TryParse(tb_saldo.Text, out decimal saldo);

            dr["Data"] = dtp.Value;
            dr["Código"] = cb_cod.Text;
            dr["Conta"] = tb_Conta.Text;
            dr["Histórico"] = tb_historico.Text;

            if (rb_c.Checked) // Inserir como Crédito
            {
                dr["Crédito"] = valor.ToString("F2");
                dr["Débito"] = "0.00";
                dr["Saldo"] = (saldo + valor).ToString("F2");
            }
            else if (rb_d.Checked) // Inserir como Débito
            {
                dr["Débito"] = valor.ToString("F2");
                dr["Crédito"] = "0.00";
                dr["Saldo"] = (saldo - valor).ToString("F2");
            }

            DS.Tables[tablename].Rows.Add(dr);
            DS.Tables[tablename].AcceptChanges(); // Confirmar as mudanças
            PassarDadosParaExer();
            LimparCampos();
        }

        private void AtualizarRegistro(DataRow row)
        {
            decimal.TryParse(tb_valor.Text, out decimal valor);
            decimal.TryParse(row["Saldo"].ToString(), out decimal saldo);

            row["Data"] = dtp.Value;
            row["Histórico"] = tb_historico.Text;

            if (rb_c.Checked)
            {
                row["Crédito"] = valor.ToString("F2");
                row["Débito"] = "0.00";
                row["Saldo"] = (saldo + valor).ToString("F2");
            }
            else if (rb_d.Checked)
            {
                row["Débito"] = valor.ToString("F2");
                row["Crédito"] = "0.00";
                row["Saldo"] = (saldo - valor).ToString("F2");
            }

            DS.Tables[tablename].AcceptChanges(); // Confirmar as mudanças
            PassarDadosParaExer();
        }

        private void LimparCampos()
        {
            tb_saldo.Clear();
            tb_valor.Clear();
            tb_Conta.Clear();
            tb_historico.Clear();
            cb_cod.SelectedIndex = -1;
            rb_c.Checked = false;
            rb_d.Checked = false;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0)
                if (!dgv.SelectedRows[0].IsNewRow)
                    dgv.Rows.RemoveAt(dgv.SelectedRows[0].Index);
        }

        private void GerarPdf(object sender, EventArgs e)
        {
            MemoryStream pdfStream = PdfGerar();

            if (pdfStream != null)
            {
                OpenPdfForViewing(pdfStream);
            }
        }

        public MemoryStream PdfGerar()
        {
            if (DS.Tables.Contains(tablename) && DS.Tables[tablename] != null)
            {
                if (DS.Tables[tablename].Rows.Count > 0)
                {
                    Classes.LDia ldia = new Classes.LDia(DS.Tables[tablename]);
                    return ldia.PdfCreate();
                }
                else
                {
                    MessageBox.Show("Não há dados para gerar o PDF do Livro Diário.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Tabela não encontrada ou está nula.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void OpenPdfForViewing(MemoryStream pdfStream)
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), "LivroDiario.pdf");

            try
            {
                using (FileStream fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                {
                    pdfStream.WriteTo(fileStream);
                }

                Process process = Process.Start(new ProcessStartInfo(tempFilePath)
                {
                    UseShellExecute = true
                });

                process.Exited += (sender, args) =>
                {
                    File.Delete(tempFilePath);
                };

                process.EnableRaisingEvents = true; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao tentar abrir o PDF: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (DS.Tables[tablename].Rows.Count == 0)
            {
                MessageBox.Show("Não há dados no Livro Diário para salvar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Arquivos CSV (*.csv)|*.csv";
                saveFileDialog.Title = "Salvar Livro Diário";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    SalvarLDia(filePath);
                }
            }
        }

        public void SalvarLDia(string filePath)
        {
            DataTable ldiaData = DataStore.LdiaData;

            if (ldiaData == null || ldiaData.Rows.Count == 0)
            {
                MessageBox.Show("Não há dados no Livro Diário para salvar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Escrever cabeçalho
                    writer.WriteLine("Código,Data,Conta,Histórico,Débito,Crédito,Saldo");

                    // Escrever dados
                    foreach (DataRow row in ldiaData.Rows)
                    {
                        string codigo = row["Código"]?.ToString() ?? "";
                        string data = row["Data"] != DBNull.Value ? Convert.ToDateTime(row["Data"]).ToString("dd/MM/yyyy") : "";
                        string conta = row["Conta"]?.ToString() ?? "";
                        string historico = row["Histórico"]?.ToString() ?? "";
                        string debito = row["Débito"] != DBNull.Value ? Convert.ToDecimal(row["Débito"]).ToString("F2", CultureInfo.InvariantCulture) : "";
                        string credito = row["Crédito"] != DBNull.Value ? Convert.ToDecimal(row["Crédito"]).ToString("F2", CultureInfo.InvariantCulture) : "";
                        string saldo = row["Saldo"] != DBNull.Value ? Convert.ToDecimal(row["Saldo"]).ToString("F2", CultureInfo.InvariantCulture) : "";

                        writer.WriteLine($"{codigo},{data},{conta},{historico},{debito},{credito},{saldo}");
                    }

                    MessageBox.Show("Lançamentos do Dia salvos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao exportar o arquivo CSV: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void btn_ImportarLdia_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "CSV files (*.csv)|*.csv";
                ofd.Title = "Importar Livro Diário";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ImportarLDia(ofd.FileName);
                }
            }
        }

        private void ImportarLDia(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line = reader.ReadLine();

                    while ((line = reader.ReadLine()) != null) 
                    {
                        string[] data = line.Split(',');

                        if (data.Length == 7) 
                        {
                            string codigo = data[0];
                            string conta = data[2];

                            bool jaExiste = DS.Tables[tablename].AsEnumerable().Any(row =>
                                row.Field<string>("Código") == codigo && row.Field<string>("Conta") == conta);

                            if (!jaExiste)
                            {
                                DataRow row = DS.Tables[tablename].NewRow();

                                row["Código"] = codigo;
                                row["Data"] = DateTime.ParseExact(data[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                row["Conta"] = conta;
                                row["Histórico"] = data[3];
                                row["Débito"] = string.IsNullOrEmpty(data[4]) ? (object)DBNull.Value : Convert.ToDecimal(data[4], CultureInfo.InvariantCulture);
                                row["Crédito"] = string.IsNullOrEmpty(data[5]) ? (object)DBNull.Value : Convert.ToDecimal(data[5], CultureInfo.InvariantCulture);
                                row["Saldo"] = string.IsNullOrEmpty(data[6]) ? (object)DBNull.Value : Convert.ToDecimal(data[6], CultureInfo.InvariantCulture);

                                DS.Tables[tablename].Rows.Add(row);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Formato incorreto na linha: {line}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    MessageBox.Show("Dados importados com sucesso! Linhas duplicadas foram ignoradas.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao importar o arquivo CSV: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dtp_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}


