using System;

public static class Themes
{
    public static void ApplyTheme(string theme)
    {
        switch (theme)
        {
            case "GruvboxDark":
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                break;
            case "GruvboxLight":
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                break;
            // Añadir más temas según sea necesario...
            default:
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                break;
        }
        Console.Clear(); // Asegurarse de que el cambio de tema se aplique inmediatamente
    }
}
