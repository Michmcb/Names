namespace MichMcb.Names
{
	public static class Formatting
	{
		public const string YearMonthDayHourMinuteSecond = "yyyy-MM-ddTHH-mm-ss";
		public const string YearMonthDayHourMinute = "yyyy-MM-ddTHH-mm";
		public const string YearMonthDayHour = "yyyy-MM-ddTHH";
		public const string YearMonthDay = "yyyy-MM-dd";
		public const string YearMonth = "yyyy-MM";
		public const string Year = "yyyy";

		/// <summary>
		/// The character that separates parts
		/// </summary>
		public const char PartDelim = '~';
		/// <summary>
		/// The character that precedes a date
		/// </summary>
		public const char DateStart = '@';
		/// <summary>
		/// The character that separates Year, Month, Day from eachother, and Hour, Minute, Second from eachother (yyyy-MM-ddTHH-mm-ss)
		/// </summary>
		public const char TimeUnitDelim = '-';
		/// <summary>
		/// Delimits the Year, Month, Day from the Hour, Minute, Second (yyyy-MM-ddTHH-mm-ss)
		/// </summary>
		public const char DateAndTimeDelim = 'T';

		// General
		/// <summary>
		/// The character that signifies the beginning of an Attribute string, after Title but before Suffix
		/// </summary>
		public const char AttributeStart = '{';
		/// <summary>
		/// Separates attributes from one another
		/// </summary>
		public const char AttributeDelim = ';';
		/// <summary>
		/// The character that signifies the end of an Attribute string, after Title but before Suffix
		/// </summary>
		public const char AttributeEnd = '}';
		/// <summary>
		/// The character that precedes the Title, after any Date/Part
		/// </summary>
		public const char TitleDelim = ' ';
		/// <summary>
		/// The character that precedes the suffix, after Attributes
		/// </summary>
		public const char SuffixDelimiter = '.';
	}
}
