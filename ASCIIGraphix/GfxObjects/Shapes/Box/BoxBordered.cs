using System;

namespace ASCIIGraphix.GfxObjects.Shapes.Box
{
    public class BoxBordered : Box
    {
        // Border chars. TODO: FIXME: Make customisable via ctor.
        protected char BorderHorizontal = '─';
        protected char BorderVertical = '│';
        protected char BorderCornerTopLeft = '┌';
        protected char BorderCornerTopRight = '┐';
        protected char BorderCornerBottomLeft = '└';
        protected char BorderCornerBottomRight = '┘';

        // Use an inner buffer in order to disallow modification of the outer rim which contains our border.
        protected SubScreenBuffer InnerBuffer;

        /// Actual workable width after padding and other processing.
        public int InnerWidth => InnerBuffer.LengthX - InnerPaddingLeft - InnerPaddingRight;
        public int InnerHeight => InnerBuffer.LengthY;
        // Padding.
        protected char PaddingChar = ' ';
        protected int InnerPaddingLeft = 1;
        protected int InnerPaddingRight = 1;

        // Box border dimensions.
        protected int BorderSize = 1;
        //                                             Left border  Right border.
        protected int HorizontalBorderWidth => Width - BorderSize - BorderSize;
        
        public BoxBordered(IScreen screen, int width, int height) 
            : base(screen, width, height)
        {
            if (height < 2) throw new ArgumentException("Box height must be >= 2 to fit top and bottom borders!");

            InnerBuffer = new SubScreenBuffer(Buffer, BorderSize);

            SetBorder();
        }

        protected string MakeLine(string s, bool appendNewLine = true)
        {
            return BorderVertical + GetPadding(InnerPaddingLeft) + s + GetPadding(InnerPaddingRight) + BorderVertical + (appendNewLine ? Environment.NewLine : "");
        }

        protected string InnerPaddedString(string s)
        {
            return GetPadding(InnerPaddingLeft) + s + GetPadding(InnerPaddingRight);
        }

        public void SetLine(ScreenChar[] scArr, int y = 0)
        {
            //if (scArr.Length > InnerWidth) throw new ArgumentException("ScreenChar array length exceeds InnerWidth!");
            if (scArr.Length != InnerWidth) throw new ArgumentException("ScreenChar array must have length equal to InnerWidth!"); // TODO: FIXME: Handle shorter content, by auto-padding or something.
            if (y < 0) throw new ArgumentException("y must be a positive integer!");
            if (y > InnerHeight) throw new ArgumentException("y size exceeds InnerHeight!");

            int x = 0;
            int padCountLeft = 0;

            // Left side padding
            for (; padCountLeft < InnerPaddingLeft; padCountLeft++)
            {
                InnerBuffer[x + padCountLeft, y] = new ScreenChar(PaddingChar, BgColor, FgColor, DefaultBgColor, DefaultFgColor);
            }

            // Content
            for (; x < InnerWidth; x++)
            {
                InnerBuffer[x + padCountLeft, y] = scArr[x];
            }

            // Move x-pos up by one so we're in the next pos, as it currently points at the last inserted char.
            x++;

            // Right side padding, iterate padding amount, using it as an offset to x to get the proper coordinate.
            for (int padCountRight = 0; padCountRight < InnerPaddingRight; padCountRight++)
            {
                InnerBuffer[x + padCountRight, y] = new ScreenChar(PaddingChar, BgColor, FgColor, DefaultBgColor, DefaultFgColor);
            }
        }

        protected string MakeHorizontalBorderLine(bool top, bool appendNewLine = true)
        {
            if (top)
            {
                return $"{BorderCornerTopLeft}{CharToString(BorderHorizontal, HorizontalBorderWidth)}{BorderCornerTopRight}" + (appendNewLine ? Environment.NewLine : "");
            }

            return $"{BorderCornerBottomLeft}{CharToString(BorderHorizontal, HorizontalBorderWidth)}{BorderCornerBottomRight}" + (appendNewLine ? Environment.NewLine : "");
        }

        protected void SetBorder(ConsoleColor? bgColor = null, ConsoleColor? fgColor = null)
        {
            bgColor ??= MyScreen.DefaultBgColor;
            fgColor ??= MyScreen.DefaultFgColor;

            int xBoundary = Buffer.LengthX - BorderSize;
            int yBoundary = Buffer.LengthY - BorderSize;

            // Corners
            Buffer[0, 0] = new ScreenChar(BorderCornerTopLeft, (ConsoleColor)bgColor, (ConsoleColor)fgColor);
            Buffer[xBoundary, 0] = new ScreenChar(BorderCornerTopRight, (ConsoleColor)bgColor, (ConsoleColor)fgColor);
            Buffer[0, yBoundary] = new ScreenChar(BorderCornerBottomLeft, (ConsoleColor)bgColor, (ConsoleColor)fgColor);
            Buffer[xBoundary, yBoundary] = new ScreenChar(BorderCornerBottomRight, (ConsoleColor)bgColor, (ConsoleColor)fgColor);

            // Horizontal lines
            for (int x = 1; x < Buffer.LengthX - 1; x++)
            {
                Buffer[x, 0] = new ScreenChar(BorderHorizontal, (ConsoleColor)bgColor, (ConsoleColor)fgColor);
                Buffer[x, yBoundary] = new ScreenChar(BorderHorizontal, (ConsoleColor)bgColor, (ConsoleColor)fgColor);
            }

            // Vertical lines
            for (int y = 1; y < Buffer.LengthY - 1; y++)
            {
                Buffer[0, y] = new ScreenChar(BorderVertical, (ConsoleColor)bgColor, (ConsoleColor)fgColor);
                Buffer[xBoundary, y] = new ScreenChar(BorderVertical, (ConsoleColor)bgColor, (ConsoleColor)fgColor);
            }
        }

        public override ScreenBuffer ToMatrix()
        {
            return Buffer.Clone();
        }
    }
}
