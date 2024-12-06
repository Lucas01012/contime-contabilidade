using ConTime.Screens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace ConTime.Classes
{
    public static class DataStore
    {
        public static DataTable BalanceteData { get; set; }
        public static DataTable LdiaData { get; set; }
        public static Dictionary<string, DataTable> DrePanéisData { get; set; } = new Dictionary<string, DataTable>();
        public static List<Razonete> RazonetesData { get; set; }
        public static DataSet BalancoPatrimonialData { get; set; } = new DataSet("BalancoPatrimonial");
        public static DataSet DreData { get; set; } = new DataSet("DreData");
        public static DataTable AtvCirculanteData { get; set; }
        public static DataTable AtvNCirculanteData { get; set; }
        public static DataTable PsvCirculanteData { get; set; }
        public static DataTable PsvNCirculanteData { get; set; }
        public static DataTable PatrimonioData { get; set; }

        public static void LimparDados()
        {
            BalanceteData = null;
            LdiaData = null;
            RazonetesData.Clear();
            DrePanéisData.Clear();
            BalancoPatrimonialData = new DataSet("BalancoPatrimonial");
            DreData = new DataSet("DreData");
            AtvCirculanteData = null;
            AtvNCirculanteData = null;
            PsvCirculanteData = null;
            PsvNCirculanteData = null;
            PatrimonioData = null;
        }

        static DataStore()
        {
            LdiaData = new DataTable();
            LdiaData.Columns.Add("Data");
            LdiaData.Columns.Add("Código");
            LdiaData.Columns.Add("Conta");
            LdiaData.Columns.Add("Histórico");
            LdiaData.Columns.Add("Débito");
            LdiaData.Columns.Add("Crédito");
            LdiaData.Columns.Add("Saldo");

            BalanceteData = new DataTable();
            BalanceteData.Columns.Add("Código");
            BalanceteData.Columns.Add("Conta");
            BalanceteData.Columns.Add("Devedor");
            BalanceteData.Columns.Add("Credor");
            BalanceteData.Columns.Add("Saldo");

            BalancoPatrimonialData = new DataSet("BalancoPatrimonial");
            AtvCirculanteData = new DataTable();
            AtvNCirculanteData = new DataTable();
            PsvNCirculanteData = new DataTable();
            PsvCirculanteData = new DataTable();
            PatrimonioData = new DataTable();

            RazonetesData = new List<Razonete>();
        }

        public static void SalvarRazonete(Razonete razonete)
        {
            if (razonete != null)
            {
                RazonetesData.Add(razonete);
                foreach (var razo in razonete.GetRazos())
                {
                    string cabecalho = razo.SalvarHeader();
                }
            }
        }

        public static void AdicionarDreData(string tabela, string receita, string valor)
        {
            if (!DreData.Tables.Contains(tabela))
            {
                DataTable dt = DreData.Tables.Add(tabela);
                dt.Columns.Add("Receita");
                dt.Columns.Add("Valor");
            }
            DataTable table = DreData.Tables[tabela];
            DataRow row = table.NewRow();
            row["Receita"] = receita;
            row["Valor"] = valor;
            table.Rows.Add(row);
        }

        public static void LimparDreData()
        {
            foreach (DataTable table in DreData.Tables)
            {
                table.Rows.Clear();
            }
        }

        public static List<Razonete> ObterRazonetes()
        {
            return RazonetesData;
        }

        public static void LimparRazonetes()
        {
            RazonetesData.Clear();
        }
    }


}