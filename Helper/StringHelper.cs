namespace maulllanam_api_be.Helper;

public static class StringHelper
{
    public static string GenerateSlug(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) return "";

        string slug = title
            .Trim()
            .ToLowerInvariant()
            .Replace(" ", "-")
            .Replace(".", "")
            .Replace(",", "")
            .Replace(":", "")
            .Replace(";", "")
            .Replace("'", "")
            .Replace("\"", "")
            .Replace("&", "and");

        // Hapus karakter yang tidak valid (hanya alphanumeric dan dash)
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\-]", "");

        // Ganti multiple dashes jadi satu
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"-+", "-");

        return slug;
    }
}
