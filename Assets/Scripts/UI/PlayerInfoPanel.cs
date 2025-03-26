using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ESG.RockPaperScissors
{
    public class PlayerInfoPanel : MonoBehaviour
    {
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _moneyText;
        [SerializeField] private Text _handSignalText;

        public void InitializePanel(string playerName, int coins) {
            _nameText.text = playerName;
            
            UpdateMoney(coins);
        }

        public void UpdateMoney(int coins) {
            _moneyText.text = $"${coins}";
        }

        public void UpdateHandSignal(string signal) {
            _handSignalText.text = signal;
        }
    }
}