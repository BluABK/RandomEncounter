using System;
using System.Collections.Generic;
using System.Threading;

namespace ASCIIGraphix
{
    public class Screen : IScreen
    {
        public (int, int) CursorStartPosition { get; set; }
        public int StartPositionTop { get; private set; }
        public int StartPositionLeft { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public ScreenChar[,] Buffer { get; private set; }
        public char DefaultChar = '█';
        public ScreenChar DefaultScreenChar { get; set; }
        public ConsoleColor BgColor { get; set; }
        public ConsoleColor FgColor { get; set; }
        public ConsoleColor DefaultBgColor { get; private set; }
        public ConsoleColor DefaultFgColor { get; private set; }

        public List<GfxObject> Objects { get; private set; }

        public Screen(int width, int height, ConsoleColor? bgColor = null, ConsoleColor? fgColor = null)
        {
            (int,int) CursorStartPosition = Console.GetCursorPosition();
            StartPositionTop = Console.CursorTop;
            StartPositionLeft = Console.CursorLeft;

            // Colors.
            DefaultBgColor = Console.BackgroundColor;
            DefaultFgColor = Console.ForegroundColor;

            BgColor = bgColor != null ? (ConsoleColor)bgColor : DefaultBgColor;
            FgColor = fgColor != null ? (ConsoleColor)fgColor : DefaultBgColor;

            // Use bgcolor as both foreground and background due to wanting placeholder to fill entire char area in mono-color.
            DefaultScreenChar = new ScreenChar(DefaultChar, BgColor, BgColor);

            // Dimensions.
            Width = width;
            Height = height;

            // Create chars coord system.
            Buffer = new ScreenChar[width, height];

            // Populate chars.
            //Array.Fill<char[,]>(Chars, PlaceholderChar);
            FillChars(DefaultScreenChar);
        }
        public void SetColors()
        {
            Console.BackgroundColor = BgColor;
            Console.ForegroundColor = FgColor;
        }

        public void ResetColorsToDefault()
        {
            Console.BackgroundColor = DefaultBgColor;
            Console.ForegroundColor = DefaultFgColor;
        }

        public void ResetColors()
        {
            Console.BackgroundColor = BgColor;
            Console.ForegroundColor = FgColor;
        }

        public void FillChars(ScreenChar sc)
        {
            for (int i = 0; i < Buffer.GetLength(0); i++)
            {
                for (int j = 0; j < Buffer.GetLength(1); j++)
                {
                    Buffer[i, j] = sc;
                }
            }
        }

        private void WriteAt(char c, int x, int y)
        {
            int computedX = StartPositionLeft + x;
            int computedY = StartPositionTop + y;

            if (x > Width || y > Height) throw new ArgumentException($"Was instructed to write at coord [{computedX},{computedY}], but that is OOB! (Bounds: [{Width}{Height}])");

            try
            {
                Console.SetCursorPosition(computedX, computedY);
                Console.Write(c);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        public void Draw()
        {
            ResetCursorPosition();

            for (int i = 0; i < Height; i++)
            {
                SetColors();
                for (int j = 0; j < Width; j++)
                {
                    //Console.Write(Chars[i, j]);
                    //Console.Write(j);
                    //Console.Write(DefaultScreenChar.Char);
                    DefaultScreenChar.Draw();

                    //WriteAt(Buffer[i, j].Char, i, j);
                }

                // Fix issue with bgcolor freaking out on resize by printing a single char of default bg color, to make it look invisible.
                ResetColorsToDefault();
                Console.ForegroundColor = DefaultBgColor;
                Console.Write('█'); 
                
                // Reset back to defaults.
                ResetColorsToDefault();
                
                // Force newline
                Console.WriteLine();
            }

        }

        public void Redraw()
        {
            Console.Clear();
            Draw();
        }

        public void ResetCursorPosition()
        {
            Console.SetCursorPosition(CursorStartPosition.Item1, CursorStartPosition.Item2);
            //for (int i = 0; i < Width * Height; i++)
            //{
            //    Console.Write('\b');
            //}
        }

        /// <summary>
        /// Resets colors, fills buffer with default char and then redraws.
        /// </summary>
        public void Clear()
        {
            // Reset colors.
            ResetColorsToDefault();
            
            // Fill buffer with default char.
            FillChars(DefaultScreenChar);

            // Draw.
            Draw();
        }



        public void Update()
        {

        }

        public void Demo(int turns)
        {
            for (int i = 0; i < turns; i++)
            {
                Draw();
                Thread.Sleep(2500);

                FillChars(new ScreenChar('A', ConsoleColor.Blue, ConsoleColor.White));
                ResetCursorPosition();
                ResetColors();
                Draw();

                Thread.Sleep(2500);

                FillChars(new ScreenChar('B', ConsoleColor.Blue, ConsoleColor.White));
                ResetCursorPosition();
                ResetColors();
                Draw();
            }
        }
    }
}
