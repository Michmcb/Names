namespace MichMcb.Names
{
	using System;

	/// <summary>
	/// Helper methods to parse stuff
	/// </summary>
	public static class Parsing
	{
		/// <summary>
		/// Finds parts of <paramref name="str"/>, and validates that it is well formed.
		/// </summary>
		/// <param name="str">The string to find parts of.</param>
		/// <param name="rules">The rules to use.</param>
		/// <param name="title">The slice which represents the title.</param>
		/// <param name="attributes">The slice which represents the attributes, without the AttributeStart/AttrbiuteEnd chars.</param>
		/// <param name="suffix">The slice which represents the suffix, with leading SuffixDelimiter.</param>
		/// <returns>true if <paramref name="str"/> is well formed, false otherwise.</returns>
		public static bool FindParts(in ReadOnlySpan<char> str, NameRules rules, out Range title, out Range attributes, out Range suffix)
		{
			int attrStart = str.IndexOf(rules.AttributeStart);
			int attrEnd = str.LastIndexOf(rules.AttributeEnd);
			if (attrStart != -1 && attrEnd != -1)
			{
				if (attrStart < attrEnd)
				{
					// We have attributes, so look for the suffix AFTER attrEnd
					int suffixIndex = str[attrEnd..].LastIndexOf(rules.SuffixDelimiter);
					if (suffixIndex != -1)
					{
						// Have attributes and a suffix
						title = ..attrStart;
						attributes = (attrStart + 1)..attrEnd;
						// Because suffixIndex is relative to the slice we need to add attrEnd onto it so it's relative to input str
						suffix = (suffixIndex + attrEnd)..;
					}
					else
					{
						// Only attributes, no suffix
						title = ..attrStart;
						attributes = (attrStart + 1)..attrEnd;
						suffix = default;
					}
					return true;
				}
			}
			else
			{
				// Only a suffix maybe
				int suffixIndex = str.LastIndexOf(rules.SuffixDelimiter);
				if (suffixIndex != -1)
				{
					title = ..suffixIndex;
					suffix = suffixIndex..;
				}
				else
				{
					title = ..;
					suffix = default;
				}
				attributes = default;
				return true;
			}
			title = default;
			attributes = default;
			suffix = default;
			return false;
		}
		/// <summary>
		/// Parses a DateTime fragment, ordered year month day hour minute second, with the provided delimiters.
		/// Will accept any number of year/month/day/hour/minute/second, so long as they are in order.
		/// For example yyyy-MM is valid, and so is yyyy-MM-dd.
		/// The kind is set to <see cref="DateTimeKind.Local"/>.
		/// </summary>
		/// <param name="str">The string containing the DateTime fragment to parse</param>
		/// <param name="rules">The rules to use when parsing</param>
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
		/// Finds the first occurrence of <paramref name="c"/> in <paramref name="s"/>, starting search from <paramref name="offset"/>.
		/// </summary>
		/// <param name="s">The span to search</param>
		/// <param name="c">The character to find</param>
		/// <param name="offset">The first index at which to begin the search</param>
		/// <returns>The index of the character, or -1 if not found</returns>
		public static int IndexOf(this in ReadOnlySpan<char> s, char c, int offset)
		{
			int i = s[offset..].IndexOf(c);
			return i != -1 ? i + offset : -1;
		}
		/// <summary>
		/// Allows iteration over slices of <paramref name="str"/>, without allocating any substrings.
		/// </summary>
		/// <param name="str">The string to take slices of.</param>
		/// <param name="separator">The separator.</param>
		/// <param name="token">A callback to handle a slice.</param>
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
					token(str[from..], ref go);
					break;
				}
			}
		}
	}
}
