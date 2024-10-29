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
            DT.Columns.Add("codigo");
            DT.Columns.Add("data");
            DT.Columns.Add("conta");
            DT.Columns.Add("historico");
            DT.Columns.Add("debito");
            DT.Columns.Add("credito");

            dgv.Columns["codigo"].Width = 65;
            dgv.Columns["historico"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            DataRow dr = DS.Tables[0].NewRow();
            if (rb_c.Checked)
                dr["credito"] = string.Format("{0:#.00}", Convert.ToDecimal(tb_saldo.Text));
            else if (rb_d.Checked)
                dr["debito"] = string.Format("{0:#.00}", Convert.ToDecimal(tb_saldo.Text));
            dr["data"] = dtp.Text;
            dr["codigo"] = cb_cod.Text;
            dr["conta"] = tb_Conta.Text;
            dr["historico"] = tb_historico.Text;

            DS.Tables[0].Rows.Add(dr);
            DS.Tables[0].AcceptChanges();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0)
                if (!dgv.SelectedRows[0].IsNewRow)
                    dgv.Rows.RemoveAt(dgv.SelectedRows[0].Index);
        }

        private void GerarPdf(object sender, EventArgs e)
        {
            Classes.LDia ldia = new(DS.Tables[tablename]);
            ldia.PdfCreate();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.Title = "Salvar Livro Diário";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    SalvarLDia(sfd.FileName);
                }
            }

        }

        private void SalvarLDia(String filePath)
        {

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("codigo,data,conta,historico,debito,credito");

                    foreach (DataRow row in DS.Tables[tablename].Rows)
                    {
                        string codigo = row["codigo"].ToString();
                        string data = Convert.ToDateTime(row["data"]).ToString("dd/MM/yyyy");
                        string conta = row["conta"].ToString();
                        string historico = row["historico"].ToString();
                        string debito = row["debito"] != DBNull.Value ? Convert.ToDecimal(row["debito"]).ToString("F2", CultureInfo.InvariantCulture) : "";
                        string credito = row["credito"] != DBNull.Value ? Convert.ToDecimal(row["credito"]).ToString("F2", CultureInfo.InvariantCulture) : "";

                        writer.WriteLine($"{codigo},{data},{conta},{historico},{debito},{credito}");
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

        private void ImportarLDia(string filePath) {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Limpar o DataTable antes de importar novos dados
                    DS.Tables[tablename].Clear();

                    // Ignorar a primeira linha (Cabeçalho)
                    string line = reader.ReadLine();

                    // Ler cada linha do arquivo CSV
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] data = line.Split(',');

                        if (data.Length == 6) // Verifica se a linha tem o número correto de colunas
                        {
                            DataRow row = DS.Tables[tablename].NewRow();
                            row["codigo"] = data[0];

                            // Formatação da data
                            row["data"] = DateTime.ParseExact(data[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);

                            row["conta"] = data[2];
                            row["historico"] = data[3];

                            // Verifica se os campos de débito e crédito não estão vazios antes de converter
                            row["debito"] = string.IsNullOrEmpty(data[4]) ? (object)DBNull.Value : Convert.ToDecimal(data[4], CultureInfo.InvariantCulture);
                            row["credito"] = string.IsNullOrEmpty(data[5]) ? (object)DBNull.Value : Convert.ToDecimal(data[5], CultureInfo.InvariantCulture);

                            // Adicionar a linha ao DataTable
                            DS.Tables[tablename].Rows.Add(row);
                        }
                        else
                        {
                            MessageBox.Show($"Formato incorreto na linha: {line}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    MessageBox.Show("Dados importados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao importar o arquivo CSV: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


