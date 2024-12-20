using System.Text.RegularExpressions;

namespace AdventOfCode2025.Days;

public class Day2
{
    public static void ExecutePart1(string[] lines)
    {
        var reports = ExtractReports(lines);
        var safeReports = VerifyReports(reports, CheckReport1);
        Console.WriteLine(safeReports);
    }
    
    public static void ExecutePart2(string[] lines)
    {
        var reports = ExtractReports(lines);
        var safeReports = VerifyReports(reports, CheckReport2);
        Console.WriteLine(safeReports);
    }
    
    private static int VerifyReports(List<List<int>> reports, Func<List<int>, bool, bool> CheckReport)
    {
        var safeReports = 0;
        foreach (var report in reports)
        {
            if (CheckReport(report, false))
            {
                safeReports++;
            }
        }

        return safeReports;
    }
    
    private static bool CheckReport1(List<int> report, bool levelExcluded = false)
    {
        var firstNumber = report[0];
        var secondNumber = report[1];
        var difference = firstNumber - secondNumber;
        if (Math.Abs(difference) > 3 || difference == 0)
        {
            return false;
        }
        bool increasing = difference > 0;
        
        for (int i = 1; i < report.Count - 1; i++)
        {
            firstNumber = report[i];
            secondNumber = report[i+1];
            difference = firstNumber - secondNumber;
            if (Math.Abs(difference) > 3 || difference == 0)
            {
                return false;
            }
            if (increasing && difference < 0)
            {
                return false;
            }
            if (!increasing && difference > 0)
            {
                return false;
            }
        }
        return true;
    }
    
    private static bool CheckReport2(List<int> report, bool levelExcluded = false)
    {
        var firstNumber = report[0];
        var secondNumber = report[1];
        var difference = firstNumber - secondNumber;
        if (Math.Abs(difference) > 3 || difference == 0)
        {
            if (levelExcluded)
            {
                return false;
            }
            for(int j = 0; j < report.Count; j++)
            {
                List<int> newReport = new(report);
                newReport.RemoveAt(j);
                if (CheckReport2(newReport, true))
                {
                    return true;
                }
            }

            return false;
        }
        bool increasing = difference > 0;

        int i = 1;
        while(i < report.Count - 1)
        {
            firstNumber = report[i];
            secondNumber = report[i + 1];
            difference = firstNumber - secondNumber;
            if (Math.Abs(difference) > 3 
                || difference == 0
                || (increasing && difference < 0)
                || (!increasing && difference > 0))
            {
                if (levelExcluded)
                {
                    return false;
                }
                for(int j = 0; j < report.Count; j++)
                {
                    List<int> newReport = new(report);
                    newReport.RemoveAt(j);
                    if (CheckReport2(newReport, true))
                    {
                        return true;
                    }
                }

                return false;
            }

            i++;
        }
        return true;
    }
    
    private static List<List<int>> ExtractReports(string[] lines)
    {
        var reports = new List<List<int>>();
        foreach (var line in lines)
        {
            var parts = Regex.Split(line, @"\s+");
            var report = new List<int>();
            foreach (var part in parts)
            {
                if (int.TryParse(part, out var number))
                {
                    report.Add(number);
                }
            }
            reports.Add(report);
        }

        return reports;
    } 
}