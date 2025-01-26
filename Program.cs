using System;
using System.Text;
using FileOperations;

class CodeEditor
{
    private static StringBuilder currentContent = new StringBuilder();
    private static string currentFileName = "Untitled";
    private static string fileType = "Plain Text";
    private static string currentTheme = "GruvboxDark";
    private static bool showLines = true;

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("Welcome to the Code Editor!");
        Console.WriteLine("Press Ctrl + O to open a file, Ctrl + S to save, Esc to exit, Ctrl + P to change theme, Ctrl + I to toggle lines, Ctrl + Space for snippets, Ctrl + J to replace line, Ctrl + E to import .fire, Ctrl + M to view Markdown.");
        Console.TreatControlCAsInput = true;

        ApplyCurrentTheme();
        UpdateDisplay(false);

        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(intercept: true);
            HandleInput(key);
            UpdateDisplay(false);
        } while (key.Key != ConsoleKey.Escape);

        // Monitor drag & drop files
        MonitorDragDrop();
    }

    private static void HandleInput(ConsoleKeyInfo key)
    {
        if (key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.O)
        {
            OpenFileHandler();
            UpdateDisplay(true);
        }
        else if (key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.S)
        {
            SaveFileHandler();
            UpdateDisplay(true);
        }
        else if (key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.P)
        {
            ChangeTheme();
            UpdateDisplay(true);
        }
        else if (key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.I)
        {
            ToggleLines();
            UpdateDisplay(true);
        }
        else if (key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.Spacebar)
        {
            Snippets.ShowSnippets(currentContent);
            UpdateDisplay(true);
        }
        else if (key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.J)
        {
            ReplaceLine.ReplaceLinePrompt(currentContent);
            UpdateDisplay(true);
        }
        else if (key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.E)
        {
            ImportFireFile();
            UpdateDisplay(true);
        }
        else if (key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.M)
        {
            ViewMarkdown();
            UpdateDisplay(true);
        }
        else
        {
            if (key.Key == ConsoleKey.Enter)
            {
                currentContent.AppendLine();
            }
            else if (key.Key == ConsoleKey.Backspace)
            {
                if (currentContent.Length > 0)
                {
                    currentContent.Length--;
                }
            }
            else if (IsPrintableChar(key.KeyChar))
            {
                currentContent.Append(key.KeyChar);
            }
        }
    }

    private static void ChangeTheme()
    {
        if (currentTheme == "GruvboxDark")
        {
            currentTheme = "GruvboxLight";
        }
        else
        {
            currentTheme = "GruvboxDark";
        }

        ApplyCurrentTheme();
    }

    private static void ApplyCurrentTheme()
    {
        Themes.ApplyTheme(currentTheme);
    }

    private static void ToggleLines()
    {
        showLines = !showLines;
    }

    private static void OpenFileHandler()
    {
        string content = OpenFile.Open();

        if (!string.IsNullOrEmpty(content))
        {
            currentContent.Clear();
            currentContent.Append(content);
            currentFileName = "Unknown File";
            fileType = "Unknown";
        }
    }

    private static void SaveFileHandler()
    {
        SaveFile.Save(currentContent.ToString());
    }

    private static void ImportFireFile()
    {
        Console.Write("\nEnter the path to the .fire file: ");
        string filePath = Console.ReadLine();

        var configurations = FireInterpreter.LoadConfigurations(filePath);
        FireInterpreter.ApplyConfigurations(configurations);
    }

    private static void ViewMarkdown()
    {
        Console.Write("\nEnter the path to the Markdown file: ");
        string filePath = Console.ReadLine();

        MarkdownViewer.ViewMarkdown(filePath);
    }

    private static void MonitorDragDrop()
    {
        while (true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(intercept: true);
                if (key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.D)
                {
                    Console.Write("\nDrag and drop files here: ");
                    string[] filePaths = Console.ReadLine().Split(' ');
                    DragDropHandler.HandleDragDrop(filePaths, currentContent);
                    UpdateDisplay(true);
                }
            }
        }
    }

    private static void UpdateDisplay(bool clear)
    {
        if (clear)
        {
            Console.Clear();
        }
        SyntaxHighlighter.HighlightSyntax(currentContent.ToString(), showLines);
        DisplayFooter();
    }

    private static void DisplayFooter()
    {
        int width = Console.WindowWidth;
        int height = Console.WindowHeight;
        var lines = currentContent.ToString().Split('\n');
        string longestLine = GetLongestLine(lines);

        Console.SetCursorPosition(0, height - 1);
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"Longest line: {longestLine.Length} | File: {currentFileName} ({fileType})".PadRight(width - 1));
        Console.ResetColor();
    }

    private static string GetLongestLine(string[] lines)
    {
        string longestLine = "";
        foreach (var line in lines)
        {
            if (line.Length > longestLine.Length)
            {
                longestLine = line;
            }
        }
        return longestLine;
    }

    private static bool IsPrintableChar(char c)
    {
        return !char.IsControl(c) || char.IsWhiteSpace(c);
    }
}
