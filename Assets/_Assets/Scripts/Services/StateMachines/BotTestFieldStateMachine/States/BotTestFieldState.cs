using _Assets.Scripts.Services.BotTestField;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.StateMachines.BotTestFieldStateMachine.States
{
    public class BotTestFieldState : IBotTestFieldState
    {
        private readonly BotTestFieldStateMachine _stateMachine;
        private readonly UIStateMachine _uiStateMachine;
        private readonly CameraFactory _cameraFactory;
        private readonly BotTestFieldSpawner _botTestFieldSpawner;

        public BotTestFieldState(BotTestFieldStateMachine stateMachine, UIStateMachine uiStateMachine, CameraFactory cameraFactory, BotTestFieldSpawner botTestFieldSpawner)
        {
            _stateMachine = stateMachine;
            _uiStateMachine = uiStateMachine;
            _cameraFactory = cameraFactory;
            _botTestFieldSpawner = botTestFieldSpawner;
        }
        
        public void Enter()
        {
            var camera = _cameraFactory.SpawnCamera(CameraFactory.CameraType.Editor);
            _botTestFieldSpawner.Spawn();
            //_uiStateMachine.SwitchState(UIStateType.Game);
        }

        public void Exit()
        {
        }
    }
}