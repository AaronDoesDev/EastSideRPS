using System;

namespace ESG.RockPaperScissors
{
    // An input strategy that simply randomizes between all possible choices on demand.
    public class RandomizedInputStrategy : RespondingInputStrategy
    {
        public override void RequestInput()
        {
            int randomInt = UnityEngine.Random.Range(0, 3);
            HandSignal intAsSignal = (HandSignal)randomInt;

            ProvideInput(intAsSignal);
        }
    }
}