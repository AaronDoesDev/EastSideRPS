using UnityEngine;

namespace ESG.RockPaperScissors
{
    // This abstract class provides some of the boilerplate functionality used by all input service implementations.
    public abstract class InputService : MonoBehaviour
    {
        // Listening input strategies await input from outside sources, like people.
        protected InputStrategy[] _listeningInputStrategies;
        // Responding input strategies have their input manually requested once all listened inputs have arrived.
        protected RespondingInputStrategy[] _respondingInputStrategies;
        
        // Hand Signal values are collected in these arrays from their respective input strategies.
        protected HandSignal[] _listenedInput;
        protected HandSignal[] _respondedInput;

        // Counts that track how many more inputs to expect from each strategy type.
        protected int _remainingListenCount;
        protected int _remainingResponseCount;

        // This method resets the service state, priming it for the next complete set of inputs.
        public virtual void BeginListening()
        {
            // TODO: Handle cases with 0 listened inputs or 0 responded inputs
            _remainingListenCount = _listeningInputStrategies.Length;
            _remainingResponseCount = _respondingInputStrategies.Length;

            for(int i = 0; i < _listenedInput.Length; i++)
            {
                _listenedInput[i] = HandSignal.None;
            }

            for(int i = 0; i < _respondedInput.Length; i++)
            {
                _respondedInput[i] = HandSignal.None;
            }
        }

        protected abstract void CreateInputStrategies(int listeningCount, int respondingCount);

        protected virtual void HandleListenedInput(int index, HandSignal signal)
        {
            _listenedInput[index] = signal;
            
            _remainingListenCount--;
            if(_remainingListenCount < 0)
            {
                Debug.LogWarning("More listened inputs received than expected - investigate InputService implementation");
            }
            
            // Once all listened input is received, input can be requested from all of the 'responding' input strategies.
            if(_remainingListenCount == 0)
            {
                PromptRespondingInputs();
            }
        }

        // Iterates through all 'responding' input strategies and requests input from each.
        protected virtual void PromptRespondingInputs()
        {
            for(int i = 0; i < _respondingInputStrategies.Length; i++)
            {
                _respondingInputStrategies[i].RequestInput();
            }
        }

        protected virtual void HandleRespondedInput(int index, HandSignal signal)
        {
            _respondedInput[index] = signal;
            
            _remainingResponseCount--;
            if(_remainingResponseCount < 0)
            {
                Debug.LogWarning("More responded inputs received than expected - investigate InputService implementation");
            }

            // Once all responding inputs have arrived, the HandleAllInputReady method is invoked.
            // This method must be defined in another non-abstract class implementing this one.
            if(_remainingResponseCount == 0)
            {
                HandleAllInputReady();
            }
        }

        protected abstract void HandleAllInputReady();
    }
}