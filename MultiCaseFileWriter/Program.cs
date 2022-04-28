// See https://aka.ms/new-console-template for more information

using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using MultiCaseFileWriter;

var path = Environment.GetCommandLineArgs()[1];

var fileInfo = new FileInfo(path);

if (!fileInfo.Exists)
{
    throw new DirectoryNotFoundException("Input file does not exist or could not be found: " + path);
}

var config = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    PrepareHeaderForMatch = args => args.Header.Trim()
};

using var reader = new StreamReader(fileInfo.OpenRead());
using var csv = new CsvReader(reader, config);

var documents = csv.GetRecords<Document>();

var sb = new StringBuilder();
sb.AppendLine("# Register Case Documents Definition File");
sb.AppendLine("#");

foreach (var document in documents)
{
    sb.AppendLine("#############################################################");
    sb.AppendLine($"Doc Location      : {document.DocLocation}");
    sb.AppendLine($"  File Name       : {document.Filename}");
    sb.AppendLine($"  ISCIS Case Ref  : {document.IscisCaseRef}");
    sb.AppendLine($"  ECF Class       : {document.EcfClass}");
    sb.AppendLine($"  Doc Title       : {document.DocTitle}");
    sb.AppendLine($"  Addressee       : {document.Addressee}");
    sb.AppendLine($"  From            : {document.From}");
}

Console.WriteLine(sb.ToString());

var now = DateTime.Now;
File.WriteAllText($"MultiCaseFiles_{now.Year}{now.Month}{now.Day}_{now.Hour}{now.Minute}.txt", sb.ToString());