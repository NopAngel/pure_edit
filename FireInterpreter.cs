using System;
using System.Collections.Generic;
using System.IO;

public static class FireInterpreter
{
    public static Dictionary<string, string> LoadConfigurations(string filePath)
    {
        var configurations = new Dictionary<string, string>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Configuration file not found.");
            return configurations;
        }

        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split('=');
            if (parts.Length == 2)
            {
                configurations[parts[0].Trim()] = parts[1].Trim();
            }
        }

        return configurations;
    }

    public static void ApplyConfigurations(Dictionary<string, string> configurations)
    {
        foreach (var config in configurations)
        {
            switch (config.Key)
            {
                case "Background":
                    Themes.ApplyTheme(config.Value);
                    break;
                case "Prettier":
                    Console.WriteLine("Prettier is set to " + config.Value);
                    break;
                case "Correciones":
                    Console.WriteLine("Correciones is set to " + config.Value);
                    break;
                default:
                    Console.WriteLine("Unknown configuration: " + config.Key);
                    break;
            }
        }
    }
}
