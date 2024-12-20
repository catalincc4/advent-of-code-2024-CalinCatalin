using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode2025.Days;

public static class Day1
{
    public static void ExecutePart1(string[] lines)
    {
        var (numbers, numbers2) = ExtractNumbers(lines);
        numbers.Sort();
        numbers2.Sort();
        var sum = CalculateDifferences(numbers, numbers2);
        Console.WriteLine(sum);
    }

    private static int CalculateDifferences(List<int> numbers, List<int> numbers2)
    {
        var sum = 0;
        for (int i = 0; i < numbers.Count; i++)
        {
            sum += Math.Abs(numbers[i] - numbers2[i]);
        }

        return sum;
    }

    private static (List<int>, List<int>) ExtractNumbers(string[] lines)
    {
        var numbers = new List<int>();
        var numbers2 = new List<int>();
        foreach (var line in lines)
        {
            var parts = Regex.Split(line, @"\s+");
            if (int.TryParse(parts[0], out var number))
            {
                numbers.Add(number);
            }

            if (int.TryParse(parts[1], out var number2))
            {
                numbers2.Add(number2);
            }
        }

        return (numbers, numbers2);
    }

    public static void ExecutePart2(string[] lines)
    {
        var (numbers, numbers2) = ExtractNumbers(lines);
        var occurence = ProcessOccurence(numbers2);
        var sum = CalculateSimilarity(occurence, numbers);
        Console.WriteLine(sum);
    }
    
    private static int CalculateSimilarity(Dictionary<int, int> occurence, List<int> numbers)
    {
        var sum = 0;
        foreach (var number in numbers)
        {
            if (occurence.ContainsKey(number))
            {
                sum += number * occurence[number];
            }
        }

        return sum;
    }

    private static Dictionary<int, int> ProcessOccurence(List<int> numbers)
    {
        Dictionary<int, int> occurence = new();
        foreach (var number in numbers)
        {
            if (occurence.ContainsKey(number))
            {
                occurence[number]++;
            }
            else
            {
                occurence.Add(number, 1);
            }
        }

        return occurence;
    }
}