var lines = File.ReadAllLines("./input.txt");
var overlappedPairs = 0;
foreach (var line in lines)
{
    var schedule = new Schedule(line);
    if (schedule.IsOverlapped())
    {
        overlappedPairs++;
        Console.WriteLine($"{schedule.AssignmentOne.From}-{schedule.AssignmentOne.To}");
        Console.WriteLine($"{schedule.AssignmentTwo.From}-{schedule.AssignmentTwo.To}");
        Console.WriteLine("^^^ overlapped");
    }
    else
    {
        Console.WriteLine($"{schedule.AssignmentOne.From}-{schedule.AssignmentOne.To}");
        Console.WriteLine($"{schedule.AssignmentTwo.From}-{schedule.AssignmentTwo.To}");
        Console.WriteLine("^^^ not overlapped");
    }

}

Console.WriteLine($"Found {overlappedPairs}  overlapped pairs");

public class Schedule
{
    public Schedule(string pair)
    {
        var parts = pair.Split(',');
        if (parts.Length != 2)
            throw new Exception($"Invalid assignment pair: {pair}");
        AssignmentOne = new Assignment(parts[0]);
        AssignmentTwo = new Assignment(parts[1]);

    }
    //2-4, 6-8
    public bool IsFullyOverlapped()
    {
        return AssignmentOne.FullyOverlaps(AssignmentTwo) ||
         AssignmentTwo.FullyOverlaps(AssignmentOne);
    }

    public bool IsOverlapped()
    {
        return AssignmentOne.Overlaps(AssignmentTwo) ||
         AssignmentTwo.Overlaps(AssignmentOne);
    }

    public Assignment AssignmentOne { get; private set; }
    public Assignment AssignmentTwo { get; private set; }
}
public class Assignment
{
    public int To { get; private set; }
    public int From { get; private set; }
    public Assignment(string content)
    {
        var parts = content.Split('-');
        if (parts.Length != 2)
            throw new Exception($"Invalid assignment: {content}");
        To = GetInt(parts[1]);
        From = GetInt(parts[0]);
        if (To < From) throw new Exception($"Invalid range: {content}");

    }

    public bool Overlaps(Assignment them)
    {           // overlaps left //2-4,1-8
        return (From >= them.From && From <= them.To) || // overlaps right //2-4,3-8
            (To >= them.From && To <= them.To);

        // 2-4, 3-8
        // 2-6, 1-8

    }

    public bool FullyOverlaps(Assignment them)
    {
        return ((this.From < them.From)
                && (this.To > them.To));
    }
    private int GetInt(string part)
    {
        if (int.TryParse(part, out int i))
            return i;

        throw new Exception($"Invalid value for assignment: {part}");
    }
}
