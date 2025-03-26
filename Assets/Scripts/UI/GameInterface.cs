using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESG.RockPaperScissors
{
    public class GameInterface : MonoBehaviour
    {
        [SerializeField] private PlayerInfoPanel _player1Panel;
		[SerializeField] private PlayerInfoPanel _player2Panel;

        public void InitializePlayerData(Player player1Data, Player player2Data) {
            _player1Panel.InitializePanel(player1Data.GetName(), player1Data.GetCoins());
            _player2Panel.InitializePanel(player2Data.GetName(), player2Data.GetCoins());
        }

        public void UpdatePlayerData(Player player1Data, Player player2Data) {
            _player1Panel.UpdateHandSignal(player1Data.lastUsedItem.ToString());
			_player1Panel.UpdateMoney(player1Data.GetCoins());

			_player2Panel.UpdateHandSignal(player2Data.lastUsedItem.ToString());
			_player2Panel.UpdateMoney(player2Data.GetCoins());
        }
    }
}