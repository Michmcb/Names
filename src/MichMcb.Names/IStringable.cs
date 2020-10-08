namespace MichMcb.Names
{
	using System;
	using System.Text;
	/// <summary>
	/// This thing can be turned into a string
	/// </summary>
	public interface IStringable
	{
		/// <summary>
		/// Turns this into a string using rules <see cref="NameRules.Default"/>.
		/// </summary>
		string ToString();
		/// <summary>
		/// Turns this into a string using the provided <paramref name="rules"/>.
		/// </summary>
		/// <param name="rules">The rules to use</param>
		string ToString(NameRules rules);
		/// <summary>
		/// Turns this into a string using the provided <paramref name="rules"/>.
		/// Prefixes the resultant string with <paramref name="prefix"/>.
		/// </summary>
		/// <param name="rules">The rules to use</param>
		/// <param name="prefix">The prefix to prepend</param>
		string ToString(NameRules rules, in ReadOnlySpan<char> prefix);
		/// <summary>
		/// Writes this Name as a string, to <paramref name="stringBuilder"/>, using <see cref="NameRules.Default"/>.
		/// </summary>
		/// <param name="stringBuilder">The StringBuilder to which the resultant string is appended.</param>
		/// <returns><paramref name="stringBuilder"/></returns>
		StringBuilder AppendTo(StringBuilder stringBuilder);
		/// <summary>
		/// Writes this Name as a string, to <paramref name="stringBuilder"/>, using the provided <paramref name="rules"/>.
		/// </summary>
		/// <param name="stringBuilder">The StringBuilder to which the resultant string is appended.</param>
		/// <param name="rules">The rules to use</param>
		/// <returns><paramref name="stringBuilder"/></returns>
		public StringBuilder AppendTo(StringBuilder stringBuilder, NameRules rules);
	}
}