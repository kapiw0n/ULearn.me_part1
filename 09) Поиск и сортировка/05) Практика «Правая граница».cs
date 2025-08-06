using System;
using System.Collections.Generic;

namespace Autocomplete
{
    public class RightBorderTask
    {
        public static int GetRightBorderIndex(IReadOnlyList<string> Phrases, string Prefix, int Left, int Right)
        {
            while (Right - Left > 1)
            {
                var Middle = Left + (Right - Left) / 2;

                if (string.Compare(Prefix, Phrases[Middle], StringComparison.OrdinalIgnoreCase) >= 0 ||
                    Phrases[Middle].StartsWith(Prefix, StringComparison.OrdinalIgnoreCase))
                    Left = Middle;
                else
                    Right = Middle;
            }
            return Right;
        }
    }
}