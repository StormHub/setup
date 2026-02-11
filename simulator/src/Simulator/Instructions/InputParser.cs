using System.Diagnostics.CodeAnalysis;
using Simulator.Robots;

namespace Simulator.Instructions;

internal sealed class InputParser(TextWriter output)
{
    public bool TryParse(string input, [NotNullWhen(true)] out IInstruction? instruction)
    {
        instruction = null;
        
        if (string.IsNullOrWhiteSpace(input))
            return false;

        var parts = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
            return false;

        var name = parts[0].ToUpperInvariant();

        instruction = name switch
        {
            "PLACE" when parts.Length == 2 => ParsePlaceCommand(parts[1]),
            "MOVE" => new MoveCommand(),
            "LEFT" => new LeftCommand(),
            "RIGHT" => new RightCommand(),
            "REPORT" => new ReportQuery(output),
            _ => null
        };

        return instruction is not null;
    }

    private static IInstruction? ParsePlaceCommand(string arguments)
    {
        var args = arguments.Split(',', StringSplitOptions.TrimEntries);

        if (args.Length is < 2 or > 3)
            return null;

        if (!int.TryParse(args[0], out var x) || !int.TryParse(args[1], out var y))
            return null;

        if (args.Length == 3)
        {
            return !Direction.TryParse(args[2], out var direction) 
                ? null 
                : new PlaceCommand(x, y, direction);
        }

        return new PlaceCommand(x, y, null);
    }
}
