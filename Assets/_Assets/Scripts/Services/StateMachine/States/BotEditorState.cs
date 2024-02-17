using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.BotEditor;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class BotEditorState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly UIStateMachine _uiStateMachine;
        private readonly CameraFactory _cameraFactory;
        private readonly PartSelectionService _partSelectionService;

        public BotEditorState(GameStateMachine stateMachine, UIStateMachine uiStateMachine, CameraFactory cameraFactory, PartSelectionService partSelectionService)
        {
            _stateMachine = stateMachine;
            _uiStateMachine = uiStateMachine;
            _cameraFactory = cameraFactory;
            _partSelectionService = partSelectionService;
        }

        public void Enter()
        {
            _uiStateMachine.SwitchState(UIStateType.BotEditor);
            var camera = _cameraFactory.SpawnCamera(CameraFactory.CameraType.Editor);
            _partSelectionService.Init(camera.GetComponent<FlyCamera>());
        }

        public void Exit() { }
    }
}