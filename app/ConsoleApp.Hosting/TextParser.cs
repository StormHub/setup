using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Text;

namespace ConsoleApp.Hosting;

internal sealed class TextParser(ILogger<TextParser> logger)
{
    private static readonly Regex HeaderPattern = new(@"\[\[ ## (\w+) ## \]\]", RegexOptions.Compiled);

    private readonly ILogger _logger = logger;
    
    public async Task Parse(string text, CancellationToken token = default)
    {
        _logger.LogInformation("Text parser {TextLength}", text.Length);
        await foreach (var (header, content) in TextLineReader.Split(text, MatchHeader, token))
        {
            _logger.LogInformation("Section {Header} \n\t {Content}", header, content);
        }
    }
    
    private static string? MatchHeader(string line)
    {
        var match = HeaderPattern.Match(line);
        return match.Success ? match.Groups[1].Value : default;
    }
}