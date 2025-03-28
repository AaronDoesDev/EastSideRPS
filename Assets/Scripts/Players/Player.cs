using UnityEngine;
using System.Collections;
using System;

namespace ESG.RockPaperScissors
{
	// A data class containing all information pertinent to a player's current state
	public class Player
	{
		public HandSignal lastUsedSignal;
		
		protected int _userId;
		protected string _displayName;
		protected int _money;

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