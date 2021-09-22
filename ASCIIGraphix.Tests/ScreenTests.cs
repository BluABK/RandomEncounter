using NUnit.Framework;
using System;
using ASCIIGraphix;
using System.Collections.Generic;
using ASCIIGraphix.Tests.Stubs;

namespace ASCIIGraphix.Tests
{
    public class ScreenTests
    {
        public IConsoleWrapper ConsoleWrapper;
        [SetUp]
        public void Setup()
        {
            ConsoleWrapper = new ConsoleWrapperStub();
        }

        [Test]
        public void FillBufferWithScreenChar()
        {
            const int width = 5;
            const int height = 4;
            const int charCount = width * height;
            const char c = 'X';
            ScreenChar[] screenChars = new ScreenChar[charCount];

            Screen screen = new(width, height, console: ConsoleWrapper);

            for (int i = 0; i < width*height; ++i)
            {
                screenChars[i] = new(c, Console.BackgroundColor, Console.ForegroundColor);
            }

            ScreenChar sc = new(c, Console.BackgroundColor, Console.ForegroundColor);

            //if (screen.Buffer.Length > 0) throw new InvalidOperationException($"screen.Buffer was not empty, Length: {screen.Buffer.Length} > 0!");

            screen.Fill(sc);

            bool allEqual = true;
            foreach (ScreenChar bsc in screen.Buffer)
            {
                if (bsc != sc) allEqual = false;
            }

            Assert.IsTrue(allEqual);
        }

        [Test]
        public void FillBufferWithString()
        {
            const string expected =
@"┌──────────────────────────────────────────────┐                                                
│ Defender Name                           LvXX │                                                
│ HP: ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ │                                                
└──────────────────────────────────────────────┘                                                
                                                                                                
                                                                                                
                                                                                                
                                                                                                
                                                                                                
                                                                                                
                                                                                                
                                                                                                
                                                                                                
                                                                                                
                                                ┌──────────────────────────────────────────────┐
                                                │ Attacker Name                           LvXX │
                                                │ HP: ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ │
                                                │     XXX / XXX                                │
                                                └──────────────────────────────────────────────┘
╔════════════════════════════════════╦═════════════════════════════╤═══════════════════════════╗
║                                    ║ ┌──── ─┬─ ┌──── ┬   ┬ ──┬── │                           ║
║                                    ║ ├────  │  │ ──┐ ├───┤   │   │            ITEM           ║
║                                    ║ ┴     ─┴─ └───┘ ┴   ┴   ┴   │                           ║
║                                    ╟─────────────────────────────┼───────────────────────────╢
║                                    ║                             │                           ║
║                                    ║             INFO            │            QUIT           ║
║                                    ║                             │                           ║
╚════════════════════════════════════╩═════════════════════════════╧═══════════════════════════╝";

            const int width = 96;
            const int height = 28;

            Screen screen = new(width, height, console: ConsoleWrapper);

            screen.CopyToBuffer(expected);

            Assert.AreEqual(expected, screen.Buffer.ToString());
        }
    }
}
