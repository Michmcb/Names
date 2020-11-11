namespace MichMcb.Names
{
	using System.Collections.Generic;

	/// <summary>
	/// A type that can be used as attributes
	/// </summary>
	public interface IAttributes : IStringable
	{
		/// <summary>
		/// Produces a series of strings. The strings should be able to be joined together with <see cref="NameRules.AttributeDelim"/>.
		/// </summary>
		/// <param name="rules">The rules to use</param>
		/// <returns>One string for each attribute/value pair</returns>
		public IEnumerable<string> ProduceAttributeFragments(NameRules rules);
	}
}
