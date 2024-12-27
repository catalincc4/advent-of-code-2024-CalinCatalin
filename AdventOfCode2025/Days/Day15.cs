using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2025.Days;

public class Day15
{
    public static void ExecutePart1(string[] lines)
    {
        (char[][] matrix, List<char> actions) = ConstructMatrixAndActions(lines);
       // // PrintMatrix(matrix);
       //  Console.WriteLine("Actions");
       //  foreach (var action in actions)
       //  {
       //      Console.Write(action);
       //  }
       //  Console.WriteLine();
        var (x, y) = FindRobotPosition(matrix);
        MoveRobotByActions(matrix, actions, x, y);
        // Console.WriteLine("After moving robot");
        //PrintMatrix(matrix);
        CalculateSumOfGpsCoordinates(matrix);
    }
    
    private static void CalculateSumOfGpsCoordinates(char[][] matrix)
    {
        var sum = 0;
        for (int i = 1; i < matrix.Length - 1 ; i++)
        {
            for (int j = 1; j < matrix[0].Length - 1; j++)
            {
                // if(matrix[i][j] == 'O')
                // {
                //     sum += i * 100 + j;
                // }
                if(matrix[i][j] == '[')
                {
                    sum += i * 100 + j;
                }
            }
        }
        Console.WriteLine(sum);
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
    
    private static void MoveRobotByActions(char[][] matrix, List<char> actions, int x, int y)
    {
        var currentX = x;
        var currentY = y;

        foreach (var action in actions)
        {
           (currentX, currentY) = Move(matrix, currentX, currentY, action);
        }
    }

    private static (int x, int y) Move(char[][] matrix, int x, int y, char action)
    {
        var directions = new List<(int, int)>()
        {
            (-1, 0), // up
            (0, 1), // right
            (1, 0), // down
            (0, -1) // left
        };
        (int x, int y) emptySpace = (-1, -1);
        if (action == '^')
        {
            emptySpace = FindEmptySpace(matrix, x, y, directions[0]);
            if (emptySpace.x != -1)
            {
                MoveAllObjects(matrix, x, y, emptySpace, directions[2]);
                return (x+directions[0].Item1, y+directions[0].Item2);
            }
        }
        else if (action == 'v')
        {
            emptySpace = FindEmptySpace(matrix, x, y, directions[2]);
            if (emptySpace.x != -1)
            {
                MoveAllObjects(matrix, x, y, emptySpace, directions[0]);   
                return (x+directions[2].Item1, y+directions[2].Item2);
            }
            
        }
        else if (action == '<')
        {
            emptySpace = FindEmptySpace(matrix, x, y, directions[3]);
            if (emptySpace.x != -1)
            {
                MoveAllObjects(matrix, x, y, emptySpace, directions[1]);  
                return (x+directions[3].Item1, y+directions[3].Item2);
            }
        }
        else if (action == '>')
        {
            emptySpace = FindEmptySpace(matrix, x, y, directions[1]);
            if (emptySpace.x != -1)
            {
                MoveAllObjects(matrix, x, y, emptySpace, directions[3]);   
                return (x+directions[1].Item1, y+directions[1].Item2);
            }
        }

        return (x, y);
    }

    private static void MoveAllObjects(char[][] matrix, int x1, int y1, (int x, int y) emptySpace, (int, int) direction)
    {
        if(emptySpace.x == x1 && emptySpace.y == y1)
        {
            matrix[x1][y1] = '.';
            return;
        }
        var newX = emptySpace.x + direction.Item1;
        var newY = emptySpace.y + direction.Item2;
        matrix[emptySpace.x][emptySpace.y] = matrix[newX][newY];
        MoveAllObjects(matrix, x1, y1, (newX, newY), direction);
    }

    private static (int x, int y) FindEmptySpace(char[][] matrix, int x, int y, (int dx, int dy) direction)
    {
        var newX = x + direction.dx;
        var newY = y + direction.dy;
        if (!(newX >= 0 && newX < matrix.Length && newY >= 0 && newY < matrix[newX].Length))
        {
            return (-1, -1);
        }

        if (matrix[newX][newY] == '#')
        {
            return (-1, -1);
        }

        if (matrix[newX][newY] == '.')
        {
            return (newX, newY);
        }

        return FindEmptySpace(matrix, newX, newY, direction);
    }

    private static (int x, int y) FindRobotPosition(char[][] matrix)
    {
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                if (matrix[i][j] == '@')
                {
                    return (i, j);
                }
            }
        }

        return (-1, -1);
    }

    private static (char[][] matrix, List<char> actions) ConstructMatrixAndActions(string[] lines)
    {
        int i = 0;
        List<char[]> matrix = new List<char[]>();
        List<char> actions = new List<char>();
        while (lines[i].Length > 2)
        {
            var line = lines[i].ToCharArray();
            char[] row = new char[line.Length - 1]; 
            Array.Copy(line, 0, row, 0, line.Length - 1);

           matrix.Add(row);
           i++;
        }

        i++;
        while (i < lines.Length)
        {
            var line = lines[i].ToCharArray();
            actions.AddRange(line.Where(x => x != '\n'));
            i++;
        }

        return (matrix.ToArray(), actions);
    }
}