
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("adventTests")]// See https://aka.ms/new-console-template for more information

var lines = System.IO.File.ReadAllLines("./input1.txt");
var calculator = new ElfCalorieCalculator();
calculator.Process(lines);
Console.WriteLine($"Processed {calculator.Elves.Count} elves!");
Console.WriteLine($"Elf with the most calories had: {calculator.Elves.Max(e => e.Calories)} calories!");
var top3 = calculator.Elves.OrderByDescending(e => e.Calories).Take(3).ToList();
var total = top3.Sum(e => e.Calories);
Console.WriteLine("The Top 3 Elves have: ");
if (top3.Count > 0)
    Console.WriteLine($"\t{top3[0].Calories}");
if (top3.Count > 1)
    Console.WriteLine($"\t{top3[1].Calories}");
if (top3.Count > 2)
    Console.WriteLine($"\t{top3[1].Calories}");
Console.WriteLine($"For a total of: {total}");
public class ElfCalorieCalculator
{

    public List<Elf> Elves;
    public ElfCalorieCalculator()
    {
        Elves = new List<Elf>();
    }

    public void Process(string[] lines)
    {
        var currentElf = new List<string>();
        foreach (var line in lines)
        {

            if (EndOfElfBlock(line))
            {
                Elves.Add(NewElf(currentElf.ToArray()));
                currentElf = new List<string>();
            }
            else
                currentElf.Add(line);
        }
        if (currentElf.Count > 0)
            Elves.Add(NewElf(currentElf.ToArray()));

    }

    private Elf NewElf(string[] input)
    {
        var elf = new Elf();
        elf.AddCalories(input);
        return elf;
    }


    private bool EndOfElfBlock(string line)
    {
        return string.IsNullOrEmpty(line);
    }

    public class Elf
    {
        public int Calories { get; private set; }
        public void AddCalories(string[] lines)
        {
            foreach (var line in lines)
            {
                if (int.TryParse(line, out int calories))
                {
                    Calories += calories;
                }
            }
        }

        internal int GetCalories(string value)
        {

            if (int.TryParse(value, out int calories))
            {
                return calories;
            }
            return 0;

        }
    }
}

