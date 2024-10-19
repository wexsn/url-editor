using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string? filePath = null;

        while (true)
        {
            Console.WriteLine("Введите путь к txt файлу (например, C:\\Users\\user\\source\\repos\\AndrewEditor\\bin\\Debug\\net8.0\\sites.txt):");
            filePath = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(filePath))
            {
                break;
            }

            Console.WriteLine("Путь к файлу не может быть пустым. Пожалуйста, укажите правильный путь к txt файлу.");
        }

        try
        {
            string[] lines = File.ReadAllLines(filePath!);
            List<string> modifiedLines = new List<string>();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                if (line.StartsWith("||") && line.EndsWith("^"))
                {
                    modifiedLines.Add(line);
                }
                else if (IsValidWebsite(line))
                {
                    modifiedLines.Add($"||{line}^");
                }
                else
                {
                    modifiedLines.Add(line);
                }
            }

            string directory = Path.GetDirectoryName(filePath!)!;
            string newFilePath = Path.Combine(directory, "readyurls.txt");

            File.WriteAllLines(newFilePath, modifiedLines);

            Console.WriteLine($"Указанный txt файл успешно отредактирован и сохранен как {newFilePath}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка, указан неверный путь к txt файлу: " + ex.Message);
        }

        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    static bool IsValidWebsite(string url)
    {
        string pattern = @"^[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}(/.*)?$";
        return Regex.IsMatch(url, pattern);
    }
}
