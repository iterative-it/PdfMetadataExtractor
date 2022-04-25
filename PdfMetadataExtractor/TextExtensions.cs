namespace PdfMetadataExtractor;

public static class TextExtensions
{
    public static string GetValue(this string text, string key)
    {
        key = $"{key}: ";

        var reader = new StringReader(text);

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            if (line.Contains(key))
            {
                return line.Split(key)[1].Split(" - ")[0];
            }
        }

        return "";
    }
}