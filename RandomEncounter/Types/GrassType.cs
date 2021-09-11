using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEncounter.MoveTypes
{
    public class GrassType : CreatureType
    {
        public GrassType() 
            : base(
                  id: EType.Grass,
                  effectiveAgainst: new List<EType>() {
                      EType.Ground,
                      EType.Rock,
                      EType.Water
                  },
                  weakAgainst: new List<EType>() {
                      EType.Flying,
                      EType.Poison,
                      EType.Bug,
                      EType.Fire,
                      EType.Ice
                  })
        {
        }
    }
}
