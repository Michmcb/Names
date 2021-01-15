namespace MichMcb.Names
{
	using System;
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// A delegate that tries to parse <paramref name="str"/> as a DateTime.
	/// </summary>
	/// <param name="str"></param>
	/// <param name="rules"></param>
	/// <param name="result"></param>
	/// <returns></returns>
	public delegate bool TryParseDateTime(in ReadOnlySpan<char> str, NameRules rules, out DateTime result);
	/// <summary>
	/// A delegate that tries to parse <paramref name="str"/> (which should NOT contain attribute start/end delimiters) as attributes of type <typeparamref name="TAttributes"/>.
	/// </summary>
	/// <typeparam name="TAttributes"></typeparam>
	/// <param name="str"></param>
	/// <param name="rules"></param>
	/// <param name="attributes"></param>
	/// <returns></returns>
	public delegate bool TryParseAttributes<TAttributes>(in ReadOnlySpan<char> str, NameRules rules, [NotNullWhen(true)]out TAttributes? attributes) where TAttributes : IAttributes;
	/// <summary>
	/// Callback to iterate through series of spans (since you can't yield ref structs)
	/// </summary>
	/// <param name="str">The span</param>
	/// <param name="go">Set to false to stop</param>
	public delegate void SpanString(in ReadOnlySpan<char> str, ref bool go);
}
