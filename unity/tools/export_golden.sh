#!/bin/sh
# Regenerates unity/Dotnet/Umoria.Core.Tests/GoldenValues.cs by running the original C++
# random number code. Needs a C++ compiler and the ncurses development headers.
set -e
here="$(cd "$(dirname "$0")" && pwd)"
src="$here/../../src"
bin="$(mktemp -d)/export_golden"
srcs=$(ls "$src"/*.cpp | grep -v '/main.cpp$')
c++ -std=c++14 -w -I"$src" -o "$bin" "$here/export_golden.cpp" $srcs -lncurses
"$bin" "$here/../Dotnet/Umoria.Core.Tests/GoldenValues.cs"
echo "Wrote $here/../Dotnet/Umoria.Core.Tests/GoldenValues.cs"
