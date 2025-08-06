using System;
using System.Collections.Generic;

namespace Autocomplete
{
    public class LeftBorderTask
    {
        public static int GetLeftBorderIndex(IReadOnlyList<string> Phrases, string Prefix, int Left, int Right)
        {
            if (Right - Left <= 1) return Left;

            var Middle = Left + (Right - Left) / 2;

            if (string.Compare(Prefix, Phrases[Middle], StringComparison.OrdinalIgnoreCase) > 0)
                return GetLeftBorderIndex(Phrases, Prefix, Middle, Right);
            else
                return GetLeftBorderIndex(Phrases, Prefix, Left, Middle);
        }
    }
}