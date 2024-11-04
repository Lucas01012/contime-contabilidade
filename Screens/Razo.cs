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
        }

        public void SetParentRazonete(Razonete parentRazonete) { 
            _parentRazonete = parentRazonete ?? throw new ArgumentNullException(nameof(_parentRazonete));
        }
        private void InitializeHeader(string cabecalho)
        {
            txtHeader = new TextBox
            {
                Name = "txtHeader",
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(200, 20),
                Text = cabecalho
            };
            this.Controls.Add(txtHeader);
        }

        public String GetCabecalho() { 
            return txtHeader.Text;
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

            dgv_result[0, 0].Value = totalDebito;
            dgv_result[1, 0].Value = totalCredito;
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

            dataGridView1.DataSource = null; 
            dataGridView1.DataSource = ds.Tables[linhasname];
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (_parentRazonete != null)
            {
                _parentRazonete.RemoveRazo(this); 
            }

        }
    }
}