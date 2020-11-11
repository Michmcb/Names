namespace MichMcb.Names
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Text;

	/// <summary>
	/// Attributes tailored towards describing music
	/// </summary>
	public class MusicAttributes : IAttributes
	{
		/// <summary>
		/// 0/2, when neutral. This is the default.
		/// </summary>
		public const char Fav_None = '\0';
		/// <summary>
		/// 1/2, when liked.
		/// </summary>
		public const char Fav_Liked = '1';
		/// <summary>
		/// 2/2, for favourites.
		/// </summary>
		public const char Fav_Favourite = '2';
		/// <summary>
		/// Creates a copy of <paramref name="attributes"/>. Any additional parameters override the properties of <paramref name="attributes"/>.
		/// </summary>
		public MusicAttributes(MusicAttributes attributes, string? artist = null, string? album = null, DateTime? dateTime = default, char favourite = default, string? tags = null, string? variation = null, bool attentionRequired = false)
		{
			Artist = artist ?? attributes.Artist;
			Album = album ?? attributes.Album;
			DateTime = dateTime ?? attributes.DateTime;
			Favourite = favourite != '\0' ? favourite : attributes.Favourite;
			Tags = tags ?? attributes.Tags;
			Variation = variation ?? attributes.Variation;
			AttentionRequired = attentionRequired || attributes.AttentionRequired;
		}
		/// <summary>
		/// Creates a new instance with the provided properties
		/// </summary>
		public MusicAttributes(string? artist = null, string? album = null, DateTime dateTime = default, char favourite = default, string? tags = null, string? variation = null, bool attentionRequired = false)
		{
			Artist = artist;
			Album = album;
			DateTime = dateTime;
			Favourite = favourite;
			Tags = tags;
			Variation = variation;
			AttentionRequired = attentionRequired;
		}
		public string? Artist { get; }
		public string? Album { get; }
		/// <summary>
		/// When this piece of music was released or published.
		/// </summary>
		public DateTime DateTime { get; }
		/// <summary>
		/// A single char denoting how liked this is. When it is \0 (null), it will not be written.
		/// </summary>
		public char Favourite { get; }
		/// <summary>
		/// Tags/Genres.
		/// </summary>
		public string? Tags { get; }
		/// <summary>
		/// Can be used to denote that this is a variation of an original piece of music.
		/// </summary>
		public string? Variation { get; }
		/// <summary>
		/// Requires some sort of effort to get it into a good state
		/// </summary>
		public bool AttentionRequired { get; }
		/// <summary>
		/// Returns this as a string, using <see cref="NameRules.Default"/>.
		/// </summary>
		/// <returns>The attributes as a string.</returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			AppendTo(sb, NameRules.Default);
			return sb.ToString();
		}
		/// <summary>
		/// Returns this as a string, using <paramref name="rules"/>.
		/// </summary>
		/// <param name="rules">The rules to use to format these attributes.</param>
		/// <returns>The attributes as a string.</returns>
		public string ToString(NameRules rules)
		{
			StringBuilder sb = new StringBuilder();
			AppendTo(sb, rules);
			return sb.ToString();
		}
		/// <summary>
		/// Returns this as a string, using <paramref name="rules"/>. The string will have <paramref name="prefix"/> prepended to it.
		/// </summary>
		/// <param name="rules">The rules to use to format these attributes.</param>
		/// <param name="prefix">A string to prefix these attributes with.</param>
		/// <returns>The attributes as a string.</returns>
		public string ToString(NameRules rules, in ReadOnlySpan<char> prefix)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(prefix);
			AppendTo(sb, rules);
			return sb.ToString();
		}
		/// <summary>
		/// Appends this as a string to <paramref name="sb"/>, using <see cref="NameRules.Default"/>.
		/// </summary>
		/// <param name="sb">The StringBuilder to which the resultant string is appended.</param>
		/// <returns><paramref name="sb"/></returns>
		public StringBuilder AppendTo(StringBuilder sb)
		{
			return AppendTo(sb, NameRules.Default);
		}
		/// <summary>
		/// Appends this as a string to <paramref name="sb"/>, using <paramref name="rules"/>.
		/// </summary>
		/// <param name="sb">The StringBuilder to which the resultant string is appended.</param>
		/// <param name="rules">The rules to use.</param>
		/// <returns><paramref name="sb"/></returns>
		public StringBuilder AppendTo(StringBuilder sb, NameRules rules)
		{
			sb.Append(rules.AttributeStart);
			sb.AppendJoin(rules.AttributeDelim, ProduceAttributeFragments(rules));
			sb.Append(rules.AttributeEnd);
			return sb;
		}
		/// <summary>
		/// Produces a series of strings; the strings are is e.g. a=Author, then b=Group, etc.
		/// Can be joined together with <see cref="NameRules.AttributeDelim"/>.
		/// </summary>
		/// <param name="rules"></param>
		/// <returns></returns>
		public IEnumerable<string> ProduceAttributeFragments(NameRules rules)
		{
			if (!string.IsNullOrEmpty(Artist))
			{
				yield return "a=" + Artist;
			}
			if (!string.IsNullOrEmpty(Album))
			{
				yield return "b=" + Album;
			}
			if (DateTime != default)
			{
				yield return "d=" + DateTime.ToString(rules.DateFormat);
			}
			if (Favourite != Fav_None)
			{
				yield return "f=" + Favourite;
			}
			if (!string.IsNullOrEmpty(Tags))
			{
				yield return "t=" + Tags;
			}
			if (!string.IsNullOrEmpty(Variation))
			{
				yield return "v=" + Variation;
			}
			if (AttentionRequired)
			{
				yield return "z=1";
			}
		}
		/// <summary>
		/// Propogates the properties of this to <paramref name="other"/>, creating a new <see cref="MusicAttributes"/> object.
		/// It's like inheritance; the values of <paramref name="other"/> are chosen, unless they are null (or default for value types)
		/// </summary>
		public MusicAttributes Propogate(MusicAttributes other)
		{
			return new MusicAttributes(other, Artist, Album, DateTime, Favourite, Tags, Variation, AttentionRequired);
		}
		/// <summary>
		/// Attempts to parse <paramref name="str"/> as an attributes string.
		/// </summary>
		/// <param name="str">The string to parse.</param>
		/// <param name="rules">The rules to use when parsing.</param>
		/// <param name="attributes">The attributes parsed.</param>
		/// <returns>True if successful, false otherwise.</returns>
		public static bool TryParse(in ReadOnlySpan<char> str, NameRules rules, [NotNullWhen(true)] out MusicAttributes? attributes)
		{
			// Empty string shouldn't ever happen
			if (str.IsEmpty || str.IsWhiteSpace())
			{
				attributes = null;
				return false;
			}
			string? author = null;
			string? group = null;
			DateTime dateTime = default;
			string? tags = null;
			string? variation = null;
			char favourite = Fav_None;
			bool z = false;
			bool outcome = true;

			Parsing.ForEachToken(str, rules.AttributeDelim, (in ReadOnlySpan<char> token, ref bool go) =>
			{
				// Make sure that there's actually 2 more chars to parse as the separator and the value. If not, bomb out
				if (token.Length < 2)
				{
					go = false;
					outcome = false;
					return;
				}
				char sep = token[1];
				if (sep == '=')
				{
					switch (token[0])
					{
						case 'a':
							author = new string(token.Slice(2));
							break;
						case 'b':
							group = new string(token.Slice(2));
							break;
						case 'd':
							if (!DateTime.TryParse(token.Slice(2), out dateTime))
							{
								go = false;
								outcome = false;
							}
							break;
						case 'f':
							favourite = token[2];
							break;
						case 't':
							tags = new string(token.Slice(2));
							break;
						case 'v':
							variation = new string(token.Slice(2));
							break;
						case 'z':
							z = true;
							break;
					}
				}
				else
				{
					go = false;
					outcome = false;
				}
			});

			if (outcome)
			{
				attributes = new MusicAttributes(author, group, dateTime, favourite, tags, variation, z);
				return true;
			}
			else
			{
				attributes = null;
				return false;
			}
		}
	}
}
