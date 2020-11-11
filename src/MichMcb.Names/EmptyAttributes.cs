namespace MichMcb.Names
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	public sealed class EmptyAttributes : IAttributes
	{
		public StringBuilder AppendTo(StringBuilder sb)
		{
			return sb;
		}
		public StringBuilder AppendTo(StringBuilder sb, NameRules rules)
		{
			return sb;
		}
		public IEnumerable<string> ProduceAttributeFragments(NameRules rules)
		{
			yield break;
		}
		public string ToString(NameRules rules)
		{
			return string.Empty;
		}
		public string ToString(NameRules rules, in ReadOnlySpan<char> prefix)
		{
			return prefix.ToString();
		}
	}
}
