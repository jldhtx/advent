﻿// See https://aka.ms/new-console-template for more information

var lines = System.IO.File.ReadAllLines("./input.txt");
int winsExpected = 0;
int tiesExpected = 0;
int lossesExpected = 0;
foreach (var line in lines)
{
    if (line.Contains("X"))
        lossesExpected += 1;
    if (line.Contains("Y"))
        tiesExpected += 1;
    if (line.Contains("Z"))
        winsExpected += 1;

}
var game = new RockPaperScissorGame2();

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
    if (round.Me.Choice == RockPaperScissorGameChoice2.MoveChoice2.Rock)
        rock += 1;
    if (round.Me.Choice == RockPaperScissorGameChoice2.MoveChoice2.Paper)
        paper += 1;
    if (round.Me.Choice == RockPaperScissorGameChoice2.MoveChoice2.Scissors)
        scissors += 1;


}
Console.WriteLine($"Played {game.Rounds.Count} games");
Console.WriteLine($"Total points: {total}");
Console.WriteLine($"Total games R: {rock}");
Console.WriteLine($"Total games P: {paper}");
Console.WriteLine($"Total games S: {scissors}");
Console.WriteLine($"Total games won: {gamesWon} | expected {winsExpected}");
Console.WriteLine($"Total games tied: {gamesTied} | expected {tiesExpected}");
Console.WriteLine($"Total games lost: {gamesLost} | expected {lossesExpected}");


public class RockPaperScissorGame2
{
    public List<RockPaperScissorGameRound2> Rounds { get; set; }

    public RockPaperScissorGame2()
    {
        Rounds = new List<RockPaperScissorGameRound2>();
    }
    public void Process(string[] lines)
    {

        Console.WriteLine($"Processing {lines.Length} lines");

        foreach (var line in lines)
        {
            var choices = GetChoices(line);
            var round = new RockPaperScissorGameRound2(choices[1], choices[0]);
            Rounds.Add(round);

        }
    }

    private List<RockPaperScissorGameChoice2> GetChoices(string line)
    {
        var choices = line.Split(' ');
        if (choices.Length != 2)
            throw new System.Exception($"Bad line. {line}");
        var list = new List<RockPaperScissorGameChoice2>();
        list.Add(new RockPaperScissorGameChoice2(choices[0]));
        list.Add(new RockPaperScissorGameChoice2(choices[1]));
        return list;
    }

}

public class RockPaperScissorGameChoice2
{
    public readonly List<string> Rock = new List<string>() { "A", "X" };
    public readonly List<string> Paper = new List<string>() { "B", "Y" };
    public readonly List<string> Scissors = new List<string>() { "C", "Z" };

    public enum MoveChoice2
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    public MoveChoice2 Choice { get; private set; }
    public RockPaperScissorGameChoice2(MoveChoice2 choice)
    {
        this.Choice = choice;
    }
    public RockPaperScissorGameChoice2(string character)
    {
        if (Paper.Contains(character))
            this.Choice = MoveChoice2.Paper;
        else if (Rock.Contains(character))
            this.Choice = MoveChoice2.Rock;
        else if (Scissors.Contains(character))
            this.Choice = MoveChoice2.Scissors;
        else
            throw new Exception($"Invalid choice: {character}");
    }
}

public class RockPaperScissorGameRound2
{
    public int TotalPoints { get; private set; }
    public RockPaperScissorGameChoice2 Me;
    public RockPaperScissorGameChoice2 Them;

    public bool TheyWon { get; private set; }
    public bool IWon { get; private set; }

    public RockPaperScissorGameRound2(RockPaperScissorGameChoice2 me, RockPaperScissorGameChoice2 them)
    {
        TotalPoints = 0;
        Me = me;
        Them = them;
        if (Me.Choice == RockPaperScissorGameChoice2.MoveChoice2.Rock)
        {
            Me = new RockPaperScissorGameChoice2(Lose(Them.Choice));
            TheyWon = true;
        }
        else if (Me.Choice == RockPaperScissorGameChoice2.MoveChoice2.Paper)
        {
            Me = new RockPaperScissorGameChoice2(Them.Choice);
        }
        else if (Me.Choice == RockPaperScissorGameChoice2.MoveChoice2.Scissors)
        {
            IWon = true;
            Me = new RockPaperScissorGameChoice2(Win(Them.Choice));
        }

        SetPoints(Me.Choice, Them.Choice);
    }

    private RockPaperScissorGameChoice2.MoveChoice2 Lose(RockPaperScissorGameChoice2.MoveChoice2 theirChoice)
    {
        if (theirChoice == RockPaperScissorGameChoice2.MoveChoice2.Paper)
            return RockPaperScissorGameChoice2.MoveChoice2.Rock;
        if (theirChoice == RockPaperScissorGameChoice2.MoveChoice2.Rock)
            return RockPaperScissorGameChoice2.MoveChoice2.Scissors;
        else
            return RockPaperScissorGameChoice2.MoveChoice2.Paper;
    }

    private RockPaperScissorGameChoice2.MoveChoice2 Win(RockPaperScissorGameChoice2.MoveChoice2 theirChoice)
    {
        if (theirChoice == RockPaperScissorGameChoice2.MoveChoice2.Paper)
            return RockPaperScissorGameChoice2.MoveChoice2.Scissors;
        if (theirChoice == RockPaperScissorGameChoice2.MoveChoice2.Rock)
            return RockPaperScissorGameChoice2.MoveChoice2.Paper;
        else
            return RockPaperScissorGameChoice2.MoveChoice2.Rock;
    }

    private void SetPoints(RockPaperScissorGameChoice2.MoveChoice2 me, RockPaperScissorGameChoice2.MoveChoice2 them)
    {
        if (me == them)
            TotalPoints += 3;
        else if (IWon)
            TotalPoints += 6;

        if (me == RockPaperScissorGameChoice2.MoveChoice2.Rock) TotalPoints += 1;
        else if (me == RockPaperScissorGameChoice2.MoveChoice2.Paper) TotalPoints += 2;
        else if (me == RockPaperScissorGameChoice2.MoveChoice2.Scissors) TotalPoints += 3;

    }

}