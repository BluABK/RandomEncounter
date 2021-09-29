using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIGraphix
{
    public class ScreenBuffer : IScreenBuffer, IEnumerable
    {
        protected ScreenChar[,] Buffer { get; private set; }
        /// <summary>
        ///     Gets the rank (number of dimensions) of the Array. For example, a one-dimensional array 
        ///     returns 1, a two-dimensional array returns 2, and so on.
        /// </summary>
        public int Rank => Buffer.Rank;
        /// <summary>
        /// 32-bit integer that represents the number of elements in the X-dimension of the System.Array.
        /// </summary>
        /// <returns>
        /// A 32-bit integer that represents the number of elements in the X-dimension.
        /// </returns>
        public int LengthX => Buffer.GetLength(0);
        /// <summary>
        /// 32-bit integer that represents the number of elements in the Y-dimension of the System.Array.
        /// </summary>
        /// <returns>
        /// A 32-bit integer that represents the number of elements in the Y-dimension.
        /// </returns>
        public int LengthY => Buffer.GetLength(1);

        public int Length => Buffer.Length;
        protected ConsoleColor bgColor;
        protected ConsoleColor fgColor;

        public ScreenBuffer(int xDimensions, int yDimensions, ConsoleColor bgColor, ConsoleColor fgColor)
        {
            Buffer = new ScreenChar[xDimensions, yDimensions];
            this.bgColor = bgColor;
            this.fgColor = fgColor;
        }

        public ScreenBuffer(ScreenChar[,] premadeSCArray, ConsoleColor bgColor, ConsoleColor fgColor)
        {
            Buffer = CloneScreenCharArray(premadeSCArray);
            this.bgColor = bgColor;
            this.fgColor = fgColor;
        }

        public static ScreenChar[,] CloneScreenCharArray(ScreenChar[,] scs)
        {
            int xLength = scs.GetLength(0);
            int yLength = scs.GetLength(1);

            ScreenChar[,] newArray = new ScreenChar[xLength, yLength];

            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    newArray[x, y] = scs[x,y].Clone();
                }
            }

            return newArray;
        }

        /// <summary>
        ///     Gets a 32-bit integer that represents the number of elements in the specified dimension of the System.Array.
        /// </summary>
        /// <param name="dimension">
        ///     A zero-based dimension of the System.Array whose length needs to be determined..
        /// </param>
        /// <returns>
        ///     A 32-bit integer that represents the number of elements in the specified dimension.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        ///     dimension is less than zero. -or- dimension is equal to or greater than System.Array.Rank.
        /// </exception>
        public int GetLength(int dimension)
        {
            return Buffer.GetLength(dimension);
        }

        public void Clear()
        {
            foreach (ScreenChar sc in Buffer)
            {
                sc.Clear();
            }
        }


        /// <summary>
        ///     Iterator for splitting string on line breaks.
        /// 
        ///     Using an iterator is preferred over string.split() as it is more efficient and avoids wasted memory (string.Split stores both copies).
        /// </summary>
        /// <param name="input">
        ///     String to iterate each line over.
        /// </param>
        protected static IEnumerable<string> StringAsLines(string input)
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
        /// Insert given ScreenChar into every index of the Buffer.
        /// </summary>
        /// <param name="sc">ScreenChar object to insert.</param>
        public void Fill(ScreenChar sc)
        {
            for (int i = 0; i < LengthX; i++)
            {
                for (int j = 0; j < LengthY; j++)
                {
                    Buffer[i, j] = sc;
                }
            }
        }

        /// <summary>
        /// Insert given ScreenChars into every index of the Buffer.
        /// </summary>
        /// <param name="sc">ScreenChar object to insert.</param>
        public void Fill(ScreenChar[] scs)
        {
            if (scs.Length != Buffer.Length) throw new ArgumentException("ScreenChar object array length not equal to buffer ScreenChar length!");

            int index = 0;
            for (int x = 0; x < LengthX; x++)
            {
                for (int y = 0; y < LengthY; y++)
                {
                    Buffer[x, y] = scs[index];
                    index++;
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
            for (int i = 0; i < LengthY; ++i)
            {
                // For each column / char
                for (int j = 0; j < LengthX; ++j)
                {
                    s += Buffer[j, i].Char;
                }

                // Force newline, if and only if not on last line, or we end up adding trailing newline to returned string.
                if (i < LengthY - 1) s += Environment.NewLine;
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
            foreach (string stringLine in StringAsLines(s)) // TODO: Replace with for? (Performance VS readability).
            {
                // If line exceeds width, it won't fit.
                if (stringLine.Length > LengthX) return false;

                lineCount++;
            }

            // If amount of lines are larger than height, it won't fit.
            if (lineCount > LengthY) return false;

            return true;
        }

        public IEnumerator<ScreenChar> GetEnumerator()
        {
            foreach(ScreenChar sc in Buffer)
            {
                yield return sc;
            }
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
                // get the item for that index.
                return Buffer[x, y];
            }
            set
            {
                // set the ScreenChar for this index.
                Buffer[x, y] = value;
            }
        }
        /// <summary>
        /// Get a diff between given buffer and the current one.
        /// </summary>
        /// <param name="bufferToDiff"></param>
        /// <param name="onlyDiffEntries"></param>
        /// <returns>List of ScreenBufferDiffItem where A is current buffer and B is the given one.</returns>
        public ScreenBufferDiff Diff(string bufferToDiff)
        {
            //if (!Fits(diffBuffer)) throw new ArgumentException("Given Buffer contents does not fit within this Buffer!");
            
            ScreenBufferDiff diff = new(LengthX, LengthY);

            int y = 0;
            foreach (string stringLine in StringAsLines(bufferToDiff)) // TODO: Replace with for? (Performance VS readability).
            {
                // If line exceeds width, it won't fit.
                if (stringLine.Length > LengthX) throw new ArgumentException("Given Buffer contents exceeds this Buffer's width!");

                for (int x = 0; x < stringLine.Length; x++)
                {
                    //bool differs = stringLine[x] != Buffer[x, y].Char;

                    diff[x, y] = new ScreenBufferDiffItem(x, y, a: Buffer[x, y], b: new(stringLine[x], Buffer[x, y].BgColor, Buffer[x, y].FgColor));
                }
                // Move y position to the next line.
                y++;
            }

            // If amount of lines are larger than height, it won't fit.
            if (y > LengthY) throw new ArgumentException("Given Buffer contents exceeds this Buffer's height!");

            return diff;
        }

        public ScreenBufferDiff Diff(in ScreenBuffer diffBuffer)
        {
            //return Diff(diffBuffer.ToString());
            ScreenBufferDiff diff = new(LengthX, LengthY);
            
            // For each row / line
            for (int y = 0; y < LengthY; ++y)
            {
                // For each column / char
                for (int x = 0; x < LengthX; ++x)
                {
                    diff[x, y] = new ScreenBufferDiffItem(x, y, a: Buffer[x, y], b: diffBuffer[x, y]);
                }
            }

            return diff;
        }

        public static ScreenBufferDiff Diff(in ScreenBuffer a, in ScreenBuffer b)
        {
            if (a.LengthX != b.LengthX || a.LengthY != b.LengthY || a.Rank != b.Rank) throw new ArgumentException("ScreenBuffer a and b have mismatching dimensions or criteria!");
            //return Diff(diffBuffer.ToString());
            ScreenBufferDiff diff = new(a.LengthX, a.LengthY);

            // For each row / line
            for (int y = 0; y < a.LengthY; ++y)
            {
                // For each column / char
                for (int x = 0; x < a.LengthX; ++x)
                {
                    diff[x, y] = new ScreenBufferDiffItem(x, y, a[x, y], b[x, y]);
                }
            }

            return diff;
        }

        /// <summary>
        /// Returns a cloned copy of this ScreenBuffer.
        /// </summary>
        /// <returns>A cloned copy of itself.</returns>
        public ScreenBuffer Clone()
        {
            return new ScreenBuffer(premadeSCArray: Buffer, bgColor, fgColor);
        }


    }
}
