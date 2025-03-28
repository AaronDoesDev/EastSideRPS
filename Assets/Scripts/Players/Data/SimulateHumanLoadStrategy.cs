using UnityEngine;
using System.Collections;
using System;

namespace ESG.RockPaperScissors
{
	// An implementation of PlayerDataLoadStrategy that simulates the load of a human player
	// by hardcoding some values onto the LoadPlayerData instance.
	public class SimulateHumanLoadStrategy : PlayerDataLoadStrategy
	{
		public override void LoadPlayerData(int uniqueId)
		{
			LoadablePlayerData mockUserData = new LoadablePlayerData();
			mockUserData.uniqueId = uniqueId;
			mockUserData.displayName = "Human User";
			mockUserData.coins = 50;

			InvokeLoadedEvent(mockUserData);
		}
	}
}