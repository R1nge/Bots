using _Assets.Scripts.Services.BotEditor;
using _Assets.Scripts.Services.StateMachine.States;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.StateMachine
{
    public class GameStatesFactory
    {
        private readonly UIStateMachine _uiStateMachine;
        private readonly CameraFactory _cameraFactory;
        private readonly PartSelectionService _partSelectionService;

        public GameStatesFactory(UIStateMachine uiStateMachine, CameraFactory cameraFactory, PartSelectionService partSelectionService)
        {
            _uiStateMachine = uiStateMachine;
            _cameraFactory = cameraFactory;
            _partSelectionService = partSelectionService;
        }
        
        public IGameState CreateBotEditorState(GameStateMachine stateMachine)
        {
            return new BotEditorState(stateMachine, _uiStateMachine, _cameraFactory, _partSelectionService);
        }

        public IGameState CreateBotTestFieldState(GameStateMachine stateMachine)
        {
            return new BotTestFieldState(stateMachine, _uiStateMachine, _cameraFactory);
        }
    }
}