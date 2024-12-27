using System.Text.RegularExpressions;

namespace AdventOfCode2025.Days;

public class Day14
{
    public static void ExecutePart1(string[] lines)
    {
        List<((int x, int y) position, (int x, int y) velocity)> robots = ParseLines(lines);
        for(int i = 2024; i < 40000000 ; i++)
        {
            CalculatePositions(robots, i, 103, 101);
        }
   
    }

    private static void CalculateAndPrintPositions(List<((int x, int y) position, (int x, int y) velocity)> robots,
        int seconds, int xDimension, int yDimension)
    {
        StreamWriter fileStream = File.AppendText($"output{seconds}.txt");
        bool[][] matrix = new bool[yDimension][];
        for (int i = 0; i < yDimension; i++)
        {
            matrix[i] = new bool[xDimension];
        }

        foreach (var robot in robots)
        {
            var pos = CalculatePositionAfterXSeconds(robot.position, robot.velocity, seconds, xDimension, yDimension);
            matrix[pos.Item2][pos.Item1] = true;
        }

        fileStream.WriteLine($"Seconds: {seconds}");
        for (int i = 0; i < yDimension; i++)
        {
            for (int j = 0; j < xDimension; j++)
            {
                if (matrix[i][j])
                {
                   fileStream.Write("#");
                }
                else
                {
                    fileStream.Write(".");
                }
            }

            fileStream.WriteLine();
        }
    }


    private static void CalculatePositions(List<((int x, int y) position, (int x, int y) velocity)> robots, int seconds,
        int xDimension, int yDimension)
    {
        var quadrant1 = 0;
        var quadrant2 = 0;
        var quadrant3 = 0;
        var quadrant4 = 0;
        foreach (var robot in robots)
        {
            var (x, y) =
                CalculatePositionAfterXSeconds(robot.position, robot.velocity, seconds, xDimension, yDimension);
            if (x >= 0 && x < xDimension / 2 && y >= 0 && y < yDimension / 2)
            {
                // Console.Write("q1 ");
                // Console.WriteLine($"x: {x}, y: {y}");
                quadrant1++;
            }
            else if (x > xDimension / 2 && x < xDimension && y >= 0 && y < yDimension / 2)
            {
                // Console.Write("q2 ");
                // Console.WriteLine($"x: {x}, y: {y}");
                quadrant2++;
            }
            else if (x >= 0 && x < xDimension / 2 && y > yDimension / 2 && y < yDimension)
            {
                // Console.Write("q3 ");
                // Console.WriteLine($"x: {x}, y: {y}");
                quadrant3++;
            }
            else if (x > xDimension / 2 && x <= xDimension && y > yDimension / 2 && y < yDimension)
            {
                // Console.Write("q4 ");
                // Console.WriteLine($"x: {x}, y: {y}");
                quadrant4++;
            }
        }

        if (quadrant1 == quadrant2 && quadrant3 == quadrant4)
        {
            Console.WriteLine("All quadrants have the same number of robots for second: " + seconds);
        }
    }

    private static (int, int) CalculatePositionAfterXSeconds((int x, int y) position, (int x, int y) velocity,
        long seconds, int xDimension, int yDimension)
    {
        long x = 0;
        long y = 0;
        if (velocity.x >= 0)
        {
            x = position.x + (velocity.x * seconds) % xDimension;
            x %= xDimension;
        }
        else
        {
            x = position.x + ((long)(velocity.x * seconds) % xDimension);
            if (x < 0)
            {
                x = xDimension + x;
            }
        }

        if (velocity.y >= 0)
        {
            y = position.y + (long)(velocity.y * seconds) % yDimension;
            y %= yDimension;
        }
        else
        {
            y = (position.y + ((long)(velocity.y * seconds) % yDimension));
            if (y < 0)
            {
                y = yDimension + y;
            }
        }

        // Console.WriteLine($"x: {x}, y: {y}");
        return ((int)x, (int)y);
    }

    private static List<((int x, int y) position, (int x, int y) velocity)> ParseLines(string[] lines)
    {
        List<((int x, int y) position, (int x, int y) velocity)> robots =
            new List<((int x, int y) position, (int x, int y) velocity)>();
        foreach (var line in lines)
        {
            var parts = Regex.Matches(line, "-*\\d+").Select(m => m.Value).ToArray();
            var position = (int.Parse(parts[0]), int.Parse(parts[1]));
            var velocity = (int.Parse(parts[2]), int.Parse(parts[3]));
            robots.Add((position, velocity));
        }

        return robots;
    }
}