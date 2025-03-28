using UnityEngine.UI;

namespace ESG.RockPaperScissors
{
    // Requires method(s) that allow a class to provide rock, paper and scissor buttons,
    // probably to an input-related service.
    public interface IInputButtonProvider
    {
        public Button[] GetInputButtons(int playerIndex);
    }
}