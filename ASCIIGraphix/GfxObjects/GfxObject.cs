using System;

namespace ASCIIGraphix.GfxObjects
{
    public class GfxObject
    {
        protected readonly IScreen MyScreen;
        protected ConsoleColor DefaultBgColor { get; set; }
        protected ConsoleColor DefaultFgColor { get; set; }
        protected ConsoleColor BgColor { get; set; }
        protected ConsoleColor FgColor { get; set; }

        public GfxObject(IScreen screen)
        {
            MyScreen = screen;
            DefaultBgColor = screen.DefaultBgColor;
            DefaultFgColor = screen.DefaultFgColor;
            BgColor = screen.BgColor;
            FgColor = screen.FgColor;
        }

        /// <summary>
        /// Turns a char c into string of n amount of c.
        ///
        /// Example:
        ///     CharToString('a', 3) == "aaa"
        /// </summary>
        /// <param name="c"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        protected static string CharToString(char c, int amount)
        {
            string s = string.Empty;

            for (int i = 0; i < amount; ++i)
            {
                s += c;
            }

            return s;
        }
    }
}