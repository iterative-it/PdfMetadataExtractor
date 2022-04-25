using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace PdfMetadataExtractor;

public static class PdfExtensions
{
    public static IDictionary<string, string> GetMetadata(this PdfDocument document)
    {
        var metadata = new Dictionary<string, string>
        {
            ["PDF.PageCount"] = $"{document.GetNumberOfPages():D}",
            ["PDF.Version"] = $"{document.GetPdfVersion()}"
        };

        var pdfTrailer = document.GetTrailer();
        var pdfDictInfo = pdfTrailer.GetAsDictionary(PdfName.Info);
        foreach (var pdfEntryPair in pdfDictInfo.EntrySet())
        {
            var key = "PDF." + pdfEntryPair.Key.ToString().Substring(1);
            string value;
            switch (pdfEntryPair.Value)
            {
                case PdfString pdfString:
                    value = pdfString.ToUnicodeString();
                    break;
                default:
                    value = pdfEntryPair.Value.ToString();
                    break;
            }
            metadata[key] = value;
        }

        return metadata;
    }

    public static string GetPageText(this PdfDocument document, int page = 1)
    {
        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
        var text = PdfTextExtractor.GetTextFromPage(document.GetPage(page), strategy);
        return text;
    }
}