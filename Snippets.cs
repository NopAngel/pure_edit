using System;
using System.Collections.Generic;
using System.Text; // Agregar esta directiva

public static class Snippets
{
    private static readonly Dictionary<string, string> snippets = new Dictionary<string, string>
    {
        {"main", "public static void Main(string[] args) { }"},
        {"class", "public class MyClass { }"},
        {"method", "public void MyMethod() { }"},
        // Añadir más snippets según sea necesario...
    };

    public static void ShowSnippets(StringBuilder currentContent)
    {
        Console.WriteLine("\nAvailable Snippets:");
        foreach (var snippet in snippets)
        {
            Console.WriteLine($"{snippet.Key} -> {snippet.Value}");
        }
        Console.Write("Insert snippet by key: ");
        string snippetKey = Console.ReadLine();
        if (snippets.ContainsKey(snippetKey))
        {
            currentContent.Append(snippets[snippetKey]);
        }
        else
        {
            Console.WriteLine("Snippet not found.");
        }
    }
}
