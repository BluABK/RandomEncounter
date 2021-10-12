using System.Collections.Generic;

namespace ASCIIGraphix
{
    public interface IScreenBuffer
    {
        /// <summary>
        ///     Gets the rank (number of dimensions) of the Array. For example, a one-dimensional array returns 1, a two-dimensional array returns 2, and so on.
        /// </summary>
        public int Rank { get; }

        /// <summary>
        /// 32-bit integer that represents the number of elements in the X-dimension of the System.Array.
        /// </summary>
        /// <returns>
        /// A 32-bit integer that represents the number of elements in the X-dimension.
        /// </returns>
        public int LengthX { get; }

        /// <summary>
        /// 32-bit integer that represents the number of elements in the Y-dimension of the System.Array.
        /// </summary>
        /// <returns>
        /// A 32-bit integer that represents the number of elements in the Y-dimension.
        /// </returns>
        public int LengthY { get; }

        public int Length { get; }

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
        public int GetLength(int dimension);

        /// <summary>
        ///     Clear the buffer.
        /// </summary>
        public void Clear();

        /// <summary>
        /// Insert given ScreenChar into every index of the Buffer.
        /// </summary>
        /// <param name="sc">ScreenChar object to insert.</param>
        public void Fill(ScreenChar sc);

        /// <summary>
        /// Insert given ScreenChars into every index of the Buffer.
        /// </summary>
        /// <param name="scs">ScreenChar objects to insert.</param>
        public void Fill(ScreenChar[] scs);

        /// <summary>
        /// Checks if the given string fits in the Buffer (NB: newlines are stripped).
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool Fits(string s);

        public IEnumerator<ScreenChar> GetEnumerator();
    }
}