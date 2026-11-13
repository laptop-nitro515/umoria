// Copyright (c) 1981-86 Robert A. Koeneke
// Copyright (c) 1987-94 James E. Wilson
//
// SPDX-License-Identifier: GPL-3.0-or-later
//
// C# versions of the base data structs used by the static game tables.
// Field order matches the original C structs so the generated tables read the same.

namespace Umoria.Core.Data
{
    /// <summary>Dice_t: an XdY roll such as 2d6.</summary>
    public readonly struct Dice
    {
        public readonly byte Count;
        public readonly byte Sides;

        public Dice(byte count, byte sides)
        {
            Count = count;
            Sides = sides;
        }

        public override string ToString() => Count + "d" + Sides;
    }

    /// <summary>Creature_t: the base definition of a monster type (Kobold, Orc, ...).</summary>
    public sealed class Creature
    {
        public readonly string Name;
        public readonly uint Movement;      // CM_* bit field
        public readonly uint Spells;        // CS_* bit field
        public readonly ushort Defenses;    // CD_* bit field
        public readonly ushort KillExpValue;
        public readonly byte SleepCounter;  // Inactive counter / 10
        public readonly byte AreaAffectRadius;
        public readonly byte Ac;
        public readonly byte Speed;         // Movement speed + 10
        public readonly byte Sprite;        // Display character
        public readonly Dice HitDie;
        public readonly byte[] Damage;      // Indexes into CreatureData.MonsterAttacks
        public readonly byte Level;

        public Creature(string name, uint movement, uint spells, ushort defenses, ushort killExpValue, byte sleepCounter,
            byte areaAffectRadius, byte ac, byte speed, byte sprite, Dice hitDie, byte[] damage, byte level)
        {
            Name = name;
            Movement = movement;
            Spells = spells;
            Defenses = defenses;
            KillExpValue = killExpValue;
            SleepCounter = sleepCounter;
            AreaAffectRadius = areaAffectRadius;
            Ac = ac;
            Speed = speed;
            Sprite = sprite;
            HitDie = hitDie;
            Damage = damage;
            Level = level;
        }
    }

    /// <summary>MonsterAttack_t: an attack type, its description and damage dice.</summary>
    public readonly struct MonsterAttack
    {
        public readonly byte TypeId;
        public readonly byte DescriptionId;
        public readonly Dice Dice;

        public MonsterAttack(byte typeId, byte descriptionId, Dice dice)
        {
            TypeId = typeId;
            DescriptionId = descriptionId;
            Dice = dice;
        }
    }

    /// <summary>DungeonObject_t: the base definition of an item type.</summary>
    public sealed class DungeonObject
    {
        public readonly string Name;
        public readonly uint Flags;
        public readonly byte CategoryId;    // tval
        public readonly byte Sprite;        // tchar
        public readonly short MiscUse;      // p1
        public readonly int Cost;
        public readonly byte SubCategoryId; // subval
        public readonly byte ItemsCount;
        public readonly ushort Weight;
        public readonly short ToHit;
        public readonly short ToDamage;
        public readonly short Ac;
        public readonly short ToAc;
        public readonly Dice Damage;
        public readonly byte DepthFirstFound;

        public DungeonObject(string name, uint flags, byte categoryId, byte sprite, short miscUse, int cost, byte subCategoryId,
            byte itemsCount, ushort weight, short toHit, short toDamage, short ac, short toAc, Dice damage, byte depthFirstFound)
        {
            Name = name;
            Flags = flags;
            CategoryId = categoryId;
            Sprite = sprite;
            MiscUse = miscUse;
            Cost = cost;
            SubCategoryId = subCategoryId;
            ItemsCount = itemsCount;
            Weight = weight;
            ToHit = toHit;
            ToDamage = toDamage;
            Ac = ac;
            ToAc = toAc;
            Damage = damage;
            DepthFirstFound = depthFirstFound;
        }
    }

    /// <summary>Race_t: a playable race.</summary>
    public sealed class Race
    {
        public readonly string Name;
        public readonly short StrAdjustment, IntAdjustment, WisAdjustment, DexAdjustment, ConAdjustment, ChrAdjustment;
        public readonly byte BaseAge, MaxAge;
        public readonly byte MaleHeightBase, MaleHeightMod, MaleWeightBase, MaleWeightMod;
        public readonly byte FemaleHeightBase, FemaleHeightMod, FemaleWeightBase, FemaleWeightMod;
        public readonly short DisarmChanceBase, SearchChanceBase, Stealth, Fos, BaseToHit, BaseToHitBows, SavingThrowBase;
        public readonly byte HitPointsBase, InfraVision, ExpFactorBase, ClassesBitField;

        public Race(string name, short str, short intel, short wis, short dex, short con, short chr,
            byte baseAge, byte maxAge, byte maleHeightBase, byte maleHeightMod, byte maleWeightBase, byte maleWeightMod,
            byte femaleHeightBase, byte femaleHeightMod, byte femaleWeightBase, byte femaleWeightMod,
            short disarmChanceBase, short searchChanceBase, short stealth, short fos, short baseToHit, short baseToHitBows,
            short savingThrowBase, byte hitPointsBase, byte infraVision, byte expFactorBase, byte classesBitField)
        {
            Name = name;
            StrAdjustment = str;
            IntAdjustment = intel;
            WisAdjustment = wis;
            DexAdjustment = dex;
            ConAdjustment = con;
            ChrAdjustment = chr;
            BaseAge = baseAge;
            MaxAge = maxAge;
            MaleHeightBase = maleHeightBase;
            MaleHeightMod = maleHeightMod;
            MaleWeightBase = maleWeightBase;
            MaleWeightMod = maleWeightMod;
            FemaleHeightBase = femaleHeightBase;
            FemaleHeightMod = femaleHeightMod;
            FemaleWeightBase = femaleWeightBase;
            FemaleWeightMod = femaleWeightMod;
            DisarmChanceBase = disarmChanceBase;
            SearchChanceBase = searchChanceBase;
            Stealth = stealth;
            Fos = fos;
            BaseToHit = baseToHit;
            BaseToHitBows = baseToHitBows;
            SavingThrowBase = savingThrowBase;
            HitPointsBase = hitPointsBase;
            InfraVision = infraVision;
            ExpFactorBase = expFactorBase;
            ClassesBitField = classesBitField;
        }
    }

    /// <summary>Class_t: a playable class.</summary>
    public sealed class Class
    {
        public readonly string Title;
        public readonly byte HitPoints, DisarmTraps, Searching, Stealth, Fos, BaseToHit, BaseToHitWithBows, SavingThrow;
        public readonly short Strength, Intelligence, Wisdom, Dexterity, Constitution, Charisma;
        public readonly byte ClassToUseMageSpells; // SPELL_TYPE_NONE / MAGE / PRIEST
        public readonly byte ExperienceFactor;
        public readonly byte MinLevelForSpellCasting;

        public Class(string title, byte hitPoints, byte disarmTraps, byte searching, byte stealth, byte fos, byte baseToHit,
            byte baseToHitWithBows, byte savingThrow, short strength, short intelligence, short wisdom, short dexterity,
            short constitution, short charisma, byte classToUseMageSpells, byte experienceFactor, byte minLevelForSpellCasting)
        {
            Title = title;
            HitPoints = hitPoints;
            DisarmTraps = disarmTraps;
            Searching = searching;
            Stealth = stealth;
            Fos = fos;
            BaseToHit = baseToHit;
            BaseToHitWithBows = baseToHitWithBows;
            SavingThrow = savingThrow;
            Strength = strength;
            Intelligence = intelligence;
            Wisdom = wisdom;
            Dexterity = dexterity;
            Constitution = constitution;
            Charisma = charisma;
            ClassToUseMageSpells = classToUseMageSpells;
            ExperienceFactor = experienceFactor;
            MinLevelForSpellCasting = minLevelForSpellCasting;
        }
    }

    /// <summary>Background_t: one line of the character history generator.</summary>
    public readonly struct Background
    {
        public readonly string Info;
        public readonly byte Roll;
        public readonly byte Chart;
        public readonly byte Next;
        public readonly byte Bonus;

        public Background(string info, byte roll, byte chart, byte next, byte bonus)
        {
            Info = info;
            Roll = roll;
            Chart = chart;
            Next = next;
            Bonus = bonus;
        }
    }

    /// <summary>Spell_t: requirements for a mage spell or priest prayer.</summary>
    public readonly struct Spell
    {
        public readonly byte LevelRequired;
        public readonly byte ManaRequired;
        public readonly byte FailureChance;
        public readonly byte ExpGainForLearning;

        public Spell(byte levelRequired, byte manaRequired, byte failureChance, byte expGainForLearning)
        {
            LevelRequired = levelRequired;
            ManaRequired = manaRequired;
            FailureChance = failureChance;
            ExpGainForLearning = expGainForLearning;
        }
    }

    /// <summary>Owner_t: a store owner and their haggling personality.</summary>
    public sealed class Owner
    {
        public readonly string Name;
        public readonly short MaxCost;
        public readonly byte MaxInflate;
        public readonly byte MinInflate;
        public readonly byte HagglesPer;
        public readonly byte Race;
        public readonly byte MaxInsults;

        public Owner(string name, short maxCost, byte maxInflate, byte minInflate, byte hagglesPer, byte race, byte maxInsults)
        {
            Name = name;
            MaxCost = maxCost;
            MaxInflate = maxInflate;
            MinInflate = minInflate;
            HagglesPer = hagglesPer;
            Race = race;
            MaxInsults = maxInsults;
        }
    }
}
