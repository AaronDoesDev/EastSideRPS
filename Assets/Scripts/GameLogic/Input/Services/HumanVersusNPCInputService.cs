using UnityEngine;

namespace ESG.RockPaperScissors
{
    public class HumanVersusNPCInputService : InputService
    {
        private const int HUMAN_PLAYER_COUNT = 1;
        private const int NPC_PLAYER_COUNT = 1;
        private const int HUMAN_INDEX = 0;
        private const int NPC_INDEX = 0;

        private IResolutionService _resolutionService;
        private IUpdatableInterface _interfaceService;
        

        protected void Awake()
        {
            if(!TryGetComponent(out _resolutionService)) {
                Debug.LogError("HumanVersusNPCInputService requires a component implementing IResolutionService");
            }

            if(!TryGetComponent(out _interfaceService)) {
                Debug.LogError("HumanVersusNPCInputService requires a component implementing IUpdateableInterface");
            }

            CreateInputStrategies(HUMAN_PLAYER_COUNT, NPC_PLAYER_COUNT);
        }

        protected void Start() {
            BeginListening();
        }

        protected override void CreateInputStrategies(int listeningCount, int respondingCount)
        {
            IInputButtonProvider buttonProvider;
            if(!TryGetComponent(out buttonProvider))
            {
                Debug.LogError("HumanVersusNPCInputService requires a component implementing IInputButtonProvider");
            }

            _listeningInputStrategies = new InputStrategy[HUMAN_PLAYER_COUNT];
            _listenedInput = new HandSignal[HUMAN_PLAYER_COUNT];

            _respondingInputStrategies = new RespondingInputStrategy[NPC_PLAYER_COUNT];
            _respondedInput = new HandSignal[NPC_PLAYER_COUNT];
            
            // Set up the human player's button-based input
            ButtonInputStrategy humanInput = new ButtonInputStrategy();
            humanInput.OnInputProvided += OnHumanHandSignal;
            humanInput.RegisterInputButtons(buttonProvider.GetInputButtons(HUMAN_INDEX));
            _listeningInputStrategies[HUMAN_INDEX] = humanInput;

            // Set up the NPC player's randomized input
            _respondingInputStrategies[NPC_INDEX] = new RandomizedInputStrategy();
            _respondingInputStrategies[NPC_INDEX].OnInputProvided += OnNPCHandSignal;
        }

        protected override void HandleAllInputReady()
        {
            HandSignal[] allPlayerSignals = new HandSignal[] {
                _listenedInput[HUMAN_INDEX],
                _respondedInput[NPC_INDEX]
            };

            _resolutionService.ResolveWinner(allPlayerSignals);
            _interfaceService.UpdateInterface(InterfaceUpdateFlags.Money | InterfaceUpdateFlags.HandSignal);

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