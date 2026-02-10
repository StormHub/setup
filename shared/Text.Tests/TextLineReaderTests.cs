namespace Text.Tests;

using System.Text.RegularExpressions;
using Text;

public sealed class TextLineReaderTests
{
    private static readonly Regex HeaderPattern = new(@"\[\[ ## (\w+) ## \]\]", RegexOptions.Compiled);

    [Fact]
    public async Task Split_ReturnsSingleSectionWithTrimmedLines()
    {
        const string input =
            """
            [[ ## FIRST ## ]]
            line one

              line two   
            """;

        var result = await ReadAll(input);

        Assert.Collection(result,
            pair =>
            {
                Assert.Equal("FIRST", pair.Key);
                Assert.Equal("line one" + Environment.NewLine + "line two", pair.Value);
            });
    }

    [Fact]
    public async Task Split_HandlesMultipleSections()
    {
        const string input =
            """
            [[ ## ALPHA ## ]]
            a1
            a2
            [[ ## BETA ## ]]
            b1
            """;

        var result = await ReadAll(input);

        Assert.Collection(result,
            first =>
            {
                Assert.Equal("ALPHA", first.Key);
                Assert.Equal("a1" + Environment.NewLine + "a2", first.Value);
            },
            second =>
            {
                Assert.Equal("BETA", second.Key);
                Assert.Equal("b1", second.Value);
            });
    }

    [Fact]
    public async Task Split_WhenNoHeader_UsesEmptyKey()
    {
        const string input =
            """
            lone line
            another line
            """;

        var result = await ReadAll(input);

        Assert.Collection(result,
            pair =>
            {
                Assert.Equal(string.Empty, pair.Key);
                Assert.Equal("lone line" + Environment.NewLine + "another line", pair.Value);
            });
    }

    private static async Task<List<KeyValuePair<string, string>>> ReadAll(
        string input,
        Func<string, string?>? matcher = default,
        CancellationToken token = default)
    {
        matcher ??= MatchHeader;
        var result = new List<KeyValuePair<string, string>>();
        await foreach (var pair in TextLineReader.Split(input, matcher, token))
        {
            result.Add(pair);
        }

        return result;
    }

    private static string? MatchHeader(string line)
    {
        var match = HeaderPattern.Match(line);
        return match.Success ? match.Groups[1].Value : null;
    }
}