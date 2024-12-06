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
using System.Diagnostics;

namespace ConTime.Screens
{
    public partial class BPat : UserControl
    {
        Classes.BPat balanco;
        DataSet ds = DataStore.BalancoPatrimonialData ?? new DataSet("BalancoPatrimonial");
        public readonly static string tablename = "balanco_patrimonial";
        DataGridView[] TablesInterface;
        DataGridView[] TablesAtivos;
        DataGridView[] TablesPassivos;
        float[] Ativos = new float[2];
        float TAtivos = 0;
        float[] Passivos = new float[3];
        float TPassivos = 0;

        private DataTable _atvCirculante;
        private DataTable _atvNCirculante;
        private DataTable _psvCirculante;
        private DataTable _psvNCirculante;
        private DataTable _patrimonio;

        public BPat(DataTable atvCirculante, DataTable atvNCirculante, DataTable psvCirculante, DataTable psvNCirculante, DataTable patrimonio)
        {
            _atvCirculante = atvCirculante;
            _atvNCirculante = atvNCirculante;
            _psvCirculante = psvCirculante;
            _psvNCirculante = psvNCirculante;
            _patrimonio = patrimonio;
        }

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

                TablesInterface[table.Key].CellValueChanged += SalvarDadosEmTempoReal;
                TablesInterface[table.Key].CellValueChanged += ContentUpdate;

                // Configurações de estilo conforme a DRE
                TablesInterface[table.Key].DefaultCellStyle.SelectionForeColor = Color.FromArgb(29, 61, 48); // Cor da fonte da célula selecionada
                TablesInterface[table.Key].DefaultCellStyle.SelectionBackColor = Color.White; // Cor de fundo da célula selecionada para branco
                TablesInterface[table.Key].Font = new Font("Yu Gothic UI", 12, FontStyle.Bold);
            }

            DataStore.BalancoPatrimonialData = ds;
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

        private void SalvarDadosEmTempoReal(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv != null)
            {
                AtualizarTotalAtivos();
                AtualizarTotalPassivos();
                SalvarDadosNoDataStore(dgv);
            }
        }

        private void SalvarDadosNoDataStore(DataGridView dgv)
        {
            DataTable dt = ColetarDadosDataGridView(dgv);
            string tableName = dgv.Name;

            if (ds.Tables.Contains(tableName))
            {
                ds.Tables[tableName].Clear();
                foreach (DataRow row in dt.Rows)
                {
                    ds.Tables[tableName].ImportRow(row);
                }
            }
            else
            {
                ds.Tables.Add(dt);
            }

            DataStore.BalancoPatrimonialData = ds; // Atualizar o DataStore com o DataSet atualizado
        }

        private DataTable ColetarDadosDataGridView(DataGridView dgv)
        {
            DataTable dt = new DataTable(dgv.Name);
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                dt.Columns.Add(column.Name);
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataRow dr = dt.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dr[cell.ColumnIndex] = cell.Value ?? DBNull.Value;
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
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


        public void SalvarBalanco()
        {
            string cabecario = tb_Header.Text;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Arquivo CSV (*.csv)|*.csv";
                saveFileDialog.Title = "Salvar Balanço Patrimonial";
                saveFileDialog.DefaultExt = "csv";

                if(SalvarDadosDataGridView == null)
                {
                    MessageBox.Show("Não há dados para salvar o Balanço Patrimonial.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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

        private void LimparTabelas()
        {
            LimparTabela("AtvCirculante");
            LimparTabela("AtvNCirculante");
            LimparTabela("PsvCirculante");
            LimparTabela("PsvNCirculante");
            LimparTabela("Patrimonio");

            TAtivos = 0;
            TPassivos = 0;

            lbl_TAtivos.Text = $"{TAtivos:C}";
            lbl_TPassivos.Text = $"{TPassivos:C}";

            // Atualizar DataStore para refletir as mudanças
            DataStore.BalancoPatrimonialData = ds;
        }

        private void LimparTabela(string nomeTabela)
        {
            if (ds.Tables.Contains(nomeTabela))
            {
                ds.Tables[nomeTabela].Rows.Clear();

                var dgv = TablesInterface.FirstOrDefault(d => d.DataSource == ds.Tables[nomeTabela]);
                if (dgv != null)
                {
                    dgv.Refresh();
                }
            }

            if (nomeTabela == "AtvCirculante" || nomeTabela == "AtvNCirculante")
            {
                TAtivos = 0;
                lbl_TAtivos.Text = $"{TAtivos:C}";
            }
            else if (nomeTabela == "PsvCirculante" || nomeTabela == "PsvNCirculante")
            {
                TPassivos = 0;
                lbl_TPassivos.Text = $"{TPassivos:C}";
            }
        }



        private void LimparPaineis()
        {
            foreach (Control painel in this.Controls)
            {
                if (painel is Panel painelControle)
                {
                    foreach (Control controle in painelControle.Controls)
                    {
                        if (controle is DataGridView dgv)
                        {
                            if (dgv.IsCurrentCellInEditMode)
                            {
                                dgv.EndEdit();
                            }

                            dgv.Rows.Clear();
                        }
                    }
                }
            }

            tb_Header.Clear();

            TAtivos = 0;
            TPassivos = 0;

            lbl_TAtivos.Text = $"{TAtivos:C}";
            lbl_TPassivos.Text = $"{TPassivos:C}";

            Array.Clear(Ativos, 0, Ativos.Length);
            Array.Clear(Passivos, 0, Passivos.Length);

            // Atualizar DataStore para refletir as mudanças
            foreach (DataTable table in ds.Tables)
            {
                table.Rows.Clear();
            }
            DataStore.BalancoPatrimonialData = ds;
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


        public MemoryStream PdfBalanço()
        {
            MemoryStream pdfStream = null;
            string headerText = tb_Header.Text ?? "Cabeçalho";

            // Verificação se as tabelas estão vazias
            if (ds.Tables["AtvCirculante"].Rows.Count == 0 &&
                ds.Tables["AtvNCirculante"].Rows.Count == 0 &&
                ds.Tables["PsvCirculante"].Rows.Count == 0 &&
                ds.Tables["PsvNCirculante"].Rows.Count == 0 &&
                ds.Tables["Patrimonio"].Rows.Count == 0)
            {
                MessageBox.Show("Não há dados para gerar o PDF do Balanço Patrimonial.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null; // Interrompe a execução caso não haja dados
            }

            // Passando cinco DataTable como argumentos para o construtor de BPat
            Classes.BPat balanco = new Classes.BPat(
                ds.Tables["AtvCirculante"],
                ds.Tables["AtvNCirculante"],
                ds.Tables["PsvCirculante"],
                ds.Tables["PsvNCirculante"],
                ds.Tables["Patrimonio"]
            );

            pdfStream = balanco.PdfCreate();

            if (pdfStream != null)
            {
                OpenPdfForViewing(pdfStream);
            }
            else
            {
                MessageBox.Show("Erro ao gerar o PDF do Balanço Patrimonial.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return pdfStream;
        }


        private void OpenPdfForViewing(MemoryStream pdfStream)
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), "Balanço Patrimonial.pdf");

            try
            {
                using (FileStream fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                {
                    pdfStream.WriteTo(fileStream);
                }

                Process.Start(new ProcessStartInfo(tempFilePath)
                {
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao tentar abrir o PDF: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btn_salvar_bpat_Click_1(object sender, EventArgs e)
        {
            SalvarBalanco();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ImportarBalanco();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PdfBalanço();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LimparTabelas();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }
    }
}
