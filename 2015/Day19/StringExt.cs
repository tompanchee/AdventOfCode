using System;
using System.Collections.Generic;
using System.Text;

namespace Day19
{
    static class StringExt
    {
        public static IEnumerable<int> AllIndexesOf(this string haystack, string needle) {
            for (var i = 0;; i += needle.Length) {
                i = haystack.IndexOf(needle, i, StringComparison.InvariantCulture);
                if (i < 0) {
                    break;
                }

                yield return i;
            }
        }

        public static string ReplaceAtIndex(this string str, string newSubstring, int index, int length) {
            var builder = new StringBuilder(str[..index]);
            builder.Append(newSubstring);
            builder.Append(str[(index + length)..]);
            return builder.ToString();
        }
    }
}