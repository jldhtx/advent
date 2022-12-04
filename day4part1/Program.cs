var lines = File.ReadAllLines("./input.txt");
var fullyOverlappedPairs = 0;
foreach (var line in lines)
{
    var schedule = new Schedule(line);
    if (schedule.IsFullyOverlapped())
        fullyOverlappedPairs++;

}

Console.WriteLine($"Found {fullyOverlappedPairs} fully overlapped pairs");

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
        return Overlaps(AssignmentOne, AssignmentTwo) ||
         Overlaps(AssignmentTwo, AssignmentOne);
    }

    public bool Overlaps(Assignment one, Assignment two)
    {
        return ((one.From - two.From <= 0)
                && (one.To - two.To >= 0));
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

    private int GetInt(string part)
    {
        if (int.TryParse(part, out int i))
            return i;

        throw new Exception($"Invalid value for assignment: {part}");
    }
}
