using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIGraphix
{
    public class ScreenBufferDiff
    {
        private readonly ScreenBufferDiffItem[,] items;

        public ScreenBufferDiff(int xDimensions, int yDimensions)
        {
            items = new ScreenBufferDiffItem[xDimensions, yDimensions];
        }

        /// <summary>
        ///     Indexer that allows ScreenBufferDiff to be treated like a 2D-Array.
        /// 
        ///     The x and y indexes are passed through directly to its items 2D-Array.
        /// </summary>
        /// <param name="x">Index in X-dimension.</param>
        /// <param name="y">Index in Y-dimension.</param>
        /// <returns>
        ///     ScreenBufferDiffItem at given X, Y coordinates.
        /// </returns>
        public ScreenBufferDiffItem this[int x, int y]
        {
            get
            {
                // get the item for that index.
                return items[x, y];
            }
            set
            {
                // set the ScreenChar for this index.
                items[x, y] = value;
            }
        }

        public bool[] DiffersToArray()
        {
            bool[] diffStatuses = new bool[items.Length];

            int index = 0;
            // For each row / line
            for (int y = 0; y < items.GetLength(1); ++y)
            {
                // For each column / char
                for (int x = 0; x < items.GetLength(0); ++x)
                {
                    if (items[x, y] != null)
                    {
                        diffStatuses[index] = items[x, y].Differs;
                    }
                    index++;
                }
            }

            return diffStatuses;
        }
    }
}
