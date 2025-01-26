using System;
using System.IO;
using System.Text;

public static class DragDropHandler
{
    public static void HandleDragDrop(string[] filePaths, StringBuilder currentContent)
    {
        foreach (string filePath in filePaths)
        {
            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                currentContent.Append(content);
            }
            else
            {
                Console.WriteLine($"File {filePath} not found.");
            }
        }
    }
}
