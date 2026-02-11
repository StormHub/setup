using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Simulator.Robots;

namespace Simulator.Instructions;

internal sealed class InputParser(TextWriter output, ILoggerFactory? loggerFactory)
{
    private readonly ILoggerFactory _loggerFactory = loggerFactory ?? NullLoggerFactory.Instance;

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
            "MOVE" => new MoveCommand(_loggerFactory),
            "LEFT" => new LeftCommand(_loggerFactory),
            "RIGHT" => new RightCommand(_loggerFactory),
            "REPORT" => new ReportQuery(output, _loggerFactory),
            _ => null
        };

        return instruction is not null;
    }

    private IInstruction? ParsePlaceCommand(string arguments)
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
                : new PlaceCommand(x, y, direction, _loggerFactory);
        }

        return new PlaceCommand(x, y, null, _loggerFactory);
    }
}
