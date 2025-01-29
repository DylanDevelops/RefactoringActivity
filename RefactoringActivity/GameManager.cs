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

            if (invalidInput(input))
            {
                return;
            }

            if (input == "help")
            {
                Help();
            }
            else if (input.StartsWith("go"))
            {
                Go(input);
            }
            else if (input.StartsWith("take"))
            {
                Take(input);
            }
            else if (input.StartsWith("use"))
            {
                Use(input);
            }
            else if (input == "inventory")
            {
                Inventory();
            }
            else if (input.StartsWith("solve"))
            {
                Solve(input);
            }
            else if (input == "quit")
            {
                Quit();
            }
            else
            {
                UnknownCommand();
            }
        }
    }

    private static bool invalidInput(string? input)
    {
        if (string.IsNullOrEmpty(input)) 
            return true;
        return false;
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

    private void Solve(string input)
    {
        string[] parts = input.Split(' ');
        if (parts.Length > 1)
        {
            string puzzleName = parts[1];
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

    private void Use(string input)
    {
        string[] parts = input.Split(' ');
        if (parts.Length > 1)
        {
            string itemName = parts[1];
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

    private void Take(string input)
    {
        string[] parts = input.Split(' ');
        if (parts.Length > 1)
        {
            string itemName = parts[1];
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

    private void Go(string input)
    {
        string[] parts = input.Split(' ');
        if (parts.Length > 1)
        {
            string direction = parts[1];
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