namespace MichMcb.Names.Test
{
	using System;
	using Xunit;
	public class NameTest
	{
		[Fact]
		public void AttributeParsing()
		{
			Assert.True(Name.TryParse("MyTitle{a=Author;b=Group;d=2002-12-25;f=1;p=Dude;t=abcdef;v=Variation}.suffix", MusicAttributes.TryParse, out Name<MusicAttributes>? name));
			Assert.NotNull(name);

			Assert.Equal("MyTitle", name!.Title);
			Assert.Equal(".suffix", name.Suffix);
			MusicAttributes? a = name.Attributes;
			Assert.Equal("Author", a.Artist);
			Assert.Equal("Group", a.Album);
			Assert.Equal(new DateTime(2002, 12, 25, 0, 0, 0, DateTimeKind.Local), a.DateTime);
			Assert.Equal('1', a.Favourite);
			Assert.Equal("abcdef", a.Tags);
			Assert.Equal("Variation", a.Variation);
		}
		[Fact]
		public void AttributeParsingEmptyTitle()
		{
			Assert.True(Name.TryParse("{a=Author;b=Group;d=2002-12-25;f=1;p=Dude;t=abcdef;v=Variation}.suffix", MusicAttributes.TryParse, out Name<MusicAttributes>? name));
			Assert.NotNull(name);

			Assert.Equal("", name!.Title);
			Assert.Equal(".suffix", name.Suffix);
			MusicAttributes? a = name.Attributes;
			Assert.Equal("Author", a.Artist);
			Assert.Equal("Group", a.Album);
			Assert.Equal(new DateTime(2002, 12, 25, 0, 0, 0, DateTimeKind.Local), a.DateTime);
			Assert.Equal('1', a.Favourite);
			Assert.Equal("abcdef", a.Tags);
			Assert.Equal("Variation", a.Variation);
		}
	}
}