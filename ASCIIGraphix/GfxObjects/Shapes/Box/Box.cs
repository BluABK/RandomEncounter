using System;

namespace ASCIIGraphix.GfxObjects.Shapes.Box
{
    public class Box : GfxObject
    {
        // Own dimensions.
        public int Width;
        public int Height;

        protected ScreenBuffer Buffer;
        /// <summary>
        /// Create a box based on the given parameters.
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="width">Box width.</param>
        /// <param name="height">Box height.</param>
        /// <param name="buffer">(Optional) Custom buffer to use instead of creating a new one.</param>
        public Box(IScreen screen, int width, int height, in ScreenBuffer buffer = null)
            : base(screen)
        {
            Width = width;
            Height = height;

            Buffer = buffer ?? new ScreenBuffer(width, height, screen.DefaultBgColor, screen.DefaultFgColor, autofillChar: ' ');
        }

        public void Clear()
        {
            foreach (ScreenChar sc in Buffer)
            {
                sc.Clear();
            }
        }

        protected static string GetPadding(int length)
        {
            string s = string.Empty;

            for (int i = 0; i < length; i++)
            {
                s += ' ';
            }

            return s;
        }

        /// <summary>
        ///     Returns the buffer matrix as a single string with regular line-breaks for each Y.
        /// </summary>
        /// <returns>
        ///     Buffer matrix as a single string with regular line-breaks for each Y.
        /// </returns>
        public override string ToString()
        {
            string s = string.Empty;

            // For each row / line
            for (int y = 0; y < Height; ++y)
            {
                // For each column / char
                for (int x = 0; x < Width; ++x)
                {
                    s += Buffer[x, y].Char;
                }

                // Force newline, if and only if not on last line, or we end up adding trailing newline to returned string.
                if (y < Height - 1) s += Environment.NewLine;
            }

            return s;
        }
        /// <summary>
        /// Return a clone of this ScreenBuffer.
        /// </summary>
        /// <returns>Cloned ScreenBuffer instance.</returns>
        public virtual ScreenBuffer ToMatrix()
        {
            return Buffer.Clone();
        }
    }
}