using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEncounter
{
    public interface IMove
    {
        public string Name { get; set; }
        public CreatureType Type { get; set; }
        public int Power { get; set; }
        public double Accuracy { get; set; }
        public bool TargetsDefender { get; set; }
    }
}
