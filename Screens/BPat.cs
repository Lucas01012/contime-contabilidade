using System.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConTime.Classes;
using MySql.Data.MySqlClient;

namespace ConTime.Screens
{
    public partial class BPat : UserControl
    {
        Classes.BPat balanco;
        DataSet ds = new();
        public readonly static string tablename = "balanco_patrimonial";
        DataGridView[] TablesInterface;
        DataGridView[] TablesAtivos;
        DataGridView[] TablesPassivos;
        float[] Ativos = new float[2];
        float TAtivos = 0;
        float[] Passivos = new float[3];
        float TPassivos = 0;
        public BPat()
        {
            InitializeComponent();

            TablesInterface = [
                AtvCirculante, AtvNCirculante, PsvCirculante, PsvNCirculante, Patrimonio
            ];
            TablesAtivos = [AtvCirculante, AtvNCirculante];
            TablesPassivos = [PsvCirculante, PsvNCirculante, Patrimonio];
            foreach (KeyValuePair<short, string> table in BPRegistro.rkey)
            {
                TableSetup(table.Value);
                TablesInterface[table.Key].DataSource = ds.Tables[table.Value];

                TablesInterface[table.Key].Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                TablesInterface[table.Key].Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
        }


        private void TableSetup(string name)
        {
            DataTable dt = ds.Tables.Add(name);
            dt.Columns.Add("Conta");
            dt.Columns.Add("Saldo");
        }

        private void PdfCreate(object sender, EventArgs e)
        {
            Classes.BPat bPat = new(ds, tb_Header.Text, TAtivos, TPassivos);
            bPat.PdfCreate();
        }

        private void ContentUpdate(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if (Array.IndexOf(TablesAtivos, dgv) != -1)
            {
                int index = Array.IndexOf(TablesAtivos, dgv);
                float result = 0;
                foreach (DataGridViewRow dr in dgv.Rows)
                {
                    float r = 0;
                    float.TryParse(Convert.ToString(dr.Cells["Saldo"].Value), out r);
                    result += r;
                }
                Ativos[index] = result;
                TAtivos = 0;
                foreach (float a in Ativos)
                    TAtivos += a;
                lbl_TAtivos.Text = $"{TAtivos:C}";
            }
            else if (Array.IndexOf(TablesPassivos, dgv) != -1)
            {
                int index = Array.IndexOf(TablesPassivos, dgv);
                float result = 0;
                foreach (DataGridViewRow dr in dgv.Rows)
                {
                    float r;
                    float.TryParse(Convert.ToString(dr.Cells["Saldo"].Value), out r);
                    result += r;
                }
                Passivos[index] = result;
                TPassivos = 0;
                foreach (float a in Passivos)
                    TPassivos += a;
                lbl_TPassivos.Text = $"{TPassivos:C}";
            }
        }

        private void btn_salvar_bpat_Click(object sender, EventArgs e)
        {
            string connectionString = "SERVER=localhost;DATABASE=bdcontime;UID=root;PASSWORD=projetocontime123;";
            string cabecario = tb_Header.Text;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO balanco_patrimonial (cabecario, total_ativo, total_passivo) VALUES (@Cabecario, @TAtivos, @TPassivos)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        
                        command.Parameters.AddWithValue("@Cabecario",cabecario);
                        command.Parameters.AddWithValue("@TAtivos", TAtivos);
                        command.Parameters.AddWithValue("@TPassivos", TPassivos);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Valores do Balanço Patrimonial salvos com sucesso!");
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erro ao salvar os valores do Balanço Patrimonial no banco de dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
        /*private void test(object sender, EventArgs e)
        {
            DataTable dt = new();
            dt.Columns.Add("id");
            dt.Columns.Add("total_ativo");
            dt.Columns.Add("total_passivo");
            DataRow dr = dt.NewRow();
            dr["id"] = 1;
            dr["total_ativo"] = 50000;
            dr["total_passivo"] = 50000;

            DataTable rotulos = new DataTable();
            rotulos.Columns.Add("id");
            rotulos.Columns.Add("bp_id");
            rotulos.Columns.Add("rkey");
            rotulos.Columns.Add("conta");
            rotulos.Columns.Add("saldo");

            int j = 0;
            for (int i = 0; i < TablesInterface.Length; i++)
            {
                DataGridView dgv = TablesInterface[i];
                foreach(DataGridViewRow row in dgv.Rows)
                {
                    if(!row.IsNewRow)
                    {
                        DataRow rotulo = rotulos.NewRow();
                        rotulo["id"] = j;
                        rotulo["bp_id"] = 1;
                        rotulo["rkey"] = i;
                        foreach (DataGridViewColumn column in dgv.Columns)
                        {
                            rotulo[column.Name.ToLower()] = row.Cells[column.Index].Value;
                        }
                        rotulos.Rows.Add(rotulo);
                        j++;
                    }
                    
                }
            }
            balanco = new Classes.BPat(dr, rotulos);
        }*/
    

