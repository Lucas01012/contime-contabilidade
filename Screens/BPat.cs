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
using System.Xml.Linq;

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
            InitializeDataSet();

            TablesInterface = new DataGridView[]
            {
                AtvCirculante, AtvNCirculante, PsvCirculante, PsvNCirculante, Patrimonio
            };
            TablesAtivos = new DataGridView[] { AtvCirculante, AtvNCirculante };
            TablesPassivos = new DataGridView[] { PsvCirculante, PsvNCirculante, Patrimonio };

            foreach (KeyValuePair<short, string> table in BPRegistro.rkey)
            {
                TableSetup(table.Value);
                TablesInterface[table.Key].DataSource = ds.Tables[table.Value];

                TablesInterface[table.Key].Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                TablesInterface[table.Key].Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                TablesInterface[table.Key].AllowUserToAddRows = true;
            }
        }

        private void InitializeDataSet()
        {
            foreach (KeyValuePair<short, string> table in BPRegistro.rkey)
            {
                TableSetup(table.Value);
            }
        }

        private void TableSetup(string name)
        {
            if (!ds.Tables.Contains(name))
            {
                DataTable dt = ds.Tables.Add(name);
                dt.Columns.Add("Conta");
                dt.Columns.Add("Saldo");
            }
            else
            {
                ds.Tables[name].Rows.Clear();
            }
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
                Ativos[index] = CalcularTotalSaldo(dgv);
                AtualizarTotalAtivos();
            }
            else if (Array.IndexOf(TablesPassivos, dgv) != -1)
            {
                int index = Array.IndexOf(TablesPassivos, dgv);
                Passivos[index] = CalcularTotalSaldo(dgv);
                AtualizarTotalPassivos();
            }
        }

        private float CalcularTotalSaldo(DataGridView dgv)
        {
            float result = 0;
            foreach (DataGridViewRow dr in dgv.Rows)
            {
                if (!dr.IsNewRow)
                {
                    float.TryParse(Convert.ToString(dr.Cells["Saldo"].Value), out float r);
                    result += r;
                }
            }
            return result;
        }

        private void AtualizarTotalAtivos()
        {
            TAtivos = Ativos.Sum();
            lbl_TAtivos.Text = $"{TAtivos:C}";
        }

        private void AtualizarTotalPassivos()
        {
            TPassivos = Passivos.Sum();
            lbl_TPassivos.Text = $"{TPassivos:C}";
        }

        private void btn_salvar_bpat_Click(object sender, EventArgs e)
        {
            SalvarBalanco();
        }


        private void SalvarBalanco()
        {
            string cabecario = tb_Header.Text;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Arquivo CSV (*.csv)|*.csv";
                saveFileDialog.Title = "Salvar Balanço Patrimonial";
                saveFileDialog.DefaultExt = "csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string caminhoArquivo = saveFileDialog.FileName;

                    try
                    {
                        using (StreamWriter sw = new StreamWriter(caminhoArquivo))
                        {
                            sw.WriteLine(cabecario);

                            SalvarDadosDataGridView(sw, "Ativos Circulantes", AtvCirculante);
                            SalvarDadosDataGridView(sw, "Ativos Não Circulantes", AtvNCirculante);
                            SalvarDadosDataGridView(sw, "Passivos Circulantes", PsvCirculante);
                            SalvarDadosDataGridView(sw, "Passivos Não Circulantes", PsvNCirculante);
                            SalvarDadosDataGridView(sw, "Patrimônio Líquido", Patrimonio);

                            sw.WriteLine();
                            sw.WriteLine($"TotalAtivo,{TAtivos}");
                            sw.WriteLine($"TotalPassivo,{TPassivos}");
                        }
                        MessageBox.Show("Balanço Patrimonial salvo com sucesso no arquivo CSV!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao salvar o Balanço Patrimonial no arquivo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



        private void SalvarDadosDataGridView(StreamWriter sw, string nomeCategoria, DataGridView dgv)
        {
            sw.WriteLine(nomeCategoria);
            sw.WriteLine("Conta,Saldo");
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow && row.Cells[0].Value != null && row.Cells[1].Value != null)
                {
                    string conta = row.Cells[0].Value?.ToString() ?? string.Empty;
                    string saldo = row.Cells[1].Value?.ToString() ?? "0";
                    sw.WriteLine($"{conta},{saldo}");
                }
            }
            sw.WriteLine();
        }


        private void btn_ImpBpat_Click(object sender, EventArgs e)
        {
            ImportarBalanco();
        }

        private void LimparTabelas()
        {
            LimparTabela("AtvCirculante");
            LimparTabela("AtvNCirculante");
            LimparTabela("PsvCirculante");
            LimparTabela("PsvNCirculante");
            LimparTabela("Patrimonio");
        }

        private void LimparTabela(string nomeTabela)
        {
            if (ds.Tables.Contains(nomeTabela))
            {
                ds.Tables[nomeTabela].Rows.Clear();
            }
        }

        private void ImportarBalanco()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivo CSV (*.csv)|*.csv";
                openFileDialog.Title = "Importar Balanço Patrimonial";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string caminhoArquivo = openFileDialog.FileName;
                    StreamReader sr = null;

                    try
                    {
                        sr = new StreamReader(caminhoArquivo);
                        tb_Header.Text = sr.ReadLine();

                        LimparTabelas();

                        LerDadosDataTable(sr, ds.Tables["AtvCirculante"], "Ativos Circulantes");
                        LerDadosDataTable(sr, ds.Tables["AtvNCirculante"], "Ativos Não Circulantes");
                        LerDadosDataTable(sr, ds.Tables["PsvCirculante"], "Passivos Circulantes");
                        LerDadosDataTable(sr, ds.Tables["PsvNCirculante"], "Passivos Não Circulantes");
                        LerDadosDataTable(sr, ds.Tables["Patrimonio"], "Patrimônio Líquido");

                        TAtivos = LerTotal(sr, "TotalAtivo");
                        TPassivos = LerTotal(sr, "TotalPassivo");

                        lbl_TAtivos.Text = $"{TAtivos:C}";
                        lbl_TPassivos.Text = $"{TPassivos:C}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao importar o Balanço Patrimonial: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (sr != null) sr.Close();
                    }
                }
            }
        }



        private float LerTotal(StreamReader sr, string nomeTotal)
        {
            string linha;
            while ((linha = sr.ReadLine()) != null)
            {
                if (linha.StartsWith(nomeTotal))
                {
                    string[] partes = linha.Split(',');
                    if (partes.Length == 2 && float.TryParse(partes[1], out float valor))
                    {
                        return valor;
                    }
                }
            }
            return 0;
        }


        private void LerDadosDataTable(StreamReader sr, DataTable dt, string nomeCategoria)
        {
            if (dt == null)
            {
                throw new ArgumentNullException(nameof(dt), "A DataTable não pode ser nula.");
            }

            string linha;
            while ((linha = sr.ReadLine()) != null)
            {
                if (linha == nomeCategoria) break;
            }

            sr.ReadLine();

            while ((linha = sr.ReadLine()) != null)
            {
                if (linha.Trim().Length == 0) break;
                string[] partes = linha.Split(',');
                if (partes.Length >= 2)
                {
                    DataRow row = dt.NewRow();
                    row["Conta"] = partes[0];
                    row["Saldo"] = partes[1];
                    dt.Rows.Add(row);
                }
            }
        }

        private void AtvCirculante_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PsvCirculante_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AtvNCirculante_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Patrimonio_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void splitContainer7_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}