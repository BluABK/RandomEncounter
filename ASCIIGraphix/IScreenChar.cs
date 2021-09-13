using System;

namespace ASCIIGraphix
{
    public interface IScreenChar
    {
        public char Char { get; set; }
        public ConsoleColor DefaultBgColor { get; set; }
        public ConsoleColor DefaultFgColor { get; set; }
        public ConsoleColor BgColor { get; set; }
        public ConsoleColor FgColor { get; set; }
        public void ResetColors();
        public void Draw();
    }
}