using ASCIIGraphix.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading;
using ASCIIGraphix.GfxObjects;

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
        public ScreenBuffer Buffer { get; private set; }
        public ScreenBuffer PreviousBuffer { get; private set; }
        public char DefaultChar = '█';
        public ScreenChar DefaultScreenChar { get; set; }
        public ConsoleColor BgColor { get; set; }
        public ConsoleColor FgColor { get; set; }
        public ConsoleColor DefaultBgColor { get; private set; }
        public ConsoleColor DefaultFgColor { get; private set; }
        public IConsoleWrapper ConsoleWrapper { get; private set; }

        public List<GfxObject> Objects { get; private set; }

        /// <summary>
        /// Defines a screen area of width x height dimensions and ensures it is impossible to draw outside of it.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="bgColor"></param>
        /// <param name="fgColor"></param>
        public Screen(int width, int height, ConsoleColor? bgColor = null, ConsoleColor? fgColor = null, IConsoleWrapper console = null)
        {
            ConsoleWrapper = console ?? new ConsoleWrapper();


            //(int,int) CursorStartPosition = consoleWrapper.GetCursorPosition();
            try
            {
                StartPositionTop = ConsoleWrapper.CursorTop;
            } catch (NullReferenceException) {
                StartPositionTop = 0;
            }
            try
            {
                StartPositionLeft = ConsoleWrapper.CursorLeft;
            }
            catch (NullReferenceException)
            {
                StartPositionLeft = 0;
            }

            // Colors.
            DefaultBgColor = ConsoleWrapper.BackgroundColor;
            DefaultFgColor = ConsoleWrapper.ForegroundColor;

            BgColor = bgColor != null ? (ConsoleColor)bgColor : DefaultBgColor;
            FgColor = fgColor != null ? (ConsoleColor)fgColor : DefaultBgColor;

            // Use bgcolor as both foreground and background due to wanting placeholder to fill entire char area in mono-color.
            DefaultScreenChar = new ScreenChar(DefaultChar, BgColor, BgColor);

            // Dimensions.
            Width = width;
            Height = height;

            // Create chars coord system.
            Buffer = new ScreenBuffer(width, height, FgColor, BgColor);

            // Populate Buffer with Default ScreenChars.
            Buffer.Fill(DefaultScreenChar);
            
            // Previous Buffer is currently the initial buffer, so set it as null to signify that buffer has no previous version.
            PreviousBuffer = null;
        }
        public void SetColors()
        {
            ConsoleWrapper.BackgroundColor = BgColor;
            ConsoleWrapper.ForegroundColor = FgColor;
        }

        public void ResetColorsToDefault()
        {
            ConsoleWrapper.BackgroundColor = DefaultBgColor;
            ConsoleWrapper.ForegroundColor = DefaultFgColor;
        }

        public void ResetColors()
        {
            ConsoleWrapper.BackgroundColor = BgColor;
            ConsoleWrapper.ForegroundColor = FgColor;
        }

        /// <summary>
        /// Iterator for splitting string on line breaks.
        /// 
        /// Using an iterator is preferred over string.split() as it is more efficient and avoids wasted memory (string.Split stores both copies).
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected static IEnumerable<string> StringAsLines(string input)
        {
            if (input == null) yield break;

            using System.IO.StringReader reader = new(input);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
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

        /// <summary>
        /// Returns int amount of lines in a string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        protected static int GetLineCount(string s)
        {
            int count = 0;

            foreach (string line in StringAsLines(s))
            {
                count++;
            }

            return count;
        }

        private void WriteAt(in ScreenChar sc, int x, int y)
        {
            int computedX = StartPositionLeft + x;
            int computedY = StartPositionTop + y;

            if (x > Buffer.LengthX || y > Buffer.LengthY)
            {
                throw new ArgumentException($"Was instructed to write at coord [{computedX}, {computedY}], " +
                    $"but that is OOB! (Bounds: [{Buffer.LengthX}, {Buffer.LengthY}])");
            }

            try
            {
                // Move cursor to x, y coordinate.
                ConsoleWrapper.SetCursorPosition(computedX, computedY);
                // Draw ScreenChar on Screen.
                sc.Draw();

                ResetColors();
            }
            catch (ArgumentOutOfRangeException e)
            {
                ConsoleWrapper.Clear();
                ConsoleWrapper.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Insert given ScreenChar into every index of the Buffer.
        /// </summary>
        /// <param name="sc">ScreenChar object to insert.</param>
        public void Fill(ScreenChar sc)
        {
            // Store a copy of buffer before it was updated.
            PreviousBuffer = Buffer.Clone();

            Buffer.Fill(sc);
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
            foreach (string stringLine in StringAsLines(s)) // TODO: Replace with for? (Performance VS readability).
            {
                if (stringLine.Length > Width) throw new ScreenWidthExceededException($"Given string exceeds width on line {lineCount + 1}, {stringLine.Length} > {Width}!");

                charLines.Add(new(stringLine.ToCharArray()));

                lineCount++;
            }

            if (lineCount > Height) throw new ScreenHeightExceededException($"Given string exceeds height, {lineCount} > {Height}!");

            return charLines;
        }

        /// <summary>
        /// Takes a string argument and copies it into the Buffer.
        /// </summary>
        /// <param name="s"></param>
        public void CopyToBuffer(string s)
        {
            // Store a copy of buffer before it was updated.
            PreviousBuffer = Buffer.Clone();

            int y = 0;

            foreach (List<char> charLine in ParseAsBufferLines(s))
            {
                for (int x = 0; x < charLine.Count; x++)
                {
                    Buffer[x, y] = new ScreenChar(charLine[x], BgColor, FgColor);
                }

                y++;
            }
        }

        private void DrawLinebreak()
        {
            // Fix issue with bgcolor freaking out on resize by printing a single char of default bg color, to make it look invisible.
            ResetColorsToDefault();
            ConsoleWrapper.ForegroundColor = DefaultBgColor;
            ConsoleWrapper.Write('█');

            // Force newline
            ConsoleWrapper.WriteLine();
        }

        public void Draw(bool onlyDrawChange = true)
        {
            ScreenBufferDiff diff;

            if (PreviousBuffer != null)
            {
                diff = ScreenBuffer.Diff(PreviousBuffer, Buffer);
            } else
            {
                diff = new(Width, Height);
            }

            ResetCursorPosition();

            // For each row / line
            for (int y = 0; y < Buffer.LengthY; y++)
            {
                // For each column / char
                for (int x = 0; x < Buffer.LengthX; x++)
                {
                    if (PreviousBuffer != null && onlyDrawChange)
                    {
                        // Only draw if the char differs.
                        if (diff[x, y].Differs == true)
                        {
                            WriteAt(Buffer[x, y], x, y);
                        }
                    } else
                    {
                        WriteAt(Buffer[x, y], x, y);
                    }
                }

                // Only insert linebreak if we're drawing a full buffer (which will have multiple lines)
                if (PreviousBuffer == null || onlyDrawChange == false) DrawLinebreak();
            }

            // Reset back to defaults.
            ResetColorsToDefault();
        }

        /// <summary>
        /// Clear console and issue a Draw().
        /// </summary>
        public void Redraw()
        {
            ConsoleWrapper.Clear();
            Draw();
        }

        public void ResetCursorPosition()
        {
            ConsoleWrapper.SetCursorPosition(CursorStartPosition.Item1, CursorStartPosition.Item2);
        }

        /// <summary>
        /// Resets colors, fills buffer with default char and then redraws.
        /// </summary>
        public void Clear()
        {
            // Store a copy of buffer before it was updated.
            PreviousBuffer = Buffer.Clone();

            // Reset colors.
            ResetColorsToDefault();
            
            // Fill buffer with default char.
            Buffer.Fill(DefaultScreenChar);

            // Draw.
            Draw();
        }

        public void Demo(int turns, int delayMs = 2500) // FIXME: Extract into own thing that makes a Screen and runs demo, not be part of Screen itself.
        {
            for (int i = 0; i < turns; i++)
            {
                Draw();
                Thread.Sleep(delayMs);

                Buffer.Fill(new ScreenChar(DefaultChar, ConsoleColor.Blue, ConsoleColor.Blue, DefaultBgColor, DefaultFgColor));
                ResetCursorPosition();
                //ResetColors();
                Draw();

                Thread.Sleep(delayMs);

                Buffer.Fill(new ScreenChar('▓', ConsoleColor.DarkYellow, DefaultBgColor, DefaultBgColor, DefaultFgColor));
                ResetCursorPosition();
                //ResetColors();
                Draw();

                Thread.Sleep(delayMs);

                Buffer.Fill(new ScreenChar('▓', DefaultBgColor, ConsoleColor.DarkYellow, DefaultBgColor, DefaultFgColor));
                ResetCursorPosition();
                //ResetColors();
                Draw();

                Thread.Sleep(delayMs);

                Buffer.Fill(new ScreenChar('╪', ConsoleColor.DarkGreen, ConsoleColor.Yellow, DefaultBgColor, DefaultFgColor));
                ResetCursorPosition();
                //ResetColors();
                Draw();

                Thread.Sleep(delayMs);

                Buffer.Fill(new ScreenChar('≡', ConsoleColor.DarkGreen, ConsoleColor.White, DefaultBgColor, DefaultFgColor));
                ResetCursorPosition();
                //ResetColors();
                Draw();
            }
        }
    }
}
