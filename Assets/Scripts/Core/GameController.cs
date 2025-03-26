using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

namespace ESG.RockPaperScissors
{
	public class GameController : MonoBehaviour
	{
		[SerializeField] private GameInterface _gameInterface;

		private Player _player1Data;
		private Player _player2Data;

		void Start()
		{
			PlayerInfoLoader userInfoLoader = new PlayerInfoLoader();
			userInfoLoader.OnLoaded += OnUserInfoLoaded;
			userInfoLoader.LoadSimulatedUserData();

			PlayerInfoLoader npcInfoLoader = new PlayerInfoLoader();
			npcInfoLoader.OnLoaded += OnNPCInfoLoaded;
			npcInfoLoader.LoadSimulatedNPCData();

			_gameInterface.InitializePlayerData(_player1Data, _player2Data);
			UpdateGameInterface();
		}

		private void OnUserInfoLoaded(Hashtable playerData)
		{
			_player1Data = new Player(playerData);
		}

		private void OnNPCInfoLoaded(Hashtable playerData)
		{
			_player2Data = new Player(playerData);
		}

		public void HandlePlayerInput(int item)
		{
			UseableItem playerChoice = UseableItem.None;

			switch (item)
			{
				case 1:
					playerChoice = UseableItem.Rock;
					break;
				case 2:
					playerChoice = UseableItem.Paper;
					break;
				case 3:
					playerChoice = UseableItem.Scissors;
					break;
			}

			UpdateGame(playerChoice);
		}

		private void UpdateGameInterface() {
			_gameInterface.UpdatePlayerData(_player1Data, _player2Data);
		}

		private void UpdateGame(UseableItem playerChoice)
		{
			UpdateGameLoader updateGameLoader = new UpdateGameLoader(playerChoice);
			updateGameLoader.OnLoaded += OnGameUpdated;
			updateGameLoader.Load();
		}

		private void OnGameUpdated(Hashtable gameUpdateData)
		{
			_player1Data.lastUsedItem = (UseableItem)gameUpdateData["resultPlayer"];
			_player2Data.lastUsedItem = (UseableItem)gameUpdateData["resultOpponent"];

			int coinChange = (int)gameUpdateData["coinsAmountChange"];
			_player1Data.ChangeCoinAmount(coinChange);
			_player2Data.ChangeCoinAmount(-coinChange);

			UpdateGameInterface();
		}
	}
}