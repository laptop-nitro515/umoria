#!/bin/sh
# Regenerates the C# data tables in Assets/Umoria/Core/Data/Generated from the
# original C++ tables in src/data_*.cpp.
set -e
here="$(cd "$(dirname "$0")" && pwd)"
src="$here/../../src"
out="$here/../Assets/Umoria/Core/Data/Generated"
bin="$(mktemp -d)/export_tables"
mkdir -p "$out"
c++ -std=c++14 -w -I"$src" -o "$bin" "$here/export_tables.cpp" \
    "$src"/data_creatures.cpp "$src"/data_player.cpp "$src"/data_recall.cpp \
    "$src"/data_store_owners.cpp "$src"/data_stores.cpp "$src"/data_tables.cpp \
    "$src"/data_treasure.cpp "$src"/config.cpp
"$bin" "$out"
echo "Wrote C# tables to $out"
