using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESG.RockPaperScissors
{
    // Classes implementing this interface may process raw inputs into
    // usable data regarding who has won or lost a match, and the ensuing changes.
    public interface IResolutionService
    {
        // returns the index of the winner, or -1 in the case of a draw
        public int ResolveWinner(HandSignal[] signals);
    }
}