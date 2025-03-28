namespace ESG.RockPaperScissors
{
    public abstract class InputStrategy
    {
        public delegate void OnInputProvidedAction(HandSignal signal);
		public event OnInputProvidedAction OnInputProvided;

        protected virtual void ProvideInput(HandSignal signal) {
            OnInputProvided(signal);
        }
    }
}