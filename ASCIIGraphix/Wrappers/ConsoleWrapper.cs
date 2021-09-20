#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIGraphix
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public bool IsInputRedirected => Console.IsInputRedirected;
        public int BufferHeight { get => Console.BufferHeight; [SupportedOSPlatform("windows")]set => Console.BufferHeight = value; }
        public int BufferWidth { get => Console.BufferWidth; [SupportedOSPlatform("windows")]set => Console.BufferWidth = value; }
        [SupportedOSPlatform("windows")]
        public bool CapsLock => Console.CapsLock;

        public int CursorLeft { get => Console.CursorLeft; set => Console.CursorLeft = value; }
        public int CursorSize { get => Console.CursorSize; [SupportedOSPlatform("windows")]set => Console.CursorSize = value; }
        public int CursorTop { get => Console.CursorTop; set => Console.CursorTop = value; }
        public bool CursorVisible { [SupportedOSPlatform("windows")]get => Console.CursorVisible; set => Console.CursorVisible = value; }
        public TextWriter Error => Console.Error;
        [UnsupportedOSPlatform("browser")]
        public ConsoleColor ForegroundColor { get => Console.ForegroundColor; set => Console.ForegroundColor = value; }
        [UnsupportedOSPlatform("browser")]
        public TextReader In { get => Console.In; }
        [UnsupportedOSPlatform("browser")]
        public Encoding InputEncoding { get => Console.InputEncoding; set => Console.InputEncoding = value; }
        public bool IsErrorRedirected { get => Console.IsErrorRedirected; }
        public int WindowWidth { get => Console.WindowWidth; [SupportedOSPlatform("windows")]set => Console.WindowWidth = value; }
        public bool IsOutputRedirected { get => Console.IsOutputRedirected; }
        public bool KeyAvailable { get => Console.KeyAvailable; }
        [UnsupportedOSPlatform("browser")]
        public int LargestWindowHeight { get => Console.LargestWindowHeight; }
        [UnsupportedOSPlatform("browser")]
        public int LargestWindowWidth { get => Console.LargestWindowWidth; }
        [SupportedOSPlatform("windows")]
        public bool NumberLock { get => Console.NumberLock; }
        public TextWriter Out { get => Console.Out; }
        public Encoding OutputEncoding { get => Console.OutputEncoding; set => Console.OutputEncoding = value; }
        public string Title { [SupportedOSPlatform("windows")]get => Console.Title; set => Console.Title = value; }
        [UnsupportedOSPlatform("browser")]
        public bool TreatControlCAsInput { get => Console.TreatControlCAsInput; set => Console.TreatControlCAsInput = value; }
        public int WindowHeight { get => Console.WindowHeight; [SupportedOSPlatform("windows")]set => Console.WindowHeight = value; }
        public int WindowLeft { get => Console.WindowLeft; [SupportedOSPlatform("windows")]set => Console.WindowLeft = value; }
        public int WindowTop { get => Console.WindowTop; [SupportedOSPlatform("windows")]set => Console.WindowTop = value; }
        [UnsupportedOSPlatform("browser")]
        public ConsoleColor BackgroundColor { get => Console.BackgroundColor; set => Console.BackgroundColor = value; }
        [UnsupportedOSPlatform("browser")]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public event ConsoleCancelEventHandler? CancelKeyPress
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            add { Console.CancelKeyPress += value; }
            remove { Console.CancelKeyPress -= value; }
        }

        public void Beep()
        {
            Console.Beep();
        }
        [SupportedOSPlatform("windows")]
        public void Beep(int frequency, int duration)
        {
            Console.Beep(frequency, duration);
        }

        public void Clear()
        {
            Console.Clear();
        }

        public (int Left, int Top) GetCursorPosition()
        {
            return Console.GetCursorPosition();
        }
        [SupportedOSPlatform("windows")]
        public void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
        {
            Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, sourceChar, sourceForeColor, sourceBackColor);
        }
        [SupportedOSPlatform("windows")]
        public void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
        {
            Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop);
        }

        public Stream OpenStandardError(int bufferSize)
        {
            return Console.OpenStandardError(bufferSize);
        }

        public Stream OpenStandardError()
        {
            return Console.OpenStandardError();
        }

        public Stream OpenStandardInput(int bufferSize)
        {
            return Console.OpenStandardInput(bufferSize);
        }

        public Stream OpenStandardInput()
        {
            return Console.OpenStandardInput();
        }

        public Stream OpenStandardOutput(int bufferSize)
        {
            return Console.OpenStandardOutput(bufferSize);
        }

        public Stream OpenStandardOutput()
        {
            return Console.OpenStandardOutput();
        }

        public int Read()
        {
            return Console.Read();
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public string? ReadLine()
        {
            return Console.ReadLine();
        }

        public void ResetColor()
        {
            Console.ResetColor();
        }
        [SupportedOSPlatform("windows")]
        public void SetBufferSize(int width, int height)
        {
            Console.SetBufferSize(width, height);
        }

        public void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }

        public void SetError(TextWriter newError)
        {
            Console.SetError(newError);
        }

        public void SetIn(TextReader newIn)
        {
            Console.SetIn(newIn);
        }

        public void SetOut(TextWriter newOut)
        {
            Console.SetOut(newOut);
        }
        [SupportedOSPlatform("windows")]
        public void SetWindowPosition(int left, int top)
        {
            Console.SetWindowPosition(left, top);
        }
        [SupportedOSPlatform("windows")]
        public void SetWindowSize(int width, int height)
        {
            Console.SetWindowSize(width, height);
        }

        public void Write(ulong value)
        {
            Console.Write(value);
        }

        public void Write(bool value)
        {
            Console.Write(value);
        }

        public void Write(char value)
        {
            Console.Write(value);
        }

        public void Write(char[]? buffer)
        {
            Console.Write(buffer);
        }

        public void Write(int value)
        {
            Console.Write(value);
        }

        public void Write(decimal value)
        {
            Console.Write(value);
        }

        public void Write(long value)
        {
            Console.Write(value);
        }

        public void Write(object? value)
        {
            Console.Write(value);
        }

        public void Write(float value)
        {
            Console.Write(value);
        }

        public void Write(string? value)
        {
            Console.Write(value);
        }

        public void Write(string format, object? arg0)
        {
            Console.Write(format, arg0);
        }

        public void Write(string format, object? arg0, object? arg1)
        {
            Console.Write(format, arg0, arg1);
        }

        public void Write(string format, object? arg0, object? arg1, object? arg2)
        {
            Console.Write(format, arg0, arg1, arg2);
        }

        public void Write(string format, params object?[]? arg)
        {
            Console.Write(format, arg);
        }

        public void Write(uint value)
        {
            Console.Write(value);
        }

        public void Write(char[] buffer, int index, int count)
        {
            Console.Write(buffer, index, count);
        }

        public void Write(double value)
        {
            Console.Write(value);
        }

        public void WriteLine(uint value)
        {
            Console.Write(value);
        }

        public void WriteLine(string format, params object?[]? arg)
        {
            Console.WriteLine(format, arg);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteLine(bool value)
        {
            Console.WriteLine(value);
        }

        public void WriteLine(char[]? buffer)
        {
            Console.WriteLine(buffer);
        }

        public void WriteLine(char[] buffer, int index, int count)
        {
            Console.WriteLine(buffer, index, count);
        }

        public void WriteLine(decimal value)
        {
            Console.WriteLine(value);
        }

        public void WriteLine(double value)
        {
            Console.WriteLine(value);
        }

        public void WriteLine(ulong value)
        {
            Console.WriteLine(value);
        }

        public void WriteLine(int value)
        {
            Console.WriteLine(value);
        }

        public void WriteLine(object? value)
        {
            Console.WriteLine(value);
        }

        public void WriteLine(float value)
        {
            Console.WriteLine(value);
        }

        public void WriteLine(string? value)
        {
            Console.WriteLine(value);
        }

        public void WriteLine(string format, object? arg0)
        {
            Console.WriteLine(format, arg0);
        }

        public void WriteLine(string format, object? arg0, object? arg1)
        {
            Console.WriteLine(format, arg0, arg1);
        }

        public void WriteLine(string format, object? arg0, object? arg1, object? arg2)
        {
            Console.WriteLine(format, arg0, arg1, arg2);
        }

        public void WriteLine(long value)
        {
            Console.WriteLine(value);
        }

        public void WriteLine(char value)
        {
            Console.WriteLine(value);
        }
    }
}
