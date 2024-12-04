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
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ConTime.Screens
{
    public partial class Razonete : UserControl
    {
        public MemoryStream PdfStream { get; private set; }

        public List<Razo> razos = new List<Razo>();
        public List<string>  Cabeçalhos { get; set; } = new List<string>();
        private int childs = 0;
        private int MaxRazonetes = 34;
        public event Action<Razo> RazoAdicionado;

        Razo _currentRazo;
        public Razonete()
        {
            InitializeComponent();
            this.HorizontalScroll.Maximum = 0;
            this.AutoScroll = false;
            this.VerticalScroll.Visible = false;
            pnl_razonetes.AutoScroll = true;
            pnl_razonetes.VerticalScroll.Visible = true;
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

            if (razos.Count >= MaxRazonetes)
            {
                MessageBox.Show($"Não é possível adicionar mais de {MaxRazonetes} razonetes.", "Limite atingido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
            DataStore.SalvarRazonete(this);
            RazoAdicionado?.Invoke(uc);
            btn_addUC.Left = pnl.Left + pnl.Width + 10;
            btn_addUC.Top = pnl.Top;
            this.pnl_razonetes.AutoScroll = true;
        }

        public List<Razo> GetRazos()
        {
            return razos;
        }
        public List<string> GetCabeçalhos()
        {
            List<string> cabeçalhos = new List<string>();
            foreach (var razo in razos)
            {
                cabeçalhos.Add(razo.SalvarHeader());
            }
            return cabeçalhos;
        }

        public void RepositionPanels()
        {
            int uc_width = this.Width;
            int childMaxW = uc_width / pnl_razo.Width;
            int padW = (uc_width % pnl_razo.Width) / Math.Max(childMaxW, 1);
            int y = 0;

            foreach (Control control in this.pnl_razonetes.Controls)
            {
                if (control is System.Windows.Forms.Button)
                {
                    continue;
                }

                if (control is RoundedPanel panel)
                {
                    panel.Left = 2 + ((panel.Width + padW) * (y % childMaxW));
                    panel.Top = 2 + ((panel.Height + 5) * (y / childMaxW));
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
                parentPanel.BackColor = Color.FromArgb(96, 128, 111);

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

        public void CreatePdf(object sender, EventArgs e)
        {
            GerarPdf();
        }

        public MemoryStream PdfRazoGeral()
        {
            Classes.Razonetes razonete = new Classes.Razonetes();

            MemoryStream pdfStream = null;

            foreach (Razo razo in razos)
            {
                razonete.AddRazonete(razo.TabelaRazonete());
                pdfStream = razonete.PdfCreate();
            }
            return pdfStream;
        }
        public MemoryStream GerarPdf()
        {
            Classes.Razonetes razonete = new Classes.Razonetes();
            
            MemoryStream pdfStream = null;

            foreach (Razo razo in razos)
            {
                razonete.AddRazonete(razo.TabelaRazonete());
                pdfStream = razonete.PdfCreate();
            }

            if (pdfStream != null)
            {
                OpenPdfForViewing(pdfStream);
            }
            return pdfStream;
        }

        private void OpenPdfForViewing(MemoryStream pdfStream)
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), "tempRazonetes.pdf");

            File.WriteAllBytes(tempFilePath, pdfStream.ToArray());

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(tempFilePath)
            {
                UseShellExecute = true
            });
        }

        public bool PdfAberto()
        {
            foreach (var process in Process.GetProcessesByName("Acrobat")){
                if (!string.IsNullOrEmpty(process.MainWindowTitle))
                {
                    return true;
                }
            }
            foreach (var process in Process.GetProcessesByName("AcroRd32"))
            {
                if (!string.IsNullOrEmpty(process.MainWindowTitle)) {
                return true;
                }
            }
            return false;
        }
        public string GerarArquivoRazonete()
        {
            var razonetes = DataStore.ObterRazonetes();

            if (razonetes == null || razonetes.Count == 0)
            {
                MessageBox.Show("Não foram encontrados razonetes!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return string.Empty;
            }

            try
            {
                string tempDirectory = Path.Combine(Path.GetTempPath(), "RazonetesExportados");
                if (!Directory.Exists(tempDirectory))
                {
                    Directory.CreateDirectory(tempDirectory);
                }

                string filePath = Path.Combine(tempDirectory, $"Razonetes_{DateTime.Now:yyyyMMddHHmmssfff}.csv");

                using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    sw.WriteLine("Cabeçalho;Débito;Crédito;Total Débito;Total Crédito");

                    decimal totalGeralDebito = 0m;
                    decimal totalGeralCredito = 0m;

                    foreach (var razonete in razonetes)
                    {
                        var cabecalhosEncontrados = new HashSet<string>();
                        var duplicados = new List<string>();

                        foreach (var razo in razonete.GetRazos())
                        {
                            string cabecalho = razo.SalvarHeader();
                            if (!string.IsNullOrWhiteSpace(cabecalho) && !cabecalho.Equals("Cabeçalho Padrão"))
                            {
                                if (!cabecalhosEncontrados.Add(cabecalho))
                                {
                                    duplicados.Add(cabecalho);
                                }
                            }
                        }

                        if (duplicados.Count > 0)
                        {
                            var result = MessageBox.Show(
                                $"O razonete contém cabeçalhos duplicados. Você deseja continuar com o salvamento?",
                                "Cabeçalhos Duplicados",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);

                            if (result == DialogResult.No)
                            {
                                continue;
                            }
                        }

                        foreach (var razo in razonete.GetRazos())
                        {
                            string cabecalho = razo.SalvarHeader();
                            if (string.IsNullOrWhiteSpace(cabecalho) || cabecalho == "Cabeçalho Padrão")
                            {
                                cabecalho = "Cabeçalho Não Definido";
                            }

                            sw.WriteLine($"Cabeçalho: {cabecalho}");

                            decimal totalDebito = 0m;
                            decimal totalCredito = 0m;

                            foreach (var registro in razo.TabelaRazonete().registros)
                            {
                                decimal debitoDecimal = Convert.ToDecimal(registro.debito);
                                decimal creditoDecimal = Convert.ToDecimal(registro.credito);

                                if (debitoDecimal != 0 || creditoDecimal != 0)
                                {
                                    sw.WriteLine($"{debitoDecimal:F2};{creditoDecimal:F2}");
                                    totalDebito += debitoDecimal;
                                    totalCredito += creditoDecimal;
                                }
                            }

                            sw.WriteLine($"Total;{totalDebito:F2};{totalCredito:F2}");

                            totalGeralDebito += totalDebito;
                            totalGeralCredito += totalCredito;
                        }
                    }

                    sw.WriteLine($"TOTAL GERAL;{totalGeralDebito:F2};{totalGeralCredito:F2}");
                }

                return filePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar os razonetes: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }




        public void SalvarRazonete()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Arquivo CSV (*.csv)|*.csv";
                saveFileDialog.Title = "Salvar Razonete";
                saveFileDialog.DefaultExt = "csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        var cabecalhosEncontrados = new HashSet<string>();
                        var duplicados = new List<string>();

                        foreach (var razo in razos)
                        {
                            string cabecalho = razo.SalvarHeader();
                            if (!string.IsNullOrWhiteSpace(cabecalho) && !cabecalho.Equals("Cabeçalho Padrão"))
                            {
                                if (!cabecalhosEncontrados.Add(cabecalho))
                                {
                                    duplicados.Add(cabecalho);
                                }
                            }
                        }

                        if (duplicados.Count > 0)
                        {
                            var result = MessageBox.Show(
                                "Existem cabeçalhos duplicados. Você deseja continuar com o salvamento?",
                                "Cabeçalhos Duplicados",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);

                            if (result == DialogResult.No)
                            {
                                return;
                            }
                        }

                        using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
                        {
                            if (razos.Count == 0)
                            {
                                MessageBox.Show("Nenhum Razo criado para salvar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            foreach (var razo in razos)
                            {
                                string cabecalho = razo.SalvarHeader();

                                if (string.IsNullOrWhiteSpace(cabecalho) || cabecalho == "Cabeçalho Padrão")
                                {
                                    cabecalho = "Cabeçalho Não Definido";
                                }

                                sw.WriteLine($"Cabeçalho: {cabecalho}");

                                decimal totalDebito = 0m;
                                decimal totalCredito = 0m;

                                foreach (var registro in razo.TabelaRazonete().registros)
                                {
                                    decimal debitoDecimal = Convert.ToDecimal(registro.debito);
                                    decimal creditoDecimal = Convert.ToDecimal(registro.credito);

                                    if (debitoDecimal != 0 || creditoDecimal != 0)
                                    {
                                        sw.WriteLine($"{debitoDecimal:F2};{creditoDecimal:F2}");
                                        totalDebito += debitoDecimal;
                                        totalCredito += creditoDecimal;
                                    }
                                }

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
            }
        }




        private void ImportarRazos(string filePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    string linhaCabecalho;
                    _currentRazo = null;

                    while ((linhaCabecalho = sr.ReadLine()) != null)
                    {
                        if (linhaCabecalho.StartsWith("Cabeçalho:"))
                        {
                            string cabecalhoTexto = linhaCabecalho.Replace("Cabeçalho: ", "").Trim();

                            var razoneteExistente = razos.FirstOrDefault(r => r.SalvarHeader() == cabecalhoTexto);

                            if (razoneteExistente != null)
                            {
                                var result = MessageBox.Show(
                                    $"O cabeçalho '{cabecalhoTexto}' já existe. O que deseja fazer?\n\n" +
                                    "1 - Substituir o razonete existente.\n" +
                                    "2 - Acrescentar um novo razonete com o mesmo cabeçalho.\n" +
                                    "3 - Ignorar e não importar este cabeçalho.",
                                    "Cabeçalho Duplicado",
                                    MessageBoxButtons.YesNoCancel,
                                    MessageBoxIcon.Question);

                                if (result == DialogResult.Yes)
                                {
                                    razoneteExistente.TabelaRazonete().registros.Clear();
                                    _currentRazo = razoneteExistente;
                                }
                                else if (result == DialogResult.No)
                                {
                                    btn_addUC_Click(this, EventArgs.Empty);
                                    _currentRazo = razos.LastOrDefault();
                                    _currentRazo?.SetCabecalho(cabecalhoTexto);
                                }
                                else if (result == DialogResult.Cancel)
                                {
                                    _currentRazo = null;
                                    continue; 
                                }
                            }
                            else
                            {
                                btn_addUC_Click(this, EventArgs.Empty);
                                _currentRazo = razos.LastOrDefault();
                                _currentRazo?.SetCabecalho(cabecalhoTexto);
                            }

                            continue;
                        }

                        if (!string.IsNullOrWhiteSpace(linhaCabecalho))
                        {
                            var valores = linhaCabecalho.Split(';');
                            if (valores.Length == 2 &&
                                decimal.TryParse(valores[0], out decimal debito) &&
                                decimal.TryParse(valores[1], out decimal credito))
                            {
                                if (_currentRazo != null)
                                {
                                    _currentRazo.AddRegistro(debito, credito);
                                }
                            }
                        }
                    }

                    foreach (var razo in razos)
                    {
                        razo.ReapplyDataGridViewSettings();
                    }

                    RepositionPanels();

                    MessageBox.Show("Razonetes importados com sucesso!", "Importar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao importar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _currentRazo = null;
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
                    SalvarRazonete();
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

        private void pnl_razonetes_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
