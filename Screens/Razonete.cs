using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConTime.Screens
{
    public partial class Razonete : UserControl
    {
        private List<Razo> razos = new List<Razo>();
        public Razonete()
        {
            InitializeComponent();

            this.HorizontalScroll.Maximum = 0;
            this.AutoScroll = false;
            this.VerticalScroll.Visible = false;
            //this.AutoScroll = true;
        }

        private void AddScreen(UserControl uc, Panel pnl)
        {
            uc.Dock = DockStyle.Fill;
            pnl.Controls.Clear();
            pnl.Controls.Add(uc);
            uc.BringToFront();
        }
        private int childs = 0;
        private void btn_addUC_Click(object sender, EventArgs e)
        {
            int uc_width = this.Width;
            int uc_height = this.Height;
            int childMaxW = uc_width / pnl_razo.Width;

            int padW = uc_width % pnl_razo.Width / childMaxW;

            childs++;
            RoundedPanel pnl = new RoundedPanel();
            pnl.Size = pnl_razo.Size;
            pnl.BackColor = pnl_razo.BackColor;
            pnl.Parent = this.pnl_razonetes;
            pnl.Left = 2 + ((pnl.Width + padW) * (childs % (uc_width / (pnl.Width + padW))));
            pnl.Top = 2 + ((pnl.Height + 5) * (int)(Math.Ceiling((double)(childs / (uc_width / (pnl.Width + padW))))));

            RoundedButton btn = new RoundedButton();
            btn.Dock = btn_addUC.Dock;
            btn.Font = btn_addUC.Font;
            btn.Text = btn_addUC.Text;
            btn.RoundedBorderColor = btn_addUC.RoundedBorderColor;
            btn.RoundedBorderSize = btn_addUC.RoundedBorderSize;
            btn.Click += btn_addUC_Click;
            btn.Parent = pnl;

            Button snd = (Button)sender;
            Razo uc = new Razo();
            razos.Add(uc);
            AddScreen(uc, (Panel)snd.Parent);


        }

        private void Razonete_SizeChanged(object sender, EventArgs e)
        {
            //Form mainForm = Application.OpenForms["FormBody"];
            if (childs != 0 && this.ParentForm.WindowState != FormWindowState.Minimized)
            {
                int childTemp = 0;
                int uc_width = this.Width;
                int uc_height = this.Height;
                int childMaxW = uc_width / pnl_razo.Width;
                int padW = uc_width % pnl_razo.Width / childMaxW;

                foreach (var pnl in this.pnl_razonetes.Controls.OfType<Panel>())
                {
                    pnl.Left = 2 + ((pnl.Width + padW) * (childTemp % (uc_width / (pnl.Width + padW))));
                    pnl.Top = 2 + ((pnl.Height + 5) * (int)(Math.Ceiling((double)(childTemp / (uc_width / (pnl.Width + padW))))));
                    childTemp++;
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
        private void SaveRazos(List<Razo> razos, MySqlConnection connection)
        {
            try
            {
                connection.Open();
                MessageBox.Show("Conexão com o banco de dados aberta com sucesso!");

                foreach (var r in razos)
                {
                    Classes.Razo razoTabela = r.TabelaRazonete();
                    foreach (var razo in razoTabela.registros)
                    {
                        if (razo.credito != null && razo.debito != null)
                        {
                            string creditoValueString = razo.credito.ToString();
                            string debitoValueString = razo.debito.ToString();

                            if (float.TryParse(creditoValueString, out float credito) && float.TryParse(debitoValueString, out float debito))
                            {
                                MySqlCommand cmd = new MySqlCommand();
                                cmd.Connection = connection;
                                cmd.CommandText = "INSERT INTO razonete (debito, credito) VALUES (@Debito, @Credito)";

                                cmd.Parameters.AddWithValue("@Debito", debito);
                                cmd.Parameters.AddWithValue("@Credito", credito);

                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                MessageBox.Show($"Valores de débito ou crédito inválidos para Razo com ID: {razoTabela.razoid}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
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
        private void btn_salvar_razo_Click(object sender, EventArgs e)
        {
            string connectionString = "SERVER=localhost;DATABASE=bdcontime;UID=root;PASSWORD=projetocontime123;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                SaveRazos(razos, connection);
            }
        }
    }
}