using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ConTime.Screens
{
    public partial class Razonete : UserControl
    {
        private List<Razo> razos = new List<Razo>();
        private int childs = 0;
     // Razonete razonete = new Razonete();

        public Razonete()
        {
            InitializeComponent();
            this.HorizontalScroll.Maximum = 0;
            this.AutoScroll = false;
            this.VerticalScroll.Visible = false;
        }

        private void AddScreen(UserControl uc, Panel pnl)
        {
            if (uc == null || pnl == null) return; 

            uc.Dock = DockStyle.Fill;
            pnl.Controls.Clear();
            pnl.Controls.Add(uc);
            uc.BringToFront();
        }

        private void btn_addUC_Click(object sender, EventArgs e)
        {
            int uc_width = this.Width;
            int uc_height = this.Height;
            int childMaxW = uc_width / pnl_razo.Width;
            int padW = uc_width % pnl_razo.Width / childMaxW;

            childs++;
            RoundedPanel pnl = new RoundedPanel
            {
                Size = pnl_razo.Size,
                BackColor = pnl_razo.BackColor,
                Parent = this.pnl_razonetes,
            };

            pnl.Left = 2 + ((pnl.Width + padW) * (childs % (uc_width / (pnl.Width + padW))));
            pnl.Top = 2 + ((pnl.Height + 5) * (childs / (uc_width / (pnl.Width + padW))));

            Razo uc = new Razo($"Cabeçalho {childs}");
            uc.SetParentRazonete(this);
            uc.PositionIndex = childs;
            uc.IsDeleted = false;
            razos.Add(uc);
            AddScreen(uc, pnl);
            RepositionPanels();
        }
        public void RepositionPanels()
        {
            int uc_width = this.Width;
            int uc_height = this.Height;
            int childMaxW = uc_width / pnl_razo.Width;
            int padW = uc_width % pnl_razo.Width / childMaxW;
            int y = 0; // Iniciar a posição Y

            foreach (Control control in this.pnl_razonetes.Controls)
            {
                if (control is RoundedPanel panel)
                {
                    panel.Left = 2 + ((panel.Width + padW) * (y % (uc_width / (panel.Width + padW))));
                    panel.Top = 2 + ((panel.Height + 5) * (y / (uc_width / (panel.Width + padW))));
                    y++;
                }
            }
        }



                private void Razonete_SizeChanged(object sender, EventArgs e)
        {
            if (childs != 0 && this.ParentForm.WindowState != FormWindowState.Minimized)
            {
                int uc_width = this.Width;
                int childTemp = 0;
                int childMaxW = uc_width / pnl_razo.Width;
                int padW = uc_width % pnl_razo.Width / childMaxW;

                foreach (var pnl in this.pnl_razonetes.Controls.OfType<Panel>())
                {
                    pnl.Left = 2 + ((pnl.Width + padW) * (childTemp % (uc_width / (pnl.Width + padW))));
                    pnl.Top = 2 + ((pnl.Height + 5) * (childTemp / (uc_width / (pnl.Width + padW))));
                    childTemp++;
                }
            }
        }
        public void RemovePanel(RoundedPanel panel)
        {
            // Verifica se o painel é nulo e se o painel pai contém este painel
            if (panel != null && panel.Parent is Panel parentPanel)
            {
                // Remove o painel do pai
                parentPanel.Controls.Remove(panel);
                panel.Dispose(); // Libera os recursos do painel

                // Altera a cor de fundo do painel pai (opcional)
                parentPanel.BackColor = Color.FromArgb(185, 220, 200);

                // Reorganiza os painéis restantes
                RepositionPanels();
            }
        }
        public void RemoveRazo(Razo razo) {
            if (razo != null)
            {
                razo.IsDeleted = true;
                RoundedPanel parentPanel = razo.Parent as RoundedPanel;

                if (parentPanel != null) {
                    RemovePanel(parentPanel);
                }
                razos.Remove(razo);

                RepositionPanels();
                {
                    
                }

            }
        }

        private void CreatePdf(object sender, EventArgs e)
        {
            Classes.Razonetes razonete = new();
            foreach (Razo razo in razos)
            {
                razonete.AddRazonete(razo.TabelaRazonete());
            }
            razonete.PdfCreate();
        }

        private void btn_salvar_razo_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Salvar Razonetes"
            })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SalvarRazos(saveFileDialog.FileName);
                }
            }
        }



        private void SalvarRazos(string filePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    foreach (var r in razos)
                    {
                        Classes.Razo razoTabela = r.TabelaRazonete();
                        if (razoTabela == null) continue;

                        sw.WriteLine($"Cabeçalho: {razoTabela.Header}");
                        sw.WriteLine("Débito,Crédito");

                        foreach (var registro in razoTabela.registros)
                        {
                            sw.WriteLine($"{registro.debito:F2},{registro.credito:F2}");
                        }

                        float totalDebito = razoTabela.registros.Sum(r => r.debito);
                        float totalCredito = razoTabela.registros.Sum(r => r.credito);

                        sw.WriteLine($"Total Débito: {totalDebito:F2}");
                        sw.WriteLine($"Total Crédito: {totalCredito:F2}");
                        sw.WriteLine(); 
                    }
                }

                MessageBox.Show("Razonetes salvos com sucesso!", "Salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AtualizaInterfaceRazonetes()
        {
            this.pnl_razonetes.Controls.Clear();

            foreach (var razo in razos)
            {
                RoundedPanel pnl = new RoundedPanel
                {
                    Size = pnl_razo.Size,
                    BackColor = pnl_razo.BackColor,
                    Parent = this.pnl_razonetes
                };

                AddScreen(razo, pnl);
            }
        }


        private void ImportarRazos(string caminhoDoArquivo)
        {
            using (var reader = new StreamReader(caminhoDoArquivo))
            {
                string linha;
                Razo razo = null;

                while ((linha = reader.ReadLine()) != null)
                {
                    if (linha.StartsWith("Cabeçalho:"))
                    {
                        string cabecalhoAtual = linha.Substring("Cabeçalho:".Length).Trim();
                        razo = new Razo(cabecalhoAtual);
                    }
                    else if (linha.StartsWith("Débito,Crédito"))
                    {
                        continue;
                    }
                    else if (linha.StartsWith("Total Débito:") || linha.StartsWith("Total Crédito:"))
                    {
                        ProcessarTotal(linha, razo);
                    }
                    else
                    {
                        ProcessarRegistro(linha, razo);
                    }
                }
            }
        }

        private void ProcessarTotal(string linha, Razo razo)
        {
            if (razo == null) return;

            float total = ExtractValue(linha);
            if (linha.StartsWith("Total Débito:"))
            {
                razo.AdicionarTotalDebito(total);
            }
            else if (linha.StartsWith("Total Crédito:"))
            {
                razo.AdicionarTotalCredito(total);
            }
        }

        private void ProcessarRegistro(string linha, Razo razo)
        {
            if (razo == null) return;

            var valores = linha.Split(',');
            if (valores.Length >= 2 &&
                float.TryParse(valores[0], out float debito) &&
                float.TryParse(valores[1], out float credito))
            {
                razo.AddRegistro(debito, credito); // Adiciona o registro ao Razo
            }
        }

        private float ExtractValue(string linha)
        {
            var partes = linha.Split(':');
            return partes.Length == 2 && float.TryParse(partes[1].Trim(), out float valor) ? valor : 0f;
        }

        private void btn_ImportarRazo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Importar Razonetes"
            })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var razo in razos)
                    {
                        razo.ImportarRegistros(openFileDialog.FileName);
                    }
                }
            }
        }

    }
}