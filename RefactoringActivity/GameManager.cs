namespace RefactoringActivity;

public class GameManager
{
    private bool IsRunning;
    private Player Player;
    private World World;
    private string input;

    public void RunGame()
    {
        Instantiate();
        Console.WriteLine("Welcome to the Text Adventure Game!");
        Console.WriteLine("Type 'help' for a list of commands.");

        while (IsRunning)
        {
            Console.WriteLine();
            Console.WriteLine(World.GetLocationDetails(Player.CurrentLocation));
            Console.Write("> ");
            input = Console.ReadLine()?.ToLower();

            if (IsInvalidInput(input))
            {
                return;
            }

            ProcessInput(input);
        }
    }

    private static bool IsInvalidInput(string? input)
    {
        return string.IsNullOrEmpty(input);
    }

    private void ProcessInput(string input)
    {
        string[] parts = input.Split(' ');
        string command = parts[0];
        string argument = parts.Length > 1 ? parts[1] : string.Empty;

        switch (command)
        {
            case "help":
                Help();
                break;
            case "go":
                Go(argument);
                break;
            case "take":
                Take(argument);
                break;
            case "use":
                Use(argument);
                break;
            case "inventory":
                Inventory();
                break;
            case "solve":
                Solve(argument);
                break;
            case "quit":
                Quit();
                break;
            default:
                UnknownCommand();
                break;
        }
    }

    private static void UnknownCommand()
    {
        Console.WriteLine("Unknown command. Try 'help'.");
    }

    private void Quit()
    {
        IsRunning = false;
        Console.WriteLine("Thanks for playing!");
    }

    private void Solve(string puzzleName)
    {
        if (!string.IsNullOrEmpty(puzzleName))
        {
            if (World.SolvePuzzle(Player, puzzleName))
            {
                Console.WriteLine($"You solved the {puzzleName} puzzle!");
            }
            else
            {
                Console.WriteLine($"That's not the right solution for the {puzzleName} puzzle.");
            }
        }
        else
        {
            Console.WriteLine("Solve what?");
        }
    }

    private void Inventory()
    {
        Player.ShowInventory();
    }

    private void Use(string itemName)
    {
        if (!string.IsNullOrEmpty(itemName))
        {
            if (!World.UseItem(Player, itemName))
            {
                Console.WriteLine($"You can't use the {itemName} here.");
            }
        }
        else
        {
            Console.WriteLine("Use what?");
        }
    }

    private void Take(string itemName)
    {
        if (!string.IsNullOrEmpty(itemName))
        {
            if (!World.TakeItem(Player, itemName))
            {
                Console.WriteLine($"There is no {itemName} here.");
            }
        }
        else
        {
            Console.WriteLine("Take what?");
        }
    }

    private void Go(string direction)
    {
        if (!string.IsNullOrEmpty(direction))
        {
            if (World.MovePlayer(Player, direction))
            {
                Console.WriteLine($"You move {direction}.");
            }
            else
            {
                Console.WriteLine("You can't go that way.");
            }
        }
        else
        {
            Console.WriteLine("Move where? (north, south, east, west)");
        }
    }

    private static void Help()
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("- go [direction]: Move in a direction (north, south, east, west).");
        Console.WriteLine("- take [item]: Take an item from your current location.");
        Console.WriteLine("- use [item]: Use an item in your inventory.");
        Console.WriteLine("- solve [puzzle]: Solve a puzzle in your current location.");
        Console.WriteLine("- inventory: View the items in your inventory.");
        Console.WriteLine("- quit: Exit the game.");
    }

    private void Instantiate()
    {
        IsRunning = true;
        Player = new Player(100);
        World = new World();
    }
}