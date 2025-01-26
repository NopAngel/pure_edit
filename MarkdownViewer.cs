using System;
using System.IO;

public static class MarkdownViewer
{
    public static void ViewMarkdown(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Markdown file not found.");
            return;
        }

        string markdownContent = File.ReadAllText(filePath);
        string htmlContent = ConvertMarkdownToHtml(markdownContent);

        DisplayHtml(htmlContent);
    }

    public static string ConvertMarkdownToHtml(string markdown)
    {
        // Aquí puedes utilizar alguna librería de conversión Markdown a HTML
        // Este es un ejemplo básico de conversión
        string html = markdown
            .Replace("# ", "<h1>")
            .Replace("## ", "<h2>")
            .Replace("### ", "<h3>")
            .Replace("#### ", "<h4>")
            .Replace("##### ", "<h5>")
            .Replace("###### ", "<h6>")
            .Replace("**", "<b>")
            .Replace("_", "<i>")
            .Replace("\n", "<br>");

        return html;
    }

    private static void DisplayHtml(string htmlContent)
    {
        Console.Clear();
        Console.WriteLine(htmlContent);
    }
}
