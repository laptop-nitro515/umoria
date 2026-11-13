// Copyright (c) 1981-86 Robert A. Koeneke
// Copyright (c) 1987-94 James E. Wilson
//
// SPDX-License-Identifier: GPL-3.0-or-later
//
// Port of src/rng.cpp and the random helpers in src/game.cpp.
//
// Park and Miller's "minimal standard" Lehmer generator, using Schrage's method.
// Kept as global state, like the original, because the game swaps seeds to make
// town layout and item flavours reproducible. Results must stay identical to
// the C version so seeded dungeons match.

namespace Umoria.Core.Random
{
    public static class Rng
    {
        private const int M = int.MaxValue; // 2^31 - 1
        private const int A = 16807;
        private const int Q = M / A;        // 127773
        private const int R = M % A;        // 2836

        private static uint seed;
        private static uint oldSeed;

        public static uint GetSeed() => seed;

        /// <summary>Sets the seed to a value between 1 and M - 1.</summary>
        public static void SetSeed(uint value)
        {
            seed = (uint) ((value % (M - 1)) + 1);
        }

        /// <summary>Returns a pseudo-random number from 1 .. 2^31 - 2.</summary>
        public static int Next()
        {
            var high = (int) (seed / Q);
            var low = (int) (seed % Q);
            var test = A * low - R * high;

            seed = test > 0 ? (uint) test : (uint) (test + M);

            return (int) seed;
        }

        /// <summary>seedsInitialize(): derive the magic and town seeds and seed the generator.</summary>
        /// <param name="value">Starting seed, or 0 to use the current time.</param>
        /// <param name="magicSeed">Seed for item flavours.</param>
        /// <param name="townSeed">Seed for town generation.</param>
        public static void InitializeSeeds(uint value, out uint magicSeed, out uint townSeed)
        {
            var clock = value == 0 ? Util.Helpers.GetCurrentUnixTime() : value;

            magicSeed = clock;

            clock += 8762;
            townSeed = clock;

            clock += 113452;
            SetSeed(clock);

            // make it a little more random
            for (clock = (uint) RandomNumber(100); clock != 0; clock--)
            {
                Next();
            }
        }

        /// <summary>seedSet(): switch to a reproducible state, remembering the current one.</summary>
        public static void PushSeed(uint value)
        {
            oldSeed = seed;
            SetSeed(value);
        }

        /// <summary>seedResetToOldSeed(): restore the state saved by PushSeed.</summary>
        public static void PopSeed()
        {
            SetSeed(oldSeed);
        }

        /// <summary>Random integer where 1 &lt;= x &lt;= max. -RAK-</summary>
        public static int RandomNumber(int max)
        {
            return (Next() % max) + 1;
        }

        /// <summary>Random integer with a normal distribution. -RAK-</summary>
        public static int RandomNumberNormalDistribution(int mean, int standard)
        {
            var normalTable = Data.TableData.NormalTable;
            const int tableSize = 256;
            const int tableSd = 64;

            var tmp = RandomNumber(short.MaxValue);

            // off scale, assign random value between 4 and 5 times SD
            if (tmp == short.MaxValue)
            {
                var off = 4 * standard + RandomNumber(standard);

                // one half are negative
                if (RandomNumber(2) == 1)
                {
                    off = -off;
                }

                return mean + off;
            }

            // binary search normal table to get index that matches tmp,
            // this takes up to 8 iterations.
            var low = 0;
            var index = tableSize >> 1;
            var high = tableSize;

            while (true)
            {
                if (normalTable[index] == tmp || high == low + 1)
                {
                    break;
                }

                if (normalTable[index] > tmp)
                {
                    high = index;
                    index = low + ((index - low) >> 1);
                }
                else
                {
                    low = index;
                    index = index + ((high - index) >> 1);
                }
            }

            // might end up one below target, check that here
            if (normalTable[index] < tmp)
            {
                index = index + 1;
            }

            // normal table is based on SD of 64, so adjust the
            // index value here, round the half way case up.
            var offset = ((standard * index) + (tableSd >> 1)) / tableSd;

            // one half should be negative
            if (RandomNumber(2) == 1)
            {
                offset = -offset;
            }

            return mean + offset;
        }

        /// <summary>diceRoll(): sum of Count rolls of 1..Sides.</summary>
        public static int DiceRoll(Data.Dice dice)
        {
            var sum = 0;
            for (var i = 0; i < dice.Count; i++)
            {
                sum += RandomNumber(dice.Sides);
            }

            return sum;
        }

        /// <summary>maxDiceRoll(): the highest possible roll. -RAK-</summary>
        public static int MaxDiceRoll(Data.Dice dice)
        {
            return dice.Count * dice.Sides;
        }
    }
}
