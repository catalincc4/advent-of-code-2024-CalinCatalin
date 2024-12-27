using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2025.Days;

public class Day16
{
    public static void ExecutePart1(string[] lines)
    {
        (char[][] matrix, (int x, int y) startPoint, (int x, int y) endpoint) result = ConstructMatrixAndActions(lines);
        int pathLength = AStarSearch(result.matrix, result.startPoint, result.endpoint);
        PrintMatrix(result.matrix);
        Console.WriteLine(pathLength);
    }

    private static void PrintMatrix(char[][] matrix)
    {
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                Console.Write(matrix[i][j]);
            }

            Console.WriteLine();
        }
    }


    private static int AStarSearch(char[][] matrix, (int x, int y) startPoint, (int x, int y) endPoint)
    {
        Queue<(int, int, int, int direction, Dictionary<(int, int), int>)> queue =
            new Queue<(int, int, int, int direction, Dictionary<(int, int), int>)>();
        queue.Enqueue((startPoint.x, startPoint.y, 0, 1,
            new Dictionary<(int, int), int>() { { (startPoint.x, startPoint.y), 1 } }));
        List<int> distances = new List<int>();
        List<(int dx, int dy)> directions = new List<(int dx, int dy)>()
        {
            (-1, 0), //up
            (0, 1), //right
            (1, 0), //down
            (0, -1) //left
        };
        int minDistance = int.MaxValue;
        while (queue.Count > 0)
        {
            var (x, y, distance, direction, coveredPositions) = queue.Dequeue();

            List<(int x, int y, int distance, int dir, int score)> candidates =
                new List<(int x, int y, int distance, int dir, int score)>();
            for (int i = 0; i < 4; i++)
            {
                var (dx, dy) = directions[i];

                int newX = x + dx;
                int newY = y + dy;
                if (newX >= 0 && newX < matrix.Length && newY >= 0 && newY < matrix[0].Length)
                {
                    if (matrix[newX][newY] == 'E' && Math.Abs(i - direction) != 2)
                    {
                        var newDistance = distance;
                        if (direction != i)
                        {
                            newDistance += 1001;
                        }
                        else
                        {
                            newDistance += 1;
                        }

                        distances.Add(newDistance);
                        if (distances.Count == 4)
                        {
                            return distances.Min();
                        }
                        //continue;
                    }

                    if (matrix[newX][newY] == '.' && Math.Abs(i - direction) != 2 &&
                        !coveredPositions.ContainsKey((newX, newY)))
                    {
                        var newDistance = distance;
                        if (direction != i)
                        {
                            newDistance += 1001;
                        }
                        else
                        {
                            newDistance += 1;
                        }
                        coveredPositions.Add((newX, newY), 1);
                        candidates.Add((newX, newY, newDistance, i, Math.Abs(y - endPoint.y) + Math.Abs(x - endPoint.x)*1000));
                    }
                }
            }
            
            candidates = candidates.OrderBy(c => c.score).ToList();

            foreach (var candidate in candidates.OrderBy(c => c.score))
            {
                queue.Enqueue((candidate.x, candidate.y, candidate.distance, candidate.dir, coveredPositions));
            }
        }

        return distances.Min();
    }

    private static (char[][] matrix, (int x, int y) startPoint, (int x, int y) endpoint) ConstructMatrixAndActions(
        string[] lines)
    {
        char[][] matrix = new char[lines.Length][];
        var endpoint = (0, 0);
        var startPoint = (0, 0);
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i].Where(c => c != '\n').ToArray();
            if (line.Contains('E'))
            {
                endpoint = (i, Array.IndexOf(line, 'E'));
            }

            if (line.Contains('S'))
            {
                startPoint = (i, Array.IndexOf(line, 'S'));
            }

            matrix[i] = line;
        }

        return (matrix, startPoint, endpoint);
    }
}