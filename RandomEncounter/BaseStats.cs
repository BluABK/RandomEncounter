using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEncounter
{
    public class BaseStats : IBaseStats
    {
        public int HP { get; set; }
        public int PhysicalAttack { get; set; }
        public int PhysicalDefense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }

        public BaseStats(int hp, int physAtk, int physDef, int spcAtk, int spcDef, int speed)
        {
            HP = hp;
            PhysicalAttack = physAtk;
            PhysicalDefense = physDef;
            SpecialAttack = spcAtk;
            SpecialDefense = spcDef;
            Speed = speed;
        }
    }
}
