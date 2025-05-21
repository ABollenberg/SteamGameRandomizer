using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<string> games = new List<string>();
    static List<string> shuffledGames = new List<string>();

    static void Main()
    {
        LoadGameList(); // Load games from the file

        while (true)
        {
            SafeClear(); // Safe clear instead of Console.Clear()

            Console.WriteLine("Allie's Steam Game Randomizer");
            Console.WriteLine("Main Menu\n");
            Console.WriteLine("1. Randomize");
            Console.WriteLine("2. View List of Games");
            Console.WriteLine("3. Search Game");
            Console.WriteLine("4. Exit Program\n");

            string choice = Console.ReadLine() ?? "";
            choice ??= string.Empty;

            switch (choice)
            {
                case "1":
                    RandomizeGame();
                    break;
                case "2":
                    ViewGameList();
                    break;
                case "3":
                    SearchGame();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.\n");
                    break;
            }
        }
    }

    static void RandomizeGame()
    {
        if (games.Count == 0)
        {
            Console.WriteLine("No games added. Add some games first.\n");
            return;
        }

        if (shuffledGames.Count == 0)
        {
            // Shuffle the games using Fisher-Yates algorithm
            shuffledGames = new List<string>(games);
            int n = shuffledGames.Count;
            Random random = new Random();

            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                string value = shuffledGames[k];
                shuffledGames[k] = shuffledGames[n];
                shuffledGames[n] = value;
            }
        }

        string randomGame = shuffledGames[0];
        shuffledGames.RemoveAt(0); // Remove the game from the shuffled list

        SafeClear(); // Safe clear
        Console.WriteLine($">> '{randomGame}' <<\n");
        Console.WriteLine("1. To Randomize Again");
        Console.WriteLine("3. To Exit to Main Menu\n");

        string exitChoice = Console.ReadLine() ?? "";
        exitChoice ??= string.Empty;
        if (exitChoice != "3")
        {
            RandomizeGame();
        }
    }

    static void ViewGameList()
    {
        SafeClear(); // Safe clear
        Console.WriteLine("List of Games\n");

        for (int i = 0; i < games.Count; i++)
        {
            Console.WriteLine($">{games[i]}\n");
        }

        Console.WriteLine("3. Exit to Main Menu\n");
        string exitChoice = Console.ReadLine() ?? "";
        if (exitChoice != "3")
        {
            Main();
        }
    }

    static void SearchGame()
    {
        while (true)
        {
            SafeClear(); // Safe clear
            Console.WriteLine("Enter a keyword to search for a game:\n");
            string keyword = Console.ReadLine()?.Trim() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                List<string> matchingGames = games.FindAll(game => game.Contains(keyword, StringComparison.OrdinalIgnoreCase));

                if (matchingGames.Count > 0)
                {
                    Console.WriteLine("\nMatching Games\n");
                    foreach (var matchingGame in matchingGames)
                    {
                        Console.WriteLine($">{matchingGame}\n");
                    }
                }
                else
                {
                    Console.WriteLine("\nNo matching games found.\n");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid keyword. Please try again.\n");
            }

            Console.WriteLine("1. Search Another Game");
            Console.WriteLine("3. Exit to Main Menu\n");

            string exitChoice = Console.ReadLine() ?? "";
            if (exitChoice == "3")
            {
                Main();
            }
        }
    }

    static void LoadGameList()
    {
        string gameListFile = "gamelist.txt";

        if (File.Exists(gameListFile))
        {
            games = new List<string>(File.ReadAllLines(gameListFile));
        }
        else
        {
            Console.WriteLine("No game list found. Please create 'gamelist.txt' and add game names.\n");
        }
    }

    static void SafeClear()
    {
        try
        {
            Console.Clear();
        }
        catch (IOException)
        {
            // Terminal doesn't support Console.Clear(), so do nothing
        }
    }
}
