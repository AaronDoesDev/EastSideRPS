using UnityEngine;

namespace ESG.RockPaperScissors
{
    // An implementation of InputService that assumes both players are human.
    // Both input sources are 'listened', so there is no 'responded' phase.
    public class HumanVersusHumanInputService : InputService
    {
        private const int HUMAN_PLAYER_COUNT = 2;
        private const int NPC_PLAYER_COUNT = 0;
        private const int P1_INDEX = 0;
        private const int P2_INDEX = 1;

        // An abstraction allowing the input service to pass all player inputs into any resolution service
        private IResolutionService _resolutionService;
        // An abstraction allowing the input service to tell any UI service to update with new information
        private IUpdatableUI _uiService;
        

        protected void Awake()
        {
            if(!TryGetComponent(out _resolutionService))
            {
                Debug.LogError("HumanVersusHumanInputService requires a component implementing IResolutionService");
            }

            if(!TryGetComponent(out _uiService))
            {
                Debug.LogError("HumanVersusHumanInputService requires a component implementing IUpdateableUI");
            }

            CreateInputStrategies(HUMAN_PLAYER_COUNT, NPC_PLAYER_COUNT);
        }

        protected void Start()
        {
            BeginListening();
        }

        // This method is implemented as a requirement of the InputService abstract class.
        protected override void CreateInputStrategies(int listeningCount, int respondingCount)
        {
            IInputButtonProvider buttonProvider;
            if(!TryGetComponent(out buttonProvider))
            {
                Debug.LogError("HumanVersusHumanInputService requires a component implementing IInputButtonProvider");
            }

            _listeningInputStrategies = new InputStrategy[HUMAN_PLAYER_COUNT];
            _listenedInput = new HandSignal[HUMAN_PLAYER_COUNT];

            // Create the empty arrays even for unused input types - safer and still valid if their length is ever checked.
            _respondingInputStrategies = new RespondingInputStrategy[] {};
            _respondedInput = new HandSignal[] {};
            
            // For this implementation, listening for the human's input means depending on a set of button listeners.
            // ButtonInputStrategy includes functionality to associate these buttons with the input service.
            for(int i = 0; i < HUMAN_PLAYER_COUNT; i++)
            {
                int playerIndex = i;
                ButtonInputStrategy humanInput = new ButtonInputStrategy();
                humanInput.OnInputProvided += (HandSignal signal) => {
                    OnHumanHandSignal(playerIndex, signal);
                };
                humanInput.RegisterInputButtons(buttonProvider.GetInputButtons(i));
                _listeningInputStrategies[i] = humanInput;
            }
        }

        // This method is implemented as a requirement of the InputService abstract class.
        protected override void HandleAllInputReady()
        {
            // Reach out to the provided resolution service to determine the winner of this round and adjust any
            // money values accordingly.
            _resolutionService.ResolveWinner(_listenedInput);
            // Reach out to the provided UI service to update it with the new money and hand signal values.
            _uiService.UpdateUI(UIUpdateFlags.Money | UIUpdateFlags.HandSignal);

            // Call BeginListening() even when we know there are no listened inputs. It handles
            // such a case and pushes the match logic forward.
            BeginListening();
        }

        private void OnHumanHandSignal(int index, HandSignal signal)
        {
            HandleListenedInput(index, signal);
        }
    }
}