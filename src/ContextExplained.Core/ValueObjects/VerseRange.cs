using System.Reflection.Metadata.Ecma335;

namespace ContextExplained.Core.ValueObjects;

public sealed class VerseRange
{
    public int Start { get; private set; }
    public int End { get; private set; }

    public VerseRange(int start, int end)
    {
        if (start <= 0) throw new ArgumentException("Starting verse must be greater than 0", nameof(start));
        if (end < start) throw new ArgumentException("Ending verse must be greater than or equal to starting verse", nameof(end));

        Start = start;
        End = end;
    }

    public override string ToString() => Start == End ? Start.ToString() : $"{Start}-{End}";
    public override bool Equals(object? obj)
    {
        if (obj is VerseRange other)
        {
            return Start == other.Start && End == other.End;
        }
        return false;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End);
    }
    public static VerseRange FromString(string verseRange)
    {
        string[] verses = verseRange.Split('-');
        if (verses.Length == 2)
        {
            if (int.TryParse(verses[0], out int start) && int.TryParse(verses[1], out int end))
            {
                return new VerseRange(start, end);
            }
            throw new ArgumentException(nameof(verseRange), "Start and end verses must both be integers.");
        }
        else if (verses.Length == 1)
        {
            if (int.TryParse(verses[0], out int start))
            {
                return new VerseRange(start, start);
            }
            throw new ArgumentException(nameof(VerseRange), "Start verse must be integer.");
        }
        throw new ArgumentException(nameof(verseRange), "Invalid verse range.");
    }
}
