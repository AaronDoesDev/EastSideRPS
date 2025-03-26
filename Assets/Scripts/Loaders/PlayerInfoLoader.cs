using UnityEngine;
using System.Collections;
using System;

namespace ESG.RockPaperScissors
{
	public class PlayerInfoLoader
	{
		public delegate void OnLoadedAction(Hashtable playerData);
		public event OnLoadedAction OnLoaded;

		public void LoadSimulatedUserData()
		{
			Hashtable mockUserData = new Hashtable();
			mockUserData["userId"] = 1;
			mockUserData["name"] = "Human User";
			mockUserData["coins"] = 50;

			OnLoaded(mockUserData);
		}

		public void LoadSimulatedNPCData()
		{
			Hashtable mockNPCData = new Hashtable();
			mockNPCData["userId"] = 2;
			mockNPCData["name"] = "NPC Opponent";
			mockNPCData["coins"] = 50;

			OnLoaded(mockNPCData);
		}
	}
}