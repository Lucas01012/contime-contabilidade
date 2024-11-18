using ConTime.Classes;
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
    public partial class Razo : UserControl
    {
        public DataSet ds = new DataSet();
        private Razonete _parentRazonete;
        public static string tablename = "Razo";
        public static string linhasname = "Razo_Linhas";
        public DataGridView dgv1; // Adicionando o primeiro DataGridView
        public DataGridView dgv2; // Adicionando o segundo DataGridView
        public Label header; // Adicionando o cabeçalho


        public TextBox txtHeader;
        private Panel panelRazonetes;
        private Button btnAddRazo;
        private Razonete razonete;

        public int PositionIndex { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Razo(string cabecalho)
        {
            InitializeComponent();
            InitializeDataTables();
            InitializeHeader(cabecalho);
            InicializaRegistros();
            InitializePanel();
            InitializeCustomComponents(); // Inicializa os componentes personalizados
        }

        private void InitializeCustomComponents()
        {
            // Inicialização do primeiro DataGridView
            dgv1 = new DataGridView
            {
                Anchor = (AnchorStyles.Top | AnchorStyles.Left),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
                Dock = DockStyle.None,
                Location = new Point(-6, 20),
                Margin = new Padding(3),
                MaximumSize = new Size(0, 0),
                MinimumSize = new Size(0, 0),
                RowHeadersWidth = 41,
                ScrollBars = ScrollBars.Vertical
            };

            // Inicialização do segundo DataGridView
            dgv2 = new DataGridView
            {
                Anchor = (AnchorStyles.Top | AnchorStyles.Left),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
                Dock = DockStyle.None,
                Location = new Point(-6, 20),
                Margin = new Padding(3),
                MaximumSize = new Size(0, 0),
                MinimumSize = new Size(0, 0),
                RowHeadersWidth = 41,
                ScrollBars = ScrollBars.Vertical
            };

            // Inicialização do cabeçalho
            header = new Label
            {
                Text = "Cabeçalho Ajustado",
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.LightBlue,
                Location = new Point(10, 5), // Ajuste conforme necessário
                Size = new Size(200, 30) // Ajuste conforme necessário
            };

            // Adicionando os componentes ao controle
            this.Controls.Add(dgv1);
            this.Controls.Add(dgv2);
            this.Controls.Add(header);
        }
        public void ReapplyDataGridViewSettings()
        {
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.WindowText;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            dataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.GridColor = Color.DarkGreen;
            dataGridView1.RowTemplate.Height = 24;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;

            ConfigurarCoresDataGridView();
        }



        public void SetParentRazonete(Razonete parentRazonete)
        {
            _parentRazonete = parentRazonete ?? throw new ArgumentNullException(nameof(_parentRazonete));
        }
        private void InitializeHeader(string cabecalho)
        {
            txtHeader = new TextBox
            {
                Name = "txtHeader",
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(200, 20),
                Text = cabecalho // Este valor deve ser o que você quer exibir como o texto inicial
            };

            // Se você está usando um placeholder, isso será exibido quando o campo estiver vazio:
            txtHeader.PlaceholderText = "Digite o cabeçalho aqui..."; // Isso é apenas o placeholder, não o valor real
            this.Controls.Add(txtHeader);
        }

        public void AtualizarCabecalho(string novoCabecalho)
        {
            txtHeader.Text = novoCabecalho;
            

        }

        public string GetCabecalho()
        {
            return txtHeader.Text; // Garanta que está pegando o texto real aqui
        }

        private void InitializePanel()
        {
            panelRazonetes = new Panel
            {
                Location = new System.Drawing.Point(10, 40),
                Size = new System.Drawing.Size(300, 200),
                AutoScroll = true
            };

            btnAddRazo = new Button
            {
                Text = "Adicionar Razonete",
                Location = new System.Drawing.Point(10, panelRazonetes.Bottom + 10)
            };
            btnAddRazo.Click += BtnAddRazo_Click;

            this.Controls.Add(panelRazonetes);
            this.Controls.Add(btnAddRazo);
        }

        private void BtnAddRazo_Click(object sender, EventArgs e)
        {
            AdicionarRazonete();
        }

        private void AdicionarRazonete()
        {
            Panel novoRazonetePanel = new Panel
            {
                Size = new System.Drawing.Size(panelRazonetes.Width - 20, 100),
                Dock = DockStyle.Top
            };

            foreach (DataRow row in ds.Tables[linhasname].Rows)
            {
                decimal debito = row.Field<decimal>("Debito");
                decimal credito = row.Field<decimal>("Credito");

                Razo novoRazo = new Razo("Razonete: " + (panelRazonetes.Controls.Count + 1));
                novoRazo.Dock = DockStyle.Top;

                novoRazo.AddRegistro(debito, credito);

                novoRazonetePanel.Controls.Add(novoRazo);
            }

            panelRazonetes.Controls.Add(novoRazonetePanel);
            panelRazonetes.Controls.Add(btnAddRazo);
        }

        public void InitializeDataTables()
        {
            DataTable dl = ds.Tables.Add(linhasname);
            dl.Columns.Add("Debito", typeof(decimal));
            dl.Columns.Add("Credito", typeof(decimal));
            dataGridView1.DataSource = dl;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            DataTable dt = ds.Tables.Add(tablename);
            dt.Columns.Add("Total_Debito", typeof(decimal));
            dt.Columns.Add("Total_Credito", typeof(decimal));

            DataRow initialRow = dt.NewRow();
            initialRow["Total_Debito"] = 0;
            initialRow["Total_Credito"] = 0;
            dt.Rows.Add(initialRow);

            dgv_result.DataSource = dt;

            foreach (DataGridViewColumn column in dgv_result.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            ConfigurarCoresDataGridView();
        }

        public void InicializaRegistros()
        {

        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["Debito"].Index ||
                dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["Credito"].Index)
            {
                var textBox = e.Control as TextBox;

                if (textBox != null)
                {
                    textBox.KeyPress -= TextBox_KeyPress;
                    textBox.KeyPress += TextBox_KeyPress;
                }
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Debito" || dataGridView1.Columns[e.ColumnIndex].Name == "Credito")
            {
                if (dataGridView1[e.ColumnIndex, e.RowIndex].Value is string value)
                {
                    if (decimal.TryParse(value.Replace(',', '.'), out decimal result))
                    {
                        dataGridView1[e.ColumnIndex, e.RowIndex].Value = result;
                    }
                    else
                    {
                        MessageBox.Show($"Valor inválido: {value}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            CalcularTotais();
        }

        public void CalcularTotais()
        {
            float totalDebito = 0, totalCredito = 0;

            // Soma os totais de débito e crédito
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Debito"].Value != null && float.TryParse(row.Cells["Debito"].Value.ToString(), out float debito))
                {
                    totalDebito += debito;
                }

                if (row.Cells["Credito"].Value != null && float.TryParse(row.Cells["Credito"].Value.ToString(), out float credito))
                {
                    totalCredito += credito;
                }
            }

            // Atualiza as células de totais
            dgv_result[0, 0].Value = totalDebito;
            dgv_result[1, 0].Value = totalCredito;

            // Força a atualização do DataGridView de resultados
            dgv_result.Refresh();
        }


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            dgv.ClearSelection();
        }

        public Classes.Razo TabelaRazonete()
        {
            return new Classes.Razo(ds, txtHeader.Text);
        }

        public void SetCabecalho(string cabecalho)
        {
            txtHeader.Text = cabecalho;
        }

        public void AddRegistro(decimal debito, decimal credito)
        {
            DataRow newRow = ds.Tables[linhasname].NewRow();
            newRow["Debito"] = debito;
            newRow["Credito"] = credito;

            ds.Tables[linhasname].Rows.Add(newRow);

            // Forçar o DataGridView a se atualizar
            dataGridView1.Refresh();  // Atualiza o DataGridView imediatamente
            dataGridView1.Invalidate(); // Redesenha o DataGridView, se necessário
            CalcularTotais();  // Pode ser interessante recalcular os totais após adicionar um registro
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (_parentRazonete != null)
            {
                _parentRazonete.RemoveRazo(this);
            }

        }
        public void ConfigurarCoresDataGridView()
        {
            // Cor de fundo da grade
            dataGridView1.BackgroundColor = Color.FromArgb(185, 220, 200);

            // Cor de fundo das células
            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(185, 220, 200);

            // Cor de seleção das células
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(185, 220, 200);

            // Cor de fundo do cabeçalho
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(185, 220, 200);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Yu Gothic UI", 12, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Yu Gothic UI", 12, FontStyle.Bold);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            // Cor de fundo das linhas alternadas
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(185, 220, 200);

            dgv_result.BackgroundColor = Color.FromArgb(185, 220, 200);
            dgv_result.DefaultCellStyle.BackColor = Color.FromArgb(185, 220, 200);
            dgv_result.DefaultCellStyle.Font = new Font("Yu Gothic UI", 12, FontStyle.Bold);

            Header.BackColor = Color.FromArgb(29, 61, 48);
            Header.Font = new Font("YU Gothic UI", 12, FontStyle.Bold);
            Header.ForeColor = Color.White;

            button1.FlatStyle = FlatStyle.Flat;          
            button1.FlatAppearance.BorderSize = 0;           
            button1.BackColor = Color.Transparent;            
            button1.ForeColor = Color.Black;                  
            button1.TextAlign = ContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            }

            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_result_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}