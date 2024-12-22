using AdventOfCode2025.Days;

namespace AdventOfCode2025
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var filePath = "D:\\AdventOfCode\\AdventOfCode2025\\AdventOfCode2025\\Resources\\input.txt";
            var fileContent = File.ReadAllText(filePath);
            var lines = fileContent.Split("\n");

            Day6.ExecutePart1(lines);
        }
    }
}