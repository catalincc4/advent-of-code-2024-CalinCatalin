using System;
using System.Collections.Generic;

namespace AdventOfCode2025.Days;

public class Day10
{
    public static void ExecutePart1(string[] lines)
    {
        List<(int, int)> trailHeads = new List<(int, int)>();
        int[][] matrix = ParseLines(lines, trailHeads);
        CalculateScore(matrix, trailHeads);
    }

    private static int CalculateScore(int[][] matrix, List<(int, int)> trailHeads)
    {
        int score = 0;
        foreach (var (row, column) in trailHeads)
        {
            List<(int, int)> tiles = new List<(int, int)>();
            Find9(matrix, row, column, 0, tiles);
            score += tiles.Count;
        }

        Console.WriteLine(score);
        return score;
    }

    private static void Find9(int[][] matrix, int currentRow, int currentColumn, int tileNumber,
        List<(int, int)> tiles)
    {
        if (tileNumber == 9 && matrix[currentRow][currentColumn] == 9)
        {
            tiles.Add((currentRow, currentColumn));
            return;
        }

        List<(int, int)> directions = new List<(int, int)>()
        {
            (1, 0),
            (0, 1),
            (-1, 0),
            (0, -1)
        };
        
        if (tileNumber == matrix[currentRow][currentColumn])
        {
            foreach (var (x, y) in directions)
            {
                if (currentRow + x >= 0 && currentRow + x < matrix.Length && currentColumn + y >= 0 &&
                    currentColumn + y < matrix[0].Length - 1)
                {
                    Find9(matrix, currentRow + x, currentColumn + y, tileNumber + 1, tiles);
                }
            }
        }
    }

    private static int[][] ParseLines(string[] lines, List<(int, int)> trailHeads)
    {
        int[][] matrix = new int[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            matrix[i] = new int[lines[i].Length];
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (int.TryParse(lines[i][j].ToString(), out int result))
                {
                    matrix[i][j] = result;
                    if (result == 0)
                    {
                        trailHeads.Add((i, j));
                    }
                }
            }
        }

        return matrix;
    }
}