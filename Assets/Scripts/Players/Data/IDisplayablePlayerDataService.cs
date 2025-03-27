namespace ESG.RockPaperScissors
{
    public interface IDisplayablePlayerDataService
    {
        public string GetDisplayName(int playerIndex);
        public int GetCoins(int playerIndex);
        public HandSignal GetLastHandSignal(int playerIndex);
    }
}