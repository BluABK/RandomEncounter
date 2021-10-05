namespace ASCIIGraphix.GfxObjects
{
    public class Box : GfxObject
    {
        // Border chars.
        protected char BorderHorizontal = '─';
        protected char BorderVertical = '│';
        protected char BorderCornerTopLeft = '┌';
        protected char BorderCornerTopRight = '┐';
        protected char BorderCornerBottomLeft = '└';
        protected char BorderCornerBottomRight = '┘';

        // Screen dimensions (our container).
        protected int ScreenWidth;
        protected int ScreenHeight;

        // Own dimensions.
        protected int Width;
        protected int Height;
        /// Actual workable width after padding and other processing.
        protected int InnerWidth;

        // Padding.
        protected bool Padding;
        protected bool HasPaddingLeft;
        protected int InnerPaddingLeft = 1;
        protected int InnerPaddingRight = 1;
        protected string PaddingStrLeft;
        protected string PaddingStrRight;

        // Box border dimensions.
        protected int HorizBorderWidth;
        protected Box(int screenWidth, int screenHeight, int width, int height, bool padding = true, bool hasPaddingLeft = false)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            Width = width;
            Height = height;
            Padding = padding;
            HasPaddingLeft = hasPaddingLeft;

            //              Total width     |   Padding left side  Padding right side  |
            InnerWidth = Width - 1 - InnerPaddingLeft - InnerPaddingRight - 1;

            HorizBorderWidth = Width - 2;
        }

        /// <summary>
        /// Turns a char c into string of n amount of c.
        ///
        /// Example:
        ///     CharToString('a', 3) == "aaa"
        /// </summary>
        /// <param name="c"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        protected static string CharToString(char c, int amount)
        {
            string s = string.Empty;

            for (int i = 0; i < amount; ++i)
            {
                s += c;
            }

            return s;
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

        protected void SetElementPadding()
        {
            // Default both sides to empty string.
            PaddingStrLeft = PaddingStrRight = string.Empty;

            if (!Padding) return;

            if (HasPaddingLeft)
            {
                PaddingStrLeft = GetPadding(ScreenWidth - Width);
            }
            else
            {
                PaddingStrRight = GetPadding(ScreenWidth - Width);
            }
        }
    }
}