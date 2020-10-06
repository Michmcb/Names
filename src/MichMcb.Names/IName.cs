namespace MichMcb.Names
{
	public interface IName : IStringable
	{
		Attributes Attributes { get; set; }
		/// <summary>
		/// Suffix with leading suffix character
		/// </summary>
		string Suffix { get; set; }
		string? Title { get; set; }
	}
}