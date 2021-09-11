using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEncounter
{
    public class Creature : ICreature
    {
        public string Name { get; set; }

        public int Level { get; set; }

        public CreatureType Type;
        public BaseStats MyBaseStats { get; }

        // Stats
        public int MaxHP            { get => ( (MyBaseStats.HP * 2 * Level)              / 100 ) + Level + 10; }
        public int PhysicalAttack   { get => ( (MyBaseStats.PhysicalAttack * 2 * Level)  / 100 ) + Level +  5; }
        public int PhysicalDefense  { get => ( (MyBaseStats.PhysicalDefense * 2 * Level) / 100 ) + Level +  5; }
        public int SpecialAttack    { get => ( (MyBaseStats.SpecialAttack * 2 * Level)   / 100 ) + Level +  5; }
        public int SpecialDefense   { get => ( (MyBaseStats.SpecialDefense * 2 * Level)  / 100 ) + Level +  5; }
        public int Speed            { get => ( (MyBaseStats.Speed * 2 * Level)           / 100 ) + Level +  5; }
        private int currentHP;
        public int HP 
        { 
            get => currentHP; 
            // Don't allow setting negative HP value.
            set => currentHP = value >= 0 ? value : 0; 
        }
        public List<Move> Moves { get; set; }
        public int Evasion { get; set; }
        public int Accuracy { get; set; }
        // FIXME: Implement below code (StatStage related)
        //private int accuracyStage { get; set => SanitiseStageInput(value); } = 0;
        //private int evasionStage { get; set; } = 0;
        //public double Accuracy 
        //{
        //    get => CalculateStageMultiplier(accuracyStage);
        //    //set => accuracyStage = SanitiseStageInput(value); 
        //}
        //public double Evasion
        //{
        //    get => CalculateStageMultiplier(evasionStage);
        //    //set => evasionStage = SanitiseStageInput(value);
        //}

        public Creature(string name, CreatureType type, int level, BaseStats baseStats, List<Move> moves)
        {
            Name = name;
            Type = type;
            Level = level > 0 ? level : 1;
            MyBaseStats = baseStats;
            currentHP = MaxHP;
            Moves = moves;
            Accuracy = 100 / 100; // FIXME: Calc based on stage!!
            Evasion = 100 / 100; // FIXME: Calc based on stage!!
        }

        public void TakeDamage(int dmg = 10)
        {
            HP -= dmg;
        }

        public MoveOutcome RandomAttack(Creature opponent)
        {
             return Moves.ElementAt(new Random().Next(Moves.Count)).Execute(this, opponent);
        }
    }
}
