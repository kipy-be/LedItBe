using System;
using System.Collections.Generic;

namespace LedItBe.Core.Devices
{
    public enum LedOperationMode
    {
        Unknown,
        Off,
        Color,
        Demo,
        Effect,
        Movie,
        Playlist,
        Rt
    }

    internal static class LedOperationModes
    {
        private static Dictionary<string, LedOperationMode> _modesByString = new();
        private static Dictionary<LedOperationMode, string> _stringsByModes = new();

        static LedOperationModes()
        {
            string name;
            foreach(LedOperationMode lom in Enum.GetValues(typeof(LedOperationMode)))
            {
                name = lom.ToString().ToLower();
                _modesByString.Add(name, lom);
                _stringsByModes.Add(lom, name);
            }
        }

        public static LedOperationMode GetModeFromString(string str)
        {
            if (_modesByString.ContainsKey(str))
            {
                return _modesByString[str];
            }

            return LedOperationMode.Unknown;
        }

        public static string GetStringFromMode(LedOperationMode mode)
        {
            if (_stringsByModes.ContainsKey(mode))
            {
                return _stringsByModes[mode];
            }

            return "?";
        }
    }
}
