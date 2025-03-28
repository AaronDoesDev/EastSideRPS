using UnityEngine;

namespace ESG.RockPaperScissors
{
    public abstract class InputService : MonoBehaviour
    {
        protected InputStrategy[] _listeningInputStrategies;
        protected RespondingInputStrategy[] _respondingInputStrategies;
        
        protected HandSignal[] _listenedInput;
        protected HandSignal[] _respondedInput;

        protected int _remainingListenCount;
        protected int _remainingResponseCount;

        public virtual void BeginListening()
        {
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
            if(_remainingListenCount < 0) {
                Debug.LogWarning("More listened inputs received than expected - investigate InputService implementation");
            }
            
            if(_remainingListenCount == 0) {
                PromptResponsiveInputs();
            }
        }

        protected virtual void PromptResponsiveInputs() {
            for(int i = 0; i < _respondingInputStrategies.Length; i++)
            {
                _respondingInputStrategies[i].RequestInput();
            }
        }

        protected virtual void HandleRespondedInput(int index, HandSignal signal)
        {
            _respondedInput[index] = signal;
            
            _remainingResponseCount--;
            if(_remainingResponseCount < 0) {
                Debug.LogWarning("More responded inputs received than expected - investigate InputService implementation");
            }

            if(_remainingResponseCount == 0) {
                HandleAllInputReady();
            }
        }

        protected abstract void HandleAllInputReady();
    }
}