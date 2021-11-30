using System;

namespace LedItBe.Core.Utils.Extensions
{
    internal static class NumericExtensions
    {
        public static int RestrictTo(this int value, int min, int max) => Math.Max(Math.Min(value, max), min);
    }
}
