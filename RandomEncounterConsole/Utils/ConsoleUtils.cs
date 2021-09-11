using System;
using System.Collections.Generic;
using System.Linq;
using RandomEncounter;

namespace RandomEncounterConsole.Utils
{
    static class ConsoleUtils
    {
        public static Dictionary<EType, ConsoleColor> TypeConsoleColor = new() { 
            { EType.Normal, ConsoleColor.White },
            { EType.Fire, ConsoleColor.Red },
            { EType.Fighting, ConsoleColor.DarkRed },
            { EType.Water, ConsoleColor.Blue },
            { EType.Flying, ConsoleColor.DarkCyan },
            { EType.Grass, ConsoleColor.DarkGreen },
            { EType.Poison, ConsoleColor.Magenta },
            { EType.Electric, ConsoleColor.DarkYellow },
            { EType.Ground, ConsoleColor.Yellow },
            { EType.Psychic, ConsoleColor.White},  // FIXME: Unset
            { EType.Rock, ConsoleColor.White }, // FIXME: Unset
            { EType.Ice, ConsoleColor.Cyan },
            { EType.Bug, ConsoleColor.Green },
            { EType.Dragon, ConsoleColor.DarkBlue },
            { EType.Ghost, ConsoleColor.DarkMagenta },
            { EType.Dark, ConsoleColor.White }, // FIXME: Should be Black bg with white fg
            { EType.Steel, ConsoleColor.DarkCyan },
            { EType.Fairy, ConsoleColor.White } // FIXME: Unset
        };

        public static IEnumerable<T> GetEnumValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static void PrintTypeColor(CreatureType type, string msg)
        {
            Console.ForegroundColor = TypeConsoleColor[type.Id];
            Console.Write($"{msg}");
            Console.ResetColor();
        }

        public static void TestColors()
        {
            foreach (var entry in GetEnumValues<ConsoleColor>())
            {
                Console.ForegroundColor = entry;
                Console.WriteLine($"{entry}");
                Console.ResetColor();
            }
        }
    }
}
