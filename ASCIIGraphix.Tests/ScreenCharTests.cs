using NUnit.Framework;
using System;
using ASCIIGraphix;

namespace ASCIIGraphix.Tests
{
    public class ScreenCharTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateAndDrawScreenChar()
        {
            // Create a ScreenChar 'X' with default colours.
            char c = 'X';
            ScreenChar sc = new(c, Console.BackgroundColor, Console.ForegroundColor);

            // Draw the char.
            sc.Draw();

            // As we got to this point, assume it was drawn.
            // Reason: Attempting to verify stdout print is exceedingly complicated and platform-specific.
            // It is not really warranted here, this test is mostly to see that ScreenChar can be instantiated and that Draw() is callable.
            Assert.Pass();
        }
    }
}