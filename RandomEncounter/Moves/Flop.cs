using RandomEncounter.MoveTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEncounter.Moves
{
    public class Flop : Move
    {
        public Flop() 
            : base(name: "Flop", type: new WaterType(), power: 0, accuracy: 255, targetsDefender: false)
        {

        }
    }
}
