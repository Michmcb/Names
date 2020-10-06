namespace MichMcb.Names
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Text;

	/// <summary>
	/// Represents a single Name. Note that for when comparing, only the parts affect equality and comparison.
	/// </summary>
	public class PartName : IComparable, IComparable<PartName>, IEquatable<PartName>, IName
	{
		public const int None = -1;
		public int TopPart { get; set; }
		public int MidPart { get; set; }
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
		public PartName() : this(null, None, None, None, Attributes.Empty, string.Empty) { }
		public PartName(string? title, int topPart, int midPart, int bottomPart, Attributes attributes, string suffix)
		{
			TopPart = topPart;
			MidPart = midPart;
			BottomPart = bottomPart;
			Attributes = attributes;
			Suffix = suffix;
			Title = title;
		}
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			return AppendTo(NameRules.Default, sb).ToString();
		}
		public string ToString(NameRules rules)
		{
			StringBuilder sb = new StringBuilder();
			return AppendTo(rules, sb).ToString();
		}
		public string ToString(NameRules rules, in ReadOnlySpan<char> prefix)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(prefix);
			return AppendTo(rules, sb).ToString();
		}
		public StringBuilder AppendTo(StringBuilder sb)
		{
			return AppendTo(NameRules.Default, sb);
		}
		public StringBuilder AppendTo(NameRules rules, StringBuilder sb)
		{
			if (TopPart != None)
			{
				sb.Append(Formatting.PartDelim + TopPart.ToString(rules.TopPartFormat));
			}
			if (MidPart != None)
			{
				sb.Append(Formatting.PartDelim + MidPart.ToString(rules.MidPartFormat));
			}
			if (BottomPart != None)
			{
				sb.Append(Formatting.PartDelim + BottomPart.ToString(rules.BottomPartFormat));
			}

			sb.Append(!string.IsNullOrEmpty(Title) ? (TopPart != None || MidPart != None || BottomPart != None) ? Formatting.TitleDelim + Title : Title : null);
			Attributes.AppendTo(sb);
			sb.Append(Suffix);
			return sb;
		}
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
		public static bool operator !=(PartName? lhs, PartName? rhs)
		{
			return !(lhs == rhs);
		}
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
		/// Parses a string as a PartName.
		/// </summary>
		/// <param name="s">The string to parse</param>
		/// <param name="name">The parsed name</param>
		public static bool TryParse(in ReadOnlySpan<char> s, [NotNullWhen(true)] out PartName? name)
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
			if (c == Formatting.PartDelim)
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
						name = new PartName(null, first, None, None, Attributes.Empty, string.Empty);
						return true;
					}
				}
			}
			c = s[i];
			// 2nd
			if (c == Formatting.PartDelim)
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
						name = new PartName(null, first, second, None, Attributes.Empty, string.Empty);
						return true;
					}
				}
			}
			c = s[i];
			// 3rd
			if (c == Formatting.PartDelim)
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
						name = new PartName(null, first, second, third, Attributes.Empty, string.Empty);
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

			name = new PartName(null, first, second, third, Attributes.Empty, string.Empty);
			if (!Parsing.ParseTitleAttributeSuffixFragment(s.Slice(i), name))
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
