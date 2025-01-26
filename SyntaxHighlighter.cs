using System;
using System.Collections.Generic;

public static class SyntaxHighlighter
{
    private static readonly Dictionary<string, ConsoleColor> HighlightWords = new Dictionary<string, ConsoleColor>
    {
        {"int", ConsoleColor.Blue},
        {"string", ConsoleColor.Blue},
        {"public", ConsoleColor.Yellow},
        {"static", ConsoleColor.Yellow},
        {"void", ConsoleColor.Blue},
        // Añadir más palabras clave y sus colores aquí...
    };

    public static void HighlightSyntax(string content, bool showLines)
    {
        var lines = content.Split('\n');
        int lineNumber = 1;

        foreach (var line in lines)
        {
            if (showLines)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"{lineNumber.ToString().PadLeft(4)} "); // Número de línea
                Console.ResetColor();
            }
            else
            {
                Console.Write("~   ");
            }

            var words = line.Split(new[] { ' ', '(', ')', '{', '}', ';', '\t' }, StringSplitOptions.None);

            foreach (var word in words)
            {
                if (HighlightWords.TryGetValue(word, out var color))
                {
                    Console.ForegroundColor = color;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray; // Color por defecto
                }
                Console.Write(word + " ");
            }
            Console.WriteLine();
            lineNumber++;
        }
        Console.ResetColor();
    }
}
