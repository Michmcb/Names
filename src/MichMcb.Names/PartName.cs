namespace MichMcb.Names
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Text;

	/// <summary>
	/// Represents a single Name which can contain digits indicating some sort of order.
	/// </summary>
	public sealed class PartName : IComparable, IComparable<PartName>, IEquatable<PartName>, IName
	{
		/// <summary>
		/// If a part is set to this value, it will be omitted when turning this into a string
		/// </summary>
		public const int None = -1;
		/// <summary>
		/// First part
		/// </summary>
		public int TopPart { get; set; }
		/// <summary>
		/// Second part
		/// </summary>
		public int MidPart { get; set; }
		/// <summary>
		/// Third party
		/// </summary>
		public int BottomPart { get; set; }
		/// <summary>
		/// The Title
		/// </summary>
		public string? Title { get; set; }
		/// <summary>
		/// The Attributes
		/// </summary>
		public Attributes Attributes { get; set; }
		/// <summary>
		/// The Suffix that will get appended to this Name on invoking .ToString()
		/// </summary>
		public string Suffix { get; set; }
		/// <summary>
		/// Creates a new instance with a null title, all parts set to <see cref="None"/>, empty suffix, and empty Attributes (<see cref="Attributes.Empty"/>)
		/// </summary>
		public PartName() : this(null, None, None, None, string.Empty, Attributes.Empty) { }
		/// <summary>
		/// Creates a new instance with the provided parameters.
		/// </summary>
		public PartName(string? title, int topPart, int midPart, int bottomPart, string suffix, Attributes attributes)
		{
			TopPart = topPart;
			MidPart = midPart;
			BottomPart = bottomPart;
			Attributes = attributes;
			Suffix = suffix;
			Title = title;
		}
		/// <summary>
		/// Turns this Name into a string using rules <see cref="NameRules.Default"/>.
		/// </summary>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			return AppendTo(sb, NameRules.Default).ToString();
		}
		/// <summary>
		/// Turns this Name into a string using the provided <paramref name="rules"/>.
		/// </summary>
		/// <param name="rules">The rules to use</param>
		public string ToString(NameRules rules)
		{
			StringBuilder sb = new StringBuilder();
			return AppendTo(sb, rules).ToString();
		}
		/// <summary>
		/// Turns this Name into a string using the provided <paramref name="rules"/>.
		/// Prefixes the resultant string with <paramref name="prefix"/>.
		/// </summary>
		/// <param name="rules">The rules to use</param>
		/// <param name="prefix">The prefix to prepend</param>
		public string ToString(NameRules rules, in ReadOnlySpan<char> prefix)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(prefix);
			return AppendTo(sb, rules).ToString();
		}
		/// <summary>
		/// Writes this Name as a string, to <paramref name="stringBuilder"/>, using <see cref="NameRules.Default"/>.
		/// </summary>
		/// <param name="stringBuilder">The StringBuilder to which the resultant string is appended.</param>
		/// <returns><paramref name="stringBuilder"/></returns>
		public StringBuilder AppendTo(StringBuilder stringBuilder)
		{
			return AppendTo(stringBuilder, NameRules.Default);
		}
		/// <summary>
		/// Writes this Name as a string, to <paramref name="stringBuilder"/>, using the provided <paramref name="rules"/>.
		/// </summary>
		/// <param name="stringBuilder">The StringBuilder to which the resultant string is appended.</param>
		/// <param name="rules">The rules to use</param>
		/// <returns><paramref name="stringBuilder"/></returns>
		public StringBuilder AppendTo(StringBuilder stringBuilder, NameRules rules)
		{
			if (TopPart != None)
			{
				stringBuilder.Append(rules.PartDelim + TopPart.ToString(rules.TopPartFormat));
			}
			if (MidPart != None)
			{
				stringBuilder.Append(rules.PartDelim + MidPart.ToString(rules.MidPartFormat));
			}
			if (BottomPart != None)
			{
				stringBuilder.Append(rules.PartDelim + BottomPart.ToString(rules.BottomPartFormat));
			}

			stringBuilder.Append(!string.IsNullOrEmpty(Title) ? (TopPart != None || MidPart != None || BottomPart != None) ? rules.TitleDelim + Title : Title : null);
			Attributes.AppendTo(stringBuilder, rules);
			stringBuilder.Append(Suffix);
			return stringBuilder;
		}
		/// <summary>
		/// Compares based on <see cref="TopPart"/>, then <see cref="MidPart"/>, then <see cref="BottomPart"/>.
		/// </summary>
		public static bool operator <(PartName? lhs, PartName? rhs)
		{
			if (!(lhs is null) && !(rhs is null))
			{
				return lhs.TopPart < rhs.TopPart || lhs.MidPart < rhs.MidPart || lhs.BottomPart < rhs.BottomPart;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// Compares based on <see cref="TopPart"/>, then <see cref="MidPart"/>, then <see cref="BottomPart"/>.
		/// </summary>
		public static bool operator <=(PartName? lhs, PartName? rhs)
		{
			if (!(lhs is null) && !(rhs is null))
			{
				return lhs.TopPart <= rhs.TopPart || lhs.MidPart <= rhs.MidPart || lhs.BottomPart <= rhs.BottomPart;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// Compares based on <see cref="TopPart"/>, then <see cref="MidPart"/>, then <see cref="BottomPart"/>.
		/// </summary>
		public static bool operator >(PartName? lhs, PartName? rhs)
		{
			if (!(lhs is null) && !(rhs is null))
			{
				return lhs.TopPart > rhs.TopPart || lhs.MidPart > rhs.MidPart || lhs.BottomPart > rhs.BottomPart;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// Compares based on <see cref="TopPart"/>, then <see cref="MidPart"/>, then <see cref="BottomPart"/>.
		/// </summary>
		public static bool operator >=(PartName? lhs, PartName? rhs)
		{
			if (!(lhs is null) && !(rhs is null))
			{
				return lhs.TopPart >= rhs.TopPart || lhs.MidPart >= rhs.MidPart || lhs.BottomPart >= rhs.BottomPart;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// Equality based on <see cref="TopPart"/> and <see cref="MidPart"/> and <see cref="BottomPart"/>.
		/// </summary>
		public static bool operator ==(PartName? lhs, PartName? rhs)
		{
			// Check for null on left side.
			if (lhs is null)
			{
				if (rhs is null)
				{
					// null == null = true.
					return true;
				}

				// Only lhs is null.
				return false;
			}
			return lhs.Equals(rhs);
		}
		/// <summary>
		/// Equality based on <see cref="TopPart"/> and <see cref="MidPart"/> and <see cref="BottomPart"/>.
		/// </summary>
		public static bool operator !=(PartName? lhs, PartName? rhs)
		{
			return !(lhs == rhs);
		}
		/// <summary>
		/// Compares based on <see cref="TopPart"/>, then <see cref="MidPart"/>, then <see cref="BottomPart"/>.
		/// </summary>
		public int CompareTo(PartName? other)
		{
			if (other is null)
			{
				return 1;
			}
			else
			{
				if (this < other)
				{
					return -1;
				}
				else if (this > other)
				{
					return 1;
				}
				else
				{
					return 0;
				}
			}
		}
		/// <summary>
		/// Compares based on <see cref="TopPart"/>, then <see cref="MidPart"/>, then <see cref="BottomPart"/>.
		/// </summary>
		public int CompareTo(object obj)
		{
			if (obj is PartName name)
			{
				return CompareTo(name);
			}
			else
			{
				throw new ArgumentException("Not a " + nameof(PartName));
			}
		}
		/// <summary>
		/// Equality based on <see cref="TopPart"/> and <see cref="MidPart"/> and <see cref="BottomPart"/>.
		/// </summary>
		public override bool Equals(object? obj)
		{
			// If obj is null, false
			if (obj is null)
			{
				return false;
			}
			// Optimization for a common success case
			if (ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj is PartName rhs)
			{
				// If rhs is not null, we can compare. Only equal if all numerical properties are equal.
				// It's about twice as fast to compare each individual property, rather than use the Order variable
				return TopPart == rhs.TopPart && MidPart == rhs.MidPart && BottomPart == rhs.BottomPart;
			}
			return false;
		}
		/// <summary>
		/// Equality based on <see cref="TopPart"/> and <see cref="MidPart"/> and <see cref="BottomPart"/>.
		/// </summary>
		public bool Equals(PartName? rhs)
		{
			// If obj is null, false
			if (rhs is null)
			{
				return false;
			}
			// Optimization for a common success case
			if (ReferenceEquals(this, rhs))
			{
				return true;
			}
			// If rhs is not null, we can compare. Only equal if all numerical properties are equal.
			return TopPart == rhs.TopPart && MidPart == rhs.MidPart && BottomPart == rhs.BottomPart;
		}
		/// <summary>
		/// Hashcode based on <see cref="TopPart"/> and <see cref="MidPart"/> and <see cref="BottomPart"/>.
		/// </summary>
		public override int GetHashCode()
		{
			return HashCode.Combine(TopPart, MidPart, BottomPart);
		}
		/// <summary>
		/// Returns how many digits <paramref name="n"/> contains.
		/// Only positive numbers are valid.
		/// </summary>
		public static int HowManyDigits(int n)
		{
			if (n > 0)
			{
				if (n < 10)
				{
					return 1;
				}
				if (n < 100)
				{
					return 2;
				}
				if (n < 1000)
				{
					return 3;
				}
				if (n < 10000)
				{
					return 4;
				}
				if (n < 100000)
				{
					return 5;
				}
				if (n < 1000000)
				{
					return 6;
				}
				if (n < 10000000)
				{
					return 7;
				}
				if (n < 100000000)
				{
					return 8;
				}
				if (n < 1000000000)
				{
					return 9;
				}
				return 10;
			}
			return 0;
		}
		/// <summary>
		/// Parses a string as a PartName, using <see cref="NameRules.Default"/>.
		/// </summary>
		/// <param name="s">The string to parse</param>
		/// <param name="name">The parsed name</param>
		public static bool TryParse(in ReadOnlySpan<char> s, [NotNullWhen(true)] out PartName? name)
		{
			return TryParse(s, NameRules.Default, out name);
		}
		/// <summary>
		/// Parses a string as a PartName.
		/// </summary>
		/// <param name="s">The string to parse</param>
		/// <param name="name">The parsed name</param>
		public static bool TryParse(in ReadOnlySpan<char> s, NameRules rules, [NotNullWhen(true)] out PartName? name)
		{
			name = null;
			if (s.Length == 0)
			{
				return false;
			}

			// TODO move the date parsing into here as well, allow appearing before or after the parts. It needs to start with @.

			int from, to;
			int first = None, second = None, third = None;

			// Note that whenever we do our s[i], it's never past the end of the string
			// Because if we can't find a non-digit, then we just exit straight away
			int i = 0;
			char c = s[0];
			// 1st
			if (c == rules.PartDelim)
			{
				from = i + 1;
				// The is only a Toppart if the next character is a digit, otherwise it isn't
				if (from < s.Length && s[from] >= '0' && s[from] <= '9')
				{
					// A Toppart maybe; start search from the next char which is a digit
					to = Parsing.IndexOfNonDigit(s, from);
					if (to != -1)
					{
						if (to - from > 9)
						{
							return false;
						}
						first = int.Parse(s[from..to]);
						i = to;
					}
					else
					{
						first = int.Parse(s[from..]);
						name = new PartName(null, first, None, None, string.Empty, Attributes.Empty);
						return true;
					}
				}
			}
			c = s[i];
			// 2nd
			if (c == rules.PartDelim)
			{
				from = i + 1;
				// The is only an episode if the next character is a digit, otherwise it isn't
				if (from < s.Length && s[from] >= '0' && s[from] <= '9')
				{
					// An episode maybe; start search from the next char which is a digit
					to = Parsing.IndexOfNonDigit(s, from);
					if (to != -1)
					{
						if (to - from > 9)
						{
							return false;
						}
						second = int.Parse(s[from..to]);
						i = to;
					}
					else
					{
						second = int.Parse(s[from..]);
						name = new PartName(null, first, second, None, string.Empty, Attributes.Empty);
						return true;
					}
				}
			}
			c = s[i];
			// 3rd
			if (c == rules.PartDelim)
			{
				from = i + 1;
				// The is only a part if the next character is a digit, otherwise it isn't
				if (from < s.Length && s[from] >= '0' && s[from] <= '9')
				{
					// A part maybe; start search from the next char which is a digit
					to = Parsing.IndexOfNonDigit(s, from);
					if (to != -1)
					{
						if (to - from > 9)
						{
							return false;
						}
						third = int.Parse(s[from..to]);
						i = to;
					}
					else
					{
						third = int.Parse(s[from..]);
						name = new PartName(null, first, second, third, string.Empty, Attributes.Empty);
						return true;
					}
				}
			}
			// If we parsed the numbers properly, then we'll have ended on a space, so jump forwards one space
			// If there were no numbers we'll be on the first char of the title/attributes, so no +1
			if (s[i] == ' ')
			{
				++i;
			}

			name = new PartName(null, first, second, third, string.Empty, Attributes.Empty);
			if (!Parsing.ParseTitleAttributeSuffixFragment(s.Slice(i), rules, name))
			{
				return false;
			}
			return true;
		}
		/// <summary>
		/// Parses a string as a DateName, according to the rules set up by this DateNameRuleSet.
		/// If the string does not adhere to the rules of the DateNameRuleSet, this method returns false.
		/// </summary>
		/// <param name="str">The string to parse</param>
		/// <param name="name">The parsed DateName</param>
		//public static bool Parse(in ReadOnlySpan<char> str, [NotNullWhen(true)] out DateName? name)
		//{
		//	name = null;
		//	// We can have anywhere from yyyy to yyyy-MM-dd_HH-mm-ss. But at least the year is required.
		//	if (str.Length < 4)
		//	{
		//		return false;
		//	}
		//	char c = str[0];
		//	if (c < '0' || c > '9')
		//	{
		//		// At least the year is required; so if the first character isn't a digit, it's not valid
		//		return false;
		//	}
		//
		//	// Since it starts with a digit, we can assume it's most likely a DateName
		//	if (!Parsing.ParseDateTimeFragment(str, out int i, out DateTime dateTime))
		//	{
		//		return false;
		//	}
		//
		//	// If there's nothing more, that is fine; just return now
		//	if (i >= str.Length)
		//	{
		//		name = new DateName(dateTime, null, Attributes.Empty, string.Empty);
		//		return true;
		//	}
		//
		//	// If there's something more, we need a space and then the rest of the stuff
		//	name = new DateName(dateTime, null, Attributes.Empty, string.Empty);
		//	if (!Parsing.ParseTitleAttributeSuffixFragment(str.Slice(i + 1), name))
		//	{
		//		return false;
		//	}
		//	return true;
		//}
	}
}
