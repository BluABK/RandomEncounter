using System;
using System.Collections.Generic;
using ASCIIGraphix;
using ASCIIGraphix.GfxObjects;
using ASCIIGraphix.GfxObjects.Shapes.Box;
using RandomEncounter;

namespace EncounterGraphix
{
    public class CombatFrame : ICombatFrame
    {
        public CreatureInfoBox OpponentInfoBox { get; private set ; }
        public CreatureInfoBox PlayerInfoBox { get; private set; }
        public Box OpponentInfoBoxSpacer { get; private set; }
        public Box PlayerInfoBoxSpacer { get; private set; }
        public Box CenterBox { get; private set; }
        public BoxBordered StatusAndActionsBox { get; private set; } // TODO: FIXME: Implement sub-boxes, should probably be its own class that is sent into this ctor.
        public List<GfxObject> Elements { get; private set; }

        protected IScreen MyScreen;
        protected int CreatureInfoBoxWidth;
        protected readonly Creature PlayerCreature;
        protected readonly Creature OpponentCreature;
        private ScreenBuffer buf;
        public CombatFrame(Creature playerCreature, Creature opponentCreature, IScreen screen, int creatureInfoBoxWidth = 48, char? initChar = null)
        {
            MyScreen = screen;
            CreatureInfoBoxWidth = creatureInfoBoxWidth;
            PlayerCreature = playerCreature;
            OpponentCreature = opponentCreature;

            Generate(initChar: initChar);

            Elements = new List<GfxObject>
            {
                OpponentInfoBox,
                OpponentInfoBoxSpacer,
                CenterBox,
                PlayerInfoBoxSpacer,
                PlayerInfoBox,
                StatusAndActionsBox
            };
        }
        /// <summary>
        /// Generates a Combat Frame and puts it into the internal buffer of this class.
        /// </summary>
        /// <param name="playerInfoBoxHeight">Height of player creature info box.</param>
        /// <param name="opponentInfoBoxHeight">Height of opponent creature info box.</param>
        /// <param name="initChar">If set, initially fill the entire buffer with this char.</param>
        public void Generate(int playerInfoBoxHeight = 5, int opponentInfoBoxHeight = 4, char? initChar = null)
        {
            // Create a new buffer.
            buf = new ScreenBuffer(MyScreen.Width, MyScreen.Height, MyScreen.DefaultBgColor, MyScreen.DefaultFgColor, autofillChar: initChar);

            // Define creature info boxes.
            PlayerInfoBox = new CreatureInfoBox(PlayerCreature, MyScreen, width: CreatureInfoBoxWidth, height: playerInfoBoxHeight);
            OpponentInfoBox = new CreatureInfoBox(OpponentCreature, MyScreen, width: CreatureInfoBoxWidth, height: opponentInfoBoxHeight);

            // Define creature info box spacers, based on data from their respective info boxes.
            OpponentInfoBoxSpacer = new Box(MyScreen, Math.Abs(MyScreen.Width - OpponentInfoBox.Width), OpponentInfoBox.Height);
            PlayerInfoBoxSpacer = new Box(MyScreen, Math.Abs(MyScreen.Width - PlayerInfoBox.Width), PlayerInfoBox.Height);

            // Define the bottom box which contains most of the interactive UI and status.
            StatusAndActionsBox = new BoxBordered(MyScreen, Math.Abs(MyScreen.Width), 8);

            // Fill the remaining center area with padding.
            CenterBox = new Box(MyScreen, MyScreen.Width, MyScreen.Height - OpponentInfoBox.Height - PlayerInfoBox.Height - StatusAndActionsBox.Height);

            // Insert the boxes into the buffer, using X and Y offsets for positioning.
            buf.Insert(OpponentInfoBox);
            buf.Insert(OpponentInfoBoxSpacer, OpponentInfoBox.Width);
            buf.Insert(CenterBox, 0, OpponentInfoBox.Height);
            buf.Insert(PlayerInfoBoxSpacer, 0, MyScreen.Height - StatusAndActionsBox.Height - PlayerInfoBox.Height);
            buf.Insert(PlayerInfoBox, PlayerInfoBoxSpacer.Width, MyScreen.Height - StatusAndActionsBox.Height - PlayerInfoBox.Height);
            buf.Insert(StatusAndActionsBox, 0, MyScreen.Height - StatusAndActionsBox.Height);
        }

        /// <summary>
        ///
        /// Example output:
        /// ┌──────────────────────────────────────────────┐                                                
        /// │ Opponent Name                           LvXX │                                                
        /// │ HP: ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ │                                                
        /// └──────────────────────────────────────────────┘                                                
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        ///                                                 ┌──────────────────────────────────────────────┐
        ///                                                 │ Attacker Name                           LvXX │
        ///                                                 │ HP: ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ │
        ///                                                 │     XXX / XXX                                │
        ///                                                 └──────────────────────────────────────────────┘
        /// ╔════════════════════════════════════╦═════════════════════════════╤═══════════════════════════╗
        /// ║                                    ║ ┌──── ─┬─ ┌──── ┬   ┬ ──┬── │                           ║
        /// ║                                    ║ ├────  │  │ ──┐ ├───┤   │   │            ITEM           ║
        /// ║                                    ║ ┴     ─┴─ └───┘ ┴   ┴   ┴   │                           ║
        /// ║                                    ╟─────────────────────────────┼───────────────────────────╢
        /// ║                                    ║                             │                           ║
        /// ║                                    ║             INFO            │            QUIT           ║
        /// ║                                    ║                             │                           ║
        /// ╚════════════════════════════════════╩═════════════════════════════╧═══════════════════════════╝
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return buf.ToString();
        }
    }
}
