using PdfSharpCore.Pdf.IO;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConTime.Classes
{
    public static class PdfUtil
    {
        public static MemoryStream CombinePDFs(List<MemoryStream> pdfStreams)
        {
            PdfDocument combinedPdf = new PdfDocument();

            foreach (var stream in pdfStreams)
            {
                if (stream == null || stream.Length == 0)
                {
                    throw new InvalidOperationException("O stream de PDF está vazio ou não foi inicializado.");
                }

                PdfDocument currentPdf = PdfReader.Open(stream, PdfDocumentOpenMode.Import);

                for (int pageIndex = 0; pageIndex < currentPdf.PageCount; pageIndex++)
                {
                    PdfPage page = currentPdf.Pages[pageIndex];
                    combinedPdf.AddPage(page);
                }
            }

            MemoryStream combinedStream = new MemoryStream();
            combinedPdf.Save(combinedStream);
            combinedStream.Position = 0;

            return combinedStream;
        }
    }
}
