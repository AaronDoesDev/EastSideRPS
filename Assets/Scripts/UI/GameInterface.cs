using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESG.RockPaperScissors
{
    public class GameInterface : MonoBehaviour
    {
        [SerializeField] private IDisplayablePlayerDataService _dataService;
        
        [SerializeField] private PlayerInfoPanel[] _playerPanels;

        protected void Awake() {
            if(!TryGetComponent(out _dataService)) {
                Debug.LogError("A component implementing IDisplayablePlayerDataService is required");
            }
        }

        protected void Start() {
            InitializePlayerData();
            UpdatePlayerData();
        }

        public void InitializePlayerData() {
            for(int i = 0; i < _playerPanels.Length; i++) {
                _playerPanels[i].InitializePanel(_dataService.GetDisplayName(i), _dataService.GetCoins(i));
            }
        }

        public void UpdatePlayerData() {
            for(int i = 0; i < _playerPanels.Length; i++) {
                _playerPanels[i].UpdateHandSignal(_dataService.GetLastHandSignal(i).ToString());
                _playerPanels[i].UpdateMoney(_dataService.GetCoins(i));
            }
        }
    }
}