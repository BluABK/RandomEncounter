using RandomEncounter;
using RandomEncounterConsole.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEncounterConsole
{
    public class MovePlayback
    {
        public readonly MoveOutcome outcome;

        public MovePlayback(MoveOutcome outcome)
        {
            this.outcome = outcome;
        }

        public void Print()
        {
            Move moveUsed = outcome.MoveUsed;

            Console.Write($"{outcome.Attacker.Name} (HP: {outcome.Attacker.HP}/{outcome.Attacker.MaxHP}) used ");
            ConsoleUtils.PrintTypeColor(moveUsed.Type, moveUsed.Name);

            // Check if missed:
            if (!outcome.Hit)
            {
                Console.WriteLine($"{outcome.Attacker.Name} (HP: {outcome.Attacker.HP}/{outcome.Attacker.MaxHP}) missed!");

                return;
            }

            // Check if useless power, or power is oob:
            if (moveUsed.Power == 0)
            {
                Console.WriteLine($", but nothing happened...");

                return;
            }
            else if (moveUsed.Power < 0) throw new ArgumentOutOfRangeException($"Power must be >= 0, but got {moveUsed.Power}!");

            // Check if crit
            string critInfo = outcome.Crit ? " (critical hit!)" : string.Empty;
            

            Console.WriteLine(outcome.EffectivenessInfo());
            if (outcome.TargetsDefender)
            {
                Console.WriteLine($"{outcome.Defender.Name} was hit for {outcome.Damage} damage{critInfo}.");
            }
            else
            {
                Console.WriteLine($"{outcome.Attacker.Name} hit itself for {outcome.Damage} damage{critInfo}.");
            }

            if (outcome.Defender.HP == 0)
            {
                Console.WriteLine();
                Console.WriteLine($"{outcome.Defender.Name} fainted!");
            } else if (outcome.Attacker.HP == 0)
            {
                Console.WriteLine();
                Console.WriteLine($"{outcome.Attacker.Name} fainted!");
            }
        }
    }
}
