using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ESG.RockPaperScissors
{
    // UI element containing all of a player's displayable information
    public class PlayerInfoPanel : MonoBehaviour
    {
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _moneyText;
        [SerializeField] private Text _handSignalText;
        
        public void UpdateDisplayName(string displayName)
        {
            _nameText.text = displayName;
        }

        public void UpdateMoney(int coins)
        {
            _moneyText.text = $"${coins}";
        }

        public void UpdateHandSignal(string signal)
        {
            _handSignalText.text = signal;
        }
    }
}