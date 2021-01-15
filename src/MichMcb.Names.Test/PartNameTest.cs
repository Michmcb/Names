namespace MichMcb.Names.Test
{
	using Xunit;

	public sealed class PartNameTest
	{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
		[Fact]
		public void Parsing()
		{
			Assert.True(PartName.TryParse("~1~02~492 Title.suffix", out PartName? name));
			Assert.NotNull(name);
			Assert.Equal(1, name.TopPart);
			Assert.Equal(2, name.MidPart);
			Assert.Equal(492, name.BottomPart);

			Assert.True(PartName.TryParse("~02~492 Title.suffix", out name));
			Assert.NotNull(name);
			Assert.Equal(2, name.TopPart);
			Assert.Equal(492, name.MidPart);
			Assert.Equal(PartName.None, name.BottomPart);

			Assert.True(PartName.TryParse("~492 Title.suffix", out name));
			Assert.NotNull(name);
			Assert.Equal(492, name.TopPart);
			Assert.Equal(PartName.None, name.MidPart);
			Assert.Equal(PartName.None, name.BottomPart);

			Assert.True(PartName.TryParse("~1 Title.suffix", out name));
			Assert.NotNull(name);
			Assert.Equal(1, name.TopPart);
			Assert.Equal(PartName.None, name.MidPart);
			Assert.Equal(PartName.None, name.BottomPart);

			Assert.True(PartName.TryParse("~1~02 Title.suffix", out name));
			Assert.NotNull(name);
			Assert.Equal(1, name.TopPart);
			Assert.Equal(2, name.MidPart);
			Assert.Equal(PartName.None, name.BottomPart);
		}
#pragma warning restore CS8602 // Dereference of a possibly null reference.
	}
}
