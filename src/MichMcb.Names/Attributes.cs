namespace MichMcb.Names
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Text;
	public sealed class Attributes : IStringable
	{
		/// <summary>
		/// Singleton instance which will produce nothing when turned into a string
		/// </summary>
		public static readonly Attributes Empty = new Attributes();
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
		/// If false, this will write an empty string.
		/// If true, this will always at least produce {}
		/// </summary>
		private readonly bool isEmpty;
		private Attributes()
		{
			isEmpty = true;
		}
		/// <summary>
		/// Creates a copy of <paramref name="attributes"/>.
		/// </summary>
		public Attributes(Attributes attributes)
		{
			Author = attributes.Author;
			Group = attributes.Group;
			Favourite = attributes.Favourite;
			Person = attributes.Person;
			Tags = attributes.Tags;
			Variation = attributes.Variation;
			AttentionRequired = attributes.AttentionRequired;
		}
		/// <summary>
		/// Creates a copy of <paramref name="attributes"/>. Any additional parameters override the properties of <paramref name="attributes"/>.
		/// </summary>
		public Attributes(Attributes attributes, string? author = null, string? group = null, DateTime? dateTime = default, char favourite = default, string? person = null, string? tags = null, string? variation = null, bool z = false)
		{
			Author = author ?? attributes.Author;
			Group = group ?? attributes.Group;
			DateTime = dateTime ?? attributes.DateTime;
			Favourite = favourite != '\0' ? favourite : attributes.Favourite;
			Person = person ?? attributes.Person;
			Tags = tags ?? attributes.Tags;
			Variation = variation ?? attributes.Variation;
			AttentionRequired = z || attributes.AttentionRequired;
		}
		/// <summary>
		/// Creates a new instance with the provided properties
		/// </summary>
		public Attributes(string? author = null, string? group = null, DateTime dateTime = default, char favourite = default, string? person = null, string? tags = null, string? variation = null, bool z = false)
		{
			Author = author;
			Group = group;
			DateTime = dateTime;
			Favourite = favourite;
			Person = person;
			Tags = tags;
			Variation = variation;
			AttentionRequired = z;
		}
		/// <summary>
		/// Author or creator
		/// </summary>
		public string? Author { get; }
		/// <summary>
		/// Part of a collective group
		/// </summary>
		public string? Group { get; }
		public DateTime DateTime { get; }
		/// <summary>
		/// A char denoting a degree of favouritism
		/// </summary>
		public char Favourite { get; }
		/// <summary>
		/// Related to a particular person or persons
		/// </summary>
		public string? Person { get; }
		/// <summary>
		/// Identifies certain things this contains
		/// </summary>
		public string? Tags { get; }
		/// <summary>
		/// Is a variation of the original
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
			if (isEmpty)
			{
				return string.Empty;
			}
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
			if (isEmpty)
			{
				return string.Empty;
			}
			StringBuilder sb = new StringBuilder();
			AppendTo(sb, rules);
			return sb.ToString();
		}
		/// <summary>
		/// Returns this as a string, using <paramref name="rules"/>. The string will have <paramref name="prefix"/> prepended to it.
		/// </summary>
		/// <param name="rules">The rules to use to format these attributes.</param>
		/// <returns>The attributes as a string.</returns>
		public string ToString(NameRules rules, in ReadOnlySpan<char> prefix)
		{
			if (isEmpty)
			{
				return string.Empty;
			}
			StringBuilder sb = new StringBuilder();
			sb.Append(prefix);
			AppendTo(sb, rules);
			return sb.ToString();
		}
		/// <summary>
		/// Appends this as a string to <paramref name="sb"/>, using <see cref="NameRules.Default"/>.
		/// </summary>
		/// <param name="sb">The StringBuilder to which the resultant string is appended</param>
		/// <returns><paramref name="sb"/></returns>
		public StringBuilder AppendTo(StringBuilder sb)
		{
			return AppendTo(sb, NameRules.Default);
		}
		/// <summary>
		/// Appends this as a string to <paramref name="sb"/>, using <paramref name="rules"/>.
		/// </summary>
		/// <param name="sb">The StringBuilder to which the resultant string is appended</param>
		/// <returns><paramref name="sb"/></returns>
		public StringBuilder AppendTo(StringBuilder sb, NameRules rules)
		{
			if (!isEmpty)
			{
				sb.Append(Formatting.AttributeStart);
				sb.AppendJoin(Formatting.AttributeDelim, ProduceAttributeFragments(rules));
				sb.Append(Formatting.AttributeEnd);
			}
			return sb;
		}
		/// <summary>
		/// Produces a series of strings; the strings are is e.g. a=Author, then b=Group, etc.
		/// Can be joined together with <see cref="Formatting.AttributeDelim"/>.
		/// </summary>
		/// <param name="rules"></param>
		/// <returns></returns>
		public IEnumerable<string> ProduceAttributeFragments(NameRules rules)
		{
			if (!string.IsNullOrEmpty(Author))
			{
				yield return "a=" + Author;
			}
			if (!string.IsNullOrEmpty(Group))
			{
				yield return "b=" + Group;
			}
			if (DateTime != default)
			{
				yield return "d=" + DateTime.ToString(rules.DateFormat);
			}
			if (Favourite != Fav_None)
			{
				yield return "f=" + Favourite;
			}
			if (!string.IsNullOrEmpty(Person))
			{
				yield return "p=" + Person;
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
		/// Propogates the attributes of this to <paramref name="other"/>, creating a new Attributes object.
		/// It's like inheritance; the values of <paramref name="other"/> are chosen, unless they are null (or default for value types)
		/// </summary>
		public Attributes Propogate(Attributes other)
		{
			return new Attributes(other, Author, Group, DateTime, Favourite, Person, Tags, Variation, AttentionRequired);
		}
		/// <summary>
		/// Attempts to parse <paramref name="str"/> as an attributes string.
		/// </summary>
		/// <param name="str">The string to parse.</param>
		/// <param name="attributes">The attributes parsed.</param>
		/// <returns>True if successful, false otherwise.</returns>
		public static bool TryParse(in ReadOnlySpan<char> str, [NotNullWhen(true)] out Attributes? attributes)
		{
			// Empty string means no attributes should be set
			if (Parsing.IsEmptyOrWhiteSpace(str))
			{
				attributes = Empty;
				return true;
			}
			string? author = null;
			string? group = null;
			DateTime dateTime = default;
			string? person = null;
			string? tags = null;
			string? variation = null;
			char favourite = Fav_None;
			bool z = false;
			bool outcome = true;

			Parsing.ForEachToken(str, Formatting.AttributeDelim, (in ReadOnlySpan<char> token, ref bool go) =>
			{
				// Make sure that there's actually 2 more chars to parse as the separator and the value. If not, skip this one
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
							Parsing.ParseDateTimeFragment(token.Slice(2), out dateTime);
							break;
						case 'f':
							favourite = token[2];
							break;
						case 'p':
							person = new string(token.Slice(2));
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
				attributes = new Attributes(author, group, dateTime, favourite, person, tags, variation, z);
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
