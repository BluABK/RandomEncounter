using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEncounter
{
    public interface ICreatureType
    {
        public static readonly EType Id;
        public static readonly List<EType> EffectiveAgainst;
        public static readonly List<EType> WeakAgainst;

        public bool IsEffectiveAgainst(CreatureType type);

        public bool IsWeakAgainst(CreatureType type);
    }
}
