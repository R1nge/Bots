using _Assets.Scripts.Services.BotEditor;
using _Assets.Scripts.Services.StateMachines.BotEditorStateMachine.States;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.StateMachines.BotEditorStateMachine
{
    public class BotEditorStatesFactory
    {
        private readonly UIStateMachine _uiStateMachine;
        private readonly CameraFactory _cameraFactory;
        private readonly PartSelectionService _partSelectionService;
        private readonly BotEditorService _botEditorService;

        public BotEditorStatesFactory(UIStateMachine uiStateMachine, CameraFactory cameraFactory, PartSelectionService partSelectionService, BotEditorService botEditorService)
        {
            _uiStateMachine = uiStateMachine;
            _cameraFactory = cameraFactory;
            _partSelectionService = partSelectionService;
            _botEditorService = botEditorService;
        }
        
        public IBotEditorState CreateBotEditorState(BotEditorStateMachine stateMachine)
        {
            return new BotEditorState(stateMachine, _uiStateMachine, _cameraFactory, _partSelectionService, _botEditorService);
        }

        
    }
}