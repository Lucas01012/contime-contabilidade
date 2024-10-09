using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace ConTime.Screens
{
    public partial class Bal : UserControl
    {
        DataSet DS = new DataSet();
        string tablename = "balancete";
        public Bal()
        {
            InitializeComponent();
            DataTable DT = DS.Tables.Add(tablename);
            dgv_bal.DataSource = DS.Tables[tablename];
            DT.Columns.Add("codigo");
            DT.Columns.Add("conta");
            DT.Columns.Add("devedor");
            DT.Columns.Add("credor");
            dgv_bal.Columns["Conta"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            DataRow dr = DS.Tables[0].NewRow();
            if (rb_c.Checked)
                dr["credor"] = string.Format("{0:#.00}", Convert.ToDecimal(tb_saldo.Text));
            else if (rb_d.Checked)
                dr["devedor"] = string.Format("{0:#.00}", Convert.ToDecimal(tb_saldo.Text));
            dr["codigo"] = cb_cod.Text;
            dr["conta"] = tb_Conta.Text;
            DS.Tables[0].Rows.Add(dr);
            DS.Tables[0].AcceptChanges();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (dgv_bal.SelectedRows.Count > 0)
                if (!dgv_bal.SelectedRows[0].IsNewRow)
                    dgv_bal.Rows.RemoveAt(dgv_bal.SelectedRows[0].Index);
                else
                    MessageBox.Show("Não se pode apagar linha vazia!");
        }

        private void GerarPdf(object sender, EventArgs e)
        {
            Classes.Balancete ldia = new(DS.Tables[tablename]);
            ldia.PdfCreate();
        }

        private void btn_salvar_bal_Click(object sender, EventArgs e)
        {
            string connectionString = "SERVER=localhost;DATABASE=bdcontime;UID=root;PASSWORD=projetocontime123;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Conexão com o banco de dados aberta com sucesso!");

                    foreach (DataGridViewRow row in dgv_bal.Rows)
                    {

                        if (row.Cells["codigo"].Value != null &&
                            row.Cells["conta"].Value != null &&
                            (row.Cells["devedor"].Value != null || row.Cells["credor"].Value != null))
                        {
                            string codigo = row.Cells["codigo"].Value.ToString();
                            string conta = row.Cells["conta"].Value.ToString();

                            bool devedorIsValid = float.TryParse(row.Cells["devedor"].Value?.ToString(), out float devedor);
                            bool credorIsValid = float.TryParse(row.Cells["credor"].Value?.ToString(), out float credor);


                            if (!devedorIsValid && !credorIsValid)
                            {
                                MessageBox.Show($"Valores de devedor ou credor inválidos na linha {row.Index + 1}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                continue;
                            }

                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = connection;
                            cmd.CommandText = "INSERT INTO balancete_linha (codigo, conta, devedor, credor) VALUES (@Codigo, @Conta, @Devedor, @Credor)";

                            cmd.Parameters.AddWithValue("@Codigo", codigo);
                            cmd.Parameters.AddWithValue("@Conta", conta);
                            cmd.Parameters.AddWithValue("@Devedor", devedorIsValid ? (object)devedor : DBNull.Value);
                            cmd.Parameters.AddWithValue("@Credor", credorIsValid ? (object)credor : DBNull.Value);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Dados salvos com sucesso!");
                        }
                    }

                    MessageBox.Show("Dados gravados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Erro ao gravar dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocorreu um erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }
    }

    }

