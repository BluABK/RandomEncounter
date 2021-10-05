using System;
using ASCIIGraphix;
using ASCIIGraphix.GfxObjects;
using RandomEncounter;

namespace EncounterGraphix
{
    public class CreatureInfoBox : Box
    {
        // Chars
        private readonly char hpBarCharFilled = '■';
        private readonly char hpBarCharEmpty = ' ';

        // Other
        private readonly Creature creature;
        private readonly int hpCharMax; // 40 chars == 100%, 
        private readonly bool showHpNumbers;

        public CreatureInfoBox(Creature creature, int screenWidth, int screenHeight, int width = 48, int height = 4, bool padding = true, bool hasPaddingLeft = false, bool showHpNumbers = true)
            : base(screenWidth, screenHeight, width, height, padding, hasPaddingLeft)
        {
            if (screenWidth != 96 || screenHeight != 28)
                throw new NotImplementedException(
                    "CreatureInfoBox only implemented to work with a 96x28 resolution!");

            Width = width;

            if (showHpNumbers)
            {
                Height = height;
            }
            else
            {
                Height = height - 1;
            }

            InnerPaddingLeft = 1;
            InnerPaddingRight = 1;

            this.showHpNumbers = showHpNumbers;

            this.creature = creature;

            hpCharMax = 40; // TODO: FIXME: Hardcoded

            SetElementPadding();
        }

        protected string MakeLine(string s, bool appendNewLine = true)
        {
            return PaddingStrLeft + '│' + GetPadding(InnerPaddingLeft) + s + GetPadding(InnerPaddingRight) + '│' + PaddingStrRight + (appendNewLine ? Environment.NewLine : "");
        }

        protected string MakeHorizontalBorderLine(bool top, bool appendNewLine = true)
        {
            if (top)
            {
                return PaddingStrLeft + $"{BorderCornerTopLeft}{CharToString(BorderHorizontal, HorizBorderWidth)}{BorderCornerTopRight}" + PaddingStrRight + (appendNewLine ? Environment.NewLine : "");
            }

            return PaddingStrLeft + $"{BorderCornerBottomLeft}{CharToString(BorderHorizontal, HorizBorderWidth)}{BorderCornerBottomRight}" + PaddingStrRight + (appendNewLine ? Environment.NewLine : "");
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

        public string HPRepresentationString()
        {
            int hpLeftPct = (int)(creature.HP / (float)creature.MaxHP * 100.0);

            return $"HP: {DrawHealthBar(hpLeftPct)}";
        }

        protected int GetIntLengthDifference(int value, int max)
        {
            return max.ToString().Length - value.ToString().Length;
        }

        public string HPNumberString(int offset = 4)
        {
            // Only offset 
            //if (creature.MaxHP <= 100) offset--;
            int padding = GetIntLengthDifference(creature.HP, creature.MaxHP);

            string hpLeft = $"{creature.HP.ToString().PadLeft(padding)}";
            string hpTotal = $"{creature.MaxHP}";
            string hpNumbersLhs = $"{CharToString(' ', offset)}{hpLeft} / {hpTotal}";
            
            return $"{hpNumbersLhs}{GetPadding(InnerWidth - hpNumbersLhs.Length)}";
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
        public override string ToString()
        {
            // 1st line: Box border (top)
            string line1 = MakeHorizontalBorderLine(top: true);
            // 2nd line: Name and level.
            string line2 = MakeLine(NameAndLevelString());
            // 3rd line: HP represented as a bar.
            string line3 = MakeLine(HPRepresentationString());
            // 4th line (optional): HP represented as numbers.
            string line4 = showHpNumbers ? MakeLine(HPNumberString()) : string.Empty;
            // Last line: Box border (bottom), don't append newline as we're on the final line.
            string line5 = MakeHorizontalBorderLine(top: false, appendNewLine: false);

            return line1 + line2 + line3 + line4 + line5;
        }

        public ScreenChar[,] ToMatrix()
        {
            // TODO: implement body
            ScreenChar[,] buf = new ScreenChar[Width, Height];

            return buf;
        }
    }
}