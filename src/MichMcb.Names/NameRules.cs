namespace MichMcb.Names
{
	using System;

	/// <summary>
	/// Defines how things are formatted to strings
	/// </summary>
	public sealed class NameRules
	{
		/// <summary>
		/// The default rules to use if none are specified. These can be changed.
		/// </summary>
		public static readonly NameRules Default = new NameRules();
		public int TopPartDigits
		{
			set
			{
				if (value < 1 || value > 9)
				{
					throw new ArgumentException(nameof(TopPartDigits) + " must be between 1 and 9 (otherwise it would possibly exceed int storage limits)", nameof(value));
				}
				TopPartFormat = new string('0', value);
			}
			get => TopPartFormat.Length;
		}
		public int MidPartDigits
		{
			set
			{
				if (value < 1 || value > 9)
				{
					throw new ArgumentException(nameof(MidPartDigits) + " must be between 1 and 9 (otherwise it would possibly exceed int storage limits)", nameof(value));
				}
				MidPartFormat = new string('0', value);
			}
			get => MidPartFormat.Length;
		}
		public int BottomPartDigits
		{
			set
			{
				if (value < 1 || value > 9)
				{
					throw new ArgumentException(nameof(BottomPartDigits) + " must be between 1 and 9 (otherwise it would possibly exceed int storage limits)", nameof(value));
				}
				BottomPartFormat = new string('0', value);
			}
			get => BottomPartFormat.Length;
		}
		public string TopPartFormat { get; private set; }
		public string MidPartFormat { get; private set; }
		public string BottomPartFormat { get; private set; }
		/// <summary>
		/// The format string used to turn DateTimes into strings.
		/// </summary>
		public string DateFormat { get; set; }
		/// <summary>
		/// The character that separates top/middle/bottom parts.
		/// Default: ~.
		/// </summary>
		public char PartDelim { get; set; }
		/// <summary>
		/// The character that separates Year, Month, Day from eachother, and Hour, Minute, Second from eachother. e.g. the - in yyyy-MM-ddTHH-mm-ss.
		/// Default: -.
		/// </summary>
		public char TimeUnitDelim { get; set; }
		/// <summary>
		/// Delimits the Year, Month, Day from the Hour, Minute, Second. e.g. the T in yyyy-MM-ddTHH-mm-ss.
		/// Default: T
		/// </summary>
		public char DateAndTimeDelim { get; set; }
		/// <summary>
		/// The character that signifies the beginning of an Attribute string, after Title but before Suffix.
		/// Default: {
		/// </summary>
		public char AttributeStart { get; set; }
		/// <summary>
		/// The character that signifies the end of an Attribute string, after Title but before Suffix.
		/// Default: }
		/// </summary>
		public char AttributeEnd { get; set; }
		/// <summary>
		/// Separates attributes from one another.
		/// Default: ;
		/// </summary>
		public char AttributeDelim { get; set; }
		/// <summary>
		/// The character that precedes the Title, after any Date/Part.
		/// Default: Space
		/// </summary>
		public char TitleDelim { get; set; }
		/// <summary>
		/// The character that precedes the suffix, after Attributes.
		/// Default: .
		/// </summary>
		public char SuffixDelimiter { get; set; }
		/// <summary>
		/// The function to use to parse DateTimes.
		/// Default: <see cref="Parsing.ParseDateTimeExtended(in ReadOnlySpan{char}, NameRules, out DateTime)"/>
		/// </summary>
		public ParseDateTime ParseDateTime { get; set; }
		/// <summary>
		/// Creates a new NameRuleSet with the rules provided.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		public NameRules(int topPartDigits = 2,
			int midDigits = 2,
			int bottomPartDigits = 2,
			string dateFormat = Formatting.YearMonthDay,
			char partDelim = Formatting.PartDelim,
			char timeUnitDelim = Formatting.TimeUnitDelim,
			char dateAndTimeDelim = Formatting.DateAndTimeDelim,
			char attributeStart = Formatting.AttributeStart,
			char attributeEnd = Formatting.AttributeEnd,
			char attributeDelim = Formatting.AttributeDelim,
			char titleDelim = Formatting.TitleDelim,
			char suffixDelimiter = Formatting.SuffixDelimiter,
			ParseDateTime? parseDateTime = null)
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		{
			TopPartDigits = topPartDigits;
			MidPartDigits = midDigits;
			BottomPartDigits = bottomPartDigits;
			DateFormat = dateFormat;
			PartDelim = partDelim;
			TimeUnitDelim = timeUnitDelim;
			DateAndTimeDelim = dateAndTimeDelim;
			AttributeStart = attributeStart;
			AttributeEnd = attributeEnd;
			AttributeDelim = attributeDelim;
			TitleDelim = titleDelim;
			SuffixDelimiter = suffixDelimiter;
			ParseDateTime = parseDateTime ?? Parsing.ParseDateTimeExtended;
		}
	}
}
