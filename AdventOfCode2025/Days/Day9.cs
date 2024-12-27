using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2025.Days;

public class Day9
{
    public static void ExecutePart1(string[] lines)
    {
        var positions = ParseLines(lines);
        CalculateChecksum(positions.unavailablePositions, positions.availablePositions);
    }

    private static void CalculateChecksum(LinkedList<(int, int)> unavailablePositions, List<int> availablePositions)
    {
        long checksum = 0;

        foreach (var pos in availablePositions)
        {
            var last = unavailablePositions.Last();
            if (last.Item2 > pos)
            {
                unavailablePositions.Remove(last);
                unavailablePositions.AddFirst((last.Item1, pos));
            }
        }

        foreach (var (id, pos) in unavailablePositions)
        {
            // Console.WriteLine($"id: {id}, pos: {pos}");
            checksum += id * pos;
        }

        Console.WriteLine(checksum);
    }

    private static (LinkedList<(int, int)> unavailablePositions, List<int> availablePositions) ParseLines(string[] lines)
    {
        var unavailablePositions = new LinkedList<(int, int)>();
        var availablePositions = new List<int>();
        int id = 0;
        var charNumbers = lines[0].ToCharArray();
        int startIndex = 0;
        Console.WriteLine(lines.Length);
        for (int i = 0; i < charNumbers.Length; i += 2)
        {
            int number = int.Parse(charNumbers[i].ToString());
            for (int j = startIndex; j < startIndex + number; j++)
            {
                    unavailablePositions.AddLast((id, j));
            }
            startIndex += number;
            if(i+1 < charNumbers.Length)
            {
                int whiteSpaces = int.Parse(charNumbers[i+1].ToString());
                for (int j = startIndex; j < startIndex + whiteSpaces; j++)
                {
                    availablePositions.Add(j);
                }
                startIndex += whiteSpaces;
            }
            id++;
        }
        
        Console.WriteLine(id);

        return (unavailablePositions, availablePositions);
    }
    
    public static void ExecutePart2(string[] lines)
    {
        var memory = ParseLines2(lines);
        CompressMemory(memory);
        CalculateChecksum2(memory.occupidedMemory);
    }

    private static void CalculateChecksum2(List<(int id, int numberOfBlocks, int startPos)> memoryOccupidedMemory)
    {
        long checksum = 0;
        foreach (var (id, numberOfBlocks, startPos) in memoryOccupidedMemory)
        {
           // Console.WriteLine($"id: {id}, numberOfBlocks: {numberOfBlocks}, startPos: {startPos}");
            for(int i = startPos; i < startPos + numberOfBlocks; i++)
            {
                checksum += id * i;
            }
        }
        
        Console.WriteLine(checksum);
    }

    private static void CompressMemory((List<(int id, int numberOfBlocks, int startPos)> occupidedMemory, List<(int startPos, int numberOfBlocks)> freeMemory) memory)
    {
        int i = 0;
        int n  = memory.occupidedMemory.Count;
        while( i < n)
        {
            var lastFile = memory.occupidedMemory[n-i-1]; 
            var freeMemorySpaceIndex = memory.freeMemory.FindIndex(x => x.numberOfBlocks >= lastFile.numberOfBlocks && x.startPos < lastFile.startPos);
            if (freeMemorySpaceIndex != -1)
            {
                var freeMemorySpace = memory.freeMemory[freeMemorySpaceIndex];
                memory.occupidedMemory.RemoveAt(n-i-1);
                memory.occupidedMemory.Add((lastFile.id, lastFile.numberOfBlocks, freeMemorySpace.startPos));
                freeMemorySpace.numberOfBlocks -= lastFile.numberOfBlocks;
                freeMemorySpace.startPos += lastFile.numberOfBlocks;
                memory.freeMemory[freeMemorySpaceIndex] = freeMemorySpace;
            }

            i++;
        }
    }

    private static (List<(int id, int numberOfBlocks, int startPos)> occupidedMemory, List<(int startPos, int numberOfBlocks)> freeMemory) ParseLines2(string[] lines)
    {
       var occupiedMemory = new List<(int id, int numberOfBlocks, int startPos)>();
         var freeMemory = new List<(int startPos, int numberOfBlocks)>();
        var charNumbers = lines[0].ToCharArray();
        int startIndex = 0;

        for (int i = 0; i < charNumbers.Length; i+=2)
        {
            int number = int.Parse(charNumbers[i].ToString());
            occupiedMemory.Add((i/2, number, startIndex));
            startIndex += number;
            if(i+1 < charNumbers.Length)
            {
                int whiteSpaces = int.Parse(charNumbers[i+1].ToString());
                freeMemory.Add((startIndex, whiteSpaces));
                startIndex += whiteSpaces;
            }  
        }
        
        return (occupiedMemory, freeMemory);
    }
}