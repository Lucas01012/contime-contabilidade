using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

using ConTime.Classes;
using ConTime.PersonalizedComponents;

namespace ConTime.Classes
{
    internal class Balancete
    {
        private List<BalanceteRegistro> Registros = new();
        string cabecario = "Lorem Ipsum";
        private class BalanceteRegistro
        {
            public string codigo;
            public string conta;
            public float debito;
            public float credito;

            public BalanceteRegistro(DataRow dr)
            {
                codigo = Convert.ToString(dr["codigo"]);
                conta = Convert.ToString(dr["conta"]);
                float.TryParse(dr["credor"].ToString(), out credito);
                float.TryParse(dr["devedor"].ToString(), out debito);

            }

            public void CreateCell(TableDescriptor table)
            {
                table.Cell().Element(PdfComponents.Cell).Text(codigo);
                table.Cell().Element(PdfComponents.Cell).Text(conta);
                table.Cell().Element(PdfComponents.Cell).Text($"{credito:C}");
                table.Cell().Element(PdfComponents.Cell).Text($"{debito:C}");
            }
        }

        public Balancete(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr != dt.NewRow())
                {
                    Registros.Add(new BalanceteRegistro(dr));
                }
            }
        }
        #region PDF
        private Document CreatePDF()
        {
            void ComposeTable(IContainer container)
            {
                container
                    .Shrink()
                    .Border(1)
                    .Table(table =>
                    {

                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(50);
                            columns.RelativeColumn(); //fill
                            columns.ConstantColumn(90);
                            columns.ConstantColumn(90); //Tamanho fixo
                        });
                        table.Header(header =>
                        {
                            header.Cell().ColumnSpan(4).BorderColor("#000000").Background("#ffffff").Text(cabecario).FontColor("#000000").AlignCenter();
                            header.Cell().Element(PdfComponents.Header).Text("Codigo").FontColor(PdfComponents.HeaderFColor);
                            header.Cell().Element(PdfComponents.Header).Text("Conta").FontColor(PdfComponents.HeaderFColor);
                            header.Cell().Element(PdfComponents.Header).Text("Credor").FontColor(PdfComponents.HeaderFColor);
                            header.Cell().Element(PdfComponents.Header).Text("Devedor").FontColor(PdfComponents.HeaderFColor);
                        });

                        foreach (var registro in Registros)
                        {
                            registro.CreateCell(table);
                        }
                    });
            }

            return Document.Create(container =>
            {
                container.Page(page => //Eu sinceramente não entendo como paginas funcionam
                {
                    page.Content().Column(c => ComposeTable(c.Item()));
                });
            });
        }

        public void PdfCreate()
        {
            var pdfdoc = CreatePDF();
            //var fileName = "Test.pdf";
            pdfdoc.GeneratePdfAndShow();
        }
        #endregion
    }
}
