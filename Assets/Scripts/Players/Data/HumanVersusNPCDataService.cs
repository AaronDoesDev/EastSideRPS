using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESG.RockPaperScissors
{
    // The player data service does not implement an abstract class, but implements
    // two interfaces that allow it to cooperate with the UI and data services
    // without any concrete dependencies
    public class HumanVersusNPCDataService : MonoBehaviour, IDisplayablePlayerDataService, IResolutionHandler
    {
        private const int PLAYER_COUNT = 2;
        private const int HUMAN_INDEX = 0;
        private const int NPC_INDEX = 1;
        private const int TEST_HUMAN_UNIQUE_ID = 0;
        private const int TEST_NPC_UNIQUE_ID = 1;

        private Player[] _players;

        public string GetDisplayName(int playerIndex)
        {
            return _players[playerIndex].GetDisplayName();
        }

        public int GetCoins(int playerIndex)
        {
            return _players[playerIndex].GetMoney();
        }

        public HandSignal GetLastHandSignal(int playerIndex)
        {
            return _players[playerIndex].lastUsedSignal;
        }

        protected void Awake()
        {
            _players = new Player[PLAYER_COUNT];

            PlayerDataLoadStrategy userDataLoader = new SimulateHumanLoadStrategy();
			userDataLoader.OnLoaded += OnUserDataLoaded;
			userDataLoader.LoadPlayerData(TEST_HUMAN_UNIQUE_ID);

			PlayerDataLoadStrategy npcDataLoader = new SimulateNPCLoadStrategy();
			npcDataLoader.OnLoaded += OnNPCDataLoaded;
			npcDataLoader.LoadPlayerData(TEST_NPC_UNIQUE_ID);
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

        private void OnUserDataLoaded(LoadablePlayerData loadedPlayerData)
        {
            _players[HUMAN_INDEX] = new Player(loadedPlayerData);
        }

		private void OnNPCDataLoaded(LoadablePlayerData loadedPlayerData)
		{
			_players[NPC_INDEX] = new Player(loadedPlayerData);
		}
    }
}