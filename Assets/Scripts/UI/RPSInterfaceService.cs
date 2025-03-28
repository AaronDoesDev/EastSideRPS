using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ESG.RockPaperScissors
{
    public class RPSInterfaceService : MonoBehaviour, IInputButtonProvider
    {
        private IDisplayablePlayerDataService _dataService;
        
        [SerializeField] private PlayerInfoPanel[] _playerPanels;
        [SerializeField] private InputButtonList[] _inputButtonLists;
        private Button[][] _inputButtons;

        protected void Awake() {
            if(!TryGetComponent(out _dataService)) {
                Debug.LogError("A component implementing IDisplayablePlayerDataService is required");
            }
        }

        protected void Start() {
            InitializePlayerData();
            UpdatePlayerData();
        }

        public void UpdatePlayerData() {
            for(int i = 0; i < _playerPanels.Length; i++) {
                _playerPanels[i].UpdateHandSignal(_dataService.GetLastHandSignal(i).ToString());
                _playerPanels[i].UpdateMoney(_dataService.GetCoins(i));
            }
        }

        public Button[] GetInputButtons(int playerIndex) {
            if(_inputButtons == null) {
                FormatButtonLists();
            }

            return _inputButtons[playerIndex];
        }

        // Input buttons are connected to the inspector as an InputButtonList[] and then converted to a Button[][] because:
        // 1) InputButtonList allows the information to be serialized and displayed much more intuitively in inspector
        // 2) Button[][] makes fewer assumptions about how the buttons will be handled going forward
        protected void FormatButtonLists() {
            _inputButtons = new Button[_inputButtonLists.Length][];
            for(int i = 0; i < _inputButtonLists.Length; i++) {
                _inputButtons[i] = new Button[] {
                    _inputButtonLists[i].RockButton,
                    _inputButtonLists[i].PaperButton,
                    _inputButtonLists[i].ScissorsButton
                };
            }
        }

        protected void InitializePlayerData() {
            for(int i = 0; i < _playerPanels.Length; i++) {
                _playerPanels[i].InitializePanel(_dataService.GetDisplayName(i), _dataService.GetCoins(i));
            }
        }
    }
}