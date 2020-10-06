namespace MichMcb.Names
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Text;
	/// <summary>
	/// Represents a single Name, with only a Title, Attributes, and Suffix.
	/// </summary>
	public class Name : IEquatable<Name>, IName
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
		public Name() : this(null, Attributes.Empty, string.Empty) { }
		public Name(string? title, Attributes attributes, string suffix)
		{
			Attributes = attributes;
			Title = title;
			Suffix = suffix;
		}
		/// <summary>
		/// Writes this Name as a string according to this Name's RuleSet
		/// </summary>
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
			sb.Append(Title);
			Attributes.AppendTo(sb);
			sb.Append(Suffix);
			return sb;
		}
		/// <summary>
		/// Parses a string as a Name.
		/// Equivalent to calling TryParse(s, out Name name);
		/// If the string does not adhere to the rules of the NameRuleSet, this method returns false.
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

			name = new Name(null, Attributes.Empty, string.Empty);
			if (Parsing.ParseTitleAttributeSuffixFragment(s, name))
			{
				return true;
			}
			return false;
		}
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
		public static bool operator !=(Name? lhs, Name? rhs)
		{
			return !(lhs == rhs);
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
			if (obj is Name rhs)
			{
				return Title == rhs.Title;
			}
			return false;
		}
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
		public override int GetHashCode()
		{
			return HashCode.Combine(Title);
		}
	}
}
