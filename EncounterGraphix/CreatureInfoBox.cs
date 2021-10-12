using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ASCIIGraphix;
using ASCIIGraphix.GfxObjects;
using ASCIIGraphix.GfxObjects.Shapes.Box;
using RandomEncounter;

namespace EncounterGraphix
{
    public class CreatureInfoBox : BoxBordered
    {
        // Chars
        private readonly char hpBarCharFilled = '■';
        private readonly char hpBarCharEmpty = ' ';

        // Other
        private readonly Creature creature;
        private readonly int hpCharMax;

        public CreatureInfoBox(Creature creature, IScreen screen, int width = 48, int height = 4)
            : base(screen, width, height)
        {
            this.creature = creature;

            hpCharMax = InnerWidth - "HP: ".Length;

            // Init.
            Init();

            // Generate the box
            Generate();
        }

        private void Init()
        {
            // Fill the buffer with whitespace to avoid null-pointers.
            InnerBuffer.Fill(new ScreenChar(' ', MyScreen.BgColor, MyScreen.FgColor, MyScreen.DefaultBgColor, MyScreen.DefaultFgColor));
        }

        private string DrawHealthBar(int hpPercentage = 100)
        {
            if (hpPercentage is < 0 or > 100) throw new ArgumentException("Given percentage is OOB!");

            char[] barArr = new char[hpCharMax];
            double hpPercentageDouble = (double)hpPercentage;
            double percentPerChar = 100.0 / (float)hpCharMax; // 2.5% per char.

            int hpCharsToFill = (int)(hpPercentageDouble / percentPerChar);

            // Fill barArray with n barChar and rest whitespace.
            for (int i = 0; i < barArr.Length; i++)
            {
                if (i < hpCharsToFill)
                {
                    barArr[i] = hpBarCharFilled;
                }
                else
                {
                    barArr[i] = hpBarCharEmpty;
                }
            }

            return new string(barArr);
        }


        public string NameAndLevelString()
        {
            string levelString = $"Lv{creature.Level,1}";
            string nameAndLevelPadding = GetPadding(InnerWidth - creature.Name.Length - levelString.Length);

            return $"{creature.Name}{nameAndLevelPadding}{levelString}";
        }
        public ScreenChar[] NameAndLevelSCLine(ConsoleColor bgColor, ConsoleColor fgColor, ConsoleColor? defaultBgColor = null, ConsoleColor? defaultFgColor = null)
        {
            return ScreenChar.FromString(NameAndLevelString(), bgColor, fgColor, defaultBgColor, defaultFgColor);
        }

        public string HPRepresentationString()
        {
            int hpLeftPct = (int)(creature.HP / (float)creature.MaxHP * 100.0);

            return $"HP: {DrawHealthBar(hpLeftPct)}";
        }

        public ScreenChar[] HPRepresentationSCLine(ConsoleColor bgColor, ConsoleColor fgColor, ConsoleColor? defaultBgColor = null, ConsoleColor? defaultFgColor = null)
        {
            return ScreenChar.FromString(HPRepresentationString(), bgColor, fgColor, defaultBgColor, defaultFgColor);
        }

        protected int GetIntLengthDifference(int value, int max)
        {
            return max.ToString().Length - value.ToString().Length;
        }

        public string HPNumberString(int offset = 4)
        {
            int padding = GetIntLengthDifference(creature.HP, creature.MaxHP);

            string hpLeft = $"{creature.HP.ToString().PadLeft(padding)}";
            string hpTotal = $"{creature.MaxHP}";
            string hpNumbersLhs = $"{CharToString(' ', offset)}{hpLeft} / {hpTotal}";
            
            return $"{hpNumbersLhs}{GetPadding(InnerWidth - hpNumbersLhs.Length)}";
        }

        public ScreenChar[] HPNumberSCLine(ConsoleColor bgColor, ConsoleColor fgColor, ConsoleColor? defaultBgColor = null, ConsoleColor? defaultFgColor = null, int offset = 4)
        {
            return ScreenChar.FromString(HPNumberString(offset), bgColor, fgColor, defaultBgColor, defaultFgColor);
        }

        public void Generate(ConsoleColor? bgColor = null, ConsoleColor? fgColor = null, ConsoleColor? defaultBgColor = null, ConsoleColor? defaultFgColor = null)
        {
            bgColor ??= BgColor;
            fgColor ??= FgColor;
            defaultBgColor ??= DefaultBgColor;
            defaultFgColor ??= DefaultFgColor;

            int linesThatFits = InnerBuffer.LengthY; // Height minus top and bottom borders
            int y = 0;

            // Add lines while they fit, else stop adding lines.
            if (y < linesThatFits)
            {
                // 2nd line: Name and level.
                SetLine(NameAndLevelSCLine((ConsoleColor)bgColor, (ConsoleColor)fgColor, defaultBgColor, defaultFgColor), y);
                y++;
            }

            if (y < linesThatFits)
            {
                // 3rd line: HP represented as a bar.
                SetLine(HPRepresentationSCLine((ConsoleColor)bgColor, (ConsoleColor)fgColor, defaultBgColor, defaultFgColor), y);
                y++;
            }

            if (y < linesThatFits)
            {
                // 4th line: HP represented as numbers.
                SetLine(HPNumberSCLine((ConsoleColor)bgColor, (ConsoleColor)fgColor, defaultBgColor, defaultFgColor), y);
            }
        }

        /// <summary>
        ///
        /// Example output:
        /// ┌──────────────────────────────────────────────┐
        /// │ Creature Name                           Lv16 │
        /// │ HP: ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ │
        /// │     100 / 100                                │
        /// └──────────────────────────────────────────────┘
        /// </summary>
        /// <returns></returns>
        public string ToStringOld()
        {
            string[] lines = new string[Height];
            int linesThatFits = lines.Length - 2; // Height minus top and bottom borders
            int y = 0;

            // 1st line: Box border (top)
            lines[y] = MakeHorizontalBorderLine(top: true);
            y++;

            // Buffer lines. Adds lines while they fit, else stop adding lines.
            if (y <= linesThatFits)
            {
                // 2nd line: Name and level.
                lines[y] = MakeLine(NameAndLevelString());
                y++;
            }

            if (y <= linesThatFits)
            {
                // 3rd line: HP represented as a bar.
                lines[y] = MakeLine(HPRepresentationString());
                y++;
            }

            if (y <= linesThatFits)
            {
                // 4th line: HP represented as numbers.
                lines[y] = MakeLine(HPNumberString());
                y++;
            }

            // No more significant content lines to add, fill the rest with blanks until height req is fulfilled.
            while (y <= linesThatFits)
            {
                lines[y] = MakeLine(string.Empty);
                y++;
            }

            // Last line: Box border (bottom), don't append newline as we're on the final line.
            lines[y] = MakeHorizontalBorderLine(top: false, appendNewLine: false);

            return lines.Aggregate("", (current, line) => current + line);
        }

        public override ScreenBuffer ToMatrix()
        {
            return Buffer.Clone();
        }

        public override string ToString()
        {
            return Buffer.ToString();
        }
    }
}