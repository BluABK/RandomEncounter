using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEncounter
{
    interface ICreature
    {
        public List<Move> Moves { get; set; }
        public string Name { get; set;}
        public int HP { get; set; }

        public BaseStats MyBaseStats { get; }

        public int Level { get; set; }

        public void TakeDamage(int dmg = 10);

    }
}
