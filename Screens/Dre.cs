using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
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

            // Certifique-se de que o panel10 está dentro do panel1 e não fora dele
            panel10.Parent = panel1;
        }

        private void TableSetup(string name)
        {
            DataTable dt = DS.Tables.Add(name);
            dt.Columns.Add("Receita");
            dt.Columns.Add("Valor");
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

            // Chamar a função para verificar se o conteúdo ultrapassou o limite de altura
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

            // Calcular a altura total, considerando todos os controles (expansíveis e não expansíveis)
            foreach (Control control in this.panel1.Controls)
            {
                if (control is Panel guia)
                {
                    // Considera a altura dos painéis visíveis (expansíveis)
                    totalHeight += guia.Visible ? guia.Height : 0;
                }
                else
                {
                    // Para outros controles não expansíveis, sempre somar a altura
                    totalHeight += control.Height;
                }
            }

            // Verificar se a altura total dos painéis ultrapassou o limite da tela
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
            //MessageBox.Show($"Column: {e.ColumnIndex}. Row: {e.RowIndex}");
            //MessageBox.Show($"Value: {dgv[e.ColumnIndex,e.RowIndex].Value}");
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
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void CreatePdf(object sender, EventArgs e)
        {
            Classes.Dre dre = new(DS, resul[2], resul[4], resul[7]);
            dre.PdfCreate();
        }

        private void btn_salvar_dre_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Arquivo CSV (*.csv)|*.csv";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SalvarDre(saveFileDialog.FileName);
                }
            }
        }

        private void SalvarDre(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (string tableName in Tables)
                    {
                        DataTable dt = DS.Tables[tableName];
                        writer.WriteLine($"Tabela: {tableName}"); // Título da tabela no CSV

                        // Escreve o cabeçalho
                        writer.WriteLine("Receita,Valor");

                        // Escreve os dados do painel
                        foreach (DataRow row in dt.Rows)
                        {
                            string receita = row["Receita"].ToString();
                            string valor = row["Valor"].ToString();
                            writer.WriteLine($"{receita},{valor}");
                        }

                        // Escreve os inputs dos controles (se houver)
                        foreach (Control ctrl in this.Controls)
                        {
                            if (ctrl is TextBox && ctrl.Name.StartsWith(tableName))  // Exemplo: TextBox para cada painel
                            {
                                writer.WriteLine($"{ctrl.Name},{ctrl.Text}");
                            }
                        }

                        // Linha em branco entre as tabelas
                        writer.WriteLine();
                    }

                    // Agora, salva os valores dos resultados calculados no final
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

                            if (values.Length == 2) // Lê dados de Receita e Valor
                            {
                                // Verificar se a tabela já foi criada antes de adicionar a linha
                                if (DS.Tables[currentTable] == null)
                                {
                                    DS.Tables.Add(currentTable); // Adicionar a tabela se não existir
                                    DataTable dt = DS.Tables[currentTable];
                                    dt.Columns.Add("Receita");
                                    dt.Columns.Add("Valor");
                                }

                                // Adicionar dados à tabela existente
                                DataRow row = DS.Tables[currentTable].NewRow();
                                row["Receita"] = values[0].Trim();
                                row["Valor"] = values[1].Trim();
                                DS.Tables[currentTable].Rows.Add(row);
                            }
                            else
                            {
                                string controlName = values[0].Trim();
                                string controlValue = values[1].Trim();

                                // Preenche os controles com os dados importados (inputs de cada painel)
                                foreach (Control ctrl in this.Controls)
                                {
                                    if (ctrl.Name == controlName)
                                    {
                                        if (ctrl is TextBox)
                                        {
                                            ((TextBox)ctrl).Text = controlValue;
                                        }
                                        // Adicione outras verificações conforme necessário (ComboBox, etc.)
                                    }
                                }
                            }
                        }
                    }

                    // Após a importação, forçar atualização dos dados nos painéis
                    AtualizarPainéis();  // Atualiza os valores nos painéis

                    // Atualiza os totais e resultados calculados
                    AtualizarResultados();  // Atualizar os totais da DRE

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

                // Encontrar o DataGridView correspondente ao painel
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
                    // Limpar apenas se houver linhas a serem removidas
                    if (dt.Rows.Count > 0)
                    {
                        dgv.Rows.Clear();
                    }

                    // Preencher o DataGridView com os dados da tabela
                    foreach (DataRow row in dt.Rows)
                    {
                        dgv.Rows.Add(row["Receita"], row["Valor"]);
                    }

                    // Atualizar os totais após a importação
                    AtualizarResultados();
                }
            }
        }

        private void AtualizarResultados()
        {
            // Calcular os totais de cada seção da DRE, com base nos valores importados.
            resul[2] = resul[0] - resul[1];  // Receita Líquida = Receita Bruta - Impostos
            resul[4] = resul[2] - resul[3];  // Lucro Operacional Bruto = Receita Líquida - Custos
            resul[7] = resul[4] - resul[5] + resul[6];  // Resultado de Lucro do Exercício = Lucro Operacional Bruto - Despesas + Outras Receitas

            // Atualizar os labels com os novos valores
            lbl[4].Text = $"{resul[2]:C}";  // Receita Líquida
            lbl[5].Text = $"{resul[3]:C}";  // Lucro Operacional Bruto
            lbl[7].Text = $"{resul[7]:C}";  // Resultado de Lucro do Exercício
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
  
