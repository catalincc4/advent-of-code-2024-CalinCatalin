namespace AdventOfCode2025.Days;

public class Day6
{
    public static void ExecutePart1(string[] lines)
    {
        var charMatrix = Day4.ConstructMatrix(lines);
        var (startX, startY) = FindStart(charMatrix);
        HashSet<(int, int)> visitedPositions = new HashSet<(int, int)>() { (startX, startY) };
        HashSet<(int, int, int)> visitedPositionsWithDirections =
            new HashSet<(int, int, int)>() { };
        HashSet<(int, int)> loopPositions = new HashSet<(int, int)>() { };
        StartGuardVisiting(charMatrix, startX, startY, visitedPositions, visitedPositionsWithDirections);
        LoopsCheck(charMatrix, startX, startY, visitedPositions, loopPositions, visitedPositionsWithDirections);
        Console.WriteLine();
        // foreach (var (x, y) in loopPositions)
        // {
        //     PrintVisitedPositionsOnMatrix(x, y, charMatrix, visitedPositions, loopPositions);
        //     Console.WriteLine();
        // }
    }

    private static void PrintVisitedPositionsOnMatrix(int xloop, int yloop, char[][] charMatrix,
        HashSet<(int, int)> visitedPositions, HashSet<(int, int)> loopPositions)
    {
        for (int i = 0; i < charMatrix.Length; i++)
        {
            for (int j = 0; j < charMatrix[i].Length; j++)
            {
                if (xloop == i && yloop == j)
                {
                    Console.Write("O");
                }
                else if (visitedPositions.Contains((i, j)))
                {
                    Console.Write("X");
                }
                else
                {
                    Console.Write(charMatrix[i][j]);
                }
            }

            Console.WriteLine();
        }
    }

    private static void StartGuardVisiting(char[][] charMatrix, int startX, int startY,
        HashSet<(int, int)> visitedPositions, HashSet<(int, int, int)> visitedPositionsWithDirections)
    {
        int x = startX - 1;
        int y = startY;
        List<(int, int)> directions = new List<(int, int)>()
        {
            (-1, 0), //up
            (0, 1), //right
            (1, 0), //down
            (0, -1) //left
        };
        int directionIndex = 0;
        while (x >= 0 && x < charMatrix.Length && y >= 0 && y < charMatrix[x].Length)
        {
            if (charMatrix[x][y] == '#')
            {
                x -= directions[directionIndex].Item1;
                y -= directions[directionIndex].Item2;
                directionIndex = (directionIndex + 1) % 4;
            }
            else
            {
                visitedPositionsWithDirections.Add((x, y, directionIndex));
                visitedPositions.Add((x, y));
                x += directions[directionIndex].Item1;
                y += directions[directionIndex].Item2;
            }
        }

        Console.WriteLine(visitedPositions.Count);
    }

    private static void LoopsCheck(char[][] charMatrix, int startX, int startY, HashSet<(int, int)> visitedPositions,
        HashSet<(int, int)> loopsPositions, HashSet<(int, int, int)> visitedPositionsWithDirections)
    {
        List<(int, int)> directions = new List<(int, int)>()
        {
            (-1, 0), //up
            (0, 1), //right
            (1, 0), //down
            (0, -1) //left
        };
        foreach (var (x, y) in visitedPositions)
        {
            charMatrix[x][y] = '#';
            if (CheckIfEnterInALoop(charMatrix, startX, startY, new HashSet<(int, int, int)>(), 0))
            {
                loopsPositions.Add((x, y));
            }
            charMatrix[x][y] = '.';
        }

        Console.WriteLine(loopsPositions.Count);
    }
    
    private static bool CheckIfEnterInALoop(char[][] charMatrix, int startX, int startY, HashSet<(int, int, int)> visitedPositionsWithDirections, int directionIndex)
    {
        int x = startX - 1;
        int y = startY;
        List<(int, int)> directions = new List<(int, int)>()
        {
            (-1, 0), //up
            (0, 1), //right
            (1, 0), //down
            (0, -1) //left
        };
        int repeatedLocations = 0;
        while (x >= 0 && x < charMatrix.Length && y >= 0 && y < charMatrix[x].Length)
        {
            if (charMatrix[x][y] == '#')
            {
                x -= directions[directionIndex].Item1;
                y -= directions[directionIndex].Item2;
                directionIndex = (directionIndex + 1) % 4;
            }
            else
            {
                if(visitedPositionsWithDirections.Contains((x, y, directionIndex)))
                {
                    repeatedLocations++;
                    if (repeatedLocations == 2)
                    {
                        return true;
                    }
                }
                visitedPositionsWithDirections.Add((x, y, directionIndex));
                x += directions[directionIndex].Item1;
                y += directions[directionIndex].Item2;
            }
        }

        return false;
    }
    
    private static bool CheckIfWeCanAddLoop(char[][] charMatrix, (int, int, int) element, List<(int, int)> directions,
        HashSet<(int, int)> loopsPositions, HashSet<(int, int, int)> visitedPositionsWithDirections)
    {
        int x = element.Item1;
        int y = element.Item2;
        int directionIndex = element.Item3;
        int oldX = x - directions[directionIndex].Item1;
        int oldY = y - directions[directionIndex].Item2;
        int newX = oldX + directions[(directionIndex + 1) % 4].Item1;
        int newY = oldY + directions[(directionIndex + 1) % 4].Item2;
        if (visitedPositionsWithDirections.Contains((newX, newY, (directionIndex + 1) % 4)))
        {
            Console.WriteLine("Loop found: " + x + " " + y + "direction: " + directionIndex);
            loopsPositions.Add((x, y));
            return true;
        }


        return false;
    }


    private static (int startX, int startY) FindStart(char[][] charMatrix)
    {
        for (int i = 0; i < charMatrix.Length; i++)
        {
            for (int j = 0; j < charMatrix[i].Length; j++)
            {
                if (charMatrix[i][j] == '^')
                {
                    return (i, j);
                }
            }
        }

        return (-1, -1);
    }
}