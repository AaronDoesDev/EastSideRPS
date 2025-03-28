using UnityEngine;

namespace ESG.RockPaperScissors
{
    // An implementation of InputService that assumes one human player and one NPC player.
    // This allows it to treat the human player as a listenable input source and the NPC
    // as a responding input source. ie - the NPC will be asked for a hand signal only once the
    // player chooses. This means that the player's input alone drives the core gameplay loop.
    public class HumanVersusNPCInputService : InputService
    {
        private const int HUMAN_PLAYER_COUNT = 1;
        private const int NPC_PLAYER_COUNT = 1;
        private const int HUMAN_INDEX = 0;
        private const int NPC_INDEX = 0;

        // An abstraction allowing the input service to pass all player inputs into any resolution service
        private IResolutionService _resolutionService;
        // An abstraction allowing the input service to tell any UI service to update with new information
        private IUpdatableUI _uiService;
        

        protected void Awake()
        {
            if(!TryGetComponent(out _resolutionService))
            {
                Debug.LogError("HumanVersusNPCInputService requires a component implementing IResolutionService");
            }

            if(!TryGetComponent(out _uiService))
            {
                Debug.LogError("HumanVersusNPCInputService requires a component implementing IUpdateableUI");
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
                Debug.LogError("HumanVersusNPCInputService requires a component implementing IInputButtonProvider");
            }

            // The human player uses a listening strategy, meaning it waits for input. This only requires a class
            // implementing the basic InputStrategy abstract class.
            _listeningInputStrategies = new InputStrategy[HUMAN_PLAYER_COUNT];
            _listenedInput = new HandSignal[HUMAN_PLAYER_COUNT];

            // The NPC player uses a responding strategy, meaning the service requests input from it once all listening
            // strategies have delivered their input. This requires a class that implements the RespondingInputStrategy
            // abstract class, which in turn requires a RequestInput method.
            _respondingInputStrategies = new RespondingInputStrategy[NPC_PLAYER_COUNT];
            _respondedInput = new HandSignal[NPC_PLAYER_COUNT];
            
            // For this implementation, listening for the human's input means depending on a set of button listeners.
            // ButtonInputStrategy includes functionality to associate these buttons with the input service.
            ButtonInputStrategy humanInput = new ButtonInputStrategy();
            humanInput.OnInputProvided += OnHumanHandSignal;
            humanInput.RegisterInputButtons(buttonProvider.GetInputButtons(HUMAN_INDEX));
            _listeningInputStrategies[HUMAN_INDEX] = humanInput;

            // For this implementation, the NPC player selects a random hand signal once the player has chosen theirs.
            _respondingInputStrategies[NPC_INDEX] = new RandomizedInputStrategy();
            _respondingInputStrategies[NPC_INDEX].OnInputProvided += OnNPCHandSignal;
        }

        // This method is implemented as a requirement of the InputService abstract class.
        protected override void HandleAllInputReady()
        {
            HandSignal[] allPlayerSignals = new HandSignal[]
            {
                _listenedInput[HUMAN_INDEX],
                _respondedInput[NPC_INDEX]
            };

            // Reach out to the provided resolution service to determine the winner of this round and adjust any
            // money values accordingly.
            _resolutionService.ResolveWinner(allPlayerSignals);
            // Reach out to the provided UI service to update it with the new money and hand signal values.
            _uiService.UpdateUI(UIUpdateFlags.Money | UIUpdateFlags.HandSignal);

            BeginListening();
        }

        private void OnHumanHandSignal(HandSignal signal)
        {
            HandleListenedInput(HUMAN_INDEX, signal);
        }

        private void OnNPCHandSignal(HandSignal signal)
        {
            HandleRespondedInput(NPC_INDEX, signal);
        }
    }
}