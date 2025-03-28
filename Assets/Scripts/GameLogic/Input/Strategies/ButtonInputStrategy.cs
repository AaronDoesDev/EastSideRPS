using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ESG.RockPaperScissors
{
    // An input strategy that associates three on-screen buttons with the rock, paper and scissors inputs.
    public sealed class ButtonInputStrategy : InputStrategy
    {
        private const int REQUIRED_BUTTON_COUNT = 3;

        public void RegisterInputButtons(Button[] buttons)
        {
            if(buttons.Length != REQUIRED_BUTTON_COUNT)
            {
                Debug.LogError($"ButtonInputStrategy is designed for exactly {REQUIRED_BUTTON_COUNT} button(s)");

                return;
            }

            for(int i = 0; i < REQUIRED_BUTTON_COUNT; i++)
            {
                // HandSignal has been reworked such that an int can be directly cast into the appropriate enum value.
                HandSignal iteratorAsSignal = (HandSignal)i;
                buttons[i].onClick.AddListener(delegate {
                    ProvideInput(iteratorAsSignal);
                });
            }
        }
    }
}