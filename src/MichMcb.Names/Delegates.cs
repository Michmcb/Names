using System;

namespace MichMcb.Names
{
	/// <summary>
	/// A delegate that should try to parse a string as a DateTime.
	/// </summary>
	/// <param name="str"></param>
	/// <param name="rules"></param>
	/// <param name="result"></param>
	/// <returns></returns>
	public delegate bool ParseDateTime(in ReadOnlySpan<char> str, NameRules rules, out DateTime result);
	public delegate bool ParseName<TName, TAttributes>(in ReadOnlySpan<char> str, NameRules rules, out TName name) where TName : IName<TAttributes> where TAttributes : class, IAttributes;
	public delegate bool ParseAttributes<TAttributes>(in ReadOnlySpan<char> str, NameRules rules, out TAttributes attributes) where TAttributes : IAttributes;
	/// <summary>
	/// Callback to help iterate through series of spans (since you can't yield ref structs)
	/// </summary>
	/// <param name="str">The span</param>
	/// <param name="go">Set to false to stop</param>
	public delegate void SpanString(in ReadOnlySpan<char> str, ref bool go);
}
