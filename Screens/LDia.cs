using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConTime.Screens
{
    public partial class LDia : UserControl
    {
        DataSet DS = new DataSet();
        string tablename = "livro_diario";
        public LDia()
        {
            InitializeComponent();
            DataTable DT = DS.Tables.Add(tablename);
            dgv.DataSource = DS.Tables[tablename];
            DT.Columns.Add("Data");
            DT.Columns.Add("Código");
            DT.Columns.Add("Conta");
            DT.Columns.Add("Histórico");
            DT.Columns.Add("Débito");
            DT.Columns.Add("Crédito");
            DT.Columns.Add("Saldo");

            dgv.Columns["Código"].Width = 65;
            dgv.Columns["Histórico"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            MudarCorDataGrid();
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            // Verifica se algum campo obrigatório está vazio
            if (string.IsNullOrWhiteSpace(tb_saldo.Text) ||
                string.IsNullOrWhiteSpace(tb_valor.Text) ||
                string.IsNullOrWhiteSpace(cb_cod.Text) ||
                string.IsNullOrWhiteSpace(tb_Conta.Text) ||
                string.IsNullOrWhiteSpace(tb_historico.Text))
            {
                MessageBox.Show("Por favor, preencha todos os campos obrigatórios.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow dr = DS.Tables[0].NewRow();

            if (decimal.TryParse(tb_saldo.Text, out decimal saldo) && decimal.TryParse(tb_valor.Text, out decimal valor))
            {
                decimal valorOriginal = valor;

                if (rb_c.Checked)
                {
                    decimal resultadoCredito = saldo + valor;
                    dr["Crédito"] = string.Format("{0:#.00}", valorOriginal);
                    saldo = resultadoCredito;
                }
                else if (rb_d.Checked)
                {
                    decimal resultadoDebito = saldo - valor;
                    dr["Débito"] = string.Format("{0:#.00}", valorOriginal);
                    saldo = resultadoDebito;
                }

                dr["Data"] = dtp.Text;
                dr["Código"] = cb_cod.Text;
                dr["Conta"] = tb_Conta.Text;
                dr["Histórico"] = tb_historico.Text;
                dr["Saldo"] = string.Format("{0:#.00}", saldo);

                DS.Tables[0].Rows.Add(dr);
                DS.Tables[0].AcceptChanges();

                LimparCampos();
            }
            else
            {
                MessageBox.Show("Por favor, insira valores válidos para o saldo e o valor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            if (DS.Tables[tablename].Rows.Count == 0) // Verifica se há linhas na tabela
            {
                MessageBox.Show("Não há dados para gerar o PDF.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Classes.LDia ldia = new(DS.Tables[tablename]);
            ldia.PdfCreate();
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
                    SalvarLDia(saveFileDialog.FileName);
                }
            }

        }

        private void SalvarLDia(String filePath)
        {

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Codigo,Data,Conta,Histórico,Débito,Crédito, Saldo");

                    foreach (DataRow row in DS.Tables[tablename].Rows)
                    {
                        string codigo = row["Código"].ToString();
                        string data = Convert.ToDateTime(row["Data"]).ToString("dd/MM/yyyy");
                        string conta = row["Conta"].ToString();
                        string historico = row["Histórico"].ToString();
                        string debito = row["Débito"] != DBNull.Value ? Convert.ToDecimal(row["Débito"]).ToString("F2", CultureInfo.InvariantCulture) : "";
                        string credito = row["Crédito"] != DBNull.Value ? Convert.ToDecimal(row["Crédito"]).ToString("F2", CultureInfo.InvariantCulture) : "";
                        String saldo = row["Saldo"] != DBNull.Value ? Convert.ToDecimal(row["Saldo"]).ToString("F2", CultureInfo.InvariantCulture) : "";

                        writer.WriteLine($"{codigo},{data},{conta},{historico},{debito},{credito}, {saldo}");
                    }

                    MessageBox.Show("Dados salvos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    string line = reader.ReadLine(); // Lê a linha do cabeçalho

                    while ((line = reader.ReadLine()) != null) // Lê cada linha subsequente
                    {
                        string[] data = line.Split(',');

                        if (data.Length == 7) // Certifica-se de que todas as colunas estão presentes
                        {
                            string codigo = data[0];
                            string conta = data[2];

                            // Verifica se já existe uma linha com o mesmo Código e Conta
                            bool jaExiste = DS.Tables[tablename].AsEnumerable().Any(row =>
                                row.Field<string>("Código") == codigo && row.Field<string>("Conta") == conta);

                            if (!jaExiste) // Só adiciona se não existir
                            {
                                DataRow row = DS.Tables[tablename].NewRow();

                                row["Código"] = codigo;
                                row["Data"] = DateTime.ParseExact(data[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                row["Conta"] = conta;
                                row["Histórico"] = data[3];
                                row["Débito"] = string.IsNullOrEmpty(data[4]) ? (object)DBNull.Value : Convert.ToDecimal(data[4], CultureInfo.InvariantCulture);
                                row["Crédito"] = string.IsNullOrEmpty(data[5]) ? (object)DBNull.Value : Convert.ToDecimal(data[5], CultureInfo.InvariantCulture);
                                row["Saldo"] = string.IsNullOrEmpty(data[6]) ? (object)DBNull.Value : Convert.ToDecimal(data[6], CultureInfo.InvariantCulture);

                                DS.Tables[tablename].Rows.Add(row); // Adiciona a nova linha
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


        private void MudarCorDataGrid()
        {
            dtp.BackColor = Color.FromArgb(185, 220, 201);
            dtp.ForeColor = Color.Black;

            dgv.BackgroundColor = Color.FromArgb(185, 220, 201);
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(185, 220, 201);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(185, 220, 201);
            dgv.DefaultCellStyle.Font = new Font("Yu Gothic UI", 12, FontStyle.Bold);

            dgv.ColumnHeadersDefaultCellStyle.Font = new Font ("Yu Gothic UI", 12, FontStyle.Bold);
            


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


