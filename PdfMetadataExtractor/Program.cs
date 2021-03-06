using System.Collections;
using System.Globalization;
using CsvHelper;
using iText.Kernel.Pdf;
using PdfMetadataExtractor;

var path = Environment.GetCommandLineArgs()[1];

var directoryInfo = new DirectoryInfo(path);

if (!directoryInfo.Exists)
{
    throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + path);
}

var documents = new List<Document>();

foreach (var fileInfo in directoryInfo.GetFiles("*.pdf").OrderBy(f => f.Name))
{
    using var pdfReader = new PdfReader(fileInfo.FullName);
    using var pdfDocument = new PdfDocument(pdfReader);

    var metadata = pdfDocument.GetMetadata();
    var text = pdfDocument.GetPageText();

    foreach (var (key, value) in metadata)
    {
        Console.WriteLine($"{key}: {value}");
    }

    Console.WriteLine(text);

    var recipient = pdfDocument.GetRecipient();

    documents.Add(new Document
    {
        Filename = fileInfo.Name,
        DocId = pdfDocument.GetDocId(),
        RecipientName = recipient.GetRecipientName(),
        RecipientAddress = recipient.GetRecipientAddress(),
        YourRef = text.GetValue("Your ref"),
        OurRef = text.GetValue("Our ref"),
        DirectLine = text.GetValue("Direct Line"),
        Email = text.GetValue("E-mail"),
        Date = text.GetValue("Date")
    });
}

var now = DateTime.Now;
using var writer = new StreamWriter($"documents_{now.Year}{now.Month}{now.Day}_{now.Hour}{now.Minute}.csv");
using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
csv.WriteRecords((IEnumerable) documents);