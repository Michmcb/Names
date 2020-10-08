namespace MichMcb.Names
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Text;
	/// <summary>
	/// Represents a single Name, with only a Title, Attributes, and Suffix.
	/// </summary>
	public sealed class Name : IEquatable<Name>, IName
	{
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
		/// Creates a new instance with a null title, empty suffix, and empty Attributes (<see cref="Attributes.Empty"/>)
		/// </summary>
		public Name() : this(null, string.Empty, Attributes.Empty) { }
		/// <summary>
		/// Creates a new instance with the provided parameters.
		/// </summary>
		public Name(string? title, string suffix, Attributes attributes)
		{
			Attributes = attributes;
			Title = title;
			Suffix = suffix;
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
			stringBuilder.Append(Title);
			Attributes.AppendTo(stringBuilder, rules);
			stringBuilder.Append(Suffix);
			return stringBuilder;
		}
		/// <summary>
		/// Parses a string as a Name.
		/// </summary>
		/// <param name="s">The string to parse</param>
		/// <param name="name">The parsed name</param>
		public static bool TryParse(in ReadOnlySpan<char> s, [NotNullWhen(true)] out Name? name)
		{
			name = null;
			if (Parsing.IsEmptyOrWhiteSpace(s))
			{
				return false;
			}

			name = new Name(null, string.Empty, Attributes.Empty);
			if (Parsing.ParseTitleAttributeSuffixFragment(s, name))
			{
				return true;
			}
			return false;
		}
		/// <summary>
		/// Equality based on <see cref="Title"/>
		/// </summary>
		public static bool operator ==(Name? lhs, Name? rhs)
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
		/// Inequality based on <see cref="Title"/>
		/// </summary>
		public static bool operator !=(Name? lhs, Name? rhs)
		{
			return !(lhs == rhs);
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
			if (obj is Name rhs)
			{
				return Title == rhs.Title;
			}
			return false;
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
	}
}
