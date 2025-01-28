namespace RefactoringActivity;

public class Puzzle
{
    public string Name { get; private set; }
    private string Question;
    private string Answer;
    
    public Puzzle(string name, string question, string answer)
    {
        Name = name;
        Question = question;
        Answer = answer;
    }

    public bool Solve()
    {
        string playerAnswer = GetPlayerAnswer();
        return playerAnswer == Answer.ToLower();
    }

    private string GetPlayerAnswer()
    {
        Console.WriteLine($"Puzzle: {Question}");
        Console.Write("Your answer: ");
        return ReadAnswer();
    }

    private static string ReadAnswer()
    {
        string playerAnswer = Console.ReadLine() ?? "";
        return playerAnswer.ToLower();
    }
}