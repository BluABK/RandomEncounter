using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIGraphix
{
    public class ScreenChar : IScreenChar
    {
        public char Char { get; set; }
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
        
    }
}
