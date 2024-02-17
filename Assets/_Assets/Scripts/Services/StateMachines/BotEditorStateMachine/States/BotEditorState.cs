using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.BotEditor;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.StateMachines.BotEditorStateMachine.States
{
    public class BotEditorState : IBotEditorState
    {
        private readonly BotEditorStateMachine _stateMachine;
        private readonly UIStateMachine _uiStateMachine;
        private readonly CameraFactory _cameraFactory;
        private readonly PartSelectionService _partSelectionService;
        private readonly BotEditorService _botEditorService;

        public BotEditorState(BotEditorStateMachine stateMachine, UIStateMachine uiStateMachine, CameraFactory cameraFactory, PartSelectionService partSelectionService, BotEditorService botEditorService)
        {
            _stateMachine = stateMachine;
            _uiStateMachine = uiStateMachine;
            _cameraFactory = cameraFactory;
            _partSelectionService = partSelectionService;
            _botEditorService = botEditorService;
        }

        public void Enter()
        {
            _botEditorService.Init();
            _uiStateMachine.SwitchState(UIStateType.BotEditor);
            var camera = _cameraFactory.SpawnCamera(CameraFactory.CameraType.Editor);
            _partSelectionService.Init(camera.GetComponent<FlyCamera>());
        }

        public void Exit()
        {
            _botEditorService.Dispose();
        }
    }
}