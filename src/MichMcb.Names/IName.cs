namespace MichMcb.Names
{
	/// <summary>
	/// A name with a title and suffix
	/// </summary>
	public interface IName : IStringable
	{
		/// <summary>
		/// Suffix with leading suffix character
		/// </summary>
		string Suffix { get; set; }
		/// <summary>
		/// The title
		/// </summary>
		string Title { get; set; }
	}
	/// <summary>
	/// A name with a title, suffix, and attributes
	/// </summary>
	/// <typeparam name="TAttributes"></typeparam>
	public interface IName<TAttributes> : IName where TAttributes : IAttributes
	{
		/// <summary>
		/// The attributes
		/// </summary>
		TAttributes Attributes { get; set; }
	}
}