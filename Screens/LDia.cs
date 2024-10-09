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
            string connectionString = "SERVER=localhost;DATABASE=bdcontime;UID=root;PASSWORD=projetocontime123;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.Cells["codigo"].Value != null && row.Cells["data"].Value != null &&
                            row.Cells["conta"].Value != null && row.Cells["historico"].Value != null &&
                            row.Cells["debito"].Value != null && row.Cells["credito"].Value != null)
                        {
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = connection;
                            cmd.CommandText = "INSERT INTO livro_diario (codigo, data, conta, historico, debito, credito) VALUES (@codigo, @data, @conta, @historico, @debito, @credito)";

                            string dataString = row.Cells["data"].Value.ToString();
                            DateTime data;
                            if (DateTime.TryParseExact(dataString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out data))
                            {
                                cmd.Parameters.AddWithValue("@codigo", row.Cells["codigo"].Value);
                                cmd.Parameters.AddWithValue("@data", data.ToString("yyyy-MM-dd"));
                                cmd.Parameters.AddWithValue("@conta", row.Cells["conta"].Value);
                                cmd.Parameters.AddWithValue("@historico", row.Cells["historico"].Value);

                                float debito = row.Cells["debito"].Value != DBNull.Value ? Convert.ToSingle(row.Cells["debito"].Value) : 0.0f;
                                float credito = row.Cells["credito"].Value != DBNull.Value ? Convert.ToSingle(row.Cells["credito"].Value) : 0.0f;

                                cmd.Parameters.AddWithValue("@debito", debito);
                                cmd.Parameters.AddWithValue("@credito", credito);

                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                MessageBox.Show($"Data inválida na linha {row.Index + 1}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    MessageBox.Show("Dados gravados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Erro ao gravar dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (InvalidCastException ex)
                {
                    MessageBox.Show($"Erro de conversão de dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

