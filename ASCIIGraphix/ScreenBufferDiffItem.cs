namespace ASCIIGraphix
{
    public class ScreenBufferDiffItem
    {
        public ScreenChar A { get; }
        public ScreenChar B { get; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Differs { get; }

        public ScreenBufferDiffItem(int x, int y, in ScreenChar a, in ScreenChar b)
        {
            X = x;
            Y = y;
            A = a;
            B = b;
            Differs = IsDifferent(a, b);
        }

        private static bool IsDifferent(in ScreenChar a, in ScreenChar b)
        {
            return !IsEqual(a, b);
        }

        private static bool IsEqual(in ScreenChar a, in ScreenChar b)
        {
            return a.IsEqualTo(b);
        }
    }
}