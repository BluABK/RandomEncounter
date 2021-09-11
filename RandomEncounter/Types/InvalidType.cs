using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEncounter.Types
{
    public class InvalidType : CreatureType
    {
        public InvalidType()
            : base (id: EType.Invalid, effectiveAgainst: null, weakAgainst: null)
        {

        }
    }
}
