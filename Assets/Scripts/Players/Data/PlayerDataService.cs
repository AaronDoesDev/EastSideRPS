using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESG.RockPaperScissors
{
    public class PlayerDataService : MonoBehaviour, IDisplayablePlayerDataService, IResolutionHandler
    {
        private Player[] _players;

        public string GetDisplayName(int playerIndex) {
            return _players[playerIndex].GetDisplayName();
        }

        public int GetCoins(int playerIndex) {
            return _players[playerIndex].GetMoney();
        }

        public HandSignal GetLastHandSignal(int playerIndex) {
            return _players[playerIndex].lastUsedSignal;
        }

        protected void Awake()
        {
            _players = new Player[2]; // hardcoded length for now

            PlayerDataLoadStrategy userDataLoader = new SimulateHumanLoadStrategy();
			userDataLoader.OnLoaded += OnUserDataLoaded;
			userDataLoader.LoadPlayerData(0);

			PlayerDataLoadStrategy npcDataLoader = new SimulateNPCLoadStrategy();
			npcDataLoader.OnLoaded += OnNPCDataLoaded;
			npcDataLoader.LoadPlayerData(1);
        }

        public void HandleResolutionData(ResolutionData resolutionData) {
            int playerCount = resolutionData.signals.Length;
            for(int i = 0; i < playerCount; i++)
            {
                _players[i].lastUsedSignal = resolutionData.signals[i];
                _players[i].AdjustMoney(resolutionData.moneyAdjustments[i]);
            }
        }

        private void OnUserDataLoaded(LoadablePlayerData loadedPlayerData)
        {
            _players[0] = new Player(loadedPlayerData);
        }

		private void OnNPCDataLoaded(LoadablePlayerData loadedPlayerData)
		{
			_players[1] = new Player(loadedPlayerData);
		}
    }
}