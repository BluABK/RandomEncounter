using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASCIIGraphix;
using RandomEncounter;

namespace EncounterGraphix
{
    public class CombatFrame : ICombatFrame
    {
        public CreatureInfoElement DefenderInfo { get; private set ; }
        public CreatureInfoElement AttackerInfo { get; private set; }
        public List<FrameElement> Elements { get; private set; }

        public CombatFrame(Creature attacker, Creature defender, Screen screen)
        {
            AttackerInfo = new CreatureInfoElement(attacker, screen.Width, screen.Height, padLeft: true);
            DefenderInfo = new CreatureInfoElement(defender, screen.Width, screen.Height, showHpNumbers: false);

            Elements = new List<FrameElement>
            {
                AttackerInfo,
                DefenderInfo
            };
        }

        private string GenerateDefenderSection()
        {
            string lines = string.Empty;

            //foreach(string line in DefenderInfo.ToString())

            return lines;
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
@$"{DefenderInfo}
{midSection}
{AttackerInfo}
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
