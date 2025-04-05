using System.Linq;
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

        // This method resets the service state, priming it for the next complete set of inputs.
        public virtual void BeginListening()
        {
            for(int i = 0; i < _listenedInput.Length; i++)
            {
                _listenedInput[i] = HandSignal.None;
            }

            for(int i = 0; i < _respondedInput.Length; i++)
            {
                _respondedInput[i] = HandSignal.None;
            }

            CheckListenedInputs();
        }

        protected void CheckListenedInputs()
        {
            // Once all listened input is received (if any), input can be requested from all of the 'responding' input strategies.
            if(!_listenedInput.Contains(HandSignal.None))
            {
                PromptRespondingInputs();
            }
        }

        protected void CheckRespondedInputs()
        {
            // Once all responding inputs have arrived (if any), the HandleAllInputReady method is invoked.
            // This method must be defined in another non-abstract class implementing this one.
            if(!_respondedInput.Contains(HandSignal.None))
            {
                HandleAllInputReady();
            }
        }

        protected abstract void CreateInputStrategies(int listeningCount, int respondingCount);

        protected virtual void HandleListenedInput(int index, HandSignal signal)
        {
            _listenedInput[index] = signal;
            CheckListenedInputs();
        }

        // Iterates through all 'responding' input strategies and requests input from each.
        protected virtual void PromptRespondingInputs()
        {
            CheckRespondedInputs(); // check for zero responded input case first

            for(int i = 0; i < _respondingInputStrategies.Length; i++)
            {
                _respondingInputStrategies[i].RequestInput();
            }
        }

        protected virtual void HandleRespondedInput(int index, HandSignal signal)
        {
            _respondedInput[index] = signal;
            CheckRespondedInputs();
        }

        protected abstract void HandleAllInputReady();
    }
}