using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIGraphix.Tests.Stubs
{
    public class ConsoleWrapperStub : IConsoleWrapper//, IConsoleWrapperStub
    {
        public char[,] Buffer { get; }
        public int CursorX = 0;
        public int CursorY = 0;

        public bool IsInputRedirected => throw new NotImplementedException();

        public int BufferHeight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int BufferWidth { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool CapsLock => throw new NotImplementedException();

        public int CursorLeft { get => 0; set => Console.CursorLeft = value; }
        public int CursorSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int CursorTop { get => 0; set => Console.CursorTop = value; }
        public bool CursorVisible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public TextWriter Error => throw new NotImplementedException();

        public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.White;
        public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;

        public TextReader In => throw new NotImplementedException();

        public Encoding InputEncoding { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsErrorRedirected => throw new NotImplementedException();

        public int WindowWidth { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsOutputRedirected => throw new NotImplementedException();

        public bool KeyAvailable => throw new NotImplementedException();

        public int LargestWindowHeight => throw new NotImplementedException();

        public int LargestWindowWidth => throw new NotImplementedException();

        public bool NumberLock => throw new NotImplementedException();

        public TextWriter Out => throw new NotImplementedException();

        public Encoding OutputEncoding { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool TreatControlCAsInput { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int WindowHeight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int WindowLeft { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int WindowTop { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event ConsoleCancelEventHandler CancelKeyPress;

        public ConsoleWrapperStub()
        {
            
        }

        //public ConsoleKeyInfo ReadKey()
        //{
        //    var result = keyCollection[this.keyIndex];
        //    keyIndex++;
        //    return new ConsoleKeyInfo((char)result, result, false, false, false);
        //}

        /// <summary>
        /// Iterator for splitting string on line breaks.
        /// 
        /// Using an iterator is preferred over string.split() as it is more efficient and avoids wasted memory (string.Split stores both copies).
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected static IEnumerable<string> IterateLines(string input)
        {
            if (input == null) yield break;

            using System.IO.StringReader reader = new(input);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }

        /// <summary>
        /// Iterator for splitting string on line breaks.
        /// 
        /// Using an iterator is preferred over string.split() as it is more efficient and avoids wasted memory (string.Split stores both copies).
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected static List<string> SplitToLines(string input)
        {
            List<string> lines = new();

            if (input == null) return lines;

            using (System.IO.StringReader reader = new(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines;
        }

        public void Beep()
        {
            throw new NotImplementedException();
        }

        public void Beep(int frequency, int duration)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public (int Left, int Top) GetCursorPosition()
        {
            throw new NotImplementedException();
        }

        public void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
        {
            throw new NotImplementedException();
        }

        public void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
        {
            throw new NotImplementedException();
        }

        public Stream OpenStandardError(int bufferSize)
        {
            throw new NotImplementedException();
        }

        public Stream OpenStandardError()
        {
            throw new NotImplementedException();
        }

        public Stream OpenStandardInput(int bufferSize)
        {
            throw new NotImplementedException();
        }

        public Stream OpenStandardInput()
        {
            throw new NotImplementedException();
        }

        public Stream OpenStandardOutput(int bufferSize)
        {
            throw new NotImplementedException();
        }

        public Stream OpenStandardOutput()
        {
            throw new NotImplementedException();
        }

        public int Read()
        {
            throw new NotImplementedException();
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            throw new NotImplementedException();
        }

        public ConsoleKeyInfo ReadKey()
        {
            throw new NotImplementedException();
        }

        public string ReadLine()
        {
            throw new NotImplementedException();
        }

        public void ResetColor()
        {
            throw new NotImplementedException();
        }

        public void SetBufferSize(int width, int height)
        {
            throw new NotImplementedException();
        }

        public void SetCursorPosition(int left, int top)
        {
            throw new NotImplementedException();
        }

        public void SetError(TextWriter newError)
        {
            throw new NotImplementedException();
        }

        public void SetIn(TextReader newIn)
        {
            throw new NotImplementedException();
        }

        public void SetOut(TextWriter newOut)
        {
            throw new NotImplementedException();
        }

        public void SetWindowPosition(int left, int top)
        {
            throw new NotImplementedException();
        }

        public void SetWindowSize(int width, int height)
        {
            throw new NotImplementedException();
        }

        public void Write(ulong value)
        {
            throw new NotImplementedException();
        }

        public void Write(bool value)
        {
            throw new NotImplementedException();
        }

        public void Write(char value)
        {
            throw new NotImplementedException();
        }

        public void Write(char[] buffer)
        {
            throw new NotImplementedException();
        }

        public void Write(int value)
        {
            throw new NotImplementedException();
        }

        public void Write(decimal value)
        {
            throw new NotImplementedException();
        }

        public void Write(long value)
        {
            throw new NotImplementedException();
        }

        public void Write(object value)
        {
            throw new NotImplementedException();
        }

        public void Write(float value)
        {
            throw new NotImplementedException();
        }

        public void Write(string value)
        {
            throw new NotImplementedException();
        }

        public void Write(string format, object arg0)
        {
            throw new NotImplementedException();
        }

        public void Write(string format, object arg0, object arg1)
        {
            throw new NotImplementedException();
        }

        public void Write(string format, object arg0, object arg1, object arg2)
        {
            throw new NotImplementedException();
        }

        public void Write(string format, params object[] arg)
        {
            throw new NotImplementedException();
        }

        public void Write(uint value)
        {
            throw new NotImplementedException();
        }

        public void Write(char[] buffer, int index, int count)
        {
            throw new NotImplementedException();
        }

        public void Write(double value)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(uint value)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string format, params object[] arg)
        {
            throw new NotImplementedException();
        }

        public void WriteLine()
        {
            throw new NotImplementedException();
        }

        public void WriteLine(bool value)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(char[] buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(char[] buffer, int index, int count)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(decimal value)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(double value)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(ulong value)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(int value)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(object value)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(float value)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string value)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string format, object arg0)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string format, object arg0, object arg1)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(long value)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(char value)
        {
            throw new NotImplementedException();
        }

        //public void Write(string data)
        //{
        //    Output += data;

        //    // Get lines
        //    List<string> lines = SplitToLines(data);

        //    if (lines.Count == 1)
        //    {
        //        // For each char in string line
        //        char[] chars = lines.ElementAt(0).ToCharArray();
        //        for (int i = 0; i < chars.Length; i++)
        //        {
        //            // Insert chars along X-axis
        //            Buffer[CursorX + i, CursorY] = chars[i];
        //            // Update Cursor X-axis position.
        //            CursorX++;
        //        }

        //        // No Y-Axis change happens on a line that is not terminated by linebreak.
        //        return;
        //    }

        //    // Insert line for each Y
        //    foreach (string stringLine in IterateLines(data))
        //    {
        //        // For each char in string line

        //    }

        //    // Insert each line along X
        //    for (int i = 0; i < data.Length; i++)
        //    {
        //        Buffer[CursorX+i, CursorY];
        //    }
        //    CursorPosition += data.Length;
        //}

        //public void SetCursorPosition(int x, int y)
        //{
        //    CursorPosition
        //}
    }
}
