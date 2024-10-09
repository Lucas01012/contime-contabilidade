using System;
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
    internal class BPat
    {
        private int id;
        private string Header;
        public List<BPRegistro> AtvCirculante = new();
        public List<BPRegistro> AtvNCirculante = new();
        public List<BPRegistro> PsvCirculante = new();
        public List<BPRegistro> PsvNCirculante = new();
        public List<BPRegistro> Patrimonio = new();
        private List<BPRegistro>[] listas = new List<BPRegistro>[5];
        public float TotalAtivo;
        public float TotalPassivo;

        public BPat(DataSet ds, string header, float TAtivo, float TPassivo)
        {
            listas = [AtvCirculante, AtvNCirculante, PsvCirculante, PsvNCirculante, Patrimonio];
            this.Header = header;
            this.TotalAtivo = TAtivo;
            this.TotalPassivo = TPassivo;
            foreach (KeyValuePair<short, string> rTable in BPRegistro.rkey)
            {
                DataTable dt = ds.Tables[rTable.Value];
                foreach(DataRow dr in dt.Rows)
                {
                    listas[rTable.Key].Add(new BPRegistro(dr, rTable.Key));
                }
            }
        }
        #region PDF
        public void PdfCreate()
        {
            var pdfdoc = CreatePdf();
            pdfdoc.GeneratePdfAndShow();
        }
        private Document CreatePdf()
        {
            Dictionary<string, List<BPRegistro>> ativos = new()
            { 
                {"Ativo Circulante", AtvCirculante}, 
                {"Ativo não Circulante", AtvNCirculante}
            };
            Dictionary<string, List<BPRegistro>> passivos = new()
            { 
                {"Passivo Circulante", PsvCirculante}, 
                {"Passivo não Circulante", PsvNCirculante},
                {"Patrimônio", Patrimonio}
            };
            Dictionary<string, Dictionary<string, List<BPRegistro>>> colunas = new()
            {
                {"Ativos", ativos},
                {"Passivos", passivos}
            };

            return Document.Create(container =>
            {
                

                void CreateTableLegend(IContainer container)
                {
                    container
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.ConstantColumn(75);
                            });
                            table.Header(header =>
                            {
                                header.Cell().Element(PdfComponents.Header).Text("Conta").FontColor(PdfComponents.HeaderFColor);
                                header.Cell().Element(PdfComponents.Header).Text("Saldo").FontColor(PdfComponents.HeaderFColor);
                            });
                        });
                }
                void CreateTableTotals(IContainer container, float total)
                {
                    container
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.ConstantColumn(75);
                            });
                            table.Header(header =>
                            {
                                header.Cell().Element(PdfComponents.Header).Text("Total:").FontColor(PdfComponents.HeaderFColor);
                                header.Cell().Element(PdfComponents.Header).Text($"{total:C}").FontColor(PdfComponents.HeaderFColor);
                            });
                        });
                }

                void CreateTable(IContainer container, KeyValuePair<string, List<BPRegistro>> tabela)
                {
                    container
                        .Border(1)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.ConstantColumn(75);
                            });
                            table.Header(header =>
                            {
                                header.Cell().ColumnSpan(2).Element(PdfComponents.TableHeader).Text(tabela.Key).FontColor(PdfComponents.TableHeaderFColor);
                            });
                            foreach(var registro  in tabela.Value)
                            {
                                registro.CreateCell(table);
                            }
                        });
                }

                container.Page(page =>
                {
                    page.Header().Text("Balanço Patrimônial").AlignCenter();
                    page.Content().Column(c =>
                    {
                        c.Item().Row(rowColumn =>
                        {
                            foreach(var coluna in colunas)
                            {
                                rowColumn.RelativeItem().Column(r =>
                                {
                                    r.Item().Element(PdfComponents.Header).Text(coluna.Key).FontColor(PdfComponents.HeaderFColor);
                                    CreateTableLegend(r.Item());
                                    foreach(var tabela in coluna.Value)
                                    {
                                        CreateTable(r.Item(), tabela);
                                    }
                                });
                            }
                        c.Item().Row(totais =>
                            {
                                CreateTableTotals(totais.RelativeItem(), TotalAtivo);
                                CreateTableTotals(totais.RelativeItem(), TotalPassivo);
                            });
                        });
                    });
                });
            });
        }
        #endregion
    }
    class BPRegistro
    {
        readonly public static Dictionary<short, string> rkey = new()
        {
            {0, "AtvCirculante"},
            {1, "AtvNCirculante"},
            {2, "PsvCirculante"},
            {3, "PsvNCirculante"},
            {4, "Patrimonio"}
        };

        #region PDF
        public void CreateCell(TableDescriptor table)
        {
            table.Cell().Element(PdfComponents.Cell).Text(this.Conta);
            table.Cell().Element(PdfComponents.Cell).Text($"{this.Saldo:C}");
        }
        #endregion

        public float Saldo;// { get; private set; }
        public string Conta { get; private set; } = "";
        public short RotuloKey { get; private set; }
        public string Rotulo { get; private set; } = "";

        public BPRegistro(float saldo, string conta, short rotulokey)
        {
            this.Saldo = saldo;
            this.Conta = conta;
            this.RotuloKey = rotulokey;
            this.Rotulo = rkey[rotulokey];
        }
        //Testar depois
        public BPRegistro(DataRow dr, short key)
        {
            float.TryParse(Convert.ToString(dr["saldo"]), out this.Saldo);
            this.Conta = Convert.ToString(dr["conta"]);
            this.RotuloKey = key;
            this.Rotulo = rkey[RotuloKey];

            //MessageBox.Show($"{Rotulo}[{RotuloKey}]\t{Conta}: {Saldo}");
        }
    }
}
