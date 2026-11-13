// SPDX-License-Identifier: GPL-3.0-or-later
//
// Checks the C# random number code reproduces the original C++ exactly.
// Expected values come from GoldenValues.cs (see unity/tools/export_golden.sh).

using NUnit.Framework;
using Umoria.Core.Data;
using Umoria.Core.Random;

namespace Umoria.Core.Tests
{
    public class RngTests
    {
        private static readonly long[][] RndExpected = { GoldenValues.Rnd0, GoldenValues.Rnd1, GoldenValues.Rnd2, GoldenValues.Rnd3 };
        private static readonly long[][] RandomExpected = { GoldenValues.RandomNumber100_0, GoldenValues.RandomNumber100_1, GoldenValues.RandomNumber100_2, GoldenValues.RandomNumber100_3 };
        private static readonly long[][] NormalExpected = { GoldenValues.Normal100_15_0, GoldenValues.Normal100_15_1, GoldenValues.Normal100_15_2, GoldenValues.Normal100_15_3 };
        private static readonly long[][] DiceExpected = { GoldenValues.Dice3d6_0, GoldenValues.Dice3d6_1, GoldenValues.Dice3d6_2, GoldenValues.Dice3d6_3 };
        private static readonly long[][] SeedsExpected = { GoldenValues.SeedsInitialize_0, GoldenValues.SeedsInitialize_1, GoldenValues.SeedsInitialize_2, GoldenValues.SeedsInitialize_3 };

        [Test]
        public void ParkMillerSelfCheck()
        {
            Rng.SetSeed(0);
            for (var i = 1; i < 10000; i++)
            {
                Rng.Next();
            }

            Assert.That(Rng.Next(), Is.EqualTo(1043618065));
            Assert.That(GoldenValues.Rnd10001, Is.EqualTo(1043618065));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void RawSequenceMatchesC(int s)
        {
            Rng.SetSeed((uint) GoldenValues.Seeds[s]);
            foreach (var expected in RndExpected[s])
            {
                Assert.That(Rng.Next(), Is.EqualTo(expected));
            }
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void RandomNumberMatchesC(int s)
        {
            Rng.SetSeed((uint) GoldenValues.Seeds[s]);
            foreach (var expected in RandomExpected[s])
            {
                Assert.That(Rng.RandomNumber(100), Is.EqualTo(expected));
            }
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void NormalDistributionMatchesC(int s)
        {
            Rng.SetSeed((uint) GoldenValues.Seeds[s]);
            foreach (var expected in NormalExpected[s])
            {
                Assert.That(Rng.RandomNumberNormalDistribution(100, 15), Is.EqualTo(expected));
            }
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void DiceRollMatchesC(int s)
        {
            Rng.SetSeed((uint) GoldenValues.Seeds[s]);
            foreach (var expected in DiceExpected[s])
            {
                Assert.That(Rng.DiceRoll(new Dice(3, 6)), Is.EqualTo(expected));
            }
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void SeedsInitializeMatchesC(int s)
        {
            Rng.InitializeSeeds((uint) GoldenValues.Seeds[s], out var magic, out var town);
            var expected = SeedsExpected[s];

            Assert.That((long) magic, Is.EqualTo(expected[0]));
            Assert.That((long) town, Is.EqualTo(expected[1]));
            for (var i = 2; i < expected.Length; i++)
            {
                Assert.That(Rng.Next(), Is.EqualTo(expected[i]));
            }
        }

        [Test]
        public void PushAndPopSeedRestoresState()
        {
            Rng.SetSeed(42);
            Rng.Next();
            var saved = Rng.GetSeed();

            Rng.PushSeed(999);
            Rng.Next();
            Rng.PopSeed();

            // Like the original, restoring goes back through setRandomSeed(),
            // so the result is the saved state normalised the same way.
            var restored = Rng.GetSeed();
            Rng.SetSeed(saved);
            Assert.That(restored, Is.EqualTo(Rng.GetSeed()));
        }
    }
}
