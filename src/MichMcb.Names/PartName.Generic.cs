namespace MichMcb.Names
{
	using System;
	using System.Text;

	/// <summary>
	/// Represents a single Name which can contain digits indicating some sort of order, as well as attributes
	/// </summary>
	public sealed class PartName<TAttributes> : PartName, IComparable<PartName<TAttributes>> where TAttributes : IAttributes
	{
		/// <summary>
		/// Creates a new instance with the provided parameters.
		/// </summary>
		public PartName(string title, int topPart, int midPart, int bottomPart, string suffix, TAttributes attributes) : base(title, topPart, midPart, bottomPart, suffix)
		{
			Attributes = attributes;
		}
		/// <summary>
		/// The attributes
		/// </summary>
		public TAttributes Attributes { get; set; }
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
		public override string ToString(NameRules rules)
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
		public override string ToString(NameRules rules, in ReadOnlySpan<char> prefix)
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
		public override StringBuilder AppendTo(StringBuilder stringBuilder)
		{
			return AppendTo(stringBuilder, NameRules.Default);
		}
		/// <summary>
		/// Writes this Name as a string, to <paramref name="stringBuilder"/>, using the provided <paramref name="rules"/>.
		/// </summary>
		/// <param name="stringBuilder">The StringBuilder to which the resultant string is appended.</param>
		/// <param name="rules">The rules to use</param>
		/// <returns><paramref name="stringBuilder"/></returns>
		public override StringBuilder AppendTo(StringBuilder stringBuilder, NameRules rules)
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
			if (Attributes != null)
			{
				stringBuilder.Append(rules.AttributeStart);
				Attributes.AppendTo(stringBuilder);
				stringBuilder.Append(rules.AttributeEnd);
			}
			stringBuilder.Append(Suffix);
			return stringBuilder;
		}
		/// <summary>
		/// Compares based on <see cref="PartName.TopPart"/>, then <see cref="PartName.MidPart"/>, then <see cref="PartName.BottomPart"/>.
		/// </summary>
		public static bool operator <(PartName<TAttributes>? lhs, PartName<TAttributes>? rhs) => !(lhs is null) && !(rhs is null) && (lhs.TopPart < rhs.TopPart || lhs.MidPart < rhs.MidPart || lhs.BottomPart < rhs.BottomPart);
		/// <summary>
		/// Compares based on <see cref="PartName.TopPart"/>, then <see cref="PartName.MidPart"/>, then <see cref="PartName.BottomPart"/>.
		/// Also returns true if both are null.
		/// </summary>
		public static bool operator <=(PartName<TAttributes>? lhs, PartName<TAttributes>? rhs) => lhs is null && rhs is null || (!(lhs is null) && !(rhs is null) && (lhs.TopPart <= rhs.TopPart || lhs.MidPart <= rhs.MidPart || lhs.BottomPart <= rhs.BottomPart));
		/// <summary>
		/// Compares based on <see cref="PartName.TopPart"/>, then <see cref="PartName.MidPart"/>, then <see cref="PartName.BottomPart"/>.
		/// </summary>
		public static bool operator >(PartName<TAttributes>? lhs, PartName<TAttributes>? rhs) => !(lhs is null) && !(rhs is null) && (lhs.TopPart > rhs.TopPart || lhs.MidPart > rhs.MidPart || lhs.BottomPart > rhs.BottomPart);
		/// <summary>
		/// Compares based on <see cref="PartName.TopPart"/>, then <see cref="PartName.MidPart"/>, then <see cref="PartName.BottomPart"/>.
		/// Also returns true if both are null.
		/// </summary>
		public static bool operator >=(PartName<TAttributes>? lhs, PartName<TAttributes>? rhs) => lhs is null && rhs is null || !(lhs is null) && !(rhs is null) && (lhs.TopPart > rhs.TopPart || lhs.MidPart > rhs.MidPart || lhs.BottomPart > rhs.BottomPart);
		/// <summary>
		/// Equality based on <see cref="PartName.TopPart"/> and <see cref="PartName.MidPart"/> and <see cref="PartName.BottomPart"/>.
		/// </summary>
		public static bool operator ==(PartName<TAttributes>? lhs, PartName<TAttributes>? rhs) => lhs is null ? rhs is null : lhs.Equals(rhs); // Returns true if both are null, otherwise calls Equals
		/// <summary>
		/// Equality based on <see cref="PartName.TopPart"/> and <see cref="PartName.MidPart"/> and <see cref="PartName.BottomPart"/>.
		/// </summary>
		public static bool operator !=(PartName<TAttributes>? lhs, PartName<TAttributes>? rhs) => !(lhs == rhs);
		/// <summary>
		/// Compares based on <see cref="PartName.TopPart"/>, then <see cref="PartName.MidPart"/>, then <see cref="PartName.BottomPart"/>.
		/// </summary>
		public int CompareTo(PartName<TAttributes>? other)
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
		/// Compares based on <see cref="PartName.TopPart"/>, then <see cref="PartName.MidPart"/>, then <see cref="PartName.BottomPart"/>.
		/// </summary>
		public override int CompareTo(object? obj)
		{
			return obj is PartName<TAttributes> name ? CompareTo(name) : throw new ArgumentException("Not a " + nameof(PartName<TAttributes>));
		}
		/// <summary>
		/// Equality based on <see cref="PartName.TopPart"/> and <see cref="PartName.MidPart"/> and <see cref="PartName.BottomPart"/>.
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
			if (obj is PartName<TAttributes> rhs)
			{
				// If rhs is not null, we can compare. Only equal if all numerical properties are equal.
				return TopPart == rhs.TopPart && MidPart == rhs.MidPart && BottomPart == rhs.BottomPart;
			}
			return false;
		}
		/// <summary>
		/// Equality based on <see cref="PartName.TopPart"/> and <see cref="PartName.MidPart"/> and <see cref="PartName.BottomPart"/>.
		/// </summary>
		public bool Equals(PartName<TAttributes>? rhs)
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
		/// Hashcode based on <see cref="PartName.TopPart"/> and <see cref="PartName.MidPart"/> and <see cref="PartName.BottomPart"/>.
		/// </summary>
		public override int GetHashCode()
		{
			return HashCode.Combine(TopPart, MidPart, BottomPart);
		}
	}
}
