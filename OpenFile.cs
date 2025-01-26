using System;
using System.IO;

namespace FileOperations
{
    public static class OpenFile
    {
        public static string Open()
        {
            Console.Write("\nEnter the path of the file to open: ");
            string? path = Console.ReadLine(); // Permitir valores nulos

            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else
            {
                Console.WriteLine("File not found.");
                return string.Empty;
            }
        }
    }
}
