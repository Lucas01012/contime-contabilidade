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
        DataSet ds = new DataSet();
        public static string tablename = "Razo";
        public static string linhasname = "Razo_Linhas";
        public Razo()
        {
            InitializeComponent();
            DataTable dl = ds.Tables.Add(linhasname);
            dataGridView1.DataSource = dl;
            dl.Columns.Add("Debito");
            dl.Columns.Add("Credito");
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataTable dt = ds.Tables.Add(tablename);
            dgv_result.DataSource = dt;
            dt.Columns.Add("Total_Debito");
            dt.Columns.Add("Total_Credito");
            dgv_result.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv_result.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            float d = 0, c = 0, result = 0;
            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    float.TryParse(Convert.ToString(dataGridView1[j, i].Value), out result);
                    if (j == 0)
                        d += result;
                    else
                        c += result;
                }
            }
            dgv_result[0, 0].Value = d;
            dgv_result[1, 0].Value = c;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            dgv.ClearSelection();
        }

        public Classes.Razo TabelaRazonete()
        {
            Classes.Razo razo = new(ds, Header.Text);

            return razo;
        }
    }
}
