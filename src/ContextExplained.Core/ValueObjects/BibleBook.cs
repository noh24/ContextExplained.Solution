namespace ContextExplained.Core.ValueObjects;

public sealed class BibleBook
{
    public string Name { get; }

    private BibleBook(string name) => Name = name;

    public override string ToString() => Name;

    public override bool Equals(object? obj)
    {
        if (obj is BibleBook other)
        {
            return Name == other.Name;
        }
        return false;
    }

    public override int GetHashCode() => Name.GetHashCode();

    public static readonly BibleBook Genesis = new("Genesis");
    public static readonly BibleBook Job = new("Job");
    public static readonly BibleBook Exodus = new("Exodus");
    public static readonly BibleBook Leviticus = new("Leviticus");
    public static readonly BibleBook Numbers = new("Numbers");
    public static readonly BibleBook Deuteronomy = new("Deuteronomy");
    public static readonly BibleBook Joshua = new("Joshua");
    public static readonly BibleBook Judges = new("Judges");
    public static readonly BibleBook Ruth = new("Ruth");
    public static readonly BibleBook Samuel1 = new("1 Samuel");
    public static readonly BibleBook Samuel2 = new("2 Samuel");
    public static readonly BibleBook Chronicles1 = new("1 Chronicles");
    public static readonly BibleBook Chronicles2 = new("2 Chronicles");
    public static readonly BibleBook Psalms = new("Psalms");
    public static readonly BibleBook Proverbs = new("Proverbs");
    public static readonly BibleBook Ecclesiastes = new("Ecclesiastes");
    public static readonly BibleBook SongOfSolomon = new("Song of Solomon");
    public static readonly BibleBook Kings1 = new("1 Kings");
    public static readonly BibleBook Kings2 = new("2 Kings");
    public static readonly BibleBook Obadiah = new("Obadiah");
    public static readonly BibleBook Jonah = new("Jonah");
    public static readonly BibleBook Amos = new("Amos");
    public static readonly BibleBook Micah = new("Micah");
    public static readonly BibleBook Isaiah = new("Isaiah");
    public static readonly BibleBook Hosea = new("Hosea");
    public static readonly BibleBook Nahum = new("Nahum");
    public static readonly BibleBook Zephaniah = new("Zephaniah");
    public static readonly BibleBook Jeremiah = new("Jeremiah");
    public static readonly BibleBook Habakkuk = new("Habakkuk");
    public static readonly BibleBook Lamentations = new("Lamentations");
    public static readonly BibleBook Ezekiel = new("Ezekiel");
    public static readonly BibleBook Joel = new("Joel");
    public static readonly BibleBook Daniel = new("Daniel");
    public static readonly BibleBook Ezra = new("Ezra");
    public static readonly BibleBook Haggai = new("Haggai");
    public static readonly BibleBook Zechariah = new("Zechariah");
    public static readonly BibleBook Esther = new("Esther");
    public static readonly BibleBook Nehemiah = new("Nehemiah");
    public static readonly BibleBook Malachi = new("Malachi");
    public static readonly BibleBook Luke = new("Luke");
    public static readonly BibleBook John = new("John");
    public static readonly BibleBook Matthew = new("Matthew");
    public static readonly BibleBook Mark = new("Mark");
    public static readonly BibleBook Acts = new("Acts");
    public static readonly BibleBook James = new("James");
    public static readonly BibleBook Galatians = new("Galatians");
    public static readonly BibleBook Thessalonians1 = new("1 Thessalonians");
    public static readonly BibleBook Thessalonians2 = new("2 Thessalonians");
    public static readonly BibleBook Corinthians1 = new("1 Corinthians");
    public static readonly BibleBook Corinthians2 = new("2 Corinthians");
    public static readonly BibleBook Romans = new("Romans");
    public static readonly BibleBook Colossians = new("Colossians");
    public static readonly BibleBook Philemon = new("Philemon");
    public static readonly BibleBook Ephesians = new("Ephesians");
    public static readonly BibleBook Philippians = new("Philippians");
    public static readonly BibleBook Timothy1 = new("1 Timothy");
    public static readonly BibleBook Titus = new("Titus");
    public static readonly BibleBook Peter1 = new("1 Peter");
    public static readonly BibleBook Hebrews = new("Hebrews");
    public static readonly BibleBook Timothy2 = new("2 Timothy");
    public static readonly BibleBook Peter2 = new("2 Peter");
    public static readonly BibleBook Jude = new("Jude");
    public static readonly BibleBook John1 = new("1 John");
    public static readonly BibleBook John2 = new("2 John");
    public static readonly BibleBook John3 = new("3 John");
    public static readonly BibleBook Revelation = new("Revelation");
 }
