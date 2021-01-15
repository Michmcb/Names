namespace MichMcb.Names
{
	using System.Text;
	/// <summary>
	/// Represents a single name with attributes
	/// </summary>
	/// <typeparam name="TAttributes"></typeparam>
	public sealed class Name<TAttributes> : Name, IName<TAttributes> where TAttributes : IAttributes
	{
		/// <summary>
		/// Creates a new instance with the provided parameters.
		/// </summary>
		public Name(string title, string suffix, TAttributes attributes)
		{
			Title = title;
			Suffix = suffix;
			Attributes = attributes;
		}
		/// <summary>
		/// The Attributes
		/// </summary>
		public TAttributes Attributes { get; set; }
		/// <summary>
		/// Writes this Name as a string, to <paramref name="stringBuilder"/>, using the provided <paramref name="rules"/>.
		/// </summary>
		/// <param name="stringBuilder">The StringBuilder to which the resultant string is appended.</param>
		/// <param name="rules">The rules to use</param>
		/// <returns><paramref name="stringBuilder"/></returns>
		public override StringBuilder AppendTo(StringBuilder stringBuilder, NameRules rules)
		{
			stringBuilder.Append(Title);
			Attributes?.AppendTo(stringBuilder, rules);
			stringBuilder.Append(Suffix);
			return stringBuilder;
		}
	}
}
