using UnityEngine;
using System.Collections;
using System;

namespace ESG.RockPaperScissors
{
	public class PlayerInfoLoader
	{
		public delegate void OnLoadedAction(Hashtable playerData);
		public event OnLoadedAction OnLoaded;

		public void Load()
		{
			Hashtable mockPlayerData = new Hashtable();
			mockPlayerData["userId"] = 1;
			mockPlayerData["name"] = "Player 1";
			mockPlayerData["coins"] = 50;

			OnLoaded(mockPlayerData);
		}
	}
}