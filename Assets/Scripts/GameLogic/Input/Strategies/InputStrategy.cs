namespace ESG.RockPaperScissors
{
    public abstract class InputStrategy
    {
        public delegate void OnInputProvidedAction(HandSignal signal);
		public event OnInputProvidedAction OnInputProvided;

        // A helper method must invoke the delegate because it cannot be referenced in an
        // implementation of an abstract class.
        protected virtual void ProvideInput(HandSignal signal)
        {
            OnInputProvided(signal);
        }
    }
}