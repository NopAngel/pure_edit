using System;
using System.IO;

namespace FileOperations
{
    public static class SaveFile
    {
        public static void Save(string content)
        {
            Console.Write("\nEnter the path to save the file: ");
            string? path = Console.ReadLine(); // Permitir valores nulos

            if (string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine("Invalid path. File not saved.");
                return;
            }

            File.WriteAllText(path, content);
            Console.WriteLine("File saved successfully.");
        }
    }
}
