using UnityEngine;
using System.Collections;
using System;

namespace ESG.RockPaperScissors
{
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