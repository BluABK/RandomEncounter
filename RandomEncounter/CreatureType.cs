using RandomEncounter.MoveTypes;
using System.Collections.Generic;

namespace RandomEncounter
{
    public class CreatureType : ICreatureType
    {
        /**
         * TypeMap:
         *      1:  Normal
         *      2:  Fire
         *      3:  Fighting
         *      4:  Water
         *      5:  Flying
         *      6:  Grass
         *      7:  Poison
         *      8:  Electric
         *      9:  Ground
         *      10: Psychic
         *      11: Rock
         *      12: Ice
         *      13: Bug
         *      14: Dragon
         *      15: Ghost
         *      16: Dark
         *      17: Steel
         *      18: Fairy
         */

        public EType Id;
        public List<EType> EffectiveAgainst;
        public List<EType> WeakAgainst;

        public CreatureType(EType id, List<EType> effectiveAgainst, List <EType> weakAgainst)
        {
            Id = id;
            EffectiveAgainst = effectiveAgainst;
            WeakAgainst = weakAgainst;
        }

        public bool IsEffectiveAgainst(CreatureType type)
        {
            return EffectiveAgainst.Contains(type.Id);
        }

        public bool IsWeakAgainst(CreatureType type)
        {
            return WeakAgainst.Contains(type.Id);
        }
    }
}