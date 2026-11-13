# Umoria for Unity

A port of [Umoria](../README.md) to Unity and C#. The goal is a faithful port of
the original 5.7 gameplay first, drawn as a coloured ASCII grid, with an optional
sprite tile mode added once the game is fully playable.

This is a work in progress. See the milestone list below for what exists so far.

## Layout

```
unity/
  Assets/Umoria/Core/     Game logic in plain C#, no UnityEngine references
    Data/                 Data types, plus Generated/ tables exported from src/data_*.cpp
    Random/               Port of the RNG and dice (rng.cpp, dice.cpp, game.cpp)
    Util/                 Port of helpers.cpp
  Dotnet/                 .NET SDK projects to build and test Core outside Unity
  tools/                  Exporters that run the original C++ to produce C# tables and test data
```

`Core` has its own assembly definition with `noEngineReferences`, so it compiles
both inside Unity and with the plain .NET SDK.

## Building and testing Core without Unity

Requires the .NET 8 SDK.

```sh
cd unity
dotnet test Dotnet/Umoria.Core.Tests
```

The tests check that the C# random number generator, dice and normal distribution
produce exactly the same values as the original C++ for fixed seeds, which is what
lets later milestones compare seeded dungeon generation against the C build.

## Regenerating from the original C++

The data tables and the golden test values are generated from the original source,
not hand-copied. Both scripts need a C++ compiler; `export_golden.sh` also needs the
ncurses development headers.

```sh
unity/tools/export_tables.sh   # src/data_*.cpp -> Assets/Umoria/Core/Data/Generated/*.cs
unity/tools/export_golden.sh   # original RNG output -> Dotnet/Umoria.Core.Tests/GoldenValues.cs
```

## Milestones

1. Scaffold: Unity project, terminal-style glyph renderer, input queue, game thread
2. **Foundations: RNG, dice, helpers, data tables (done)**
3. Character creation
4. Dungeon generation, line of sight, movement
5. Items and inventory
6. Monsters and combat
7. Magic
8. Town stores, save/load, high scores, monster recall
9. Polish: options, tile mode, sound, desktop builds, CI

## License

Umoria is licensed under the GNU General Public License v3.0 or later, and so is
this port. See [LICENSE](../LICENSE). The original copyright notices are kept on
every ported file.
