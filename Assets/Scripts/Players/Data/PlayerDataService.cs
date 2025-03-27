using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESG.RockPaperScissors
{
    public class PlayerDataService : MonoBehaviour, IDisplayablePlayerDataService
    {
        private Player[] _players;

        public string GetDisplayName(int playerIndex) {
            return _players[playerIndex].GetDisplayName();
        }

        public int GetCoins(int playerIndex) {
            return _players[playerIndex].GetCoins();
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

        public void HandleGameUpdateData(Hashtable gameUpdateData) {
            _players[0].lastUsedSignal = (HandSignal)gameUpdateData["resultPlayer"];
			_players[1].lastUsedSignal = (HandSignal)gameUpdateData["resultOpponent"];

			int coinChange = (int)gameUpdateData["coinsAmountChange"];
			_players[0].ChangeCoinAmount(coinChange);
			_players[1].ChangeCoinAmount(-coinChange);
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