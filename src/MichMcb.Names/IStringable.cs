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
		/// Writes using default rules
		/// </summary>
		string ToString();
		/// <summary>
		/// Writes using the rules provided
		/// </summary>
		/// <param name="rules">The rules to use</param>
		string ToString(NameRules rules);
		/// <summary>
		/// Writes using the rules provided, with <paramref name="prefix"/> prepended.
		/// </summary>
		/// <param name="rules">The rules to use</param>
		/// <param name="prefix">The prefix to append to the resultant string</param>
		string ToString(NameRules rules, in ReadOnlySpan<char> prefix);
		/// <summary>
		/// Writes using default rules to <paramref name="sb"/>
		/// </summary>
		/// <param name="sb">The StringBuilder to write the name to</param>
		StringBuilder AppendTo(StringBuilder sb);
		/// <summary>
		/// Writes using the rules provided to <paramref name="sb"/>
		/// </summary>
		/// <param name="rules">The rules to use</param>
		/// <param name="sb">The StringBuilder to write the name to</param>
		StringBuilder AppendTo(NameRules rules, StringBuilder sb);
	}
}