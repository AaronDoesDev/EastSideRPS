using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ESG.RockPaperScissors
{
    public class RPSInterfaceService : MonoBehaviour, IInputButtonProvider, IUpdatableInterface
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
            UpdateInterface(InterfaceUpdateFlags.All);
        }

        // This flag-based system is admittedly a bit overengineered for a feature like this.
        // Still, I think bitwise enums are useful and wanted to squeeze in a demo.
        public void UpdateInterface(InterfaceUpdateFlags flags) {
            for(int i = 0; i < _playerPanels.Length; i++) {
                if((flags & InterfaceUpdateFlags.DisplayName) != 0 || (flags & InterfaceUpdateFlags.All) != 0)
                {
                    _playerPanels[i].UpdateDisplayName(_dataService.GetDisplayName(i));
                }

                if((flags & InterfaceUpdateFlags.Money) != 0 || (flags & InterfaceUpdateFlags.All) != 0)
                {
                    _playerPanels[i].UpdateMoney(_dataService.GetCoins(i));
                }

                if((flags & InterfaceUpdateFlags.HandSignal) != 0 || (flags & InterfaceUpdateFlags.All) != 0)
                {
                    _playerPanels[i].UpdateHandSignal(_dataService.GetLastHandSignal(i).ToString());
                }
            }
        }

        public Button[] GetInputButtons(int playerIndex) {
            if(_inputButtons == null) {
                FormatButtonLists();
            }

            return _inputButtons[playerIndex];
        }

        // Input buttons are connected to the inspector as an InputButtonList[] and then converted to a Button[][] because:
        // 1) InputButtonList allows the information to be serialized and displayed much more intuitively in the Inspector
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
    }
}