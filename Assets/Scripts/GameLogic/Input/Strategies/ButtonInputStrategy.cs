using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ESG.RockPaperScissors
{
    public sealed class ButtonInputStrategy : InputStrategy
    {
        private const int REQUIRED_BUTTON_COUNT = 3;

        public void RegisterInputButtons(Button[] buttons) {
            if(buttons.Length != REQUIRED_BUTTON_COUNT) {
                Debug.LogError($"ButtonInputStrategy is designed for exactly {REQUIRED_BUTTON_COUNT} button(s)");

                return;
            }

            for(int i = 0; i < REQUIRED_BUTTON_COUNT; i++) {
                HandSignal iteratorAsSignal = (HandSignal)i;
                buttons[i].onClick.AddListener(delegate {
                    ProvideInput(iteratorAsSignal);
                });
            }
        }
    }
}