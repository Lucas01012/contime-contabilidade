using ConTime.Classes;
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
        public List<Razo> razos = new List<Razo>();
        private int childs = 0;
        Razo _currentRazo;
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

        public void SetCurrentRazo(Razo razo)
        {
            _currentRazo = razo ?? throw new ArgumentNullException(nameof(razo));
        }

        public void btn_addUC_Click(object sender, EventArgs e)
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
            SetCurrentRazo(uc);
            RepositionPanels();
        }
        public void RepositionPanels()
        {
            int uc_width = this.Width;
            int uc_height = this.Height;
            int childMaxW = uc_width / pnl_razo.Width;
            int padW = uc_width % pnl_razo.Width / childMaxW;
            int y = 0; 

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
            if (panel != null && panel.Parent is Panel parentPanel)
            {
                parentPanel.Controls.Remove(panel);
                panel.Dispose();
                parentPanel.BackColor = Color.FromArgb(185, 220, 200);

                RepositionPanels();
            }
        }
        public void RemoveRazo(Razo razo)
        {
            if (razo != null)
            {
                razo.IsDeleted = true;
                RoundedPanel parentPanel = razo.Parent as RoundedPanel;

                if (parentPanel != null)
                {
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
        private void SalvarRazos(string filePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    if (razos.Count == 0)
                    {
                        MessageBox.Show("Nenhum Razo criado para salvar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    foreach (var razo in razos)
                    {
                        string cabecalho = razo.GetCabecalho();

                        if (string.IsNullOrWhiteSpace(cabecalho))
                        {
                            cabecalho = "Cabeçalho Padrão"; 
                        }

                        sw.WriteLine($"Cabeçalho: {cabecalho}"); 
                        sw.WriteLine("Débito;Crédito"); 

                        decimal totalDebito = 0m; 
                        decimal totalCredito = 0m; 

                        foreach (var registro in razo.TabelaRazonete().registros)
                        {
                            decimal debitoDecimal = Convert.ToDecimal(registro.debito); 
                            decimal creditoDecimal = Convert.ToDecimal(registro.credito); 

                            sw.WriteLine($"{debitoDecimal:F2};{creditoDecimal:F2}"); 
                            totalDebito += debitoDecimal;
                            totalCredito += creditoDecimal; 
                        }

                        // Salva os totais
                        sw.WriteLine($"Total;{totalDebito:F2};{totalCredito:F2}"); 
                    }

                    MessageBox.Show("Razonetes salvos com sucesso!", "Salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ImportarRazos(string filePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    string linhaCabecalho;
                    decimal totalDebito = 0;
                    decimal totalCredito = 0;

                    while ((linhaCabecalho = sr.ReadLine()) != null)
                    {
                        if (linhaCabecalho.StartsWith("Cabeçalho:"))
                        {
                            btn_addUC_Click(this, EventArgs.Empty);
                            _currentRazo = razos.LastOrDefault(); 

                            // Define o cabeçalho do Razo
                            string cabecalhoTexto = linhaCabecalho.Replace("Cabeçalho: ", "").Trim();
                            _currentRazo.SetCabecalho(cabecalhoTexto);
                            MessageBox.Show($"Cabeçalho importado: '{cabecalhoTexto}'", "Cabeçalho", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            sr.ReadLine();
                            continue;
                        }

                        if (linhaCabecalho.StartsWith("Total:"))
                        {
                            
                            var totais = linhaCabecalho.Split(';');
                            if (totais.Length == 3) 
                            {
                                decimal.TryParse(totais[1].Trim(), out totalDebito);
                                decimal.TryParse(totais[2].Trim(), out totalCredito);
                            }
                            continue; 
                        }

                        var valores = linhaCabecalho.Split(';'); 

                        if (valores.Length == 2 &&
                            decimal.TryParse(valores[0], out decimal debito) &&
                            decimal.TryParse(valores[1], out decimal credito))
                        {
                            if (_currentRazo == null)
                            {
                                btn_addUC_Click(this, EventArgs.Empty);
                                _currentRazo = razos.LastOrDefault();
                            }

                            if (_currentRazo != null)
                            {
                                _currentRazo.AddRegistro(debito, credito);
                            }
                        }

                    }

                    MessageBox.Show("Razonetes importados com sucesso!", "Importar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao importar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btn_salvar_razo_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog.Title = "Salvar Razonetes como CSV";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SalvarRazos(saveFileDialog.FileName);
                }
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



        private void btn_ImportarRazo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.Title = "Importar Razonetes de um CSV";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ImportarRazos(openFileDialog.FileName);
                }

            }

        }
    }
}
