using RandomEncounter.MoveTypes;
using RandomEncounter.Moves;
using System;

namespace RandomEncounter
{
    public class Move : IMove
    {
        public string Name { get; set; }
        public CreatureType Type { get; set; }
        public int Power { get; set; }
        public double Accuracy { get; set; }
        public bool TargetsDefender { get; set; }
        public float CritChance = 2;

        public Move(string name, CreatureType type, int power, double accuracy, bool targetsDefender = true)
        {
            Name = name;
            Type = type;
            Power = power;
            Accuracy = accuracy;
            TargetsDefender = targetsDefender;
        }

        public MoveOutcome Execute(Creature attacker, Creature defender)
        {
            return new MoveOutcome(attacker, defender, this, TargetsDefender);
        }
    }
}