using System;
using System.Text.RegularExpressions;

namespace AdventOfCode2025.Days;

public static class Day3
{
    public static void ExecutePart1(string[] lines)
    {
        long sum = 0;
        foreach (var line in lines)
        {
            var instructions = Regex.Matches(line, "mul\\(\\d+,\\d+\\)");
            sum += ProcessInstructions(instructions);
        }

        Console.WriteLine(sum);
    }

    public static void ExecutePart2(string[] lines)
    {
        long sum = 0;
        long result = 0;
        var ignoreMul = false;
        foreach (var line in lines)
        {
            var instructions = Regex.Matches(line, "mul\\(\\d+,\\d+\\)|do\\(\\)|don't\\(\\)");
            (result, ignoreMul) = ProcessInstructions2(instructions, ignoreMul);
            sum += result;
        }

        Console.WriteLine(sum);
    }

    private static long ProcessInstructions(MatchCollection instructions)
    {
        var sum = 0;
        foreach (Match instruction in instructions)
        {
            var parts = Regex.Split(instruction.Value, @"\(|,|\)");
            var firstNumber = int.Parse(parts[1]);
            var secondNumber = int.Parse(parts[2]);
            sum += firstNumber * secondNumber;
        }

        return sum;
    }
    
    private static (long, bool) ProcessInstructions2(MatchCollection instructions, bool ignoreMul = false)
    {
        var sum = 0;
        foreach (Match instruction in instructions)
        {
            Console.WriteLine(instruction.Value);
            if(instruction.Value == "do()")
            {
                ignoreMul = false;
                continue;
            }
            if(instruction.Value == "don't()")
            {
                ignoreMul = true;
                continue;
            }
            if(ignoreMul)
            {
                continue;
            }
            var parts = Regex.Split(instruction.Value, @"\(|,|\)");
            var firstNumber = int.Parse(parts[1]);
            var secondNumber = int.Parse(parts[2]);
            sum += firstNumber * secondNumber;
        }

        return (sum, ignoreMul);
    }
}