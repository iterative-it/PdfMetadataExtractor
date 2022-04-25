using System.Diagnostics;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

// See https://aka.ms/new-console-template for more information

//var metainfo = await GetMetaInfoAsync(@"");

/*foreach (var info in metainfo.MetaInfo)
{
    Console.WriteLine($"{info.Key}: {info.Value}");
}*/

var pageText = ExtractTextFromPDF(@"", 1);
Console.WriteLine(pageText);

static async Task<(Dictionary<string, string> MetaInfo, string Error)> GetMetaInfoAsync(string path)
{
    try
    {
        var metaInfo = await Task.Run(() =>
        {
            var metaInfoDict = new Dictionary<string, string>();
            using (var pdfReader = new PdfReader(path))
            using (var pdfDocument = new PdfDocument(pdfReader))
            {
                metaInfoDict["PDF.PageCount"] = $"{pdfDocument.GetNumberOfPages():D}";
                metaInfoDict["PDF.Version"] = $"{pdfDocument.GetPdfVersion()}";

                var pdfTrailer = pdfDocument.GetTrailer();
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
                    metaInfoDict[key] = value;
                }
                return metaInfoDict;
            }
        });
        return (metaInfo, null);
    }
    catch (Exception ex)
    {
        if (Debugger.IsAttached) Debugger.Break();
        return (null, ex.Message);
    }
}

static string ExtractTextFromPDF(string filePath, int page)
{
    PdfReader pdfReader = new PdfReader(filePath);
    PdfDocument pdfDoc = new PdfDocument(pdfReader);
    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
    string pageContent = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page), strategy);
    pdfDoc.Close();
    pdfReader.Close();
    return pageContent;
}
