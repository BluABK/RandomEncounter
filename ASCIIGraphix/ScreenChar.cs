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

        public ScreenChar(char c, ConsoleColor bgColor, ConsoleColor fgColor)
        {
            Char = c;
            BgColor = bgColor;
            FgColor = fgColor;
        }

        public void Draw()
        {
            Console.ForegroundColor = FgColor;
            Console.BackgroundColor = BgColor;
            Console.Write(Char);
        }
        
    }
}
