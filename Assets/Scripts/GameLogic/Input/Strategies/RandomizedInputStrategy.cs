using System;

namespace ESG.RockPaperScissors
{
    public class RandomizedInputStrategy : RespondingInputStrategy
    {
        public override void RequestInput() {
            int randomInt = UnityEngine.Random.Range(0, 3);
            HandSignal intAsSignal = (HandSignal)randomInt;

            ProvideInput(intAsSignal);
        }
    }
}