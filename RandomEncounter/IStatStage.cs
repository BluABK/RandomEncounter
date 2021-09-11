using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEncounter
{
    public interface IStatStage
    {
        public int SanitiseStageInput(int value);
        public double CalculateStageMultiplier(int stage);
    }
}
