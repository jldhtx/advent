// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


public class RockPaperScissorGame
{
    public List<RockPaperScissorGameRound> Rounds => new List<RockPaperScissorGameRound>();

    public void Process(string[] lines)
    {
        foreach (var line in lines)
        {
            var choices = GetChoices(line);
            Rounds.Add(new RockPaperScissorGameRound(choices[0], choices[1]));
        }
    }

    private List<RockPaperScissorGameChoice> GetChoices(string line)
    {
        var choices = line.Split(' ');
        if (choices.Length != 2)
            throw new System.Exception($"Bad line. {line}");
        var list = new List<RockPaperScissorGameChoice>();
        list.Add(new RockPaperScissorGameChoice(choices[0]));
        list.Add(new RockPaperScissorGameChoice(choices[1]));
        return list;
    }
}

public class RockPaperScissorGameChoice
{
    public readonly List<string> Rock = new List<string>() { "A", "X" };
    public readonly List<string> Paper = new List<string>() { "B", "Y" };
    public readonly List<string> Scissors = new List<string>() { "C", "Z" };

    public enum MoveChoice
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    public MoveChoice Choice { get; private set; }
    public RockPaperScissorGameChoice(string character)
    {
        if (Paper.Contains(character))
            this.Choice = MoveChoice.Paper;
        else if (Rock.Contains(character))
            this.Choice = MoveChoice.Rock;
        else if (Scissors.Contains(character))
            this.Choice = MoveChoice.Scissors;
        else
            throw new Exception($"Invalid choice: {character}");
    }
}

public class RockPaperScissorGameRound
{
    public RockPaperScissorGameChoice Me;
    public RockPaperScissorGameChoice Them;

    public bool TheyWon { get; private set; }

    public RockPaperScissorGameRound(RockPaperScissorGameChoice me, RockPaperScissorGameChoice them)
    {
        Me = me;
        Them = them;
        if (me.Choice == RockPaperScissorGameChoice.MoveChoice.Rock)
            TheyWon = (Them.Choice == RockPaperScissorGameChoice.MoveChoice.Paper);
        else if (me.Choice == RockPaperScissorGameChoice.MoveChoice.Paper)
            TheyWon = (Them.Choice == RockPaperScissorGameChoice.MoveChoice.Scissors);
        else if (me.Choice == RockPaperScissorGameChoice.MoveChoice.Scissors)
            TheyWon = (Them.Choice == RockPaperScissorGameChoice.MoveChoice.Rock);
    }

}