namespace MichMcb.Names
{
	using System;

	/// <summary>
	/// Parses stuff
	/// </summary>
	public static class Parsing
	{
		public static bool ParseName<TName, TAttributes>(in ReadOnlySpan<char> str, NameRules rules, ParseName<TName, TAttributes> pn, ParseAttributes<TAttributes> pa, out TName name)
			where TName : IName<TAttributes>
			where TAttributes : class, IAttributes
		{
			if (pn(str, rules, out name))
			{
				if (pa(str, rules, out TAttributes attributes))
				{
				}
			}
			name = null;
			return false;
		}


		/// <summary>
		/// Parses a DateTime fragment, ordered year month day hour minute second, with the provided delimiters.
		/// Will accept any number of year/month/day/hour/minute/second, so long as they are in order.
		/// For example yyyy-MM is valid, and so is yyyy-MM-dd.
		/// The kind is set to <see cref="DateTimeKind.Local"/>.
		/// </summary>
		/// <param name="str">The string containing the DateTime fragment to parse</param>
		/// <param name="dateTime">If successful, the parsed DateTime. Otherwise, DateTime.MinValue</param>
		public static bool ParseDateTimeExtended(in ReadOnlySpan<char> str, NameRules rules, out DateTime dateTime)
		{
			dateTime = DateTime.MinValue;
			// The order HAS to be year, month, day, hour, minute, second.
			int month = 1, day = 1, hour = 0, minute = 0, second = 0;
			// I'm using goto here is because I didn't want to indent this like crazy and have a huge "Arrow" code.
			int startFrom = 0;

			// Year; 4 chars
			if (str.Length < startFrom + 4)
			{
				return false;// return "A least a year is required";
			}
			if (!int.TryParse(str.Slice(startFrom, 4), out int year))
			{
				return false;// return "Year was not a valid integer";
			}
			// Month; delimiter and 2 chars
			if (str.Length < startFrom + 6 || (str[startFrom + 4] != rules.TimeUnitDelim))
			{
				goto doneParsingDate;
			}
			if (!int.TryParse(str.Slice(startFrom + 5, 2), out month))
			{
				return false;// return "Month was not a valid integer";
			}
			// Day; delimiter and 2 chars
			if (str.Length < startFrom + 9 || (str[startFrom + 7] != rules.TimeUnitDelim))
			{
				goto doneParsingDate;
			}
			if (!int.TryParse(str.Slice(startFrom + 8, 2), out day))
			{
				return false;// return "Day was not a valid integer";
			}
			// Hour; delimiter and 2 chars
			if (str.Length < startFrom + 12 || (str[startFrom + 10] != rules.DateAndTimeDelim))
			{
				goto doneParsingDate;
			}
			if (!int.TryParse(str.Slice(startFrom + 11, 2), out hour))
			{
				return false;// return "Hour was not a valid integer";
			}
			// Minute; delimiter and 2 chars
			if (str.Length < startFrom + 15 || (str[startFrom + 13] != rules.TimeUnitDelim))
			{
				goto doneParsingDate;
			}
			if (!int.TryParse(str.Slice(startFrom + 14, 2), out minute))
			{
				return false;// return "Minute was not a valid integer";
			}
			// Second; delimiter and 2 chars
			if (str.Length < startFrom + 18 || (str[startFrom + 16] != rules.TimeUnitDelim))
			{
				goto doneParsingDate;
			}
			if (!int.TryParse(str.Slice(startFrom + 17, 2), out second))
			{
				return false;// return "Month was not a valid integer";
			}
			
			// TODO make sure things are in the allowable range
		doneParsingDate:
			dateTime = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Local);
			return true;// return string.Empty;
		}
		/// <summary>
		/// Parses a string fragment to extract a Title, Attributes, and Suffix.
		/// These are then set on <paramref name="name"/>
		/// </summary>
		public static bool ParseTitleAttributeSuffixFragment<TAttributes>(in ReadOnlySpan<char> str, NameRules rules, Name<TAttributes> name) where TAttributes : class, IAttributes
		{
			int i = 0;
			char c = str[i];
			// Now, there are a few different ways things can go. There can be any of a title, attributes, and suffix; all are optional.
			int attribStart = str.IndexOf(rules.AttributeStart);
			int attribEnd = attribStart != -1 ? str.IndexOf(rules.AttributeEnd, attribStart) : -1;
			// Suffixes come after attributes, so doing from the end lets us have suffix characters in the title
			int suffixStart = str.LastIndexOf(rules.SuffixDelimiter);

			// Suffix should only be allowed to start AFTER the attributes end. If it appears before the attributes, set it to -1, as if it wasn't there, and assume the name has no suffix.
			if (suffixStart < attribEnd)
			{
				suffixStart = -1;
			}

			// We SHOULD see the delimiter if we found any volume, episode, or part. But if we didn't, and thus we're at the start of the string still, then that's where the title starts
			int to;
			if ((i > 0 && c == rules.TitleDelim) || (i == 0 && c != rules.TitleDelim))
			{
				to = attribStart == -1 || suffixStart == -1 ? Math.Max(attribStart, suffixStart) : Math.Min(attribStart, suffixStart);

				if (to != -1)
				{
					// If the delimiter appeared, skip it. Otherwise, keep the char we started on
					name.Title = new string(str[(i > 0 ? i + 1 : i)..to]);
				}
				else
				{
					name.Title = new string(str[(i > 0 ? i + 1 : i)..]);
					name.Attributes = null;
					return true;
				}
			}

			if (attribStart != -1)
			{
				// Attributes MUST be enclosed in the start/end chars
				to = attribEnd;
				if (to != -1)
				{
					// The suffix has to begin immediately after the attributes do. The reason for this is a round trip will remove those spaces
					if (suffixStart != -1 && (suffixStart - to != 1))
					{
						return false;
					}
					if (TAttributes.TryParse(str[(attribStart + 1)..to], rules, out Attributes? attributes))
					{
						name.Attributes = attributes;
					}
					else
					{
						return false;
					}
				}
				else
				{
					return false;
				}
			}
			else
			{
				name.Attributes = null;
			}

			if (suffixStart != -1)
			{
				name.Suffix = new string(str[suffixStart..]);
			}
			return true;
		}
		/// <summary>
		/// Returns the index of the first character found which is not a latin digit.
		/// </summary>
		/// <param name="s">The string to search</param>
		/// <param name="offset">First index to start searching from</param>
		public static int IndexOfNonDigit(in ReadOnlySpan<char> s, int offset)
		{
			// TODO just use slice and indexof
			for (int i = offset; i < s.Length; i++)
			{
				// Find the first char not between
				if (!(s[i] >= '0' && s[i] <= '9'))
				{
					return i;
				}
			}
			return -1;
		}
		/// <summary>
		/// Finds the first occurrence of <paramref name="c"/> in <paramref name="s"/>, starting search from <paramref name="offset"/>.
		/// </summary>
		/// <param name="s">The span to search</param>
		/// <param name="c">The character to find</param>
		/// <param name="offset">The first index at which to begin the search</param>
		/// <returns>The index of the character, or -1 if not found</returns>
		public static int IndexOf(this in ReadOnlySpan<char> s, char c, int offset)
		{
			for (int i = offset; i < s.Length; i++)
			{
				if (s[i] == c)
				{
					return i;
				}
			}
			return -1;
		}
		public static bool IsEmptyOrWhiteSpace(in ReadOnlySpan<char> s)
		{
			return s.IsEmpty || s.IsWhiteSpace();
		}
		public static void ForEachToken(in ReadOnlySpan<char> str, char separator, SpanString token)
		{
			int from;
			int to = -1;
			bool go = true;
			while (go)
			{
				from = to + 1;
				to = str.IndexOf(separator, from);
				// Last one
				if (to != -1)
				{
					token(str[from..to], ref go);
				}
				else
				{
					token(str.Slice(from), ref go);
					break;
				}
			}
		}
	}
}
