namespace AdventOfCode2025.Days;

public class Day19
{
    public static void ExecutePart1(string[] lines)
    {
        (List<string> components, List<string> designs) result = ExtractComponentsAndDesigns(lines);
        VerifyDesigns(result.components, result.designs);
    }
    
    private static void VerifyDesigns(List<string> components, List<string> designs)
    {
        int validDesigns = 0;

        for (int i = 0; i < designs.Count; i++)
        {
            Console.WriteLine($"Verifying Design {i + 1}: {designs[i]}");
            if (VerifyDesign(components, designs[i], new Dictionary<string, bool>()))
            {
                validDesigns++;
            }
        }

        Console.WriteLine($"Number of valid designs: {validDesigns}");
    }

    private static bool VerifyDesign(List<string> components, string design, Dictionary<string, bool> cache)
    {
        if (cache.TryGetValue(design, out bool result))
        {
            return result;
        }

        if (components.Contains(design))
        {
            cache[design] = true;
            return true;
        }

        for (int i = 1; i < design.Length; i++)
        {
            var prefix = design.Substring(0, i);
            var suffix = design.Substring(i);

            if (components.Contains(prefix) && VerifyDesign(components, suffix, cache))
            {
                cache[design] = true;
                return true;
            }
        }

        cache[design] = false;
        return false;
    }

    private static (List<string> components, List<string> designs) ExtractComponentsAndDesigns(string[] lines)
    {
        int i = 2;
        List<string> components = lines[0].Split(", ").ToList();
        components[^1] = components[^1].Trim();
        List<string> designs = new List<string>();
        while (i < lines.Length)
        {
            lines[i] = lines[i].Trim();
            designs.Add(lines[i]);
            i++;
        }

        return (components, designs);
    }
}