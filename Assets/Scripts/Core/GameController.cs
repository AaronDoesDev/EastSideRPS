using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

namespace ESG.RockPaperScissors
{
	public class GameController : MonoBehaviour
	{
		// These are both concretions but they'll be refactored later
		[SerializeField] private GameInterface _gameInterface;
		[SerializeField] private PlayerDataService _playerDataService;
		

		public void HandlePlayerInput(int signalIndex)
		{
			HandSignal playerChoice = HandSignal.None;

			switch (signalIndex)
			{
				case 1:
					playerChoice = HandSignal.Rock;
					break;
				case 2:
					playerChoice = HandSignal.Paper;
					break;
				case 3:
					playerChoice = HandSignal.Scissors;
					break;
			}

			UpdateGame(playerChoice);
		}

		private void UpdateGame(HandSignal playerChoice)
		{
			UpdateGameLoader updateGameLoader = new UpdateGameLoader(playerChoice);
			updateGameLoader.OnLoaded += OnGameUpdated;
			updateGameLoader.Load();
		}

		private void OnGameUpdated(Hashtable gameUpdateData)
		{
			_playerDataService.HandleGameUpdateData(gameUpdateData);
			_gameInterface.UpdatePlayerData();
		}
	}
}