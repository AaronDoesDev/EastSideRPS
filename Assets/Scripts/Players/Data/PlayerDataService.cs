using UnityEngine;

namespace ESG.RockPaperScissors
{
    public abstract class PlayerDataService : MonoBehaviour
    {
        protected Player[] _players;

        public string GetDisplayName(int playerIndex)
        {
            return _players[playerIndex].GetDisplayName();
        }

        public int GetCoins(int playerIndex)
        {
            return _players[playerIndex].GetMoney();
        }

        public HandSignal GetLastHandSignal(int playerIndex)
        {
            return _players[playerIndex].lastUsedSignal;
        }
    }
}