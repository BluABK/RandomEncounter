using System;

namespace ASCIIGraphix.Tests.Stubs
{
    public interface IConsoleWrapperStub
    {
        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
    }
}