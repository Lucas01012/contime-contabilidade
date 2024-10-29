using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlX.XDevAPI.Relational;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

using ConTime.PersonalizedComponents;

namespace ConTime.Classes
{

    internal class Razonetes
    {

        string cabecario = "Lorem Ipsum";
        string id;
        List<Razo> razonetes = new();

        public void AddRazonete(Razo razonete)
        {
            razonetes.Add(razonete);
        }
        #region PDF
        public void PdfCreate()
        {
            var pdfdoc = CreatePDF();
            pdfdoc.GeneratePdfAndShow();
        }
        private Document CreatePDF()
        {
            string
                HeaderBGColor = "#000000", HeaderFColor = "#ffffff",
                LabelBGColor = "#aaaaaa", LabelFColor = "#000000", LabelBorderColor = "#666666",
                CellBGColor = "#ffffff", CellFColor = "#000000";

            /*void CreateTable(IContainer container, Razo razo)
            {
                container
                        .Border(1).BorderColor("#000000")
                        .Table(t =>
                        {
                            void addcell(string? value) => t.Cell().Border(1).BorderColor("#000000").Background(CellBGColor).Text(value).FontColor(CellFColor).AlignCenter();
                            void addlabel(string? value) => t.Cell().ColumnSpan(2).Border(1).BorderColor(LabelBorderColor).Background(LabelBGColor).Text(value).FontColor(LabelFColor);
                            void addempty() => t.Cell().ColumnSpan(2).Border(1).BorderColor("#000000").Background(CellBGColor);

                            t.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();

                            });
                            t.Header(header =>
                            {
                                header.Cell().ColumnSpan(2).BorderColor("#ffffff").Background(HeaderBGColor).Text(razo.Header).FontColor(HeaderFColor).AlignCenter();
                                header.Cell().ColumnSpan(1).Border(1).BorderColor("#ffffff").Background(HeaderBGColor).Text("Debito").FontColor(HeaderFColor);
                                header.Cell().ColumnSpan(1).Border(1).BorderColor("#ffffff").Background(HeaderBGColor).Text("Credito").FontColor(HeaderFColor);
                            });

                            foreach (Razo.RazoRegistro linha in razo.registros)
                            {
                                addcell($"{linha.debito:C}");
                                addcell($"{linha.credito:C}");
                            }
                            t.Footer(footer =>
                            {
                                footer.Cell().ColumnSpan(1).Border(1).BorderColor("#000000").Background(HeaderBGColor).Text($"{razo.totalD:C}").FontColor(HeaderFColor).AlignCenter();
                                footer.Cell().ColumnSpan(1).Border(1).BorderColor("#000000").Background(HeaderBGColor).Text($"{razo.totalC:C}").FontColor(HeaderFColor).AlignCenter();
                            });
                        });
            }*/
            return Document.Create(container =>
            {
                container.Page(page => //Eu sinceramente não entendo como paginas funcionam // Eu Agora entendo mais ou menos
                {
                    page.Content().Column(c => //Confusão mental
                    {
                        int count = razonetes.Count;
                        int footerCount = count;
                        int rounds = (int)Math.Ceiling((double)(count / 4f));
                        int index = 0;
                        int footerIndex = index;
                        for (var i = 0; i < rounds; i++)
                        {
                            c.Item().Height(10);
                            c.Item().Row(rowColumn => //Coluna que contem os Razonetes
                            {
                                for (var j = 0; j < 4 && count != 0; j++)
                                {
                                    count--;
                                    rowColumn.ConstantItem(10);
                                    rowColumn.ConstantItem(136).Border(1).Background(PdfComponents.TableHeaderBGColor).Column(row => //Linha da coluna que contém o cabeçalho, a tabela e os totais
                                    {
                                        razonetes[index].CreateHeader(row.Item().Background(HeaderFColor));
                                        razonetes[index].CreateTable(row.Item());
                                    });
                                    index++;
                                }
                                rowColumn.ConstantItem(10);
                            });
                            c.Item().Row(rowColumn => //Coluna que contem os Razonetes
                            {
                                for (var j = 0; j < 4 && footerCount != 0; j++)
                                {
                                    footerCount--;
                                    rowColumn.ConstantItem(10);
                                    rowColumn.ConstantItem(136).Column(row => //Linha da coluna que contém o cabeçalho, a tabela e os totais
                                    {
                                        razonetes[footerIndex].CreateTableTotals(row.Item());
                                    });
                                    footerIndex++;
                                }
                                rowColumn.ConstantItem(10);
                            });
                        }

                    });
                });
            });
        }
    }
    #endregion
    public class Razo
    {
        public string razoid;
        internal string Header;
        public List<RazoRegistro> registros = new();
        internal float totalD;
        internal float totalC;
        private DataSet ds;
        private TextBox header;
        #region Pdf
        public void CreateHeader(IContainer container)
        {
            container
                .Border(1)
                .Text(Header)
                .AlignCenter();
        }
        public void CreateTable(IContainer container)
        {
            container
                        .Border(1).BorderColor("#000000")
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });
                            table.Header(header =>
                            {
                                header.Cell().Element(PdfComponents.Header).Text("Debito").FontColor(PdfComponents.HeaderFColor);
                                header.Cell().Element(PdfComponents.Header).Text("Credito").FontColor(PdfComponents.HeaderFColor);
                            });
                            foreach(var registro in registros)
                            {
                                registro.CreateCell(table);
                            }
                        });
        }
        public void CreateTableTotals(IContainer container)
        {
            container
                .Border(1)
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });
                    table.Header(header =>
                    {
                        header.Cell().Element(PdfComponents.Header).Text($"{totalD:C}").FontColor(PdfComponents.HeaderFColor);
                        header.Cell().Element(PdfComponents.Header).Text($"{totalC:C}").FontColor(PdfComponents.HeaderFColor);
                    });
                });
        }
        #endregion

     
        public Razo(DataSet ds, String Cabecario)
        {
            Header = Cabecario;
            DataTable dt = ds.Tables[Screens.Razo.tablename];
            DataTable dl = ds.Tables[Screens.Razo.linhasname];

            foreach (DataRow dr in dl.Rows)
            {
                var temp = new RazoRegistro(dr);
                registros.Add(temp);
                totalC += temp.credito;
                totalD += temp.debito;
            }

            if (dt.Rows.Count > 0)
            {
                DataRow? totais = dt.Rows[0];
                float.TryParse(Convert.ToString(totais["Total_Debito"]), out this.totalD);
                float.TryParse(Convert.ToString(totais["Total_Credito"]), out this.totalC);
            }

        }

        public Razo(DataSet ds, TextBox header)
        {
            this.ds = ds;
            this.header = header;
        }

        public class RazoRegistro
        {
            public float debito;
            public float credito;
            #region Pdf
            public void CreateCell(TableDescriptor table)
            {
                table.Cell().Element(PdfComponents.Cell).Text($"{debito:C}");
                table.Cell().Element(PdfComponents.Cell).Text($"{credito:C}");
            }
            #endregion
            public RazoRegistro(DataRow dr)
            {
                float.TryParse(Convert.ToString(dr["Debito"]), out this.debito);
                float.TryParse(Convert.ToString(dr["Credito"]), out this.credito);
            }
        }
    }
}
