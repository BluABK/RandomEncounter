using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIGraphix
{
    public class ScreenChar : IScreenChar
    {
        private const char DefaultChar = ' ';
        public char Char { get; set; }
        public ConsoleColor BgColor { get; set; }
        public ConsoleColor FgColor { get; set; } 
        public ConsoleColor DefaultBgColor { get; set; }
        public ConsoleColor DefaultFgColor { get; set; }

        public ScreenChar(char c, ConsoleColor bgColor, ConsoleColor fgColor, ConsoleColor? defaultBgColor = null, ConsoleColor? defaultFgColor = null)
        {
            Char = c;

            DefaultBgColor = defaultBgColor ?? bgColor;
            DefaultFgColor = defaultFgColor ?? fgColor;

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
            Char = DefaultChar;
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

        public override string ToString()
        {
            return string.Empty + Char;
        }
        /// <summary>
        /// Takes a string and turns it into a ScreenChar[] of the same length.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="bgColor"></param>
        /// <param name="fgColor"></param>
        /// <param name="defaultBgColor"></param>
        /// <param name="defaultFgColor"></param>
        /// <returns></returns>
        public static ScreenChar[] FromString(string s, ConsoleColor bgColor, ConsoleColor fgColor, ConsoleColor? defaultBgColor = null, ConsoleColor? defaultFgColor = null)
        {
            ScreenChar[] arr = new ScreenChar[s.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new ScreenChar(s.ElementAt(i), bgColor, fgColor, defaultBgColor, defaultFgColor);
            }

            return arr;
        }
    }
}
