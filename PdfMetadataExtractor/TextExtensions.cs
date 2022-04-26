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

    public static string GetRecipientName(this string recipient)
    {
        if (string.IsNullOrWhiteSpace(recipient)) return "";

        var reader = new StringReader(recipient);

        var lines = new List<string>();

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            lines.Add(line);
        }

        return string.Join(" ", lines.Take(lines.Count - 1));
    }

    public static string GetRecipientAddress(this string recipient)
    {
        if (string.IsNullOrWhiteSpace(recipient)) return "";

        var reader = new StringReader(recipient);

        var lines = new List<string>();

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            lines.Add(line);
        }

        return lines.Last();
    }
}