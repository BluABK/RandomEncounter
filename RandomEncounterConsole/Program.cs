using ASCIIGraphix;
using RandomEncounter;
using RandomEncounter.Moves;
using RandomEncounter.MoveTypes;
using RandomEncounterConsole.Handlers;
using RandomEncounterConsole.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using EncounterGraphix;

namespace RandomEncounterConsole
{
    class Program
    {
        static void Main()
        {
            // Setup...
            SettingsHandler settingHandler = new();

            // Do stuff:
            Creature opponent = CreateDefaultOpponent(level: 20);
            Creature player = CreateDefaultPlayer(level: 15);

            AutoplayEncounterLog(player, opponent);

            // Experimental stuff
            Console.Clear();

            Screen screen = new(96, 28, bgColor: ConsoleColor.White, fgColor: ConsoleColor.Black);

            // (Re-)create creatures (due to having used the previous ones already).
            opponent = CreateDefaultOpponent(level: 20);
            player = CreateDefaultPlayer(level: 15);
            AutoplayEncounter(screen, player, opponent, 1000);
            Thread.Sleep(1500);

            screen.Clear();
            screen.ResetColorsToDefault();
            Thread.Sleep(2000);
            Console.Clear();
            Console.WriteLine("End of program.");
        }
        /// <summary>
        /// The old text log of an automatic encounter.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="opponent"></param>
        private static void AutoplayEncounterLog(Creature player, Creature opponent)
        {
            while (true)
            {
                new MovePlayback(opponent.RandomAttack(player)).Print(); // Hint: Will always be a useless self-targeted attack (can miss)
                new MovePlayback(player.RandomAttack(opponent)).Print();

                if (opponent.HP <= 0 || player.HP <= 0) break;
            }

            Console.WriteLine();
            Console.WriteLine("Oh wow, you won. I am so shocked...");
        }
        /// <summary>
        /// Performs a move and draws the resulting frame to screen.
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="player"></param>
        /// <param name="opponent"></param>
        /// <param name="playerIsAttacker"></param>
        /// <returns></returns>
        private static MoveOutcome ProcessMoveAndDrawFrame(Screen screen, Creature player, Creature opponent, bool playerIsAttacker)
        {
            MovePlayback movePlayback;

            if (playerIsAttacker)
            {
                movePlayback = new MovePlayback(player.RandomAttack(opponent));
            }
            else
            {
                movePlayback = new MovePlayback(opponent.RandomAttack(player));
            }
            

            MoveOutcome outcome = movePlayback.outcome;

            CombatFrame frame = new CombatFrame(player, opponent, screen);
            string frameStr = frame.ToString(); // TODO: Remove (debug)
            string atk = frame.AttackerInfo.ToString(); // TODO: Remove (debug)
            string def = frame.DefenderInfo.ToString(); // TODO: Remove (debug)
            screen.CopyToBuffer(frame.ToString());
            screen.Draw();

            return outcome;
        }

        /// <summary>
        /// ASCIIGraphix Screen-based encounter graphic (automatic playback)
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="player"></param>
        /// <param name="opponent"></param>
        private static void AutoplayEncounter(Screen screen, Creature player, Creature opponent, int frameDelay = 1000)
        {
            screen.Clear();

            while (true)
            {
                // Player makes move.
                ProcessMoveAndDrawFrame(screen, player, opponent, true);
                
                Thread.Sleep(frameDelay);

                // Opponent makes move.
                ProcessMoveAndDrawFrame(screen, player, opponent, false); // Hint: Will always be a useless self-targeted attack (can miss)

                if (opponent.HP <= 0 || player.HP <= 0) break;
            }
        }

        private static Creature CreateDefaultPlayer(int level)
        {
            return new Creature("Dragon Lizard", new FireType(), level, new BaseStats(39, 52, 43, 60, 50, 65), new List<Move>() { new FireAttack() });
        }

        private static Creature CreateDefaultOpponent(int level)
        {
            return new Creature("Dragon Fish", new WaterType(), level, new BaseStats(20, 10, 55, 15, 20, 80), new List<Move>() { new Flop() });
        }

        private static void ASCIIFun(Screen screen)
        {
            Console.Clear();

            // Draw!
            screen.Demo(2, 0);
        }

        private static string DrawHealthBar(int hpPercentage = 100)
        {
            if (hpPercentage < 0 || hpPercentage > 100) throw new ArgumentException("Given percentage is OOB!");
            
            int charMax = 40; // 40 chars == 100%, 
            char[] barArr = new char[charMax];
            double hpPercentageDouble = (double)hpPercentage;
            char barChar = '■';
            string bar = string.Empty;
            double percentPerChar = 100.0 / 40.0; // 2.5% per char.

            int hpCharsToFill = (int)(hpPercentageDouble / percentPerChar);
            
            // Fill barAray with n barChar and rest whitespace.
            for (int i = 0; i < barArr.Length; i++)
            {
                if (i < hpCharsToFill)
                {
                    barArr[i] = barChar;
                } else
                {
                    barArr[i] = ' ';
                }
            }

            return new string(barArr);
        }

        private static string GenerateFrame(int defenderHpPercent = 100)
        {
            string frame =
@$"┌──────────────────────────────────────────────┐                                                
│ Defender Name                           LvXX │                                                
│ HP: {DrawHealthBar(defenderHpPercent)} │                                                
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

            return frame;
        }

        private static void AnimatedDecreasingHealthBarDemo(Screen screen)
        {
            screen.Draw();

            const int turns = 3;
            const int delayMs = 300;
            const int turnDelay = 1000;
            for (int i = 0; i < turns; i++)
            {
                string prevFrame = string.Empty;
                for (int defenderHP = 100; defenderHP >= 0; defenderHP--)
                {
                    string thisFrame = GenerateFrame(defenderHP);

                    if (thisFrame != prevFrame) // Don't draw unchanged frames.
                    {
                        // Put frame in buffer.
                        screen.CopyToBuffer( GenerateFrame(defenderHP) );
                    
                        // Draw frame inside screen.
                        screen.Draw();

                        // Delay for next frame.
                        Thread.Sleep(delayMs);
                    }
                    
                    // Update previous frame.
                    prevFrame = thisFrame;
                }
                
                // Delay for animation restart.
                Thread.Sleep(turnDelay);
            }
        }
    }
}
