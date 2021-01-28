namespace MichMcb.Names
{
	using System;
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using System.Text;
	
	/// <summary>
	/// Represents a single Name which can contain leading digits indicating some sort of order.
	/// The digits are like so: ~n~n~n, for top/mid/bottom parts.
	/// </summary>
	public class PartName : IComparable, IComparable<PartName>, IEquatable<PartName>, IName
	{
		/// <summary>
		/// If a part is set to this value, it will be omitted when turning this into a string
		/// </summary>
		public const int None = int.MinValue;
		/// <summary>
		/// Creates a new instance with a null title, all parts set to <see cref="None"/>, empty suffix.
		/// </summary>
		public PartName() : this(string.Empty, None, None, None, string.Empty) { }
		/// <summary>
		/// Creates a new instance with the provided parameters.
		/// </summary>
		public PartName(string title, int topPart, int midPart, int bottomPart, string suffix)
		{
			TopPart = topPart;
			MidPart = midPart;
			BottomPart = bottomPart;
			Suffix = suffix;
			Title = title;
		}
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
		public string Title { get; set; }
		/// <summary>
		/// The Suffix that will get appended to this Name on invoking .ToString()
		/// </summary>
		public string Suffix { get; set; }
		/// <summary>
		/// Turns this Name into a string using rules <see cref="NameRules.Default"/>.
		/// </summary>
		public override string ToString()
		{
			StringBuilder sb = new();
			return AppendTo(sb, NameRules.Default).ToString();
		}
		/// <summary>
		/// Turns this Name into a string using the provided <paramref name="rules"/>.
		/// </summary>
		/// <param name="rules">The rules to use</param>
		public virtual string ToString(NameRules rules)
		{
			StringBuilder sb = new();
			return AppendTo(sb, rules).ToString();
		}
		/// <summary>
		/// Turns this Name into a string using the provided <paramref name="rules"/>.
		/// Prefixes the resultant string with <paramref name="prefix"/>.
		/// </summary>
		/// <param name="rules">The rules to use</param>
		/// <param name="prefix">The prefix to prepend</param>
		public virtual string ToString(NameRules rules, in ReadOnlySpan<char> prefix)
		{
			StringBuilder sb = new();
			sb.Append(prefix);
			return AppendTo(sb, rules).ToString();
		}
		/// <summary>
		/// Writes this Name as a string, to <paramref name="stringBuilder"/>, using <see cref="NameRules.Default"/>.
		/// </summary>
		/// <param name="stringBuilder">The StringBuilder to which the resultant string is appended.</param>
		/// <returns><paramref name="stringBuilder"/></returns>
		public virtual StringBuilder AppendTo(StringBuilder stringBuilder)
		{
			return AppendTo(stringBuilder, NameRules.Default);
		}
		/// <summary>
		/// Writes this Name as a string, to <paramref name="stringBuilder"/>, using the provided <paramref name="rules"/>.
		/// </summary>
		/// <param name="stringBuilder">The StringBuilder to which the resultant string is appended.</param>
		/// <param name="rules">The rules to use</param>
		/// <returns><paramref name="stringBuilder"/></returns>
		public virtual StringBuilder AppendTo(StringBuilder stringBuilder, NameRules rules)
		{
			if (TopPart != None)
			{
				stringBuilder.Append('~' + TopPart.ToString(rules.TopPartFormat));
			}
			if (MidPart != None)
			{
				stringBuilder.Append('~' + MidPart.ToString(rules.MidPartFormat));
			}
			if (BottomPart != None)
			{
				stringBuilder.Append('~' + BottomPart.ToString(rules.BottomPartFormat));
			}
			stringBuilder.Append(!string.IsNullOrEmpty(Title) ? (TopPart != None || MidPart != None || BottomPart != None) ? rules.TitleDelim + Title : Title : null);
			stringBuilder.Append(Suffix);
			return stringBuilder;
		}
		/// <summary>
		/// Compares based on <see cref="TopPart"/>, then <see cref="MidPart"/>, then <see cref="BottomPart"/>.
		/// </summary>
		public static bool operator <(PartName? lhs, PartName? rhs) => !(lhs is null) && !(rhs is null) && (lhs.TopPart < rhs.TopPart || lhs.MidPart < rhs.MidPart || lhs.BottomPart < rhs.BottomPart);
		/// <summary>
		/// Compares based on <see cref="TopPart"/>, then <see cref="MidPart"/>, then <see cref="BottomPart"/>.
		/// Also returns true if both are null.
		/// </summary>
		public static bool operator <=(PartName? lhs, PartName? rhs) => lhs is null && rhs is null || (!(lhs is null) && !(rhs is null) && (lhs.TopPart <= rhs.TopPart || lhs.MidPart <= rhs.MidPart || lhs.BottomPart <= rhs.BottomPart));
		/// <summary>
		/// Compares based on <see cref="TopPart"/>, then <see cref="MidPart"/>, then <see cref="BottomPart"/>.
		/// </summary>
		public static bool operator >(PartName? lhs, PartName? rhs) => !(lhs is null) && !(rhs is null) && (lhs.TopPart > rhs.TopPart || lhs.MidPart > rhs.MidPart || lhs.BottomPart > rhs.BottomPart);
		/// <summary>
		/// Compares based on <see cref="TopPart"/>, then <see cref="MidPart"/>, then <see cref="BottomPart"/>.
		/// Also returns true if both are null.
		/// </summary>
		public static bool operator >=(PartName? lhs, PartName? rhs) => lhs is null && rhs is null || !(lhs is null) && !(rhs is null) && (lhs.TopPart > rhs.TopPart || lhs.MidPart > rhs.MidPart || lhs.BottomPart > rhs.BottomPart);
		/// <summary>
		/// Equality based on <see cref="TopPart"/> and <see cref="MidPart"/> and <see cref="BottomPart"/>.
		/// </summary>
		public static bool operator ==(PartName? lhs, PartName? rhs) => lhs is null ? rhs is null : lhs.Equals(rhs); // Returns true if both are null, otherwise calls Equals
		/// <summary>
		/// Equality based on <see cref="TopPart"/> and <see cref="MidPart"/> and <see cref="BottomPart"/>.
		/// </summary>
		public static bool operator !=(PartName? lhs, PartName? rhs) => !(lhs == rhs);
		/// <summary>
		/// Compares based on <see cref="TopPart"/>, then <see cref="MidPart"/>, then <see cref="BottomPart"/>.
		/// </summary>
		public int CompareTo(PartName? other)
		{
			return other is null
				? 1
				: this < other
					? -1
					: this > other
						? 1
						: 0;
		}
		/// <summary>
		/// Compares based on <see cref="TopPart"/>, then <see cref="MidPart"/>, then <see cref="BottomPart"/>.
		/// </summary>
		public virtual int CompareTo(object? obj)
		{
			return obj is PartName name ? CompareTo(name) : throw new ArgumentException("Not a " + nameof(PartName));
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
			return n > 0
				? n < 10 ? 1
				: n < 100 ? 2
				: n < 1000 ? 3
				: n < 10000 ? 4
				: n < 100000 ? 5
				: n < 1000000 ? 6
				: n < 10000000 ? 7
				: n < 100000000 ? 8
				: n < 1000000000 ? 9
				: 10
				: 0; // <0
		}
		/// <summary>
		/// Parses a string as a <see cref="PartName"/>, using <see cref="NameRules.Default"/>.
		/// </summary>
		/// <param name="str">The string to parse</param>
		/// <param name="name">The parsed name</param>
		public static bool TryParse(in ReadOnlySpan<char> str, [NotNullWhen(true)] out PartName? name)
		{
			return TryParse(str, NameRules.Default, out name);
		}
		/// <summary>
		/// Parses a string as a <see cref="PartName"/>.
		/// </summary>
		/// <param name="str">The string to parse</param>
		/// <param name="rules">The rules to use when parsing</param>
		/// <param name="name">The parsed name</param>
		public static bool TryParse(in ReadOnlySpan<char> str, NameRules rules, [NotNullWhen(true)] out PartName? name)
		{
			name = null;
			if (str.Length == 0)
			{
				return false;
			}

			if (!TryParsePartFragment(str, rules.TitleDelim, out Range remainder, out int top, out int mid, out int bot))
			{
				return false;
			}
			ReadOnlySpan<char> remainingFrag = str[remainder];

			if (Parsing.FindParts(remainingFrag, rules, out Range tr, out Range ar, out Range sr))
			{
				name = new PartName(new string(remainingFrag[tr]), top, mid, bot, new string(remainingFrag[sr]));
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// Parses a string as a PartName, using <see cref="NameRules.Default"/>.
		/// </summary>
		/// <param name="str">The string to parse</param>
		/// <param name="pn">The callback to parse the attributes</param>
		/// <param name="name">The parsed name</param>
		public static bool TryParse<TAttributes>(in ReadOnlySpan<char> str, bool partRequired, TryParseAttributes<TAttributes> pn, [NotNullWhen(true)] out PartName<TAttributes>? name) where TAttributes : IAttributes
		{
			return TryParse(str, partRequired, NameRules.Default, pn, out name);
		}
		/// <summary>
		/// Parses a string as a PartName.
		/// </summary>
		/// <param name="str">The string to parse</param>
		/// <param name="partRequired">If true, the leading ~0~1~2 is required. Otherwise it's optional.</param>
		/// <param name="rules">The rules to use when parsing</param>
		/// <param name="pn">The callback to parse the attributes</param>
		/// <param name="name">The parsed name</param>
		public static bool TryParse<TAttributes>(in ReadOnlySpan<char> str, bool partRequired, NameRules rules, TryParseAttributes<TAttributes> pn, [NotNullWhen(true)] out PartName<TAttributes>? name) where TAttributes : IAttributes
		{
			name = null;
			if (str.Length == 0)
			{
				return false;
			}

			ReadOnlySpan<char> remainingFrag;
			int top, mid, bot;
			if (partRequired)
			{
				if (str[0] == '~' && TryParsePartFragment(str, rules.TitleDelim, out Range remainder, out top, out mid, out bot))
				{
					remainingFrag = str[remainder];
				}
				else
				{
					return false;
				}
			}
			else
			{
				if (str[0] == '~')
				{
					if (TryParsePartFragment(str, rules.TitleDelim, out Range remainder, out top, out mid, out bot))
					{
						remainingFrag = str[remainder];
					}
					else
					{
						return false;
					}
				}
				else
				{
					top = mid = bot = None;
					remainingFrag = str;
				}
			}

			if (Parsing.FindParts(remainingFrag, rules, out Range tr, out Range ar, out Range sr))
			{
				if (!pn(remainingFrag[ar], rules, out TAttributes? attributes))
				{
					return false;
				}
				name = new PartName<TAttributes>(new string(remainingFrag[tr]), top, mid, bot, new string(remainingFrag[sr]), attributes);
				return true;
			}
			else
			{
				return false;
			}
		}
		private static bool TryParsePartFragment(in ReadOnlySpan<char> str, char titleDelim, out Range remainder, out int top, out int mid, out int bot)
		{
			top = None;
			mid = None;
			bot = None;
			int space = str.IndexOf(titleDelim);
			// This only gets called when str[0] == '~'
			Debug.Assert(str[0] == '~', nameof(TryParsePartFragment) + " should never get called when str does not start with a ~");
			// We need at least 1 digit, and we have to find either the title/attribute/suffix delimiter.
			// TODO fix this it's wrong!
			if (space <= 1)
			{
				remainder = default;
				return false; 
			}
			remainder = (space + 1)..;
			ReadOnlySpan<char> partFrag = str[1..space];

			// We found the first squiggle so, only the 2nd and 3rd are left to find
			int midSquig = partFrag.IndexOf('~');
			// No more squiggles, so we only have the top part
			if (midSquig == -1)
			{
				if (!int.TryParse(partFrag, out top)) return false;
			}
			else
			{
				int botSquig = partFrag.LastIndexOf('~');
				// Being the same, that means there were only 2 squiggles
				if (botSquig == midSquig)
				{
					if (midSquig == partFrag.Length - 1) return false;
					if (!int.TryParse(partFrag[..midSquig], out top)) return false;
					if (!int.TryParse(partFrag[(midSquig + 1)..], out mid)) return false;
				}
				// Otherwise we have 3 squiggles
				else
				{
					if (botSquig == partFrag.Length - 1) return false;
					if (!int.TryParse(partFrag[..midSquig], out top)) return false;
					if (!int.TryParse(partFrag[(midSquig + 1)..botSquig], out mid)) return false;
					if (!int.TryParse(partFrag[(botSquig + 1)..], out bot)) return false;
				}
			}
			return true;
		}
	}
}
