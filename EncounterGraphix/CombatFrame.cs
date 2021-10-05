using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASCIIGraphix;
using ASCIIGraphix.GfxObjects;
using RandomEncounter;

namespace EncounterGraphix
{
    public class CombatFrame : ICombatFrame
    {
        public CreatureInfoBox Defender { get; private set ; }
        public CreatureInfoBox Attacker { get; private set; }
        public List<GfxObject> Elements { get; private set; }

        public CombatFrame(Creature attacker, Creature defender, Screen screen)
        {
            Attacker = new CreatureInfoBox(attacker, screen.Width, screen.Height, hasPaddingLeft: true);
            Defender = new CreatureInfoBox(defender, screen.Width, screen.Height, showHpNumbers: false);

            Elements = new List<GfxObject>
            {
                Attacker,
                Defender
            };
        }

        /// <summary>
        ///
        /// Example output:
        /// ┌──────────────────────────────────────────────┐                                                
        /// │ Defender Name                           LvXX │                                                
        /// │ HP: ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ │                                                
        /// └──────────────────────────────────────────────┘                                                
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        ///                                                 ┌──────────────────────────────────────────────┐
        ///                                                 │ Attacker Name                           LvXX │
        ///                                                 │ HP: ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ │
        ///                                                 │     XXX / XXX                                │
        ///                                                 └──────────────────────────────────────────────┘
        /// ╔════════════════════════════════════╦═════════════════════════════╤═══════════════════════════╗
        /// ║                                    ║ ┌──── ─┬─ ┌──── ┬   ┬ ──┬── │                           ║
        /// ║                                    ║ ├────  │  │ ──┐ ├───┤   │   │            ITEM           ║
        /// ║                                    ║ ┴     ─┴─ └───┘ ┴   ┴   ┴   │                           ║
        /// ║                                    ╟─────────────────────────────┼───────────────────────────╢
        /// ║                                    ║                             │                           ║
        /// ║                                    ║             INFO            │            QUIT           ║
        /// ║                                    ║                             │                           ║
        /// ╚════════════════════════════════════╩═════════════════════════════╧═══════════════════════════╝
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            

            string midSection =
                @"                                                                                                
                                                                                                
                                                                                                
                                                                                                
                                                                                                
                                                                                                
                                                                                                
                                                                                                
                                                                                                
                                                                                                ";

            string frame =
@$"{Defender}
{midSection}
{Attacker}
╔════════════════════════════════════╦═════════════════════════════╤═══════════════════════════╗
║                                    ║ ┌──── ─┬─ ┌──── ┬   ┬ ──┬── │                           ║
║                                    ║ ├────  │  │ ──┐ ├───┤   │   │            ITEM           ║
║                                    ║ ┴     ─┴─ └───┘ ┴   ┴   ┴   │                           ║
║                                    ╟─────────────────────────────┼───────────────────────────╢
║                                    ║                             │                           ║
║                                    ║             INFO            │            QUIT           ║
║                                    ║                             │                           ║
╚════════════════════════════════════╩═════════════════════════════╧═══════════════════════════╝";

            return frame;
        }
    }
}
