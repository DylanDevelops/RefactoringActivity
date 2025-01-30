namespace RefactoringActivity;

public class World
{
    public Dictionary<string, Location> Locations;

    public World()
    {
        Locations = new Dictionary<string, Location>();
        InitializeWorld();
    }

    private void InitializeWorld()
    {
        AddLocation("Start", "You are at the starting point of your adventure.", 
            new Dictionary<string, string> { { "north", "Forest" } },
            new List<string> { "map" },
            new List<Puzzle> { new Puzzle("riddle", "What's tall as a house, round as a cup, and all the king's horses can't draw it up?", "well") });

        AddLocation("Forest", "You are in a dense, dark forest.", 
            new Dictionary<string, string> { { "south", "Start" }, { "east", "Cave" } },
            new List<string> { "key", "potion" },
            new List<Puzzle>());

        AddLocation("Cave", "You see a dark, ominous cave.", 
            new Dictionary<string, string> { { "west", "Forest" } },
            new List<string> { "sword" },
            new List<Puzzle>());
    }

    private void AddLocation(string name, string description, Dictionary<string, string> exits, List<string> items, List<Puzzle> puzzles)
    {
        Location location = new(description);
        foreach (var exit in exits)
        {
            location.Exits.Add(exit.Key, exit.Value);
        }
        foreach (var item in items)
        {
            location.Items.Add(item);
        }
        foreach (var puzzle in puzzles)
        {
            location.Puzzles.Add(puzzle);
        }
        Locations.Add(name, location);
    }

    public bool MovePlayer(Player player, string direction)
    {
        if (Locations[player.CurrentLocation].Exits.ContainsKey(direction))
        {
            player.CurrentLocation = Locations[player.CurrentLocation].Exits[direction];
            return true;
        }

        return false;
    }

    public string GetLocationDescription(string locationName)
    {
        if (Locations.ContainsKey(locationName)) 
            return Locations[locationName].Description;
        return "Unknown location.";
    }

    public string GetLocationDetails(string locationName)
    {
        if (!Locations.ContainsKey(locationName)) 
            return "Unknown location.";

        Location location = Locations[locationName];
        string details = location.Description;
        
        if (location.Exits.Count > 0)
        {
            details += " Exits lead: ";
            foreach (string exit in location.Exits.Keys)
                details += exit + ", ";
            details = details.Substring(0, details.Length - 2);
        }

        if (location.Items.Count > 0)
        {
            details += "\nYou see the following items:";
            foreach (string item in location.Items) 
                details += $"\n- {item}";
        }

        if (location.Puzzles.Count > 0)
        {
            details += "\nYou see the following puzzles:";
            foreach (Puzzle puzzle in location.Puzzles) 
                details += $"\n- {puzzle.Name}";
        }

        return details;
    }

    public bool TakeItem(Player player, string itemName)
    {
        Location location = Locations[player.CurrentLocation];
        if (location.Items.Contains(itemName))
        {
            location.Items.Remove(itemName);
            player.Inventory.Add(itemName);
            Console.WriteLine($"You take the {itemName}.");
            return true;
        }

        return false;
    }

    public bool UseItem(Player player, string itemName)
    {
        if (player.Inventory.Contains(itemName))
        {
            if (itemName == "potion")
            {
                Console.WriteLine("Ouch! That tasted like poison!");
                player.Health -= 10;
                Console.WriteLine($"Your health is now {player.Health}.");
            }
            else
            {
                Console.WriteLine($"The {itemName} disappears in a puff of smoke!");
            }
            player.Inventory.Remove(itemName);
            return true;
        }

        return false;
    }

    public bool SolvePuzzle(Player player, string puzzleName)
    {
        Location location = Locations[player.CurrentLocation];
        Puzzle? puzzle = location.Puzzles.Find(p => p.Name == puzzleName);

        if (puzzle != null && puzzle.Solve())
        {
            location.Puzzles.Remove(puzzle);
            return true;
        }

        return false;
    }
}