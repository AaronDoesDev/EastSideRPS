using UnityEngine;
using System.Collections;
using System;

namespace ESG.RockPaperScissors
{
	// An implementation of PlayerDataLoadStrategy that simulates the load of an NPC player
	// by hardcoding some values onto the LoadPlayerData instance.
	public class SimulateNPCLoadStrategy : PlayerDataLoadStrategy
	{
		public override void LoadPlayerData(int uniqueId)
		{
			LoadablePlayerData mockUserData = new LoadablePlayerData();
			mockUserData.uniqueId = uniqueId;
			mockUserData.displayName = "NPC Opponent";
			mockUserData.coins = 0;

			InvokeLoadedEvent(mockUserData);
		}
	}
}