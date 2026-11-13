// SPDX-License-Identifier: GPL-3.0-or-later

using NUnit.Framework;
using Umoria.Core.Util;

namespace Umoria.Core.Tests
{
    public class HelpersTests
    {
        [Test]
        public void GetAndClearFirstBitReturnsLowestBit()
        {
            uint flags = 0b10100;
            Assert.That(Helpers.GetAndClearFirstBit(ref flags), Is.EqualTo(2));
            Assert.That(flags, Is.EqualTo(0b10000u));
            Assert.That(Helpers.GetAndClearFirstBit(ref flags), Is.EqualTo(4));
            Assert.That(Helpers.GetAndClearFirstBit(ref flags), Is.EqualTo(-1));
        }

        [Test]
        public void InsertNumberIntoStringReplacesFirstMatch()
        {
            Assert.That(Helpers.InsertNumberIntoString("a (%P1) b (%P1)", "%P1", 3, true), Is.EqualTo("a (+3) b (%P1)"));
            Assert.That(Helpers.InsertNumberIntoString("(%P1)", "%P1", -2, true), Is.EqualTo("(-2)"));
            Assert.That(Helpers.InsertNumberIntoString("(%P1)", "%P1", 5, false), Is.EqualTo("(5)"));
            Assert.That(Helpers.InsertNumberIntoString("none", "%P1", 5, false), Is.EqualTo("none"));
        }

        [Test]
        public void InsertStringIntoStringReplacesFirstMatch()
        {
            Assert.That(Helpers.InsertStringIntoString("& Potion~ of %s", "%s", "Speed"), Is.EqualTo("& Potion~ of Speed"));
            Assert.That(Helpers.InsertStringIntoString("abc", "x", "y"), Is.EqualTo("abc"));
        }

        [Test]
        public void StringToNumberRejectsGarbage()
        {
            Assert.That(Helpers.StringToNumber("42", out var n), Is.True);
            Assert.That(n, Is.EqualTo(42));
            Assert.That(Helpers.StringToNumber("-7", out n), Is.True);
            Assert.That(n, Is.EqualTo(-7));
            Assert.That(Helpers.StringToNumber("12abc", out _), Is.False);
        }

        [Test]
        public void IsVowel()
        {
            Assert.That(Helpers.IsVowel('a'), Is.True);
            Assert.That(Helpers.IsVowel('E'), Is.True);
            Assert.That(Helpers.IsVowel('k'), Is.False);
        }
    }
}
