namespace MichMcb.Names
{
	public interface IName : IStringable
	{
		/// <summary>
		/// Suffix with leading suffix character
		/// </summary>
		string Suffix { get; set; }
		string? Title { get; set; }
	}
	public interface IName<TAttributes> where TAttributes : class, IAttributes
	{

	}
}