using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESG.RockPaperScissors
{
    // Contains all of the data resulting from a match resolution
    public class ResolutionData
    {
        public HandSignal[] signals;
        public int[] moneyAdjustments;
    }
}