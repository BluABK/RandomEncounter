using RandomEncounter;
using RandomEncounter.Moves;
using RandomEncounter.MoveTypes;
using System;
using System.Collections.Generic;

namespace RandomEncounterConsole
{
    class Program
    {
        static void Main()
        {
            Creature opponent = CreateDefaultOpponent(level: 20);
            Creature player = CreateDefaultPlayer(level: 15);

            while (true)
            {
                new MovePlayback(opponent.RandomAttack(player)).Print(); // Hint: Will always be a useless self-targeted attack (can miss)
                new MovePlayback(player.RandomAttack(opponent)).Print();

                if (opponent.HP <= 0 || player.HP <= 0) break;
            }

            Console.WriteLine();
            Console.WriteLine("Oh wow, you won. I am so shocked...");
        }

        static Creature CreateDefaultPlayer(int level)
        {
            return new Creature("Dragon Lizard", new FireType(), level, new BaseStats(39, 52, 43, 60, 50, 65), new List<Move>() { new FireAttack() });
        }

        static Creature CreateDefaultOpponent(int level)
        {
            return new Creature("Dragon Fish", new WaterType(), level, new BaseStats(20, 10, 55, 15, 20, 80), new List<Move>() { new Flop() });
        }
    }
}
