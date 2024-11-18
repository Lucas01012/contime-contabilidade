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
            public float saldo;

            public BalanceteRegistro(DataRow dr)
            {
                codigo = Convert.ToString(dr["Código"]);
                conta = Convert.ToString(dr["Conta"]);
                float.TryParse(dr["Credor"].ToString(), out credito);
                float.TryParse(dr["Devedor"].ToString(), out debito);
                float.TryParse(dr["Saldo"].ToString(), out saldo);

            }

            public void CreateCell(TableDescriptor table)
            {
                table.Cell().Element(PdfComponents.Cell).Text(codigo);
                table.Cell().Element(PdfComponents.Cell).Text(conta);
                table.Cell().Element(PdfComponents.Cell).Text($"{credito:C}");
                table.Cell().Element(PdfComponents.Cell).Text($"{debito:C}");
                table.Cell().Element(PdfComponents.Cell).Text($"{saldo:C}");
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
                            columns.ConstantColumn(50);    // Código
                            columns.RelativeColumn();      // Conta
                            columns.ConstantColumn(90);    // Credor
                            columns.ConstantColumn(90);    // Devedor
                            columns.ConstantColumn(90);    // Saldo
                        });

                        // Cabeçalho
                        table.Header(header =>
                        {
                            header.Cell().ColumnSpan(5).BorderColor("#000000").Background("#ffffff")
                                .Text(cabecario).FontColor("#000000").AlignCenter();
                            header.Cell().Element(PdfComponents.Header).Text("Código").FontColor(PdfComponents.HeaderFColor);
                            header.Cell().Element(PdfComponents.Header).Text("Conta").FontColor(PdfComponents.HeaderFColor);
                            header.Cell().Element(PdfComponents.Header).Text("Credor").FontColor(PdfComponents.HeaderFColor);
                            header.Cell().Element(PdfComponents.Header).Text("Devedor").FontColor(PdfComponents.HeaderFColor);
                            header.Cell().Element(PdfComponents.Header).Text("Saldo").FontColor(PdfComponents.HeaderFColor);
                        });

                        // Linhas de dados
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
