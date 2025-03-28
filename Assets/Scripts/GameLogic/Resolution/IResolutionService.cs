using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESG.RockPaperScissors
{
    public interface IResolutionService
    {
        // returns the index of the winner, or -1 in the case of a draw
        public int ResolveWinner(HandSignal[] signals);
    }
}