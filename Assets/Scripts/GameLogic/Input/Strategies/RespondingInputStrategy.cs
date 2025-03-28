namespace ESG.RockPaperScissors
{
    // Responding input strategies build upon the standard input strategy abstraction
    // by requiring a RequestInput method, which provides input only once specifically
    // asked for it.
    public abstract class RespondingInputStrategy : InputStrategy
    {
        public abstract void RequestInput();
    }
}