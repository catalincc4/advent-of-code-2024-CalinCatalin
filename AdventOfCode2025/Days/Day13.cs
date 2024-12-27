﻿using System.Text.RegularExpressions;

namespace AdventOfCode2025.Days;

public class Day13
{
    public static void ExecutePart1(string[] lines)
    {
        List<(List<(int x, int y)> buttons, int xTarget, int yTarget)> games = ParseLines(lines);
        //PrintInput(games);
        CalculateTokens(games);
    }

    static void CalculateTokens(List<(List<(int x, int y)> buttons, int xTarget, int yTarget)> games)
    {
        var tokens = 0;
        foreach (var game in games)
        {
            var (A, B) = CalculateNumberOfPressesForButtons(game.buttons, game.xTarget, game.yTarget);
            tokens += A * 3 + B * 1;
        }
        
        Console.WriteLine(tokens);
    }

    static (int A, int B) CalculateNumberOfPressesForButtons(List<(int x, int y)> buttons, int xTarget, int yTarget)
    {
        var XA = buttons[0].x;
        var YA = buttons[0].y;
        var XB = buttons[1].x;
        var YB = buttons[1].y;
        
        var totalY = XB*YA - XA*YB;
        var totalYTarget = YA*xTarget - XA*yTarget;
        if(totalYTarget % totalY != 0)
        {
            return (0, 0);
        }
        
        var B = totalYTarget / totalY;
        var A = (xTarget - B * XB) / XA;
        return (A, B);
    }

    static void PrintInput(List<(List<(int x, int y)> buttons, int xTarget, int yTarget)> games)
    {
        foreach (var (buttons, xTarget, yTarget) in games)
        {
            foreach (var button in buttons)
            {
                Console.WriteLine($"Button: {button.x}, {button.y}");
            }

            Console.WriteLine($"Target: {xTarget}, {yTarget}");
        }
    }

    private static List<(List<(int x, int y)> buttons, int xTarget, int yTarget)> ParseLines(string[] lines)
    {
        var currentLine = 0;
        var games = new List<(List<(int x, int y)> buttons, int xTarget, int yTarget)>();
        while (currentLine + 3 <= lines.Length)
        {
            var buttons = new List<(int x, int y)>();
            var buttonsParts = Regex.Matches(lines[currentLine], "\\d+");
            buttons.Add((int.Parse(buttonsParts[0].Value), int.Parse(buttonsParts[1].Value)));
            buttonsParts = Regex.Matches(lines[currentLine + 1], "\\d+");
            buttons.Add((int.Parse(buttonsParts[0].Value), int.Parse(buttonsParts[1].Value)));
            var targetParts = Regex.Matches(lines[currentLine + 2], "\\d+");
            games.Add((buttons, int.Parse(targetParts[0].Value), int.Parse(targetParts[1].Value)));

            currentLine += 4;
        }

        return games;
    }
}