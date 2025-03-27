using UnityEngine;
using System.Collections;
using System;

namespace ESG.RockPaperScissors
{
	public class Player
	{
		public HandSignal lastUsedSignal;
		
		private int _userId;
		private string _displayName;
		private int _coins;

		public Player(LoadablePlayerData loadablePlayerData)
		{
			lastUsedSignal = HandSignal.None;

			_userId = loadablePlayerData.uniqueId;
			_displayName = loadablePlayerData.displayName; 
			_coins = loadablePlayerData.coins;
		}
		
		public int GetUserId()
		{
			return _userId;
		}
		
		public string GetDisplayName()
		{
			return _displayName;
		}

		public int GetCoins()
		{
			return _coins;
		}

		public void ChangeCoinAmount(int amount)
		{
			_coins += amount;
		}
	}
}