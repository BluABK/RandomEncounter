using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEncounter
{
    class StatStage : IStatStage
    {
        /// <summary>
        /// Takes an integer value and returns it if between -6 and 6, else return the closest boundary.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int SanitiseStageInput(int value)
        {
            if (value >= -6 || value <= 6)
            {
                return value;
            }
            else if (value < -6)
            {
                return -6;
            }
            else
            {
                return 6;
            }
        }

        public double CalculateStageMultiplier(int stage)
        {
            return stage switch
            {
                -6 => 25 / 100,
                -5 => 28 / 100,
                -4 => 33 / 100,
                -3 => 40 / 100,
                -2 => 50 / 100,
                -1 => 66 / 100,
                0 => 100 / 100,
                1 => 150 / 100,
                2 => 200 / 100,
                3 => 250 / 100,
                4 => 300 / 100,
                5 => 350 / 100,
                6 => 400 / 100,
                _ => throw new ArgumentException($"Expected int value between -6 and 6, got: {stage}"),
            };
        }
    }
}
