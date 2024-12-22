namespace AdventOfCode2025.Days;

public class Day7
{
    public static void ExecutePart1(string[] lines)
    {
        var equations = ParseLines(lines);
        var sumOfCorrectEquations = CheckEquations(equations);
        Console.WriteLine(sumOfCorrectEquations);
    }

    private static long CheckEquations(List<(long resultOfEquation, List<long> numbers)> equations)
    {
        long sumOfCorrectEquations = 0;
        foreach (var equation in equations)
        {
            if (CheckEquation(equation, 1, equation.numbers[0]))
            {
                sumOfCorrectEquations += equation.resultOfEquation;
            }
        }

        return sumOfCorrectEquations;
    }

    private static bool CheckEquation((long resultOfEquation, List<long> numbers) equation,int startIndex, long result)
    {
        if(equation.resultOfEquation == result && equation.numbers.Count == startIndex)
        {
            return true;
        }

        for (int i = startIndex; i < equation.numbers.Count; i++)
        {
            return CheckEquation(equation, i + 1, result + equation.numbers[i]) ||
                   CheckEquation(equation, i + 1, result * equation.numbers[i]) ||
                   CheckEquation(equation, i + 1, long.Parse(result.ToString() + equation.numbers[i].ToString()));
        }

        return false;
    }

    private static List<(long resultOfEquation, List<long> numbers)> ParseLines(string[] lines)
    {
        var equations = new List<(long resultOfEquation, List<long> numbers)>();
        foreach (var line in lines)
        {
            equations.Add(ParseLine(line));
        }

        return equations;
    }
    private static (long resultOfEquation, List<long> numbers) ParseLine(string line)
    {
        var parts = line.Split(":");
        var resultOfEquation = long.Parse(parts[0]);

        List<long> numbers = new();
        var numbersParts = parts[1].Split(" ");
        foreach (var number in numbersParts)
        {
            if (long.TryParse(number, out var numberValue))
            {
                numbers.Add(numberValue);
            }
        }

        return (resultOfEquation, numbers);
    }
}