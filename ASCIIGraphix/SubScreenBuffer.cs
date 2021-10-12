using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ASCIIGraphix
{
    /// <summary>
    /// Designates a subsection of a buffer as a separate entity with its own boundaries.
    ///
    /// Methods are used against the root buffer, with offsets calculated automatically.
    /// This makes the usage of a SubScreenBuffer feel like you are directly using the root ScreenBuffer, just with defined boundaries.
    /// </summary>
    public class SubScreenBuffer : IScreenBuffer, IEnumerable
    {
        // Own properties.
        private readonly ScreenBuffer root;
        private readonly int xOffsetStart;
        private readonly int xOffsetEnd;
        private readonly int yOffsetStart;
        private readonly int yOffsetEnd;
        public int LengthX => root.LengthX - xOffsetStart - xOffsetEnd; // offset n on each side
        public int LengthY => root.LengthY - yOffsetStart - yOffsetEnd; // offset n on each side

        // Root ScreenBuffer's properties.
        public int Rank => root.Rank;

        public SubScreenBuffer(ScreenBuffer rootBuffer, int offset)
        {
            root = rootBuffer;
            
            // Offsets.
            xOffsetStart = offset;
            xOffsetEnd = offset;
            yOffsetStart = offset;
            yOffsetEnd = offset;
        }

        private ScreenChar[,] GetSubBuffer()
        {
            ScreenChar[,] matrix = new ScreenChar[LengthX, LengthY];

            for (int x = xOffsetStart; x < root.LengthX - xOffsetEnd; x++)
            {
                for (int y = yOffsetStart; y < root.LengthY - yOffsetEnd; y++)
                {
                    matrix[x, y] = root[x, y];
                }
            }

            return matrix;
        }

        public ScreenChar[,] ToMatrix()
        {
            ScreenChar[,] clonedMatrix = new ScreenChar[LengthX, LengthY];

            for (int x = xOffsetStart; x < root.LengthX - xOffsetEnd; x++)
            {
                for (int y = yOffsetStart; y < root.LengthY - yOffsetEnd; y++)
                {
                    clonedMatrix[x, y] = root[x, y].Clone();
                }
            }

            return clonedMatrix;
        }

        public int Length => ToMatrix().Length;
        public int GetLength(int dimension)
        {
            return ToMatrix().GetLength(dimension);
        }

        public void Clear()
        {
            for (int x = xOffsetStart; x < root.LengthX - xOffsetEnd; x++)
            {
                for (int y = yOffsetStart; y < root.LengthY - yOffsetEnd; y++)
                {
                    root[x, y].Clear();
                }
            }
        }

        public void Fill(ScreenChar sc)
        {
            for (int x = xOffsetStart; x < root.LengthX - xOffsetEnd; x++)
            {
                for (int y = yOffsetStart; y < root.LengthY - yOffsetEnd; y++)
                {
                    root[x, y] = sc;
                }
            }
        }

        /// <summary>
        /// Insert given ScreenChars into every index of the Buffer.
        /// </summary>
        /// <param name="scs">ScreenChar object to insert.</param>
        public void Fill(ScreenChar[] scs)
        {
            if (scs.Length != Length) throw new ArgumentException("ScreenChar object array length not equal to buffer ScreenChar length!");

            int index = 0;
            for (int x = xOffsetStart; x < root.LengthX - xOffsetEnd; x++)
            {
                for (int y = yOffsetStart; y < root.LengthY - yOffsetEnd; y++)
                {
                    root[x, y] = scs[index];
                    index++;
                }
            }
        }

        /// <summary>
        /// Insert given char (converted to ScreenChar) into every index of the Buffer.
        /// </summary>
        /// <param name="c">char to insert.</param>
        public void Fill(char c)
        {
            for (int x = xOffsetStart; x < root.LengthX - xOffsetEnd; x++)
            {
                for (int y = yOffsetStart; y < root.LengthY - yOffsetEnd; y++)
                {
                    root[x, y] = new ScreenChar(c, root.BgColor, root.FgColor);
                }
            }
        }

        /// <summary>
        ///     Returns the Buffer as a single string with regular linebreaks for each Y.
        /// </summary>
        /// <returns>
        ///     Buffer as a single string with regular linebreaks for each Y.
        /// </returns>
        public override string ToString()
        {
            string s = string.Empty;

            // For each row / line
            for (int y = yOffsetStart; y < root.LengthY - yOffsetEnd; y++)
            {
                // For each column / char
                for (int x = xOffsetStart; x < root.LengthX - xOffsetEnd; x++)
                {
                    s += root[x, y].Char;
                }

                // Force newline, if and only if not on last line, or we end up adding trailing newline to returned string.
                if (y < LengthY - 1) s += Environment.NewLine;
            }

            return s;
        }

        /// <summary>
        ///     Checks if the given string fits in the Buffer (NB: linebreaks are stripped).
        /// </summary>
        /// <param name="s">
        ///     String to check if fits in buffer.
        /// </param>
        /// <returns>
        ///     Whether string fits in buffer or not.
        /// </returns>
        public bool Fits(string s)
        {
            int lineCount = 0;
            foreach (string stringLine in ScreenBuffer.StringAsLines(s)) // TODO: Replace with for? (Performance VS readability).
            {
                // If line exceeds width, it won't fit.
                if (stringLine.Length > LengthX) return false;

                lineCount++;
            }

            // If amount of lines are larger than height, it won't fit.
            return lineCount <= LengthY;
        }

        public IEnumerator<ScreenChar> GetEnumerator()
        {
            return GetSubBuffer().Cast<ScreenChar>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Indexer that allows ScreenBuffer to be treated like a 2D-Array.
        /// 
        ///     The x and y indexes are passed through directly to its Buffer 2D-Array.
        /// </summary>
        /// <param name="x">Index in X-dimension.</param>
        /// <param name="y">Index in Y-dimension.</param>
        /// <returns>
        ///     ScreenChar at given X, Y coordinates.
        /// </returns>
        public ScreenChar this[int x, int y]
        {
            get
            {
                if (x > LengthX) throw new IndexOutOfRangeException("x exceeds LengthX!");
                if (y > LengthY) throw new IndexOutOfRangeException("y exceeds LengthY!");
                // get the item for that index.
                return root[x + xOffsetStart, y + yOffsetStart];
            }
            set
            {
                if (x > LengthX) throw new IndexOutOfRangeException("x exceeds LengthX!");
                if (y > LengthY) throw new IndexOutOfRangeException("y exceeds LengthY!");
                // set the ScreenChar for this index.
                root[x + xOffsetStart, y+ yOffsetStart] = value;
            }
        }

        /// <summary>
        /// Returns a cloned copy of this ScreenBuffer.
        /// </summary>
        /// <returns>A cloned copy of itself.</returns>
        public ScreenBuffer Clone()
        {
            return new ScreenBuffer(GetSubBuffer(), root.BgColor, root.FgColor);
        }
    }
}
