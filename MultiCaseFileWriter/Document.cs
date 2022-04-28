using CsvHelper.Configuration.Attributes;

namespace MultiCaseFileWriter;

public class Document
{

    [Name("Doc Location")]
    public string DocLocation { get; set; }
    [Name("Filename")]
    public string Filename { get; set; }
    [Name("ISCIS Case Ref")]
    public string IscisCaseRef { get; set; }
    [Name("ECF Class")]
    public string EcfClass { get; set; }
    [Name("Doc Title")]
    public string DocTitle { get; set; }
    [Name("Addressee")]
    public string Addressee { get; set; }
    [Name("From")]
    public string From { get; set; }
}