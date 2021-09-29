using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIGraphix
{
    public class ScreenChar : IScreenChar
    {
        private static readonly char defaultChar = ' ';
        public char Char { get; set; } = defaultChar;
        public ConsoleColor BgColor { get; set; }
        public ConsoleColor FgColor { get; set; } 
        public ConsoleColor DefaultBgColor { get; set; }
        public ConsoleColor DefaultFgColor { get; set; }

        public ScreenChar(char c, ConsoleColor bgColor, ConsoleColor fgColor, ConsoleColor? defaultBgColor = null, ConsoleColor? defaultFgColor = null)
        {
            Char = c;

            DefaultBgColor = defaultBgColor != null ? (ConsoleColor)defaultBgColor : bgColor;
            DefaultFgColor = defaultFgColor != null ? (ConsoleColor)defaultFgColor : fgColor;

            BgColor = bgColor;
            FgColor = fgColor;
        }

        public void ResetColors()
        {
            Console.BackgroundColor = DefaultBgColor;
            Console.ForegroundColor = DefaultFgColor;
        }

        public void Draw()
        {
            Console.ForegroundColor = FgColor;
            Console.BackgroundColor = BgColor;
            Console.Write(Char);
        }

        /// <summary>
        ///     Reset Char back to the default char (whitespace).
        /// </summary>
        public void Clear()
        {
            Char = defaultChar;
        }

        public bool IsEqualTo(ScreenChar sc)
        {
            return (Char == sc.Char) 
                && (BgColor == sc.BgColor) 
                && (FgColor == sc.FgColor);
        }

        public bool IsEqualToChar(char c)
        {
            return Char == c;
        }

        /// <summary>
        /// Returns a cloned copy of this ScreenChar.
        /// </summary>
        /// <returns>A cloned copy of itself.</returns>
        public ScreenChar Clone()
        {
            return new ScreenChar(Char, BgColor, FgColor, DefaultBgColor, DefaultFgColor);
        }
    }
}
