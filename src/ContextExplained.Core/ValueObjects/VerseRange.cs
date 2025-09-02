namespace ContextExplained.Core.ValueObjects;

public sealed class VerseRange
{
    public int Start { get; private set; }
    public int End { get; private set; }

    public VerseRange(int start, int end)
    {
        if (start <= 0) throw new ArgumentNullException("Starting verse must be greater than 0", nameof(start));
        if (end < start) throw new ArgumentNullException("Ending verse must be greater than or equal to starting verse", nameof(end));

        Start = start;
        End = end;
    }

    public override string ToString() => Start == End ? Start.ToString() : $"{Start}-{End}";
}
