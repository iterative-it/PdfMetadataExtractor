using iText.Kernel.Pdf;
using iText.Kernel.XMP;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

PdfDocument pdfDoc = new PdfDocument(new PdfReader(@""));

XMPUtils.

pdfDoc.GetXmpMetadata

PdfPage page = pdfDoc.GetFirstPage();
//page.SetXmpMetadata(XMPMetaFactory.Create());

pdfDoc.Close();
