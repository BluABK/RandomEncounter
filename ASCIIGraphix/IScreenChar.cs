using System;

namespace ASCIIGraphix
{
    public interface IScreenChar
    {
        public char Char { get; set; }
        public ConsoleColor BgColor { get; set; }
        public ConsoleColor FgColor { get; set; }

        public void Draw();
    }
}