using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConTime.Screens;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ConTime.Classes
{


    public class Dre
    {
        public static string[] tipo = { "RBruta", "Imposto", "Custos", "Despesas", "OutrasR" };

        private readonly List<DreRegistro> RBruta = new();
        private readonly List<DreRegistro> Impostos = new();
        private readonly List<DreRegistro> Custos = new();
        private readonly List<DreRegistro> Despesas = new();
        private readonly List<DreRegistro> OutrasR = new();

        private readonly List<DreRegistro>[] DreRegistros;

        private readonly float RLiquida;
        private readonly float LBruto;
        private readonly float LLiquido;

        internal class DreRegistro
        {
            public string Conta { get; set; }
            public float Valor { get; set; }

            public DreRegistro(DataRow dr)
            {
                Conta = Convert.ToString(dr["Receita"]);
                float.TryParse(Convert.ToString(dr["Valor"]), out var valor);
                Valor = valor;
            }
        }

        public Dre(DataTable rBruta, DataTable imposto, DataTable custos, DataTable despesas, DataTable outrasR, float liquida, float bruta, float lucro)
        {
            AdicionarRegistros(rBruta, RBruta);
            AdicionarRegistros(imposto, Impostos);
            AdicionarRegistros(custos, Custos);
            AdicionarRegistros(despesas, Despesas);
            AdicionarRegistros(outrasR, OutrasR);

            RLiquida = liquida;
            LBruto = bruta;
            LLiquido = lucro;
        }

        private void AdicionarRegistros(DataTable table, List<DreRegistro> list)
        {
            foreach (DataRow dr in table.Rows)
            {
                list.Add(new DreRegistro(dr));
            }
        }

        public Dre(DataSet ds, float liquida, float bruta, float lucro)
        {
            DreRegistros = new[] { RBruta, Impostos, Custos, Despesas, OutrasR };

            foreach (string nomeDreRegistro in tipo)
            {
                if (ds.Tables.Contains(nomeDreRegistro))
                {
                    DataTable dt = ds.Tables[nomeDreRegistro];

                    foreach (DataRow dr in dt.Rows)
                    {
                        DreRegistros[Array.FindIndex(tipo, w => w == nomeDreRegistro)].Add(new DreRegistro(dr));
                    }
                }
            }

            RLiquida = liquida;
            LBruto = bruta;
            LLiquido = lucro;
        }

        public void PreencherDataStore()
        {
            DataStore.LimparDreData();

            PreencherDataStore("RBruta", RBruta);
            PreencherDataStore("Imposto", Impostos);
            PreencherDataStore("Custos", Custos);
            PreencherDataStore("Despesas", Despesas);
            PreencherDataStore("OutrasR", OutrasR);

            DataStore.AdicionarDreData("Resultados Calculados", "Receita Líquida", RLiquida.ToString("C", CultureInfo.CurrentCulture));
            DataStore.AdicionarDreData("Resultados Calculados", "Lucro Bruto", LBruto.ToString("C", CultureInfo.CurrentCulture));
            DataStore.AdicionarDreData("Resultados Calculados", "Lucro Líquido", LLiquido.ToString("C", CultureInfo.CurrentCulture));
        }

        private void PreencherDataStore(string tabela, List<DreRegistro> registros)
        {
            foreach (var registro in registros)
            {
                DataStore.AdicionarDreData(tabela, registro.Conta, registro.Valor.ToString("C", CultureInfo.CurrentCulture));
            }
        }

        public MemoryStream PdfCreate()
        {
            var pdfDocument = new PdfDocument();
            var page = pdfDocument.AddPage();
            var graphics = XGraphics.FromPdfPage(page);

            var headerFont = new XFont("Arial", 12, XFontStyle.Bold);
            var cellFont = new XFont("Arial", 10, XFontStyle.Regular);
            var headerBrush = XBrushes.White;
            var cellBrush = XBrushes.Black;
            var headerBackground = XBrushes.Black;
            var labelBackground = XBrushes.LightGray;

            double marginLeft = 50, marginTop = 50, cellHeight = 20, tableWidth = 500;
            double x = marginLeft, y = marginTop;

            graphics.DrawRectangle(headerBackground, x, y, tableWidth, cellHeight);
            graphics.DrawString("Cabeçalho DRE", headerFont, headerBrush, new XRect(x, y, tableWidth, cellHeight), XStringFormats.Center);
            y += cellHeight;

            void DrawRow(string label, string value, bool isLabel = false)
            {
                var background = isLabel ? labelBackground : XBrushes.White;
                graphics.DrawRectangle(background, x, y, tableWidth, cellHeight);
                graphics.DrawString(label, cellFont, cellBrush, new XRect(x, y, tableWidth / 2, cellHeight), XStringFormats.CenterLeft);
                graphics.DrawString(value, cellFont, cellBrush, new XRect(x + tableWidth / 2, y, tableWidth / 2, cellHeight), XStringFormats.CenterRight);
                y += cellHeight;
            }

            void AddSection(string sectionTitle, List<DreRegistro> registros)
            {
                DrawRow(sectionTitle, string.Empty, isLabel: true);
                foreach (var registro in registros)
                {
                    DrawRow(registro.Conta, registro.Valor.ToString("C", CultureInfo.CurrentCulture));
                }
            }

            AddSection("Receita Bruta", RBruta);
            AddSection("(-) Impostos", Impostos);
            DrawRow("(=) Receita Líquida", RLiquida.ToString("C", CultureInfo.CurrentCulture), isLabel: true);
            AddSection("(-) Custos", Custos);
            DrawRow("(=) Lucro Operacional Bruto", LBruto.ToString("C", CultureInfo.CurrentCulture), isLabel: true);
            AddSection("(-) Despesas", Despesas);
            AddSection("(+) Outras Receitas", OutrasR);
            DrawRow("(=) Resultado de Lucro do Exercício", LLiquido.ToString("C", CultureInfo.CurrentCulture), isLabel: true);

            var stream = new MemoryStream();
            pdfDocument.Save(stream, false);
            stream.Position = 0;
            return stream;
        }
    }


}
