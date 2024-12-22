namespace AdventOfCode2025.Days;

public class Day4
{
    private static HashSet<(int, int)> positions = new HashSet<(int, int)>();
    public static void ExecutePart1(string[] lines)
    {
        var charMatrix = ConstructMatrix(lines);
        var xmasWords = FindWords(charMatrix);
        Console.WriteLine(xmasWords);
        PrintMatrix(charMatrix);
    }
    
    public static void ExecutePart2(string[] lines)
    {
        var charMatrix = ConstructMatrix(lines);
        var xmasWords = FindWords2(charMatrix);
        Console.WriteLine(xmasWords);
        PrintMatrix(charMatrix);
    }
    
    private static int FindWords2(char[][] matrix)
    {
        int xmasWords = 0;
        for(int i = 0; i < matrix.Length; i++)
        {
            for(int j = 0; j < matrix[i].Length; j++)
            {
                if(matrix[i][j] == 'A')
                {
                    xmasWords += StartSearch2(matrix, i, j);
                }
            }
        }

        return xmasWords;
    }
    
    private static int StartSearch2(char[][] matrix, int i, int j)
    {
        // bottom right
        int x = i + 1;
        int y = j + 1;
        // top left
        int x2 = i - 1;
        int y2 = j - 1;
        if(x >= 0 && x < matrix.Length && y >= 0 && y < matrix[x].Length && x2 >= 0 && x2 < matrix.Length && y2 >= 0 && y2 < matrix[x2].Length)
        {
            if(!((matrix[x][y] == 'M' && matrix[x2][y2] == 'S') || (matrix[x][y] == 'S' && matrix[x2][y2] == 'M')))
            {
                return 0;
            }
        }else
        {
            return 0;
        }
        
        // top right
        int x3 = i - 1;
        int y3 = j + 1;

        // bottom left
        int x4 = i + 1;
        int y4 = j - 1;
        if (x3 >= 0 && x3 < matrix.Length && y3 >= 0 && y3 < matrix[x3].Length && x4 >= 0 && x4 < matrix.Length &&
            y4 >= 0 && y4 < matrix[x4].Length)
        {
            if (!((matrix[x3][y3] == 'M' && matrix[x4][y4] == 'S') || (matrix[x3][y3] == 'S' && matrix[x4][y4] == 'M')))
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }

        positions.Add((i, j));
        positions.Add((x, y));
        positions.Add((x2, y2));
        positions.Add((x3, y3));
        positions.Add((x4, y4));
        return 1;
    }
    
    private static void PrintMatrix(char[][] matrix)
    {
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                if (positions.Contains((i, j)))
                {
                    Console.Write(matrix[i][j]);   
                }else
                {
                    Console.Write('.');
                }
            }
            Console.WriteLine();
        }   
    }

    private static char[][] ConstructMatrix(string[] lines)
    {
        char[][] charMatrix = new char[lines.Length][];
        int i = 0;
        foreach (var line in lines)
        {
            charMatrix[i] = new char[line.Length];
            var charArray = line.ToCharArray();
            charMatrix[i] = charArray;
            i++;
        }
        return charMatrix;
    }

    private static int FindWords(char[][] matrix)
    {
        int xmasWords = 0;
        int[] directions = new int[] {1, 0, -1};
        for(int i = 0; i < matrix.Length; i++)
        {
            for(int j = 0; j < matrix[i].Length; j++)
            {
                if(matrix[i][j] == 'X')
                {
                   xmasWords += StartSearch(matrix, i, j, directions);
                }
            }
        }

        return xmasWords;
    }

    private static int StartSearch(char[][] matrix, int i, int j, int[] directions)
    {
        int xmasWords = 0;
        for(int l = 0; l < directions.Length; l++)
        {
            for(int k = 0; k < directions.Length; k++)
            {
                if(directions[l] == 0 && directions[k] == 0)
                {
                    continue;
                }
                xmasWords += SearchWord(matrix, i, j, directions[l], directions[k]);
            }
        }

        return xmasWords;
    }
    
    private static int SearchWord(char[][] matrix, int i, int j, int l, int k)
    {
        List<(int, int)> newPositions = new List<(int, int)>() { (i, j) };
        int x = i + l;
        int y = j + k;
        char[] word = new char[] {'X', 'M', 'A', 'S'};
        int index = 1;
        while(index < word.Length && x >= 0 && x < matrix.Length && y >= 0 && y < matrix[x].Length)
        {
            if(matrix[x][y] != word[index])
            {
                return 0;
            }
            newPositions.Add((x, y));
            index++;
            x += l;
            y += k;
        }
        
        if(index == word.Length)
        {
            foreach (var pos in newPositions)
            {
                positions.Add(pos);
            }
            return 1;
        }
        return 0;
    }
}