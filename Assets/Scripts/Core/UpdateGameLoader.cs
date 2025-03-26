using UnityEngine;
using System.Collections;
using System;

namespace ESG.RockPaperScissors
{
	public class UpdateGameLoader
	{
		public delegate void OnLoadedAction(Hashtable gameUpdateData);
		public event OnLoadedAction OnLoaded;

		private HandSignal _choice;

		public UpdateGameLoader(HandSignal playerChoice)
		{
			_choice = playerChoice;
		}

		public void Load()
		{
			HandSignal opponentHand = (HandSignal)Enum.GetValues(typeof(HandSignal)).GetValue(UnityEngine.Random.Range(1, 4));

			Hashtable mockGameUpdate = new Hashtable();
			mockGameUpdate["resultPlayer"] = _choice;
			mockGameUpdate["resultOpponent"] = opponentHand;
			mockGameUpdate["coinsAmountChange"] = GetCoinsAmount(_choice, opponentHand);
			
			OnLoaded(mockGameUpdate);
		}

		private int GetCoinsAmount (HandSignal playerHand, HandSignal opponentHand)
		{
			Result drawResult = ResultAnalyzer.GetResultState(playerHand, opponentHand);

			if (drawResult.Equals (Result.Won))
			{
				return 10;
			}
			else if (drawResult.Equals (Result.Lost))
			{
				return -10;
			}

			return 0;
		}
	}
}