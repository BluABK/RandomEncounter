using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEncounter.MoveTypes
{
    public class WaterType : CreatureType
    {
        public WaterType()
            : base(
                  id: EType.Water,
                  effectiveAgainst: new List<EType>() {
                      EType.Ground,
                      EType.Rock,
                      EType.Fire
                  },
                  weakAgainst: new List<EType>() {
                      EType.Grass,
                      EType.Electric
                  }
                  )
        {
        }
    }
}
