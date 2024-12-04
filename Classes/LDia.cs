using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;

namespace ConTime.Classes
{
    public class LDia
    {
        public List<LDiaRegistro> Registros = new();
        string cabecario = "Livro Diário";

        public class LDiaRegistro
        {
            public string codigo;
            public DateTime data;
            public string conta;
            public string? historico;
            public float debito;
            public float credito;
            public float saldo;

            public LDiaRegistro(DataRow dr)
            {
                codigo = Convert.ToString(dr["Código"]);
                data = Convert.ToDateTime(dr["Data"]);
                conta = Convert.ToString(dr["Conta"]);
                historico = Convert.ToString(dr["Histórico"]);
                float.TryParse(dr["Crédito"].ToString(), out credito);
                float.TryParse(dr["Débito"].ToString(), out debito);
                float.TryParse(dr["Saldo"].ToString(), out saldo);
            }

            public void DrawCells(XGraphics gfx, XFont font, double yPosition)
            {
                // Desenhando as células com bordas
                double[] xPositions = { 30, 105, 180, 255, 330, 405, 480 }; // Posições X para as colunas
                string[] cellContents = {
                    data.ToString("d"),
                    codigo,
                    conta,
                    historico ?? "",
                    $"{credito:C}",
                    $"{debito:C}",
                    $"{saldo:C}"
                };

                // Desenhando as células
                for (int i = 0; i < cellContents.Length; i++)
                {
                    gfx.DrawRectangle(XPens.Black, xPositions[i], yPosition, 75, 20); // Desenhando borda de cada célula
                    gfx.DrawString(cellContents[i], font, XBrushes.Black, new XRect(xPositions[i], yPosition, 75, 20), XStringFormats.Center); // Texto centralizado
                }
            }
        }

        public LDia(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr != dt.NewRow())
                {
                    Registros.Add(new LDiaRegistro(dr));
                }
            }
        }

        #region PDF

        public MemoryStream PdfCreate()
        {
            // Criando um fluxo de memória para armazenar o PDF
            MemoryStream pdfStream = new MemoryStream();
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 10);

            double yPosition = 20;
            double xPosition = 30; // Inicializando o xPosition para a tabela

            // Cabeçalho da Tabela
            gfx.DrawString(cabecario, new XFont("Arial", 14, XFontStyle.Bold), XBrushes.Black, new XRect(0, yPosition, page.Width, 20), XStringFormats.Center);
            yPosition += 25;

            // Desenhando a primeira linha do cabeçalho da tabela
            gfx.DrawRectangle(XPens.Black, xPosition, yPosition, 75 * 7, 20); // Linha do cabeçalho
            gfx.DrawString("Data", font, XBrushes.Black, new XRect(xPosition, yPosition, 75, 20), XStringFormats.Center);
            gfx.DrawString("Código", font, XBrushes.Black, new XRect(xPosition + 75, yPosition, 75, 20), XStringFormats.Center);
            gfx.DrawString("Conta", font, XBrushes.Black, new XRect(xPosition + 150, yPosition, 75, 20), XStringFormats.Center);
            gfx.DrawString("Histórico", font, XBrushes.Black, new XRect(xPosition + 225, yPosition, 75, 20), XStringFormats.Center);
            gfx.DrawString("Crédito", font, XBrushes.Black, new XRect(xPosition + 300, yPosition, 75, 20), XStringFormats.Center);
            gfx.DrawString("Débito", font, XBrushes.Black, new XRect(xPosition + 375, yPosition, 75, 20), XStringFormats.Center);
            gfx.DrawString("Saldo", font, XBrushes.Black, new XRect(xPosition + 450, yPosition, 75, 20), XStringFormats.Center);
            yPosition += 25;

            // Desenhando as linhas de dados da tabela
            foreach (var registro in Registros)
            {
                registro.DrawCells(gfx, font, yPosition);
                yPosition += 25;

                // Desenhando a linha após cada linha de dados
                gfx.DrawLine(XPens.Black, xPosition, yPosition, xPosition + 75 * 7, yPosition); // Linha horizontal entre os dados
            }

            // Salvando o PDF em um fluxo de memória
            document.Save(pdfStream);
            pdfStream.Position = 0; // Ajuste para garantir que o fluxo seja lido do início

            return pdfStream;
        }

        #endregion
    }
}
