using UnityEngine.UI;

namespace ESG.RockPaperScissors
{
    // A struct that makes button referencing much more intuitive in the Inspector
    [System.Serializable]
    public struct InputButtonList
    {
        public Button RockButton;
        public Button PaperButton;
        public Button ScissorsButton;
    }
}