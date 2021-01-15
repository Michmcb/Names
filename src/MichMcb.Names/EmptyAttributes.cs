namespace MichMcb.Names
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	public sealed class EmptyAttributes : IAttributes
	{
		/// <summary>
		/// Appends nothing
		/// </summary>
		public StringBuilder AppendTo(StringBuilder sb)
		{
			return sb;
		}
		/// <summary>
		/// Appends nothing
		/// </summary>
		public StringBuilder AppendTo(StringBuilder sb, NameRules rules)
		{
			return sb;
		}
		/// <summary>
		/// Returns <see cref="string.Empty"/>.
		/// </summary>
		/// <returns><see cref="string.Empty"/>.</returns>
		public override string ToString()
		{
			return string.Empty;
		}
		/// <summary>
		/// Returns <see cref="string.Empty"/>.
		/// </summary>
		/// <returns><see cref="string.Empty"/>.</returns>
		public string ToString(NameRules rules)
		{
			return string.Empty;
		}
		/// <summary>
		/// Returns <paramref name="prefix"/> as a string.
		/// </summary>
		/// <returns><paramref name="prefix"/> as a string.</returns>
		public string ToString(NameRules rules, in ReadOnlySpan<char> prefix)
		{
			return prefix.ToString();
		}
	}
}
