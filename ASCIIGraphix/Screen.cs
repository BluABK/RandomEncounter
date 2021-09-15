using ASCIIGraphix.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ASCIIGraphix
{
    public class Screen : IScreen
    {
        protected readonly static string[] LineBreakCharacters = { "\r\n", "\r", "\n" }; // NB: More exist, but each one added negatively impacts performance.
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

        /// <summary>
        /// Defines a screen area of width x height dimensions and ensures it is impossible to draw outside of it.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="bgColor"></param>
        /// <param name="fgColor"></param>
        public Screen(int width, int height, ConsoleColor? bgColor = null, ConsoleColor? fgColor = null)
        {
            //(int,int) CursorStartPosition = Console.GetCursorPosition();
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
            FillBuffer(DefaultScreenChar);
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

        /// <summary>
        /// Insert given ScreenChar into every index of the Buffer.
        /// </summary>
        /// <param name="sc">ScreenChar object to insert.</param>
        public void FillBuffer(ScreenChar sc)
        {
            for (int i = 0; i < Buffer.GetLength(0); i++)
            {
                for (int j = 0; j < Buffer.GetLength(1); j++)
                {
                    Buffer[i, j] = sc;
                }
            }
        }

        /// <summary>
        /// Iterator for splitting string on line breaks.
        /// 
        /// Using an iterator is preferred over string.split() as it is more efficient and avoids wasted memory (string.Split stores both copies).
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected static IEnumerable<string> SplitToLines(string input)
        {
            if (input == null) yield break;

            using (System.IO.StringReader reader = new(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        /// <summary>
        /// Split string on either type of line break while preserving empty lines and spacing.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        protected static string[] SplitOnLineBreak(string s)
        {
            return s.Split(LineBreakCharacters, StringSplitOptions.None);
        }

        //protected static int GetLineCount(string s)
        //{
        //    int count = 0;
        //    foreach (string c in s)
        //        if (c == '/') count++;

        //    return count;
        //}

        /// <summary>
        /// Returns int amount of lines in a string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        protected static int GetLineCount(string s)
        {
            int count = 0;

            foreach (string line in SplitToLines(s))
            {
                count++;
            }

            return count;
        }

        /// <summary>
        /// Takes a string and parses it as a List of char lists.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public List<List<char>> ParseAsBufferLines(string s)
        {
            List<List<char>> charLines = new();
            //int sourceLineCount = GetLineCount(s);
            
            //if (sourceLineCount > Height) throw new ScreenHeightExceededException($"Given string exceeds height, {sourceLineCount} > {Height}!");
            
            int lineCount = 0;
            foreach (string stringLine in SplitToLines(s)) // TODO: Replace with for? (Performance VS readability).
            {
                if (stringLine.Length > Width) throw new ScreenWidthExceededException($"Given string exceeds width on line {lineCount+1}, {stringLine.Length} > {Width}!");

                charLines.Add( new(stringLine.ToCharArray()) );

                lineCount++;
            }

            if (lineCount > Height) throw new ScreenHeightExceededException($"Given string exceeds height, {lineCount} > {Height}!");

            return charLines;
        }


        /// <summary>
        /// Checks if the given string fits in the Buffer (NB: newlines are stripped).
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool FitsInBuffer(string s)
        {
            int lineCount = 0;
            foreach (string stringLine in SplitToLines(s)) // TODO: Replace with for? (Performance VS readability).
            {
                // If line exceeds width, it won't fit.
                if (stringLine.Length > Width) return false;

                lineCount++;
            }

            // If amount of lines are larger than height, it won't fit.
            if (lineCount > Height) return false;

            return true;
        }

        public void CopyToBuffer(String s)
        {
            
        }

        private void WriteAt(ScreenChar sc, int x, int y)
        {
            int computedX = StartPositionLeft + x;
            int computedY = StartPositionTop + y;

            if (x > Buffer.GetLength(0) || y > Buffer.GetLength(1))
            {
                throw new ArgumentException($"Was instructed to write at coord [{computedX},{computedY}], " +
                    $"but that is OOB! (Bounds: [{Buffer.GetLength(0)},{Buffer.GetLength(1)}])");
            }

            try
            {

                Console.SetCursorPosition(computedX, computedY);
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

            // For each row / line
            for (int i = 0; i < Buffer.GetLength(1); ++i)
            {
                SetColors();
                // For each column / char
                for (int j = 0; j < Buffer.GetLength(0); ++j)
                {
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
            FillBuffer(DefaultScreenChar);

            // Draw.
            Draw();
        }



        //public void Update()
        //{

        //}

        public void Demo(int turns, int delayMs = 2500)
        {
            for (int i = 0; i < turns; i++)
            {
                Draw();
                Thread.Sleep(delayMs);

                FillBuffer(new ScreenChar(DefaultChar, ConsoleColor.Blue, ConsoleColor.Blue, DefaultBgColor, DefaultFgColor));
                ResetCursorPosition();
                //ResetColors();
                Draw();

                Thread.Sleep(delayMs);

                FillBuffer(new ScreenChar('▓', ConsoleColor.DarkYellow, DefaultBgColor, DefaultBgColor, DefaultFgColor));
                ResetCursorPosition();
                //ResetColors();
                Draw();

                Thread.Sleep(delayMs);

                FillBuffer(new ScreenChar('▓', DefaultBgColor, ConsoleColor.DarkYellow, DefaultBgColor, DefaultFgColor));
                ResetCursorPosition();
                //ResetColors();
                Draw();

                Thread.Sleep(delayMs);

                FillBuffer(new ScreenChar('╪', ConsoleColor.DarkGreen, ConsoleColor.Yellow, DefaultBgColor, DefaultFgColor));
                ResetCursorPosition();
                //ResetColors();
                Draw();

                Thread.Sleep(delayMs);

                FillBuffer(new ScreenChar('≡', ConsoleColor.DarkGreen, ConsoleColor.White, DefaultBgColor, DefaultFgColor));
                ResetCursorPosition();
                //ResetColors();
                Draw();
            }
        }
    }
}
