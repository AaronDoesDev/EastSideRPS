namespace ESG.RockPaperScissors
{
    // Classes that implement this interface can provide data to UI that wants
    // to display player-related information.
    public interface IDisplayablePlayerDataService
    {
        public string GetDisplayName(int playerIndex);
        public int GetCoins(int playerIndex);
        public HandSignal GetLastHandSignal(int playerIndex);
    }
}