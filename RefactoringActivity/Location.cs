namespace RefactoringActivity;

public class Location
{
    public string Description { get; private set; }
    public Dictionary<string, string> Exits { get; private set; }
    public List<string> Items { get; private set; }
    public List<Puzzle> Puzzles { get; private set; }
    
    public Location(string description)
    {
        Description = description;
        Exits = new Dictionary<string, string>();
        Items = new List<string>();
        Puzzles = new List<Puzzle>();
    }
}