// Copyright (c) 1981-86 Robert A. Koeneke
// Copyright (c) 1987-94 James E. Wilson
//
// SPDX-License-Identifier: GPL-3.0-or-later
//
// Port of src/helpers.cpp: generic string and bit helpers.

using System;

namespace Umoria.Core.Util
{
    public static class Helpers
    {
        /// <summary>Returns the position of the lowest set bit and clears it, or -1 if none. -RAK-</summary>
        public static int GetAndClearFirstBit(ref uint flag)
        {
            uint mask = 0x1;

            for (var i = 0; i < 32; i++)
            {
                if ((flag & mask) != 0u)
                {
                    flag &= ~mask;
                    return i;
                }

                mask <<= 1;
            }

            return -1;
        }

        /// <summary>
        /// insertNumberIntoString(): replaces the first occurrence of <paramref name="from"/> with <paramref name="number"/>.
        /// </summary>
        public static string InsertNumberIntoString(string to, string from, int number, bool showSign)
        {
            var index = to.IndexOf(from, StringComparison.Ordinal);
            if (index < 0)
            {
                return to;
            }

            var formatted = number >= 0 && showSign ? "+" + number : number.ToString();
            return to.Substring(0, index) + formatted + to.Substring(index + from.Length);
        }

        /// <summary>insertStringIntoString(): replaces the first occurrence of <paramref name="from"/>.</summary>
        public static string InsertStringIntoString(string to, string from, string insert)
        {
            var index = to.IndexOf(from, StringComparison.Ordinal);
            if (index < 0)
            {
                return to;
            }

            return to.Substring(0, index) + (insert ?? string.Empty) + to.Substring(index + from.Length);
        }

        public static bool IsVowel(char ch)
        {
            switch (ch)
            {
                case 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'u':
                case 'A':
                case 'E':
                case 'I':
                case 'O':
                case 'U':
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>stringToNumber(): strict base-10 parse, no trailing garbage.</summary>
        public static bool StringToNumber(string str, out int number)
        {
            return int.TryParse(str, System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowLeadingWhite,
                System.Globalization.CultureInfo.InvariantCulture, out number);
        }

        public static uint GetCurrentUnixTime()
        {
            return (uint) DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        /// <summary>humanDateString(): e.g. "Fri Nov 13".</summary>
        public static string HumanDateString()
        {
            var now = DateTime.Now;
            return now.ToString("ddd MMM ", System.Globalization.CultureInfo.InvariantCulture) + now.Day.ToString().PadLeft(2);
        }
    }
}
