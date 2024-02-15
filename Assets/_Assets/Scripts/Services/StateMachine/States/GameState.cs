using _Assets.Scripts.Services.BotEditor;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class GameState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly UIStateMachine _uiStateMachine;
        private readonly BotEditorGridService _botEditorGridService;

        public GameState(GameStateMachine stateMachine, UIStateMachine uiStateMachine, BotEditorGridService botEditorGridService)
        {
            _stateMachine = stateMachine;
            _uiStateMachine = uiStateMachine;
            _botEditorGridService = botEditorGridService;
        }

        public void Enter()
        {
            _uiStateMachine.SwitchState(UIStateType.BotEditor);
            _botEditorGridService.Init(100);
        }

        public void Exit() { }
    }
}