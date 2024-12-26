namespace AdventOfCode2025.Days;

public class Day11
{
    public static void ExecutePart1(string[] lines)
    {
        var numbers = ParseLines(lines);
        //BlinkXTimes(numbers, 25);
        CountStonesAfterBlinks(numbers, 75);
    }

    static long CountStonesAfterBlinks(List<long> stones, int blinks)
    {
        Dictionary<long, long> stoneCounts = new Dictionary<long, long>();

        foreach (var stone in stones)
        {
            if (!stoneCounts.ContainsKey(stone))
            {
                stoneCounts[stone] = 0;
            }

            stoneCounts[stone]++;
        }

        for (int i = 0; i < blinks; i++)
        {
            Dictionary<long, long> newCounts = new Dictionary<long, long>();

            foreach (var stoneCount in stoneCounts)
            {
                long stone = stoneCount.Key;
                long count = stoneCount.Value;

                if (stone == 0)
                {
                    if (!newCounts.ContainsKey(1))
                        newCounts[1] = 0;
                    newCounts[1] += count;
                }
                else if (stone.ToString().Length % 2 == 0)
                {
                    int length = stone.ToString().Length;
                    var leftSide = stone.ToString().Substring(0, length / 2);
                    var rightSide = stone.ToString().Substring(length / 2);

                    long left = long.Parse(leftSide);
                    long right = long.Parse(rightSide);
                    if (!newCounts.ContainsKey(left))
                        newCounts[left] = 0;
                    if (!newCounts.ContainsKey(right))
                        newCounts[right] = 0;

                    newCounts[left] += count;
                    newCounts[right] += count;
                }
                else
                {
                    long newStone = stone * 2024;

                    if (!newCounts.ContainsKey(newStone))
                        newCounts[newStone] = 0;

                    newCounts[newStone] += count;
                }
            }

            stoneCounts = newCounts;
        }

        long totalStones = 0;
        foreach (var count in stoneCounts.Values)
        {
            totalStones += count;
        }

        Console.WriteLine(totalStones);
        return totalStones;
    }

    private static List<long> Blink1(List<long> stones)
    {
        List<long> newStones = new List<long>();
        for (int i = 0; i < stones.Count; i++)
        {
            if (stones[i] == 0)
            {
                newStones.Add(1);
            }
            else if (stones[i].ToString().Length % 2 == 0)
            {
                int length = stones[i].ToString().Length;
                var leftSide = stones[i].ToString().Substring(0, length / 2);
                var rightSide = stones[i].ToString().Substring(length / 2);

                newStones.Add(long.Parse(leftSide));
                newStones.Add(long.Parse(rightSide));
            }
            else
            {
                newStones.Add(stones[i] * 2024);
            }
        }

        return newStones;
    }

    private static List<long> ParseLines(string[] lines)
    {
        var numbers = new List<long>();
        var parts = lines[0].Split(" ");
        foreach (var part in parts)
        {
            if (long.TryParse(part, out long number))
            {
                numbers.Add(number);
            }
        }

        return numbers;
    }
}