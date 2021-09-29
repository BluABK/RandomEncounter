using NUnit.Framework;
using System;
using ASCIIGraphix;
using ASCIIGraphix.Tests.Stubs;
using System.Linq;

namespace ASCIIGraphix.Tests
{
    public class ScreenBufferTests
    {
        public IConsoleWrapper ConsoleWrapper;

        [SetUp]
        public void Setup()
        {
            ConsoleWrapper = new ConsoleWrapperStub();
        }

        private static ScreenChar[] StringToSCArray(string s)
        {
            ScreenChar[] scs = new ScreenChar[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                scs[i] = new ScreenChar(s[i], ConsoleColor.Black, ConsoleColor.White);
            }

            return scs;
        }

        private static string MakeCharString(char c, int count)
        {
            string s = string.Empty;

            for (int i = 0; i < count; i++)
            {
                s += c;
            }

            return s;
        }

        private static void PrettyPrintAssertionTable(bool[] expected, bool[] actual)
        {
            const string lhs = "Expected ";
            const string rhs = " Actual";
            const char sep = '│';
            string itemSep = $"{MakeCharString(' ', lhs.Length - 5)}| ";
            System.Console.WriteLine($"{lhs}{sep}{rhs}");
            System.Console.WriteLine($"{MakeCharString('—', lhs.Length)}{sep}{MakeCharString('—', rhs.Length)}");
            
            foreach ((bool first, bool second) in expected.Zip(actual))
            {
                string expectedStr = first ? first.ToString() + ' ' : first.ToString();
                string actualStr = second ? second.ToString() + ' ' : second.ToString();
                System.Console.WriteLine($"{expectedStr}{itemSep}{actualStr}");
            }
        }

        [Test]
        public void ScreenBufferDiffStaticDiffersVerification()
        {
            const string origString = "■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■";
            const string diffString = "  ■■■■■■■■■   ■■■■■■■■■■■■■■■■■■■■■■■■  ";
            bool[] expected = new bool[] 
            { 
                true, true, false, false, false, false, false, false, false, false,
                false, true, true, true, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, true, true,
            };

            const int width = 40;
            const int height = 1;

            ScreenBuffer origBuffer = new(width, height, ConsoleColor.Black, ConsoleColor.White);
            ScreenBuffer diffBuffer = new(width, height, ConsoleColor.Black, ConsoleColor.White);

            origBuffer.Fill(StringToSCArray(origString));
            diffBuffer.Fill(StringToSCArray(diffString));

            ScreenBufferDiff diff = ScreenBuffer.Diff(origBuffer, diffBuffer);

            bool[] actual = diff.DiffersToArray();

            //PrettyPrintAssertionTable(expected, actual);

            Assert.AreEqual(expected, actual);
        }
    }
}
