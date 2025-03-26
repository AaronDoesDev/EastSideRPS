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
			PlayerDataLoadStrategy userDataLoader = new SimulateHumanLoadStrategy();
			userDataLoader.OnLoaded += OnUserDataLoaded;
			userDataLoader.LoadPlayerData(0);

			PlayerDataLoadStrategy npcDataLoader = new SimulateNPCLoadStrategy();
			npcDataLoader.OnLoaded += OnNPCDataLoaded;
			npcDataLoader.LoadPlayerData(1);

			_gameInterface.InitializePlayerData(_player1Data, _player2Data);
			UpdateGameInterface();
		}

		private void OnUserDataLoaded(LoadablePlayerData loadedPlayerData)
		{
			_player1Data = new Player(loadedPlayerData);
		}

		private void OnNPCDataLoaded(LoadablePlayerData loadedPlayerData)
		{
			_player2Data = new Player(loadedPlayerData);
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

		private void UpdateGameInterface()
		{
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