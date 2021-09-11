using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEncounter.MoveTypes
{
    public class FireType : CreatureType
    {
        public FireType()
            : base(
                  id: EType.Fire,
                  effectiveAgainst: new List<EType>() { 
                      EType.Bug,
                      EType.Steel,
                      EType.Grass,
                      EType.Ice 
                  },
                  weakAgainst: new List<EType>() { 
                      EType.Ground,
                      EType.Rock,
                      EType.Water 
                  })
        {

        }
    }
}
