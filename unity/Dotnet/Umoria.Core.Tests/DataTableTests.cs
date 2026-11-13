// SPDX-License-Identifier: GPL-3.0-or-later
//
// Sanity checks on the generated data tables: sizes match the C constants and
// a few spot values match the original source.

using NUnit.Framework;
using Umoria.Core.Data;

namespace Umoria.Core.Tests
{
    public class DataTableTests
    {
        [Test]
        public void TableSizesMatchOriginalConstants()
        {
            Assert.That(CreatureData.Creatures.Length, Is.EqualTo(279));      // MON_MAX_CREATURES
            Assert.That(CreatureData.MonsterAttacks.Length, Is.EqualTo(215)); // MON_ATTACK_TYPES
            Assert.That(TreasureData.GameObjects.Length, Is.EqualTo(420));    // MAX_OBJECTS_IN_GAME
            Assert.That(TreasureData.SpecialItemNames.Length, Is.EqualTo(56));
            Assert.That(PlayerData.Races.Length, Is.EqualTo(8));
            Assert.That(PlayerData.Classes.Length, Is.EqualTo(6));
            Assert.That(PlayerData.Backgrounds.Length, Is.EqualTo(128));
            Assert.That(PlayerData.ClassRankTitles.Length, Is.EqualTo(6));
            Assert.That(PlayerData.ClassRankTitles[0].Length, Is.EqualTo(40));
            Assert.That(PlayerData.MagicSpells.Length, Is.EqualTo(5));
            Assert.That(PlayerData.MagicSpells[0].Length, Is.EqualTo(31));
            Assert.That(PlayerData.SpellNames.Length, Is.EqualTo(62));
            Assert.That(StoreData.StoreOwners.Length, Is.EqualTo(18));
            Assert.That(StoreData.StoreChoices.Length, Is.EqualTo(6));
            Assert.That(StoreData.StoreChoices[0].Length, Is.EqualTo(26));
            Assert.That(TableData.NormalTable.Length, Is.EqualTo(256));
            Assert.That(TableData.Colors.Length, Is.EqualTo(49));
            Assert.That(TableData.Syllables.Length, Is.EqualTo(153));
        }

        [Test]
        public void SpotValuesMatchOriginalSource()
        {
            var urchin = CreatureData.Creatures[0];
            Assert.That(urchin.Name, Is.EqualTo("Filthy Street Urchin"));
            Assert.That(urchin.Movement, Is.EqualTo(0x0012000Au));
            Assert.That(urchin.Sprite, Is.EqualTo((byte) 'p'));
            Assert.That(urchin.HitDie.Count, Is.EqualTo(1));
            Assert.That(urchin.HitDie.Sides, Is.EqualTo(4));
            Assert.That(urchin.Damage, Is.EqualTo(new byte[] { 72, 148, 0, 0 }));

            Assert.That(CreatureData.Creatures[278].Name, Is.EqualTo("Balrog"));

            var poison = TreasureData.GameObjects[0];
            Assert.That(poison.Name, Is.EqualTo("Poison"));
            Assert.That(poison.Sprite, Is.EqualTo((byte) ','));
            Assert.That(poison.Cost, Is.EqualTo(0));

            Assert.That(TreasureData.SpecialItemNames[0], Is.Null);
            Assert.That(PlayerData.Races[0].Name, Is.EqualTo("Human"));
            Assert.That(PlayerData.Classes[5].Title, Is.EqualTo("Paladin"));
            Assert.That(PlayerData.ClassRankTitles[0][0], Is.EqualTo("Rookie"));
        }
    }
}
