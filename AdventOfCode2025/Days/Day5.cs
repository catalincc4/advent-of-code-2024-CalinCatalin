namespace AdventOfCode2025.Days;

public class Day5
{
    public static void ExecutePart1(string[] lines)
    {
        var (rules, printersSets) = ParseLines(lines);
        var correctPrinters = FindIncorrectPrintersAndRearrange(rules, printersSets);
        Console.WriteLine(correctPrinters);
    }

    private static int CheckCorrectPrinters(Dictionary<int, List<int>> rules, List<List<int>> printersSets)
    {
        int correctPrinters = 0;
        var sumOfMiddlePages = 0;
        foreach (var printersSet in printersSets)
        {
            if (CheckPrintersSet(rules, printersSet))
            {
                correctPrinters++;
                sumOfMiddlePages += printersSet[printersSet.Count / 2];
            }
        }

        Console.WriteLine(sumOfMiddlePages);
        return correctPrinters;
    }
    
    private static int FindIncorrectPrintersAndRearrange(Dictionary<int, List<int>> rules, List<List<int>> printersSets)
    {
        int incorrectPrinters = 0;
        var sumOfMiddlePages = 0;
        foreach (var printersSet in printersSets)
        {
            if (!CheckPrintersSet(rules, printersSet))
            {
                incorrectPrinters++;
                RearrangeList(rules, printersSet);
                sumOfMiddlePages += printersSet[printersSet.Count / 2];
            }
        }

        Console.WriteLine(sumOfMiddlePages);
        return incorrectPrinters;
    }

    private static void RearrangeList(Dictionary<int, List<int>> rules, List<int> printersSet)
    {
        for (int i = printersSet.Count - 1; i > 0; i--)
        {
            for (int j = i; j >= 0; j--)
            {
                if (rules.TryGetValue(printersSet[i], out var pagesAfter))
                {
                    if (pagesAfter.Contains(printersSet[j]))
                    {
                        (printersSet[i], printersSet[j]) = (printersSet[j], printersSet[i]);
                        RearrangeList(rules, printersSet);
                    }
                }
            }
        }
    }

    private static bool CheckPrintersSet(Dictionary<int, List<int>> rules, List<int> printersSet)
    {
        for (int i = printersSet.Count - 1; i > 0; i--)
        {
            for (int j = i; j >= 0; j--)
            {
                if (rules.TryGetValue(printersSet[i], out var pagesAfter))
                {
                    if (pagesAfter.Contains(printersSet[j]))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }


    private static (Dictionary<int, List<int>>, List<List<int>>) ParseLines(string[] lines)
    {
        int i = 0;
        Dictionary<int, List<int>> rules = new Dictionary<int, List<int>>();
        while (lines[i] != "\n" && i < lines.Length && lines[i].Length > 1 && !string.IsNullOrEmpty(lines[i]))
        {
            var parts = lines[i].Split("|");
            var pageNumberFirst = int.Parse(parts[0]);
            var pageNumberSecond = int.Parse(parts[1]);
            if (rules.ContainsKey(pageNumberFirst))
            {
                rules[pageNumberFirst].Add(pageNumberSecond);
            }
            else
            {
                rules.Add(pageNumberFirst, new List<int> { pageNumberSecond });
            }

            i++;
        }

        i++;
        List<List<int>> printersSets = new List<List<int>>();
        while (i < lines.Length)
        {
            List<int> set = new List<int>();
            var parts = lines[i].Split(",");
            foreach (var part in parts)
            {
                set.Add(int.Parse(part));
            }

            i++;
            printersSets.Add(set);
        }

        return (rules, printersSets);
    }
}