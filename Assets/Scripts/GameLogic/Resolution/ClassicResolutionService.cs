using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESG.RockPaperScissors
{
    // An implementation of IResolutionService that uses the classic rules of Rock, Paper, Scissors.
    // It therefore assumes two players, but makes no assumptions about their human/npc nature.
    // Several methods in this implementation use the virtual keyword, because it could feasibly be extended.
    public class ClassicResolutionService : MonoBehaviour, IResolutionService
    {
        private const int REQUIRED_COUNT = 2;

        // An Inspector-set reference to the game config ScriptableObject
        [SerializeField] GameConfig _gameConfig;

        // An abstraction allowing the resolution service to pass the outcome on for purposes like
        // processing and display.
        protected IResolutionHandler _resolutionHandler;

        protected virtual void Awake()
        {
            if(!TryGetComponent(out _resolutionHandler))
            {
                Debug.LogError("ClassicResolutionService requires a component implementing IResolutionHandler");
            }
        }

        // This implementation resolves between two signals in the traditionally expected manner.
        public virtual int ResolveWinner(HandSignal[] signals)
        {
            int winnerIndex;

            if(signals.Length != REQUIRED_COUNT)
            {
                Debug.LogError("RPSResolutionService is designed only for one-on-one resolutions -" +
                    "signals.Length must be 2.");
            }

            if(signals[0] == signals[1])
            {
                // No winner in the case of matching signals
                winnerIndex = -1;
            }
            else
            {
                // This could be reduced to one math-based line of code, but that's a lot less readable.
                // We'll stick with something resembling the original switch statement.
                switch(signals[0])
                {
                    case HandSignal.Scissors:
                        winnerIndex = signals[1] == HandSignal.Paper ? 0 : 1;
                        break;
                    case HandSignal.Paper:
                        winnerIndex = signals[1] == HandSignal.Rock ? 0 : 1;
                        break;
                    case HandSignal.Rock:
                    default:
                        winnerIndex = signals[1] == HandSignal.Scissors ? 0 : 1;
                        break;
                }
            }

            RecordResolution(signals, winnerIndex);

            return winnerIndex;
        }

        // Compiles resolution data based on the determined winner of the match.
        // This includes any adjustments to the money of the involved players.
        protected virtual void RecordResolution(HandSignal[] signals, int winnerIndex)
        {
            ResolutionData resolutionData;
            int winValue = _gameConfig.BetAmount;

            resolutionData = new ResolutionData();
            resolutionData.signals = signals;
            resolutionData.moneyAdjustments = new int[REQUIRED_COUNT];

            for(int i = 0; i < REQUIRED_COUNT; i++)
            {
                if(winnerIndex == -1)
                {
                    resolutionData.moneyAdjustments[i] = 0;
                }
                else
                {
                    resolutionData.moneyAdjustments[i] = winnerIndex == i ? winValue : -winValue;
                }
            }

			// Pass the compiled data onto whatever implementation of IResolutionHandler has been provided.
            _resolutionHandler.HandleResolutionData(resolutionData);
        }
    }
}