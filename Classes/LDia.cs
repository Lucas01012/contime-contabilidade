﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

using ConTime.PersonalizedComponents;

namespace ConTime.Classes
{
    internal class LDia
    {
        private List<LDiaRegistro> Registros = new();
        string cabecario = "Lorem Ipsum";
        private class LDiaRegistro
        {
            public string codigo;
            public DateTime data;
            public string conta;
            public string? historico;
            public float debito;
            public float credito;

            public LDiaRegistro(DataRow dr)
            {
                codigo = Convert.ToString(dr["codigo"]);
                data = Convert.ToDateTime(dr["data"]);
                conta = Convert.ToString(dr["conta"]);
                historico = Convert.ToString(dr["historico"]);
                float.TryParse(dr["credito"].ToString(), out credito);
                float.TryParse(dr["debito"].ToString(), out debito);

            }

            public void CreateCell(TableDescriptor table)
            {
                table.Cell().Element(PdfComponents.Cell).Text(codigo);
                table.Cell().Element(PdfComponents.Cell).Text($"{data:d}");
                table.Cell().Element(PdfComponents.Cell).Text(conta);
                table.Cell().Element(PdfComponents.Cell).Text(historico);
                table.Cell().Element(PdfComponents.Cell).Text($"{credito:C}");
                table.Cell().Element(PdfComponents.Cell).Text($"{debito:C}");
            }
        }

        public LDia(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if(dr != dt.NewRow())
                {
                    Registros.Add(new LDiaRegistro(dr));
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
                            columns.ConstantColumn(75); //Tamanho fixo
                            columns.RelativeColumn(); //fill
                            columns.RelativeColumn(2);
                            columns.ConstantColumn(90); 
                            columns.ConstantColumn(90); //Tamanho fixo
                        });
                        table.Header(header =>
                        {
                            header.Cell().ColumnSpan(6).Background("#ffffff").Text(cabecario).FontColor("#000000").AlignCenter();
                            header.Cell().Element(PdfComponents.Header).Text("Codigo").FontColor(PdfComponents.HeaderFColor);
                            header.Cell().Element(PdfComponents.Header).Text("Data").FontColor(PdfComponents.HeaderFColor);
                            header.Cell().Element(PdfComponents.Header).Text("Conta").FontColor(PdfComponents.HeaderFColor);
                            header.Cell().Element(PdfComponents.Header).Text("Historico").FontColor(PdfComponents.HeaderFColor);
                            header.Cell().Element(PdfComponents.Header).Text("Credito").FontColor(PdfComponents.HeaderFColor);
                            header.Cell().Element(PdfComponents.Header).Text("Debito").FontColor(PdfComponents.HeaderFColor);
                        });

                        foreach(var registro in Registros)
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
