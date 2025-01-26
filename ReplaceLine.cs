using System;
using System.Text; // Agregar esta directiva

public static class ReplaceLine
{
    public static void ReplaceLinePrompt(StringBuilder currentContent)
    {
        Console.Write("\nEnter the line number to replace: ");
        int lineNumber;
        if (int.TryParse(Console.ReadLine(), out lineNumber))
        {
            Console.Write("Enter new content for the line: ");
            string newLineContent = Console.ReadLine();
            ReplaceLineAt(currentContent, lineNumber, newLineContent);
        }
        else
        {
            Console.WriteLine("Invalid line number.");
        }
    }

    private static void ReplaceLineAt(StringBuilder content, int lineNumber, string newLineContent)
    {
        var lines = content.ToString().Split('\n');
        if (lineNumber > 0 && lineNumber <= lines.Length)
        {
            lines[lineNumber - 1] = newLineContent;
            content.Clear();
            foreach (var line in lines)
            {
                content.AppendLine(line);
            }
        }
        else
        {
            Console.WriteLine("Line number out of range.");
        }
    }
}
