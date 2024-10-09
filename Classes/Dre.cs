using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConTime.Screens;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ConTime.Classes
{
    internal class Dre
    {
        readonly public static string[] tipo =
        [
            "RBruta",
            "Imposto",
            "Custos",
            "Despesas",
            "OutrasR"
        ];
        List<DreRegistro> RBruta = new(); // (+)
        List<DreRegistro> Impostos = new(); // (-)
        float RLiquida; // (=)
        List<DreRegistro> Custos = new(); // (-)
        float LBruto; // (=)
        List<DreRegistro> Despesas = new(); // (-)
        List<DreRegistro> OutrasR = new(); // (+)
        float LLiquido; // (=)

        private List<DreRegistro>[] DreRegistros;

        internal class DreRegistro
        {
            internal string conta;
            internal float valor;

            public DreRegistro(DataRow dr)
            {
                this.conta = Convert.ToString(dr["Receita"]);
                float.TryParse(Convert.ToString(dr["Valor"]),out valor);
            }
        }

        public Dre(DataSet ds, float liquida, float bruta, float lucro)
        {
            DreRegistros = [RBruta, Impostos, Custos, Despesas, OutrasR];

            foreach(string nomeDreRegistro in tipo)
            {
                DataTable dt = ds.Tables[nomeDreRegistro];
                foreach(DataRow dr in dt.Rows)
                {
                    DreRegistros[Array.FindIndex(tipo, w =>  w == nomeDreRegistro)].Add(new DreRegistro(dr));
                }
            }
            this.RLiquida = liquida;
            this.LBruto = bruta;
            this.LLiquido = lucro;
        }
        #region PDF
        public void PdfCreate()
        {
            var pdf = CreatePDF();
            pdf.GeneratePdfAndShow();
        }

        private Document CreatePDF()
        {
            string
                HeaderBGColor = "#000000", HeaderFColor = "#ffffff",
                LabelBGColor = "#aaaaaa", LabelFColor = "#000000", LabelBorderColor = "#666666",
                CellBGColor = "#ffffff", CellFColor = "#000000";


            void ComposeTable(IContainer container)
            {
                container
                    .Table(t =>
                    {
                        void addcell(string? value) => t.Cell().Border(1).BorderColor("#000000").Background(CellBGColor).Text(value).FontColor(CellFColor);
                        void addlabel(string? value, uint ColumnSpan) => t.Cell().ColumnSpan(ColumnSpan).Border(1).BorderColor(LabelBorderColor).Background(LabelBGColor).Text(value).FontColor(LabelFColor);
                        void addempty() => t.Cell().ColumnSpan(2).Border(1).BorderColor("#000000").Background(CellBGColor);

                        void CreateTable(List<DreRegistro> DreRegistros)
                        {
                            foreach(DreRegistro t in DreRegistros)
                            {
                                addcell(t.conta);
                                addcell($"{t.valor:C}");
                            }
                        }

                        t.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn();
                            c.ConstantColumn(100);
                        });
                        t.Header(h =>
                        {
                            h.Cell().ColumnSpan(2).BorderColor("#ffffff").Background(HeaderBGColor).Text("Cabeçario DRE").FontColor(HeaderFColor).AlignCenter();
                            h.Cell().ColumnSpan(1).Border(1).BorderColor("#ffffff").Background(HeaderBGColor).Text("Conta").FontColor(HeaderFColor);
                            h.Cell().ColumnSpan(1).Border(1).BorderColor("#ffffff").Background(HeaderBGColor).Text("Saldo").FontColor(HeaderFColor);
                        });
                        addlabel("Receita Bruta", 2);
                        CreateTable(DreRegistros[0]);
                        addlabel("(-)Impostos", 2);
                        CreateTable(DreRegistros[1]);
                        addlabel("(=)Receita Liquida:", 1);
                        addlabel($"{RLiquida:C}", 1);
                        addlabel("(-)Custos", 2);
                        CreateTable(DreRegistros[2]);
                        addlabel("(=)Lucro Operacional Bruto:", 1);
                        addlabel($"{LBruto:C}", 1);
                        addlabel("(-)Despesas", 2);
                        CreateTable(DreRegistros[3]);
                        addlabel("(+)Outras Receitas", 2);
                        CreateTable(DreRegistros[4]);
                        addlabel("(=)Resultado de Lucro do Exercicio:", 1);
                        addlabel($"{LLiquido:C}", 1);
                    });
            }

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Content().Column(c =>
                    {
                        ComposeTable(c.Item());
                    });
                });
            });
        }
        #endregion
    }
}
