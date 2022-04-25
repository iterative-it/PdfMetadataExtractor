using iText.Kernel.Pdf;
using PdfMetadataExtractor;

var path = Environment.GetCommandLineArgs()[1];

var directoryInfo = new DirectoryInfo(path);

if (!directoryInfo.Exists)
{
    throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + path);
}

foreach (var fileInfo in directoryInfo.GetFiles())
{
    var fileName = fileInfo.Name;

    using var pdfReader = new PdfReader(fileInfo.FullName);
    using var pdfDocument = new PdfDocument(pdfReader);

    var metadata = pdfDocument.GetMetadata();

    foreach ((var key, var value) in metadata)
    {
        Console.WriteLine($"{key}: {value}");
    }

    Console.WriteLine(pdfDocument.GetPageText());
}
