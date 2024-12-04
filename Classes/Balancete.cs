using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf.IO;

namespace ConTime.Classes
{
    public class Balancete
    {
        public List<BalanceteRegistro> Registros = new();
        string cabecario = "Balancete";

        public class BalanceteRegistro
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

            // Método para desenhar as células
            public void DrawCells(XGraphics gfx, XFont font, double yPosition, double xPosition)
            {
                // Posições X para as colunas, agora com "Débito" antes de "Crédito"
                double[] xPositions = { xPosition, xPosition + 75, xPosition + 150, xPosition + 225, xPosition + 300 }; // Ajustado para 5 colunas

                // Conteúdo das células, com "Débito" antes de "Crédito"
                string[] cellContents = {
                codigo,
                conta,
                $"{debito:C}",
                $"{credito:C}",
                $"{saldo:C}"
            };

                // Desenhando as células com bordas e texto centralizado
                for (int i = 0; i < cellContents.Length; i++)
                {
                    gfx.DrawRectangle(XPens.Black, xPositions[i], yPosition, 75, 20); // Borda de cada célula
                    gfx.DrawString(cellContents[i], font, XBrushes.Black, new XRect(xPositions[i], yPosition, 75, 20), XStringFormats.Center); // Texto centralizado
                }
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

        public MemoryStream CreatePDF()
        {
            // Criando um fluxo de memória para armazenar o PDF
            MemoryStream pdfStream = new MemoryStream();
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 10);

            double yPosition = 20;
            double xPosition = 30; // Inicializando a posição X da tabela
            double tableWidth = 75 * 5; // Largura da tabela (5 colunas)
            double pageWidth = page.Width; // Largura da página

            // Centralizando a tabela na página
            xPosition = (pageWidth - tableWidth) / 2; // Calculando a posição X para centralizar

            // Cabeçalho da Tabela
            gfx.DrawString(cabecario, new XFont("Arial", 14, XFontStyle.Bold), XBrushes.Black, new XRect(0, yPosition, page.Width, 20), XStringFormats.Center);
            yPosition += 25;

            // Desenhando a primeira linha do cabeçalho da tabela (ajustado para 5 colunas)
            gfx.DrawRectangle(XPens.Black, xPosition, yPosition, 75 * 5, 20); // Linha do cabeçalho
            gfx.DrawString("Código", font, XBrushes.Black, new XRect(xPosition, yPosition, 75, 20), XStringFormats.Center);
            gfx.DrawString("Conta", font, XBrushes.Black, new XRect(xPosition + 75, yPosition, 75, 20), XStringFormats.Center);
            gfx.DrawString("Débito", font, XBrushes.Black, new XRect(xPosition + 150, yPosition, 75, 20), XStringFormats.Center);
            gfx.DrawString("Crédito", font, XBrushes.Black, new XRect(xPosition + 225, yPosition, 75, 20), XStringFormats.Center);
            gfx.DrawString("Saldo", font, XBrushes.Black, new XRect(xPosition + 300, yPosition, 75, 20), XStringFormats.Center);
            yPosition += 25;

            // Não há mais a linha de separação do cabeçalho, conforme solicitado

            // Desenhando as linhas de dados da tabela
            foreach (var registro in Registros)
            {
                registro.DrawCells(gfx, font, yPosition, xPosition);
                yPosition += 25;

                // Desenhando a linha após cada linha de dados
                gfx.DrawLine(XPens.Black, xPosition, yPosition, xPosition + 75 * 5, yPosition); // Linha horizontal entre os dados
            }

            // Linha de demarcação no final
            gfx.DrawLine(XPens.Black, xPosition, yPosition, xPosition + 75 * 5, yPosition); // Linha de demarcação final

            // Salvando o PDF em um fluxo de memória
            document.Save(pdfStream);
            pdfStream.Position = 0; // Ajuste para garantir que o fluxo seja lido do início

            return pdfStream;
        }

        public MemoryStream PdfCreate()
        {
            // Gerar o PDF com o método CreatePDF
            return CreatePDF();
        }

    }
}
#endregion