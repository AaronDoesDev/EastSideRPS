using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESG.RockPaperScissors
{
    // This player data service implements the general PlayerDataService abstract class,
    // as well as an interface allowing it to communicate with the match resolution service
    public class HumanVersusHumanDataService : PlayerDataService, IResolutionHandler
    {
        private const int PLAYER_COUNT = 2;
        private const int P1_INDEX = 0;
        private const int P2_INDEX = 1;
        private const int TEST_P1_UNIQUE_ID = 0;
        private const int TEST_P2_UNIQUE_ID = 1;

        // An Inspector-set reference to the game config ScriptableObject
        [SerializeField] GameConfig _gameConfig;

        protected void Awake()
        {
            _players = new Player[PLAYER_COUNT];

            PlayerDataLoadStrategy p1DataLoader = new SimulateHumanLoadStrategy();
			p1DataLoader.OnLoaded += OnPlayerDataLoaded;
			p1DataLoader.LoadPlayerData(TEST_P1_UNIQUE_ID);

			PlayerDataLoadStrategy p2DataLoader = new SimulateHumanLoadStrategy();
			p2DataLoader.OnLoaded += OnPlayerDataLoaded;
			p2DataLoader.LoadPlayerData(TEST_P2_UNIQUE_ID);
        }

        public void HandleResolutionData(ResolutionData resolutionData)
        {
            int playerCount = resolutionData.signals.Length;
            for(int i = 0; i < playerCount; i++)
            {
                _players[i].lastUsedSignal = resolutionData.signals[i];
                _players[i].AdjustMoney(resolutionData.moneyAdjustments[i]);
            }
        }

        private void OnPlayerDataLoaded(LoadablePlayerData loadedPlayerData)
        {
            // This would have actual generalized handling in a real world scenario. A switch statement
            // the unique player id is only acceptable in this extremely small-scoped demo.
            switch(loadedPlayerData.uniqueId)
            {
                case TEST_P1_UNIQUE_ID:
                    _players[P1_INDEX] = new Player(loadedPlayerData);
                    _players[P1_INDEX].AdjustMoney(_gameConfig.StartingMoney);
                    break;
                case TEST_P2_UNIQUE_ID:
                default:
                    _players[P2_INDEX] = new Player(loadedPlayerData);
                    _players[P2_INDEX].AdjustMoney(_gameConfig.StartingMoney);
                    break;
            }
        }
    }
}