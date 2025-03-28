using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESG.RockPaperScissors
{
    public class ClassicResolutionService : MonoBehaviour, IResolutionService
    {
        private const int REQUIRED_COUNT = 2;

        private IResolutionHandler _resolutionHandler;

        protected void Awake() {
            if(!TryGetComponent(out _resolutionHandler)) {
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
            ResolutionData resolutionData;
            int winValue = 10; // a magic number that I'll source properly soon
            int playerCount = signals.Length;

            resolutionData = new ResolutionData();
            resolutionData.signals = signals;
            resolutionData.moneyAdjustments = new int[playerCount];

            for(int i = 0; i < playerCount; i++) {
                if(winnerIndex == -1)
                {
                    resolutionData.moneyAdjustments[i] = 0;
                }
                else
                {
                    resolutionData.moneyAdjustments[i] = winnerIndex == i ? winValue : -winValue;
                }
            }

			_resolutionHandler.HandleResolutionData(resolutionData);
        }
    }
}