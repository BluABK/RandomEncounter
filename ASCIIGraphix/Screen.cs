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

        private void WriteAt(ScreenChar sc, int x, int y, ConsoleColor? bgColor = null, ConsoleColor? fgColor = null)
        {
            int computedX = StartPositionLeft + x;
            int computedY = StartPositionTop + y;

            if (x > Width || y > Height) throw new ArgumentException($"Was instructed to write at coord [{computedX},{computedY}], but that is OOB! (Bounds: [{Width}{Height}])");

            ConsoleColor _bgColor = bgColor != null ? (ConsoleColor)bgColor : BgColor;
            ConsoleColor _fgColor = fgColor != null ? (ConsoleColor)fgColor : FgColor;

            try
            {
                Console.SetCursorPosition(computedX, computedY);
                //SetColors();
                //Console.Write(c);
                sc.Draw();
                ResetColors();
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

            // Note to self: 96x28 = Buffer[0,0]..Buffer[95,27]

            // For each row / line
            for (int i = 0; i < Buffer.GetLength(1); ++i)
            {
                SetColors();
                // For each column / char
                for (int j = 0; j < Buffer.GetLength(0); ++j)
                {
                    //Console.Write(Chars[i, j]);
                    //Console.Write(j);
                    //Console.Write(DefaultScreenChar.Char);
                    //DefaultScreenChar.Draw();


                    WriteAt(Buffer[j, i], j, i);
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

        /// <summary>
        /// Clear console and issue a Draw().
        /// </summary>
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

        public void Demo(int turns, int delayMs = 2500)
        {
            int debugBreakpointSenpai = 0;
            for (int i = 0; i < turns; i++)
            {
                Draw();
                Thread.Sleep(delayMs);

                FillChars(new ScreenChar(DefaultChar, ConsoleColor.Blue, ConsoleColor.Blue, DefaultBgColor, DefaultFgColor));
                ResetCursorPosition();
                //ResetColors();
                Draw();

                Thread.Sleep(delayMs);

                FillChars(new ScreenChar('▓', ConsoleColor.DarkYellow, DefaultBgColor, DefaultBgColor, DefaultFgColor));
                ResetCursorPosition();
                //ResetColors();
                Draw();

                Thread.Sleep(delayMs);

                FillChars(new ScreenChar('▓', DefaultBgColor, ConsoleColor.DarkYellow, DefaultBgColor, DefaultFgColor));
                ResetCursorPosition();
                //ResetColors();
                Draw();

                Thread.Sleep(delayMs);

                FillChars(new ScreenChar('╪', ConsoleColor.DarkGreen, ConsoleColor.Yellow, DefaultBgColor, DefaultFgColor));
                ResetCursorPosition();
                //ResetColors();
                Draw();

                Thread.Sleep(delayMs);

                FillChars(new ScreenChar('≡', ConsoleColor.DarkGreen, ConsoleColor.White, DefaultBgColor, DefaultFgColor));
                ResetCursorPosition();
                //ResetColors();
                Draw();
            }
                int debugBreakpointSenpai2 = 0;
        }
    }
}
