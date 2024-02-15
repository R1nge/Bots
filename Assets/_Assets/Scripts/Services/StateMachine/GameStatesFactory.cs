using _Assets.Scripts.Services.BotEditor;
using _Assets.Scripts.Services.StateMachine.States;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.StateMachine
{
    public class GameStatesFactory
    {
        private readonly UIStateMachine _uiStateMachine;
        private readonly BotEditorGridService _botEditorGridService;

        public GameStatesFactory(UIStateMachine uiStateMachine, BotEditorGridService botEditorGridService)
        {
            _uiStateMachine = uiStateMachine;
            _botEditorGridService = botEditorGridService;
        }
        
        public IGameState CreateGameState(GameStateMachine stateMachine)
        {
            return new GameState(stateMachine, _uiStateMachine, _botEditorGridService);
        }
    }
}