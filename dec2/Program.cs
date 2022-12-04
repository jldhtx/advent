// See https://aka.ms/new-console-template for more information
var lines = System.IO.File.ReadAllLines("./input.txt");
var game = new RockPaperScissorGame();

game.Process(lines);

int total = 0;
int gamesWon = 0;
int gamesTied = 0;
int gamesLost = 0;
int rock = 0;
int paper = 0;
int scissors = 0;

foreach (var round in game.Rounds)
{
    total += round.TotalPoints;
    if (round.IWon)
        gamesWon += 1;
    else if (round.TheyWon)
        gamesLost += 1;
    else
        gamesTied += 1;
    if (round.Me.Choice == RockPaperScissorGameChoice.MoveChoice.Rock)
        rock += 1;
    if (round.Me.Choice == RockPaperScissorGameChoice.MoveChoice.Paper)
        paper += 1;
    if (round.Me.Choice == RockPaperScissorGameChoice.MoveChoice.Scissors)
        scissors += 1;


}
Console.WriteLine($"Played {game.Rounds.Count} games");
Console.WriteLine($"Total points: {total}");
Console.WriteLine($"Total games won: {gamesWon}");
Console.WriteLine($"Total games R: {rock}");
Console.WriteLine($"Total games P: {paper}");
Console.WriteLine($"Total games S: {scissors}");
Console.WriteLine($"Total games tied: {gamesTied}");
Console.WriteLine($"Total games lost: {gamesLost}");


public class RockPaperScissorGame
{
    public List<RockPaperScissorGameRound> Rounds { get; set; }

    public RockPaperScissorGame()
    {
        Rounds = new List<RockPaperScissorGameRound>();
    }
    public void Process(string[] lines)
    {

        Console.WriteLine($"Processing {lines.Length} lines");

        foreach (var line in lines)
        {
            var choices = GetChoices(line);
            var round = new RockPaperScissorGameRound(choices[1], choices[0]);
            Rounds.Add(round);

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
    public RockPaperScissorGameChoice(MoveChoice choice)
    {
        this.Choice = choice;
    }
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
    public int TotalPoints { get; private set; }
    public RockPaperScissorGameChoice Me;
    public RockPaperScissorGameChoice Them;

    public bool TheyWon { get; private set; }
    public bool IWon { get; private set; }

    public RockPaperScissorGameRound(RockPaperScissorGameChoice me, RockPaperScissorGameChoice them)
    {
        TotalPoints = 0;
        Me = me;
        Them = them;

        if (me.Choice == RockPaperScissorGameChoice.MoveChoice.Rock)
            TheyWon = (Them.Choice == RockPaperScissorGameChoice.MoveChoice.Paper);
        else if (me.Choice == RockPaperScissorGameChoice.MoveChoice.Paper)
            TheyWon = (Them.Choice == RockPaperScissorGameChoice.MoveChoice.Scissors);
        else if (me.Choice == RockPaperScissorGameChoice.MoveChoice.Scissors)
            TheyWon = (Them.Choice == RockPaperScissorGameChoice.MoveChoice.Rock);

        if (Them.Choice == Me.Choice)
            IWon = false;
        else
            IWon = !TheyWon;

        SetPoints(Me.Choice, Them.Choice);
    }

    private void SetPoints(RockPaperScissorGameChoice.MoveChoice me, RockPaperScissorGameChoice.MoveChoice them)
    {
        if (me == them)
            TotalPoints += 3;
        else if (IWon)
            TotalPoints += 6;

        if (me == RockPaperScissorGameChoice.MoveChoice.Rock) TotalPoints += 1;
        else if (me == RockPaperScissorGameChoice.MoveChoice.Paper) TotalPoints += 2;
        else if (me == RockPaperScissorGameChoice.MoveChoice.Scissors) TotalPoints += 3;

    }

}