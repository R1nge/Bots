using _Assets.Scripts.Services.BotTestField;
using _Assets.Scripts.Services.StateMachines.BotTestFieldStateMachine.States;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.StateMachines.BotTestFieldStateMachine
{
    public class BotTestFieldStatesFactory
    {
        private readonly UIStateMachine _uiStateMachine;
        private readonly CameraFactory _cameraFactory;
        private readonly BotTestFieldSpawner _botTestFieldSpawner;

        public BotTestFieldStatesFactory(UIStateMachine uiStateMachine, CameraFactory cameraFactory, BotTestFieldSpawner botTestFieldSpawner)
        {
            _uiStateMachine = uiStateMachine;
            _cameraFactory = cameraFactory;
            _botTestFieldSpawner = botTestFieldSpawner;
        }
        
        public IBotTestFieldState CreateBotTestFieldState(BotTestFieldStateMachine botTestFieldStateMachine)
        {
            return new BotTestFieldState(botTestFieldStateMachine, _uiStateMachine, _cameraFactory, _botTestFieldSpawner);
        }
    }
}