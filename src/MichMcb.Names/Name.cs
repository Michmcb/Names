namespace MichMcb.Names
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Text;

	/// <summary>
	/// Represents a single Name, with only a Title and Suffix.
	/// </summary>
	public class Name : IComparable, IComparable<Name>, IEquatable<Name>, IName
	{
		/// <summary>
		/// Creates a new instance with a null title and empty suffix
		/// </summary>
		public Name() : this(string.Empty, string.Empty) { }
		/// <summary>
		/// Creates a new instance with the provided parameters.
		/// </summary>
		public Name(string title, string suffix)
		{
			Title = title;
			Suffix = suffix;
		}
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
		public virtual StringBuilder AppendTo(StringBuilder stringBuilder, NameRules rules)
		{
			stringBuilder.Append(Title);
			stringBuilder.Append(Suffix);
			return stringBuilder;
		}
		/// <summary>
		/// Equality based on <see cref="Title"/>.
		/// </summary>
		public static bool operator ==(Name? lhs, Name? rhs) => lhs is null ? rhs is null : lhs.Equals(rhs); // Returns true if both are null, otherwise calls Equals
		/// <summary>
		/// Equality based on <see cref="Title"/>\.
		/// </summary>
		public static bool operator !=(Name? lhs, Name? rhs) => !(lhs == rhs);
		/// <summary>
		/// Compares based on <see cref="Title"/>.
		/// </summary>
		public int CompareTo(Name? other)
		{
			return other is null ? 1 : string.Compare(Title, other.Title);
		}
		/// <summary>
		/// Compares based on <see cref="Title"/>.
		/// </summary>
		public virtual int CompareTo(object? obj)
		{
			return obj is Name name ? CompareTo(name) : throw new ArgumentException("Not a " + nameof(Name));
		}
		/// <summary>
		/// Equality based on <see cref="Title"/>
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
			return obj is Name rhs ? Title == rhs.Title : false;
		}
		/// <summary>
		/// Equality based on <see cref="Title"/>
		/// </summary>
		public bool Equals(Name? rhs)
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
			// If rhs is not null, we can compare
			return Title == rhs.Title;
		}
		/// <summary>
		/// Hashcode based on <see cref="Title"/>
		/// </summary>
		public override int GetHashCode()
		{
			return HashCode.Combine(Title);
		}
		/// <summary>
		/// Parses a string as a <see cref="Name"/>, using <see cref="NameRules.Default"/>.
		/// </summary>
		/// <param name="s">The string to parse</param>
		/// <param name="name">The parsed name</param>
		public static bool TryParse(in ReadOnlySpan<char> s, [NotNullWhen(true)] out Name? name)
		{
			return TryParse(s, NameRules.Default, out name);
		}
		/// <summary>
		/// Parses a string as a <see cref="Name"/>.
		/// </summary>
		/// <param name="str">The string to parse</param>
		/// <param name="rules">The rules to use when parsing</param>
		/// <param name="name">The parsed name</param>
		public static bool TryParse(in ReadOnlySpan<char> str, NameRules rules, [NotNullWhen(true)] out Name? name)
		{
			if (!Parsing.FindParts(str, rules, out Range rt, out Range ra, out Range rs))
			{
				name = null;
				return false;
			}

			name = new Name(new string(str[rt]), new string(str[rs]));
			return true;
		}
		/// <summary>
		/// Parses a string as a <see cref="Name"/>, using <see cref="NameRules.Default"/>.
		/// </summary>
		/// <param name="s">The string to parse</param>
		/// <param name="pn">The callback to parse the attributes</param>
		/// <param name="name">The parsed name</param>
		public static bool TryParse<TAttributes>(in ReadOnlySpan<char> s, TryParseAttributes<TAttributes> pn, [NotNullWhen(true)] out Name<TAttributes>? name) where TAttributes : IAttributes
		{
			return TryParse(s, NameRules.Default, pn, out name);
		}
		/// <summary>
		/// Parses a string as a <see cref="Name"/>.
		/// </summary>
		/// <param name="str">The string to parse</param>
		/// <param name="rules">The rules to use when parsing</param>
		/// <param name="pn">The callback to parse the attributes</param>
		/// <param name="name">The parsed name</param>
		public static bool TryParse<TAttributes>(in ReadOnlySpan<char> str, NameRules rules, TryParseAttributes<TAttributes> pn, [NotNullWhen(true)] out Name<TAttributes>? name) where TAttributes : IAttributes
		{
			if (!Parsing.FindParts(str, rules, out Range rt, out Range ra, out Range rs) || !pn(str[ra], rules, out TAttributes? attributes))
			{
				name = null;
				return false;
			}

			name = new Name<TAttributes>(new string(str[rt]), new string(str[rs]), attributes);
			return true;
		}
	}
}