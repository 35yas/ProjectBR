using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using System.IO;

namespace LightweightPlatform
{
    public static class PDFGenerator
    {
        public static void GenerateReport(string filePath, string[] steps)
        {
            var doc = new PdfDocument();
            var page = doc.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Arial", 12);

            gfx.DrawString("Test Report", font, XBrushes.Black, 50, 50);

            int yOffset = 70;
            foreach (var step in steps)
            {
                gfx.DrawString(step, font, XBrushes.Black, 50, yOffset);
                yOffset += 20;
            }

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            doc.Save(filePath);
        }
    }
}
