//using System;
//using Xunit;

//namespace MichMcb.Names.Test
//{
//	public class DateNameTest
//	{
//		[Fact]
//#pragma warning disable CS8602 // Dereference of a possibly null reference.
//		public void Parsing()
//		{
//			Assert.True(DateName.TryParse("2020 Title.suffix", out DateName? name));
//			Assert.NotNull(name);
//			Assert.Equal(new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Local), name.Date);
//			Assert.Equal("Title", name.Title);
//			Assert.Equal(".suffix", name.Suffix);

//			Assert.True(DateName.TryParse("2020-05 Title.suffix", out name));
//			Assert.NotNull(name);
//			Assert.Equal(new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Local), name.Date);
//			Assert.Equal("Title", name.Title);
//			Assert.Equal(".suffix", name.Suffix);

//			Assert.True(DateName.TryParse("2020-05-15 Title.suffix", out name));
//			Assert.NotNull(name);
//			Assert.Equal(new DateTime(2020, 5, 15, 0, 0, 0, DateTimeKind.Local), name.Date);
//			Assert.Equal("Title", name.Title);
//			Assert.Equal(".suffix", name.Suffix);

//			Assert.True(DateName.TryParse("2020-05-15_20 Title.suffix", out name));
//			Assert.NotNull(name);
//			Assert.Equal(new DateTime(2020, 5, 15, 20, 0, 0, DateTimeKind.Local), name.Date);
//			Assert.Equal("Title", name.Title);
//			Assert.Equal(".suffix", name.Suffix);

//			Assert.True(DateName.TryParse("2020-05-15_20-15 Title.suffix", out name));
//			Assert.NotNull(name);
//			Assert.Equal(new DateTime(2020, 5, 15, 20, 15, 0, DateTimeKind.Local), name.Date);
//			Assert.Equal("Title", name.Title);
//			Assert.Equal(".suffix", name.Suffix);

//			Assert.True(DateName.TryParse("2020-05-15_20-15-20 Title.suffix", out name));
//			Assert.NotNull(name);
//			Assert.Equal(new DateTime(2020, 5, 15, 20, 15, 20, DateTimeKind.Local), name.Date);
//			Assert.Equal("Title", name.Title);
//			Assert.Equal(".suffix", name.Suffix);
//		}
//#pragma warning restore CS8602 // Dereference of a possibly null reference.
//	}
//}
