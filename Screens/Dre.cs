using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        //DataGridView[] TablesInterface;
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
            panel1.AutoScroll = false;
            panel1.VerticalScroll.Visible = false;
            panel1.AutoScroll = true;

            for (int i = 0; i < TablesInterface.Length; i++)
            {
                TableSetup(Tables[i]);
                TablesInterface[i].DataSource = DS.Tables[Tables[i]];
                TablesInterface[i].Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
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
             string connectionString = "SERVER=localhost;DATABASE=bdcontime;UID=root;PASSWORD=projetocontime123;";


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                string query = "INSERT INTO dre (receita_liquida, lucro_bruto, resultado_exercicio) VALUES (@Valor2, @Valor4, @Valor7)";

                try
                {

                    connection.Open();


                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@Valor2", resul[2]);
                        command.Parameters.AddWithValue("@Valor4", resul[4]);
                        command.Parameters.AddWithValue("@Valor7", resul[7]);


                        command.ExecuteNonQuery();

                        MessageBox.Show("Valores da DRE salvos com sucesso!");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Erro ao salvar os valores da DRE no banco de dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocorreu um erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
  
