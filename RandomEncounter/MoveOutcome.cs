using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEncounter
{
    public class MoveOutcome : IMoveOutcome
    {
        // Readonly backing properties.
        private readonly Creature attacker;
        private readonly Creature defender;
        private readonly Move moveUsed;
        private readonly bool hit;
        private readonly bool crit;
        private readonly int damage;
        private readonly bool targetsDefender;
        
        // Getters
        public Creature Attacker { get => attacker; }
        public Creature Defender { get => defender; }
        public Move MoveUsed { get => moveUsed; }
        public bool Hit { get => hit; }
        public bool Crit { get => crit; }
        public int Damage { get => damage; }
        public bool TargetsDefender { get => targetsDefender; }

        public MoveOutcome(Creature attacker, Creature defender, Move moveUsed, bool targetsDefender)
        {
            // Collect and calculate all pertinent info.
            this.attacker = attacker;
            this.defender = defender;
            this.moveUsed = moveUsed;
            this.targetsDefender = targetsDefender;
            hit = DetermineIfHit();
            crit = DetermineIfCrit();
            damage = CalculateDamage();

            // Deal damage, if applicable.
            if (hit && moveUsed.Power > 0) Defender.TakeDamage(Damage);
        }

        protected private bool DetermineIfCrit()
        {
            // Whether a move scores a critical hit is determined by comparing a 1-byte random number (0 to 255) against a 1-byte threshold value (also 0 to 255).
            int threshhold = Attacker.Speed / 2;

            // If the threshold would exceed 255, it instead becomes 255.
            // Consequently, the maximum possible chance of landing a critical hit is 255/256.
            // (If the generated random number is 255, that number can never be less than the threshold, regardless of the value of the threshold.)
            if (threshhold > 255) threshhold = 255;

            // If the random number is strictly less than the threshold, the Pokémon scores a critical hit.
            // For a given threshold value T, the probability of scoring a critical hit is T/256.
            if (new Random().Next(256) < (threshhold / 256)) return true;

            return false;
        }

        /**
         * This can be 0 (ineffective); 0.25, 0.5 (not very effective); 1 (normally effective); 2, or 4 (super effective), depending on both the move's and target's types.
         */
        public double CalculateTypeEffectiveness()
        {
            double effectiveness = 1.0;

            if (Defender.Type.IsEffectiveAgainst(MoveUsed.Type))
            {
                effectiveness = 0.5;
            }
            else if (Defender.Type.IsWeakAgainst(MoveUsed.Type))
            {
                effectiveness = 2.0;
            }

            return effectiveness;

        }

        protected private int CalculateDamage()
        {
            double ivAndEvSubstitute = new Random().Next(1, 5); // Compensate for the lack of IV, EV and such, which makes damage calc far too low. // FIXME: Implemented these!

            int critModifier = Crit ? 2 : 1;
            double random = new Random().Next(85, 101) / 100.0; // Random factor between 0.85 and 1.00 (inclusive).
            double typeEffectiveness = CalculateTypeEffectiveness();

            double preciseDmg = ((((2 * (attacker.Level / 5)) + 2) * MoveUsed.Power * (attacker.PhysicalAttack / defender.PhysicalDefense) / 50) + 2) * critModifier * random * typeEffectiveness + ivAndEvSubstitute;

            return Convert.ToInt32(preciseDmg);
        }

        public string EffectivenessInfo()
        {
            string effectivenessInfo = "!";

            if (Defender.Type.IsEffectiveAgainst(MoveUsed.Type))
            {
                effectivenessInfo = ", it was not very effective...";
            }
            else if (Defender.Type.IsWeakAgainst(MoveUsed.Type))
            {
                effectivenessInfo = ", it was super effective!";
            }

            return $"{effectivenessInfo}";
        }

        protected private int CalculateAccuracy()
        {
            return Convert.ToInt32((MoveUsed.Accuracy * Attacker.Accuracy * Defender.Evasion));
        }

        protected private bool DetermineIfHit()
        {
            int modifiedAccuracy = CalculateAccuracy();

            int r = new Random().Next(1, 256);

            return modifiedAccuracy > r;
        }
    }
}
