namespace MichMcb.Names.Test
{
	using System;
	using Xunit;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
	public class NameTest
	{
		[Fact]
		public void AttributeParsing()
		{
			Assert.True(Name.TryParse("MyTitle{a=Author;b=Group;d=2002-12-25;f=1;p=Dude;t=abcdef;v=Variation}.suffix", out Name? name));

			Assert.Equal("MyTitle", name.Title);
			Assert.Equal(".suffix", name.Suffix);
			Attributes a = name.Attributes;
			Assert.Equal("Author", a.Author);
			Assert.Equal("Group", a.Group);
			Assert.Equal(new DateTime(2002, 12, 25, 0, 0, 0, DateTimeKind.Local), a.DateTime);
			Assert.Equal('1', a.Favourite);
			Assert.Equal("Dude", a.Person);
			Assert.Equal("abcdef", a.Tags);
			Assert.Equal("Variation", a.Variation);
		}
		//[Fact]
		//public void AttributeAliasing()
		//{
		//	Assert.True(Name.TryParse("MyTitle{a~T;b~T;c~T;p~T;t~T;v~T}.suffix", out Name? name));

		//	Assert.Equal("MyTitle", name.Title);
		//	Attributes a = name.Attributes;
		//	Assert.Equal("MyTitle", a.Author);
		//	Assert.Equal("MyTitle", a.Group);
		//	Assert.Equal("MyTitle", a.Category);
		//	Assert.Equal("MyTitle", a.Person);
		//	Assert.Equal("MyTitle", a.Tags);
		//	Assert.Equal("MyTitle", a.Variation);
		//}
	}
#pragma warning restore CS8602 // Dereference of a possibly null reference.
}