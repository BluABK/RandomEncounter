using RandomEncounter.MoveTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEncounter.Moves
{
    public class FireAttack : Move
    {
        public FireAttack()
            : base(name: "Fire Attack", type: new FireType(), power: 90, accuracy: 255, targetsDefender: true)
        {

        }
    }
}
