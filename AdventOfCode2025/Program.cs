using AdventOfCode2025.Days;

namespace AdventOfCode2025
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var filePath = "D:\\AdventOfCode\\AdventOfCode2025\\AdventOfCode2025\\Resources\\input.txt";
            var fileContent = File.ReadAllText(filePath);
            var lines = fileContent.Split("\n");

            Day15.ExecutePart1(lines);
        }
    }
}