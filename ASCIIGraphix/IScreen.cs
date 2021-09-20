using System;
using System.Collections.Generic;

namespace ASCIIGraphix
{
    public interface IScreen
    {
        public (int, int) CursorStartPosition { get; set; }
        public int Width { get; }
        public int Height { get; }
        public ScreenChar[,] Buffer { get; }
        public ScreenChar DefaultScreenChar { get; set; }
        public ConsoleColor BgColor { get; }
        public ConsoleColor FgColor { get; }
        public ConsoleColor DefaultBgColor { get; }
        public ConsoleColor DefaultFgColor { get; }
        public IConsoleWrapper ConsoleWrapper { get; }
        public List<GfxObject> Objects { get; }
        public void SetColors();
        public void ResetColorsToDefault();
        public void ResetCursorPosition();
        public void Draw();

    }
}