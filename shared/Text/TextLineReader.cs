using System.Runtime.CompilerServices;

namespace Text;

public sealed class TextLineReader(TextReader textReader)
{
    public static async IAsyncEnumerable<KeyValuePair<string, string>> Split(string text, Func<string, string?> match, [EnumeratorCancellation] CancellationToken token = default)
    {
        using var reader = new StringReader(text);
        var lineReader = new TextLineReader(reader);
        await foreach(var pair in lineReader.Split(match, token))
        {
            yield return pair;
        }
    }
    
    public async IAsyncEnumerable<KeyValuePair<string, string>> Split(Func<string, string?> match, [EnumeratorCancellation] CancellationToken token = default)
    {
        string? header = null;
        List<string> lines = [];

        while (!token.IsCancellationRequested)
        {
            var line = await textReader.ReadLineAsync(token);
            if (line is null)
            {
                break;
            }
            
            line = line.Trim();
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            var value = match(line);
            if (value is not null)
            {
                if (lines.Count > 0)
                {
                    yield return new (header ?? string.Empty, string.Join(Environment.NewLine, lines));

                    lines = [];
                }

                header = value;
            }
            else
            {
                lines.Add(line);
            }
        }

        if (lines.Count > 0)
        {
            yield return new (header ?? string.Empty, string.Join(Environment.NewLine, lines));
        }
    }
}