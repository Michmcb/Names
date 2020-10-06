namespace MichMcb.Names
{
	using System;
	/// <summary>
	/// Defines how things are formatted to strings
	/// </summary>
	public sealed class NameRules
	{
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
		public string DateFormat { get; private set; }
		private NameRules() : this(2, 2, 2, Formatting.YearMonthDay) { }
		/// <summary>
		/// Creates a new NameRuleSet with the rules provided
		/// </summary>
		/// <param name="dateFormat">How to format the Date. Use one of the const strings in Formatting for this</param>
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		public NameRules(int topPartDigits = 2, int midDigits = 2, int bottomPartDigits = 2, string dateFormat = Formatting.YearMonthDay)
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		{
			TopPartDigits = topPartDigits;
			MidPartDigits = midDigits;
			BottomPartDigits = bottomPartDigits;
			DateFormat = dateFormat;
		}
	}
}
