using System;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;

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
        private List<BPRegistro>[] listas;
        public float TotalAtivo { get; set; }
        public float TotalPassivo { get; set; }

        public BPat(DataSet ds, string header, float totalAtivo, float totalPassivo)
        {
            this.Header = header;
            this.TotalAtivo = totalAtivo;
            this.TotalPassivo = totalPassivo;
            listas = new List<BPRegistro>[] { AtvCirculante, AtvNCirculante, PsvCirculante, PsvNCirculante, Patrimonio };

            foreach (KeyValuePair<short, string> rTable in BPRegistro.rkey)
            {
                DataTable dt = ds.Tables[rTable.Value];
                foreach (DataRow dr in dt.Rows)
                {
                    listas[rTable.Key].Add(new BPRegistro(dr, rTable.Key));
                }
            }
        }


        #region PDF
        public MemoryStream PdfCreate()
        {
            var pdfdoc = CreatePdf();

            MemoryStream ms = new MemoryStream();

            pdfdoc.Save(ms);

            ms.Seek(0, SeekOrigin.Begin);

            return ms;
        }

        public PdfDocument CreatePdf()
        {
            PdfDocument pdfdoc = new PdfDocument();
            PdfPage page = pdfdoc.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 12);
            XFont fontBold = new XFont("Arial", 12, XFontStyle.Bold);

            double x = 50;
            double y = 50;

            gfx.DrawString("Balanço Patrimonial", fontBold, XBrushes.Black, new XRect(x, y, page.Width, page.Height), XStringFormats.TopCenter);
            y += 30;

            gfx.DrawString("Ativos", fontBold, XBrushes.Black, x, y);
            y += 20;

            var ativos = new Dictionary<string, List<BPRegistro>>()
    {
        {"Ativo Circulante", AtvCirculante},
        {"Ativo não Circulante", AtvNCirculante}
    };

            foreach (var tabela in ativos)
            {
                gfx.DrawString(tabela.Key, fontBold, XBrushes.Black, x, y);
                y += 20;

                gfx.DrawString("Conta", fontBold, XBrushes.Black, x, y);
                gfx.DrawString("Saldo", fontBold, XBrushes.Black, x + 300, y); 
                y += 20;

                foreach (var registro in tabela.Value)
                {
                    gfx.DrawString(registro.Conta, font, XBrushes.Black, x, y);
                    gfx.DrawString($"{registro.Saldo:C}", font, XBrushes.Black, x + 300, y); 
                    y += 20;
                }

                y += 10; 
            }

            gfx.DrawString("Passivos", fontBold, XBrushes.Black, x, y);
            y += 20;

            var passivos = new Dictionary<string, List<BPRegistro>>()
    {
        {"Passivo Circulante", PsvCirculante},
        {"Passivo não Circulante", PsvNCirculante},
        {"Patrimônio", Patrimonio}
    };

            foreach (var tabela in passivos)
            {
                gfx.DrawString(tabela.Key, fontBold, XBrushes.Black, x, y);
                y += 20;

                gfx.DrawString("Conta", fontBold, XBrushes.Black, x, y);
                gfx.DrawString("Saldo", fontBold, XBrushes.Black, x + 300, y);
                y += 20;

                foreach (var registro in tabela.Value)
                {
                    gfx.DrawString(registro.Conta, font, XBrushes.Black, x, y);
                    gfx.DrawString($"{registro.Saldo:C}", font, XBrushes.Black, x + 300, y);
                    y += 20; 
                }

                y += 10; 
            }

            y += 10; 

            gfx.DrawString("Total Ativos:", fontBold, XBrushes.Black, x, y);
            gfx.DrawString($"{TotalAtivo:C}", font, XBrushes.Black, x + 300, y);  
            y += 20;

            gfx.DrawString("Total Passivos:", fontBold, XBrushes.Black, x, y);
            gfx.DrawString($"{TotalPassivo:C}", font, XBrushes.Black, x + 300, y); 
            y += 20;

            return pdfdoc;
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

        public float Saldo; // { get; private set; }
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

        public BPRegistro(DataRow dr, short key)
        {
            float.TryParse(Convert.ToString(dr["saldo"]), out this.Saldo);
            this.Conta = Convert.ToString(dr["conta"]);
            this.RotuloKey = key;
            this.Rotulo = rkey[RotuloKey];
        }

        #region PDF
        public void CreateCell(XGraphics gfx, XFont font, double x, ref double y)
        {
            gfx.DrawString(this.Conta, font, XBrushes.Black, x, y);
            gfx.DrawString($"{this.Saldo:C}", font, XBrushes.Black, x + 150, y);

            y += 20; 
        }
        #endregion
    }
}