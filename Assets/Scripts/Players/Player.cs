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
		private int _money;

		public Player(LoadablePlayerData loadablePlayerData)
		{
			lastUsedSignal = HandSignal.None;

			_userId = loadablePlayerData.uniqueId;
			_displayName = loadablePlayerData.displayName; 
			_money = loadablePlayerData.coins;
		}
		
		public int GetUserId()
		{
			return _userId;
		}
		
		public string GetDisplayName()
		{
			return _displayName;
		}

		public int GetMoney()
		{
			return _money;
		}

		public void AdjustMoney(int amount)
		{
			_money += amount;
		}
	}
}