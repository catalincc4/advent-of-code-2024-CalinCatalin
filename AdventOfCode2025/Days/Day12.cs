namespace AdventOfCode2025.Days;

public class Day12
{
    public static void ExecutePart1(string[] lines)
    {
        var matrix = Day4.ConstructMatrix(lines);
        FindAllElements(matrix);
    }

    private static void FindAllElements(char[][] matrix)
    {
        var result = 0;
        List<(int, int)> coveredPositions = new List<(int, int)>();
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[0].Length - 1; j++)
            {
                if (!coveredPositions.Contains((i, j)))
                {
                    var (area, perimeter) = CoverElements(matrix, i, j, coveredPositions);
                    Console.WriteLine($"{matrix[i][j]}: Area: {area}, Perimeter: {perimeter}");
                    result += area * perimeter;
                }
            }
        }

        Console.WriteLine(result);
    }

    private static (int, int) CoverElements(char[][] matrix, int i, int j, List<(int, int)> coveredPositions)
    {
        int area = 0;
        int perimeter = 0;
        Queue<(int row, int col)> queue = new Queue<(int row, int col)>();
        queue.Enqueue((i, j));
        Dictionary<(int col, int direction), List<int>> sidesCols = new Dictionary<(int col, int direction), List<int>>();
        Dictionary<(int row, int direction), List<int>> sidesRows = new Dictionary<(int row, int direction), List<int>>();
        var directions = new List<(int, int)>()
        {
            (-1, 0), // up
            (0, 1), // right
            (1, 0), // down
            (0, -1) // left
        };
        coveredPositions.Add((i, j));

        while (queue.Count > 0)
        {
            var (row, col) = queue.Dequeue();
            area += 1;

            foreach (var direction in directions)
            {
                var newRow = row + direction.Item1;
                var newCol = col + direction.Item2;
                if (newRow >= 0 && newRow < matrix.Length && newCol >= 0 && newCol < matrix[0].Length - 1 &&
                    matrix[newRow][newCol] == matrix[i][j])
                {
                    if (!coveredPositions.Contains((newRow, newCol)))
                    {
                        coveredPositions.Add((newRow, newCol));
                        queue.Enqueue((newRow, newCol));
                    }
                }
                else
                {
                    if (direction.Item1 != 0)
                    {
                        if (!sidesRows.ContainsKey((newRow, direction.Item1)))
                        {
                            Console.WriteLine($"({newRow}, {newCol})");
                            perimeter += 1;
                            sidesRows.Add((newRow, direction.Item1), new List<int>(){newCol});
                        }
                        else
                        {
                            sidesRows[(newRow, direction.Item1)].Add(newCol);
                        }
                    }

                    if (direction.Item2 != 0)
                    {
                        if (!sidesCols.ContainsKey((newCol, direction.Item2)))
                        {
                            Console.WriteLine($"({newRow}, {newCol})");
                            perimeter += 1;
                            sidesCols.Add((newCol, direction.Item2), new List<int>() {newRow});
                        }
                        else
                        {
                            sidesCols[(newCol, direction.Item2)].Add(newRow);
                        }
                    }
                }
            }
        }

        foreach (var side in sidesCols)
        {
            perimeter += SearchGaps(side.Value);
        }
        Console.WriteLine("From Gaps roWS");
        foreach (var side in sidesRows)
        {
            perimeter += SearchGaps(side.Value);
        }

        return (area, perimeter);
    }

    private static int SearchGaps(List<int> list)
    {
        int gaps = 0;
        if (list.Count <= 1)
        {
            return 0;
        }
    
        list.Sort();
        for (int i = 0; i < list.Count - 1; i++)
        {
            if (Math.Abs(list[i] - list[i + 1]) > 1)
            {
                gaps += 1;
            }
            
            if (list[i] == list[i+1])
            {
                gaps += 1;
            }
        }
    
        return gaps;
    }
}