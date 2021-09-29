namespace ASCIIGraphix
{
    public class ScreenBufferDiffItem
    {
        public ScreenChar A {get; private set;}
        public ScreenChar B {get; private set;}
        public int X { get; set; }
        public int Y { get; set; }
        public bool Differs { get; private set;}

        public ScreenBufferDiffItem(int x, int y, in ScreenChar a, in ScreenChar b)
        {
            X = x;
            Y = y;
            A = a;
            B = b;
            Differs = IsDifferent(a, b);
        }

        private bool IsDifferent(in ScreenChar a, in ScreenChar b)
        {
            return !IsEqual(a, b);
        }

        private bool IsEqual(in ScreenChar a, in ScreenChar b)
        {
            return a.IsEqualTo(b);
        }
    }
}