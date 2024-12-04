using ConTime.Screens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConTime.Classes
{
    public class DataStore
    {
        public static DataTable BalanceteData { get; set; }
        public static DataTable LdiaData { get; set; }
        public static Dictionary<string, DataTable> DrePanéisData { get; set; }
        public static List<BalancoPatrimonialData> BalancoPatrimoniais { get; set; }
        public static List<Razonete> RazonetesData { get; set; }

        public static void LimparDados()
        {
            BalanceteData = null;
            LdiaData = null;
            RazonetesData = new List<Razonete>();
            DrePanéisData.Clear();
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

            DrePanéisData = new Dictionary<string, DataTable>
        {
            { "Receita Bruta", new DataTable() },
            { "Impostos", new DataTable() },
            { "Receita Líquida", new DataTable() },
            { "Lucro Operacional Bruto", new DataTable() },
            { "Despesas", new DataTable() },
            { "Outras Receitas", new DataTable() },
            { "Resultado de Lucro do Exercício", new DataTable() }
        };

            foreach (var painel in DrePanéisData.Values)
            {
                painel.Columns.Add("Conta");
                painel.Columns.Add("Valor");
            }

            BalancoPatrimoniais = new List<BalancoPatrimonialData>();

            RazonetesData = new List<Razonete>();
        }
        public static void AdicionarBalancoPatrimonial(BalancoPatrimonialData balanco)
        {
            if (balanco == null)
            {
                BalancoPatrimoniais.Add(balanco);
            }
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

        public static List<Razonete> ObterRazonetes()
        {
            return RazonetesData;
        }

        public static void LimparRazonetes()
        {
            RazonetesData.Clear();
        }

        public static void AdicionarDreData(string painel, string conta, string valor)
        {
            if (DrePanéisData.ContainsKey(painel) && !string.IsNullOrEmpty(conta) && !string.IsNullOrEmpty(valor))
            {
                DataTable painelData = DrePanéisData[painel];
                DataRow newRow = painelData.NewRow();
                newRow["Conta"] = conta;
                newRow["Valor"] = valor;
                painelData.Rows.Add(newRow);
            }
        }

        public static void AtualizarValorDre(string painel, string conta, string novoValor)
        {
            if (DrePanéisData.ContainsKey(painel))
            {
                DataTable painelData = DrePanéisData[painel];
                foreach (DataRow row in painelData.Rows)
                {
                    if (row["Conta"].ToString() == conta)
                    {
                        row["Valor"] = novoValor;
                        break;
                    }
                }
            }
        }

        public static DataTable ObterDreDataPorPainel(string painel)
        {
            if (DrePanéisData.ContainsKey(painel))
            {
                return DrePanéisData[painel];
            }
            return null;
        }

        public static void LimparDreDataPorPainel(string painel)
        {
            if (DrePanéisData.ContainsKey(painel))
            {
                DrePanéisData[painel].Clear();
            }
        }

    } 

}