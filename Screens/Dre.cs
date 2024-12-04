using ConTime.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConTime.Screens
{
    public partial class Dre : UserControl
    {
        string tablename = "dre";
        DataSet DS = new DataSet();
        readonly string[] Tables =
        [
            "RBruta",
        "Imposto",
        "Custos",
        "Despesas",
        "OutrasR"
        ];
        Label[] lbl;

        public Dre()
        {
            InitializeComponent();

            DataGridView[] TablesInterface = [RBruta, Imposto, Custos, Despesas, ROutras];
            lbl = [r0, r1, r2, r3, r4, r5, r6, r7];

            panel1.HorizontalScroll.Maximum = 0;
            panel1.AutoScroll = true;
            panel1.VerticalScroll.Visible = false;
            panel1.AutoScroll = true;

            for (int i = 0; i < TablesInterface.Length; i++)
            {
                TableSetup(Tables[i]);
                TablesInterface[i].DataSource = DS.Tables[Tables[i]];
                TablesInterface[i].Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                TablesInterface[i].ScrollBars = ScrollBars.Vertical;
            }

            panel10.Parent = panel1;
        }

        private void painel_Leave(object sender, EventArgs e)
        {
            EnviarDreParaDataStore();
        }

        private void TableSetup(string name)
        {
            DataTable dt = DS.Tables.Add(name);
            dt.Columns.Add("Receita");
            dt.Columns.Add("Valor");
        }

        private void EnviarDreParaDataStore()
        {
            // Limpa os dados existentes na DRE antes de adicionar novos
            foreach (var painel in DataStore.DrePanéisData.Values)
            {
                painel.Clear();  // Limpa os dados de cada painel
            }

            foreach (Control control in this.Controls)
            {
                if (control is Panel painel)
                {
                    foreach (Control subControl in painel.Controls)
                    {
                        if (subControl is TextBox txt)
                        {
                            string receita = txt.Name.Replace(painel.Name, "").Trim();
                            string valor = txt.Text;

                            // Adiciona os dados ao painel correspondente no DataStore
                            DataStore.AdicionarDreData(painel.Name, receita, valor);
                        }
                        else if (subControl is Label lbl)
                        {
                            string receita = lbl.Name.Replace(painel.Name, "").Trim();
                            string valor = lbl.Text;

                            // Adiciona os dados ao painel correspondente no DataStore
                            DataStore.AdicionarDreData(painel.Name, receita, valor);
                        }
                    }
                }
            }

            // Chama para adicionar os resultados calculados após o preenchimento dos dados da DRE
            AdicionarResultadosCalculados();
        }


        private void AdicionarResultadosCalculados()
        {
            // Adiciona os resultados calculados nos painéis da DRE
            for (int i = 0; i < resul.Length; i++)
            {
                string resultadoConta = $"Resultado {i + 1}";
                string resultadoValor = resul[i].ToString("C", CultureInfo.CurrentCulture);

                // Adiciona o resultado ao painel "Resultado de Lucro do Exercício"
                DataStore.AdicionarDreData("Resultado de Lucro do Exercício", resultadoConta, resultadoValor);
            }
        }


        private void btnDrop_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Control header = btn.Parent;
            Control guia = btn.Parent?.Parent;
            Control mainbody = btn.Parent?.Parent?.Parent;
            int minsize = guia.Height - (header.Height + 10);
            int maxsize = guia.Height + 150;

            if (btn.Text == "˄")
            {
                guia.Height -= minsize;
                btn.Text = "˅";

                foreach (DataGridView dg in guia.Controls.OfType<DataGridView>())
                {
                    dg.Visible = false;
                }

                Reposition(mainbody, guia.TabIndex, -minsize);
            }
            else if (btn.Text == "˅")
            {
                guia.Height += maxsize;
                btn.Text = "˄";

                foreach (DataGridView dg in guia.Controls.OfType<DataGridView>())
                {
                    dg.Visible = true;
                }

                Reposition(mainbody, guia.TabIndex, maxsize);
            }

            CheckScrollVisibility();
        }

        private void Reposition(Control parent, int tabIndex, int p)
        {
            foreach (Panel panel in parent.Controls.OfType<Panel>())
            {
                if (tabIndex < panel.TabIndex)
                {
                    panel.Top += p;
                }
            }
        }

        private void CheckScrollVisibility()
        {
            int totalHeight = 0;

            foreach (Control control in this.panel1.Controls)
            {
                if (control is Panel guia)
                {
                    totalHeight += guia.Visible ? guia.Height : 0;
                }
                else
                {
                    totalHeight += control.Height;
                }
            }

            if (totalHeight > this.Height)
            {
                panel1.VerticalScroll.Visible = true; // Habilita o scroll
            }
            else
            {
                panel1.VerticalScroll.Visible = false; // Desabilita o scroll
            }
        }
    


    private float[] resul = new float[8];
        private void ContentUpdate(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            Control guia = dgv.Parent;
            foreach (Panel panel in guia.Controls.OfType<Panel>())
            {
                foreach (Label label in panel.Controls.OfType<Label>())
                {
                    if (label.Tag == "Resultado")
                    {
                        float result = 0;
                        for (int i = 0; i < dgv.Rows.Count; i++)
                        {
                            float r = 0;
                            if (dgv[1, i].Value != null)
                                float.TryParse((dgv[1, i].Value.ToString()), out r);
                            result += r;
                        }
                        resul[label.TabIndex - 1] = result;
                        resulchange();
                    }
                }
            }
            EnviarDreParaDataStore();
        }

        private void resulchange()
        {
            resul[2] = resul[0] - resul[1];
            resul[4] = resul[2] - resul[3];
            resul[7] = resul[4] - resul[5] + resul[6];
            for (int i = 0; i < resul.Length; i++)
            {
                lbl[i].Text = $"{resul[i]:C}";
            }
            EnviarDreParaDataStore();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void CreatePdf(object sender, EventArgs e)
        {
            GerarPdf();
        }

        public MemoryStream GerarPdf()
        {
            Classes.Dre dre = new(DS, resul[2], resul[4], resul[7]);

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
            EnviarDreParaDataStore();
        }


        private void OpenPdfForViewing(MemoryStream pdfStream)
        {
            if (pdfStream == null || pdfStream.Length == 0)
            {
                throw new ArgumentNullException(nameof(pdfStream), "O stream de PDF está vazio ou é nulo.");
            }

            string tempFilePath = Path.Combine(Path.GetTempPath(), "tempPDFView.pdf");

            try
            {
                using (FileStream fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                {
                    pdfStream.Position = 0;
                    pdfStream.CopyTo(fileStream);
                }

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

        private void btn_salvar_dre_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Arquivo CSV (*.csv)|*.csv";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SalvarDre();
                }
            }
        }

        public void SalvarDre()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Arquivo CSV (*.csv)|*.csv";
                saveFileDialog.Title = "Salvar DRE";
                saveFileDialog.DefaultExt = "csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        using (StreamWriter writer = new StreamWriter(filePath))
                        {
                            foreach (string tableName in Tables)
                            {
                                DataTable dt = DS.Tables[tableName];
                                writer.WriteLine($"Tabela: {tableName}");

                                writer.WriteLine("Receita,Valor");

                                foreach (DataRow row in dt.Rows)
                                {
                                    string receita = row["Receita"].ToString();
                                    string valor = row["Valor"].ToString();
                                    writer.WriteLine($"{receita},{valor}");
                                }

                                foreach (Control ctrl in this.Controls)
                                {
                                    if (ctrl is TextBox && ctrl.Name.StartsWith(tableName))
                                    {
                                        writer.WriteLine($"{ctrl.Name},{ctrl.Text}");
                                    }
                                }

                                writer.WriteLine();
                            }

                            writer.WriteLine($"Resultado Receita Líquida,{lbl[4].Text}");
                            writer.WriteLine($"Resultado Lucro Operacional,{lbl[5].Text}");
                            writer.WriteLine($"Resultado Lucro do Exercício,{lbl[7].Text}");

                            MessageBox.Show("Dados salvos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao exportar o arquivo CSV: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ImportarDre(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    string currentTable = string.Empty;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("Tabela:"))
                        {
                            currentTable = line.Replace("Tabela: ", "").Trim();
                        }
                        else if (!string.IsNullOrEmpty(currentTable) && line.Contains(","))
                        {
                            string[] values = line.Split(',');

                            if (values.Length == 2)
                            {
                                if (DS.Tables[currentTable] == null)
                                {
                                    DS.Tables.Add(currentTable);
                                    DataTable dt = DS.Tables[currentTable];
                                    dt.Columns.Add("Receita");
                                    dt.Columns.Add("Valor");
                                }

                                DataRow row = DS.Tables[currentTable].NewRow();
                                row["Receita"] = values[0].Trim();
                                row["Valor"] = values[1].Trim();
                                DS.Tables[currentTable].Rows.Add(row);
                            }
                            else
                            {
                                string controlName = values[0].Trim();
                                string controlValue = values[1].Trim();

                                foreach (Control ctrl in this.Controls)
                                {
                                    if (ctrl.Name == controlName)
                                    {
                                        if (ctrl is TextBox)
                                        {
                                            ((TextBox)ctrl).Text = controlValue;
                                        }
                                     
                                    }
                                }
                            }
                        }
                    }

                    AtualizarPainéis();

                    AtualizarResultados();

                    MessageBox.Show("Dados importados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao importar o arquivo CSV: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AtualizarPainéis()
        {
            foreach (string tableName in Tables)
            {
                DataTable dt = DS.Tables[tableName];
                DataGridView dgv = null;

                switch (tableName)
                {
                    case "RBruta":
                        dgv = RBruta;
                        break;
                    case "Imposto":
                        dgv = Imposto;
                        break;
                    case "Custos":
                        dgv = Custos;
                        break;
                    case "Despesas":
                        dgv = Despesas;
                        break;
                    case "OutrasR":
                        dgv = ROutras;
                        break;
                }

                if (dgv != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dgv.Rows.Clear();
                    }

                    foreach (DataRow row in dt.Rows)
                    {
                        dgv.Rows.Add(row["Receita"], row["Valor"]);
                    }

                    AtualizarResultados();
                }
            }
        }

        private void AtualizarResultados()
        {
            resul[2] = resul[0] - resul[1];
            resul[4] = resul[2] - resul[3];
            resul[7] = resul[4] - resul[5] + resul[6];

            lbl[4].Text = $"{resul[2]:C}";
            lbl[5].Text = $"{resul[3]:C}";
            lbl[7].Text = $"{resul[7]:C}";
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivo CSV (*.csv)|*.csv";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ImportarDre(openFileDialog.FileName);
                }
            }
        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Dre_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
  
