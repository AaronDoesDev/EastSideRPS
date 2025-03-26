using System;

namespace ESG.RockPaperScissors
{
    public abstract class PlayerDataLoadStrategy
    {
        public delegate void OnLoadedAction(LoadablePlayerData loadedPlayerData);
		public event OnLoadedAction OnLoaded;

        public abstract void LoadPlayerData(int uniqueId);

        protected virtual void InvokeLoadedEvent(LoadablePlayerData loadablePlayerData) {
            OnLoaded(loadablePlayerData);
        }
    }
}