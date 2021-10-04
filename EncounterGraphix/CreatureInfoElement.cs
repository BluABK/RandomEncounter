using System;
using ASCIIGraphix;
using RandomEncounter;

namespace EncounterGraphix
{
    public class CreatureInfoElement : FrameElement
    {
        private Creature creature;
        private int totalWidth;
        private int totalHeight;
        private bool padding = true;
        private int innerPaddingLeft = 1;
        private int innerPaddingRight = 1;
        private int ownOuterWidth = 48;
        private int ownInnerWidth;
        private char hpBarChar = '■';
        private int hpCharMax = 40; // 40 chars == 100%, 
        private bool showHpNumbers;
        private string paddingStrLeft;
        private string paddingStrRight;
        private bool padLeft;
        public CreatureInfoElement(Creature creature, int totalWidth, int totalHeight, bool padding = true, bool padLeft = false, bool showHpNumbers = true)
        {
            if (totalWidth != 96 || totalHeight != 28)
                throw new NotImplementedException(
                    "CreatureInfoElement only implemented to work with a 96x28 resolution!");

            this.showHpNumbers = showHpNumbers;

            this.creature = creature;
            this.totalWidth = totalWidth;
            this.totalHeight = totalHeight;
            this.padding = padding;
            this.padLeft = padLeft;
            
            SetElementPadding();

            //              Total width     |   Padding left side  Padding right side  |
            ownInnerWidth = ownOuterWidth - 1 - innerPaddingLeft - innerPaddingRight - 1;
        }

        private void SetElementPadding()
        {
            // Default both sides to empty string.
            paddingStrLeft = paddingStrRight = string.Empty;

            if (!padding) return;

            if (padLeft)
            {
                paddingStrLeft = GetPadding(totalWidth - ownOuterWidth);
            }
            else
            {
                paddingStrRight = GetPadding(totalWidth - ownOuterWidth);
            }
        }

        public static string GetPadding(int length)
        {
            string s = string.Empty;
            for (int i = 0; i < length; i++)
            {
                s += ' ';
            }

            int sLenDebug = s.Length;

            return s;
        }

        private string DrawHealthBar(int hpPercentage = 100)
        {
            if (hpPercentage is < 0 or > 100) throw new ArgumentException("Given percentage is OOB!");

            char[] barArr = new char[hpCharMax];
            double hpPercentageDouble = (double)hpPercentage;
            string bar = string.Empty;
            double percentPerChar = 100.0 / 40.0; // 2.5% per char.

            int hpCharsToFill = (int)(hpPercentageDouble / percentPerChar);

            // Fill barArray with n barChar and rest whitespace.
            for (int i = 0; i < barArr.Length; i++)
            {
                if (i < hpCharsToFill)
                {
                    barArr[i] = hpBarChar;
                }
                else
                {
                    barArr[i] = ' ';
                }
            }

            return new string(barArr);
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
            string s = string.Empty;

            // 2nd line: Name and level.
            string levelString = $"Lv{creature.Level.ToString().PadLeft(1)}";
            string nameAndLevelPadding = GetPadding(ownInnerWidth - creature.Name.Length - levelString.Length); // FIXME: Figure out why it is off by one, which is the reason for that last -1.
            string nameAndLevel = $"{creature.Name}{nameAndLevelPadding}{levelString}";

            // 3rd line: HP bar.

            // 4th line: HP numbers.
            string hpLeft = $"{creature.HP.ToString().PadLeft(3)}";
            string hpTotal = $"{creature.MaxHP.ToString().PadRight(3)}";
            string hpNumbersLhs = $"{hpLeft} / {hpTotal}";
            string hpNumbersString = $"{hpNumbersLhs}{GetPadding(ownInnerWidth - 4 - hpNumbersLhs.Length)}";

            int hpLeftPct = (int) ((float)creature.HP / (float)creature.MaxHP * 100.0);

            string line1 = paddingStrLeft + "┌──────────────────────────────────────────────┐" + paddingStrRight + Environment.NewLine;
            string line2 = paddingStrLeft + '│' + GetPadding(innerPaddingLeft) + nameAndLevel + GetPadding(innerPaddingRight) + '│' + paddingStrRight + Environment.NewLine;
            string line3 = paddingStrLeft + '│' + GetPadding(innerPaddingLeft) + $"HP: {DrawHealthBar(hpLeftPct)}" + GetPadding(innerPaddingRight) + '│' + paddingStrRight + Environment.NewLine;
            string line4 = showHpNumbers ? paddingStrLeft + '│' + GetPadding(innerPaddingLeft + 4) + hpNumbersString + GetPadding(innerPaddingRight) + '│' + paddingStrRight + Environment.NewLine : string.Empty;
            string line5 = paddingStrLeft + "└──────────────────────────────────────────────┘" + paddingStrRight;

            s = line1 + line2 + line3 + line4 + line5;

            return s;
        }
    }
}