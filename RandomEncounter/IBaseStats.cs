using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEncounter
{
    public interface IBaseStats
    {
        public int HP { get; set; }
        public int PhysicalAttack { get; set; }
        public int PhysicalDefense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
    }
}
