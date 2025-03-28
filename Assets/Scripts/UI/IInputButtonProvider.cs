using UnityEngine.UI;

namespace ESG.RockPaperScissors
{
    public interface IInputButtonProvider
    {
        public Button[] GetInputButtons(int playerIndex);
    }
}