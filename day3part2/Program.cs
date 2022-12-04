// See https://aka.ms/new-console-template for more information
using System.Diagnostics.CodeAnalysis;
using System.Text;

var lines = System.IO.File.ReadAllLines("./input.txt");
var dictionary = new Dictionary<int, RucksackGroup>();

int priorityTotal = 0;
int groupId = 1;

var sacks = new List<Rucksack>();

foreach (var line in lines)
{
    var sack = new Rucksack(line);
    sacks.Add(sack);
    if (sacks.Count == 3)
    {
        var group = new RucksackGroup();
        group.AddRange(sacks);
        dictionary.Add(groupId, group);
        groupId++;
        sacks = new List<Rucksack>();
    }

}

Console.WriteLine($"Processed {dictionary.Keys.Count} groups");
foreach (var key in dictionary.Keys)
{
    var group = dictionary[key];
    Console.WriteLine($"Group ID {key}");
    var item = group.GetDuplicates().First();
    priorityTotal += item.Priority;
    Console.WriteLine($"\t Badge:{item.Value} - {item.Priority}");

}

Console.WriteLine($"PriorityTotal: {priorityTotal}");

public class RucksackGroup
{
    public List<Rucksack> GroupRucksacks { get; private set; }
    public RucksackGroup()
    {
        GroupRucksacks = new List<Rucksack>();
    }

    public void AddRange(List<Rucksack> sacks)
    {
        GroupRucksacks.AddRange(sacks);
    }

    public List<RucksackItem> GetDuplicates()
    {
        List<List<RucksackItem>> allItems = GroupRucksacks.Select(g => g.AllItems()).ToList();

        var dups = allItems
                    .Skip(1)
                    .Aggregate(new HashSet<RucksackItem>(allItems.First()),
                    AggregateFunction).ToList();

        return dups;
    }

    private HashSet<RucksackItem> AggregateFunction(HashSet<RucksackItem> list, List<RucksackItem> next)
    {
        list.IntersectWith(next);
        return list;
    }
}
public class Rucksack
{
    private string contents;
    public Rucksack(string contents)
    {
        this.contents = contents;
        var length = contents.Length;
        var half = length / 2;
        if (contents.Length % 2 != 0)
            throw new Exception("invalid string length for rucksack");
        var compartmentOne = contents.Substring(0, half);
        var compartmentTwo = contents.Substring(half, half);

        CompartmentOne = new RucksackCompartment(compartmentOne);
        CompartmentTwo = new RucksackCompartment(compartmentTwo);
    }

    public List<RucksackItem> AllItems()
    {
        return CompartmentOne.Items.Union(CompartmentTwo.Items).ToList();
    }
    public override string ToString()
    {
        var output = new StringBuilder();
        output.AppendLine($"Contents of this backpack ({contents})");
        output.AppendLine("\tCompartment One:");
        foreach (var item in CompartmentOne.Items)
            output.AppendLine($"\t\t{item.Value} - {item.Priority}");
        output.AppendLine("\tCompartment Two:");
        foreach (var item in CompartmentTwo.Items)
            output.AppendLine($"\t\t{item.Value} - {item.Priority}");

        return output.ToString();
    }


    public RucksackItem? GetDuplicate()
    {
        var duplicate = CompartmentOne.Items.Where(i => CompartmentTwo.Items.Contains(i)).FirstOrDefault();
        return duplicate;

    }
    public RucksackCompartment CompartmentOne { get; private set; }
    public RucksackCompartment CompartmentTwo { get; private set; }
}

public class RucksackCompartment
{
    public List<RucksackItem> Items { get; private set; }
    public RucksackCompartment(string contents)
    {
        Items = new List<RucksackItem>();
        foreach (char item in contents.ToCharArray())
        {
            Items.Add(new RucksackItem(item));
        }
    }
}

public class RucksackItemComparer : IEqualityComparer<RucksackItem>
{
    public bool Equals(RucksackItem? item, RucksackItem? item2)
    {
        if (item == null || item2 == null) return false;

        return (item.Value == item2.Value && item.Priority == item2.Value);
    }

    public int GetHashCode(RucksackItem item)
    {
        return item.GetHashCode();
    }
}
public class RucksackItem
{
    public override bool Equals(object? obj)
    {
        var them = obj as RucksackItem;
        if (them == null) return false;
        return them.Value == this.Value && them.Priority == this.Priority;
    }

    public override int GetHashCode()
    {
        return new { Value, Priority }.GetHashCode();
    }

    public char Value { get; private set; }
    private const int UPPER_CASE_LOW = 65;
    private const int UPPER_CASE_HIGH = 90;
    private const int UPPER_CASE_OFFSET = 38;

    private const int LOWER_CASE_LOW = 97;
    private const int LOWER_CASE_HIGH = 122;
    private const int LOWER_CASE_OFFSET = 96;
    public RucksackItem(char value)
    {
        Priority = GetPriorityFromCode(value);
        Value = value;

    }

    private int GetPriorityFromCode(char value)
    {

        if (value >= UPPER_CASE_LOW && value <= UPPER_CASE_HIGH)
            return value - UPPER_CASE_OFFSET;
        if (value >= LOWER_CASE_LOW && value <= LOWER_CASE_HIGH)
            return value - LOWER_CASE_OFFSET;
        throw new Exception($"Invalid char: {value}");

    }
    public int Priority { get; private set; }

    // 97-122 is a-z
    // 65-90 is A-Z
    /*Lowercase item types a through z have priorities 1 through 26.
Uppercase item types A through Z have priorities 27 through 52.
*/
}