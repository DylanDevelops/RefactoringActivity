namespace RefactoringActivity;

public class Player
{
    public int Health;
    public string CurrentLocation;
    public List<string> Inventory { get; private set; }

    public Player(int health)
    {
        Health = health;
        CurrentLocation = "Start";
        Inventory = new List<string>();
    }

    public void ShowInventory()
    {
        if (Inventory.Count == 0)
        {
            ShowInventoryEmpty();
        }
        else
        {
            ShowInventoryFull();
        }
    }

    private void ShowInventoryFull()
    {
        Console.WriteLine("You are carrying:");
        foreach (string item in Inventory)
        {
            Console.WriteLine($"- {item}");
        }
    }

    private static void ShowInventoryEmpty()
    {
        Console.WriteLine("Your inventory is empty.");
    }
}