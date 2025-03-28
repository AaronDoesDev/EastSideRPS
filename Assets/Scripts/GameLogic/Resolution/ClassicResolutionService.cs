using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESG.RockPaperScissors
{
    public class ClassicResolutionService : MonoBehaviour, IResolutionService
    {
        private const int REQUIRED_COUNT = 2;

        // A concretion I will refactor soon
        private PlayerDataService _dataService;

        protected void Awake() {
            if(!TryGetComponent(out _dataService)) {
                Debug.LogError("ClassicResolutionService requires a PlayerDataService component");
            }
        }

        public virtual int ResolveWinner(HandSignal[] signals) {
            int winnerIndex;

            if(signals.Length != REQUIRED_COUNT) {
                Debug.LogError("RPSResolutionService is designed only for one-on-one resolutions -" +
                    "signals.Length must be 2.");
            }

            if(signals[0] == signals[1]) {
                // no winner in the case of matching signals
                winnerIndex = -1;
            }
            else
            {
                switch(signals[0]) {
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

        protected void RecordResolution(HandSignal[] signals, int winnerIndex) {
            Hashtable gameUpdateData;

            gameUpdateData = new Hashtable();

            gameUpdateData["resultPlayer"] = signals[0];
            gameUpdateData["resultOpponent"] = signals[1];

            int coinChange = 0;
            if(winnerIndex == 0)
            {
                coinChange = 10;
            }
            else if(winnerIndex == 1)
            {
                coinChange = -10;
            }
            gameUpdateData["coinsAmountChange"] = coinChange;

			_dataService.HandleGameUpdateData(gameUpdateData);
        }
    }
}