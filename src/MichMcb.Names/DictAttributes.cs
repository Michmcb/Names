namespace MichMcb.Names
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Text;

	/// <summary>
	/// Holds a dictionary of char/string
	/// </summary>
	public sealed class DictAttributes : IAttributes
	{
		public DictAttributes()
		{
			Attributes = new Dictionary<char, string>();
		}
		public DictAttributes(IDictionary<char, string> attributes)
		{
			Attributes = attributes;
		}
		/// <summary>
		/// Same thing as using the indexing operator on <see cref="Attributes"/>
		/// </summary>
		public string this[char c]
		{
			get => Attributes[c];
			set => Attributes[c] = value;
		}
		/// <summary>
		/// The attributes. You can use the indexing operator directly on this object for the same effect.
		/// </summary>
		public IDictionary<char, string> Attributes { get; set; }
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
			using (IEnumerator<KeyValuePair<char, string>> en = Attributes.GetEnumerator())
			{
				// Just stop on empty
				if (!en.MoveNext())
				{
					return sb;
				}
				// Append the first, without a separator after it
				KeyValuePair<char, string> val = en.Current;
				sb.Append(val.Key);
				sb.Append('=');
				sb.Append(val.Value);
				while (en.MoveNext())
				{
					sb.Append(rules.AttributeDelim);
					val = en.Current;
					sb.Append(val.Key);
					sb.Append('=');
					sb.Append(val.Value);
				}
			}
			return sb;
		}
		/// <summary>
		/// Attempts to parse <paramref name="str"/> as an attributes string.
		/// </summary>
		/// <param name="str">The string to parse, not including the attribute delimiters.</param>
		/// <param name="rules">The rules to use when parsing.</param>
		/// <param name="attributes">The attributes parsed.</param>
		/// <returns>True if successful, false otherwise.</returns>
		public static bool TryParse(in ReadOnlySpan<char> str, NameRules rules, [NotNullWhen(true)] out DictAttributes? attributes)
		{
			if (str.IsEmpty || str.IsWhiteSpace())
			{
				attributes = null;
				return false;
			}

			bool outcome = true;
			Dictionary<char, string> attrs = new();
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
					attrs[token[0]] = new string(token[2..]);
				}
				else
				{
					go = false;
					outcome = false;
				}
			});

			if (outcome)
			{
				attributes = new DictAttributes(attrs);
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
