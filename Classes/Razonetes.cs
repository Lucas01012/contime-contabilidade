using ConTime.Screens;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace ConTime.Classes
{
    public class Razonetes
    {
        public string id;
        string cabecario = "Lorem Ipsu";
        public  List<Razo> razonetes = new();
        private List<Razo> _razos;

        public void AddRazonete(Razo razonete)
        {
            razonetes.Add(razonete);
        }

        #region PDF
        public MemoryStream CreatePDF()
        {
            var pdfStream = new MemoryStream();
            PdfDocument pdfDoc = new PdfDocument();

            PdfPage page = pdfDoc.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            int margin = 20;
            int yPos = margin;
            int xPos = margin;
            int pageHeight = (int)page.Height.Point;
            int pageWidth = (int)page.Width.Point;
            int razoneteWidth = (pageWidth - 5 * margin) / 4;
            int maxRazoneteHeight = 0;
            int count = 0;



            foreach (var razonete in razonetes)
            {
                int razoneteHeight = razonete.GetHeight(gfx, razoneteWidth);
                maxRazoneteHeight = Math.Max(maxRazoneteHeight, razoneteHeight);

                if (count > 0 && count % 4 == 0)
                {
                    yPos += maxRazoneteHeight + margin;
                    xPos = margin;
                    maxRazoneteHeight = razoneteHeight;

                    if (yPos + razoneteHeight > pageHeight - margin)
                    {
                        page = pdfDoc.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        yPos = margin;
                    }
                }

                gfx.DrawRectangle(XPens.Black, xPos, yPos, razoneteWidth, razoneteHeight);
                gfx.DrawString(razonete.Header, new XFont("Helvetica", 12, XFontStyle.Bold), XBrushes.Black, new XRect(xPos + 5, yPos + 5, razoneteWidth, 20), XStringFormats.TopLeft);
                razonete.CreateTable(gfx, xPos + 5, yPos + 30, razoneteWidth - 10);

                xPos += razoneteWidth + margin;
                count++;
            }

            pdfDoc.Save(pdfStream, false);
            pdfStream.Position = 0;
            

            return pdfStream;
        }

        public MemoryStream PdfCreate()
        {
            // Gera o PDF e obtém o MemoryStream
            var pdfStream = CreatePDF();

            // Retorna o MemoryStream gerado
            return pdfStream;
        }


        public void VisualizarPDF(MemoryStream pdfStream)
        {
            if (pdfStream == null || pdfStream.Length == 0)
            {
                MessageBox.Show("Erro ao gerar o PDF. O stream está vazio.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string tempFilePath = Path.Combine(Path.GetTempPath(), "tempRazonetes.pdf");
            File.WriteAllBytes(tempFilePath, pdfStream.ToArray());

            var startInfo = new System.Diagnostics.ProcessStartInfo(tempFilePath)
            {
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(startInfo);
            #endregion
        }
        public void CarregarDados(List<Razo> razos)
        {
            _razos = razos;  // Armazena a lista de razonetes
        }


    }

    public class Razo
    {
        public string razoid;
        internal string Header;
        public List<RazoRegistro> registros = new();
        internal float totalD;
        internal float totalC;

        public Razo(DataSet ds, string cabecario)
        {
            Header = cabecario;
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
                DataRow totais = dt.Rows[0];
                float.TryParse(Convert.ToString(totais["Total_Debito"]), out totalD);
                float.TryParse(Convert.ToString(totais["Total_Credito"]), out totalC);
            }
        }
        public void CreateTableTotals(XGraphics gfx, int xPos, int yPos, int width)
        {
            int rowHeight = 20;
            int columnWidth = width / 2;

            // Desenhar o cabeçalho dos totais (rótulos)
            gfx.DrawString("Total Débito",
                new XFont("Helvetica", 10, XFontStyle.Bold),
                XBrushes.Black,
                new XRect(xPos + 5, yPos, columnWidth - 10, rowHeight),
                XStringFormats.TopLeft);

            gfx.DrawString("Total Crédito",
                new XFont("Helvetica", 10, XFontStyle.Bold),
                XBrushes.Black,
                new XRect(xPos + columnWidth + 5, yPos, columnWidth - 10, rowHeight),
                XStringFormats.TopLeft);

            // Desenhar linha horizontal abaixo das labels de totais
            gfx.DrawLine(XPens.Black, xPos, yPos + rowHeight, xPos + width, yPos + rowHeight);

            // Desenhar os valores dos totais
            yPos += rowHeight; // Avançar para a linha dos valores
            gfx.DrawString(
                totalD.ToString("C"),
                new XFont("Helvetica", 10, XFontStyle.Bold),
                XBrushes.Black,
                new XRect(xPos + 5, yPos, columnWidth - 10, rowHeight),
                XStringFormats.TopLeft);

            gfx.DrawString(
                totalC.ToString("C"),
                new XFont("Helvetica", 10, XFontStyle.Bold),
                XBrushes.Black,
                new XRect(xPos + columnWidth + 5, yPos, columnWidth - 10, rowHeight),
                XStringFormats.TopLeft);

            // Desenhar linha vertical que separa os dois valores
            gfx.DrawLine(XPens.Black, xPos + columnWidth, yPos - rowHeight, xPos + columnWidth, yPos + rowHeight);

            // Desenhar contorno ao redor dos totais
            gfx.DrawRectangle(
                XPens.Black,
                xPos,           // Linha começa na borda esquerda do painel
                yPos - rowHeight, // Parte superior do cabeçalho dos totais
                width,          // Largura total do painel
                rowHeight * 2); // Altura total dos dois totais (rótulo + valores)
        }

        public void CreateTable(XGraphics gfx, int xPos, int yPos, int width)
        {
            int rowHeight = 20;
            int currentY = yPos;
            int columnWidth = width / 2;

            // Cabeçalho das colunas
            gfx.DrawRectangle(XPens.Black, xPos, currentY, width, rowHeight);
            gfx.DrawString("Débito", new XFont("Helvetica", 10, XFontStyle.Bold), XBrushes.Black, new XRect(xPos + 5, currentY + 5, columnWidth, rowHeight), XStringFormats.TopLeft);
            gfx.DrawString("Crédito", new XFont("Helvetica", 10, XFontStyle.Bold), XBrushes.Black, new XRect(xPos + columnWidth + 5, currentY + 5, columnWidth, rowHeight), XStringFormats.TopLeft);
            currentY += rowHeight;

            // Adicionar registros
            foreach (var registro in registros)
            {
                gfx.DrawRectangle(XPens.Black, xPos, currentY, width, rowHeight);
                gfx.DrawString(registro.debito.ToString("C"), new XFont("Helvetica", 10), XBrushes.Black, new XRect(xPos + 5, currentY + 5, columnWidth, rowHeight), XStringFormats.TopLeft);
                gfx.DrawString(registro.credito.ToString("C"), new XFont("Helvetica", 10), XBrushes.Black, new XRect(xPos + columnWidth + 5, currentY + 5, columnWidth, rowHeight), XStringFormats.TopLeft);
                currentY += rowHeight;
            }

            // Adicionar espaço para totais dentro do razonete
            currentY += 5; // Pequeno espaço antes dos totais

            // Adicionar os totais ao final (dentro do razonete)
            CreateTableTotals(gfx, xPos, currentY, width);
        }

        public void CreatePanel(XGraphics gfx, int xPos, int yPos, int width)
        {
            int rowHeight = 20;

            // Desenha o restante do conteúdo dentro do painel
            CreateTable(gfx, xPos + 5, yPos + 30, width - 10);

            // Ajuste a altura do painel para incluir os totais e o cabeçalho
            int razoneteHeight = GetHeight(gfx, width);

            // Desenhar o painel completo com a altura correta por último
            gfx.DrawRectangle(XPens.Black, xPos, yPos, width, razoneteHeight);

            // Adicionar o cabeçalho
            gfx.DrawString(Header,
                new XFont("Helvetica", 12, XFontStyle.Bold),
                XBrushes.Black,
                new XRect(xPos + 5, yPos + 5, width - 10, 20),
                XStringFormats.TopLeft);
        }

        public int GetHeight(XGraphics gfx, int width)
        {
            int rowHeight = 20;       // Altura de cada linha
            int headerHeight = 30;    // Altura fixa do cabeçalho
            int totalHeight = rowHeight * 2; // Altura para os dois totais

            // Altura total: cabeçalho + registros + totais
            int height = headerHeight + (registros.Count * rowHeight) + totalHeight + 35; // Ajuste para incluir o espaço do cabeçalho

            return height;
        }




        public class RazoRegistro
        {
            public float debito;
            public float credito;

            public RazoRegistro(DataRow dr)
            {
                float.TryParse(Convert.ToString(dr["Debito"]), out debito);
                float.TryParse(Convert.ToString(dr["Credito"]), out credito);
            }
        }
    }
}
