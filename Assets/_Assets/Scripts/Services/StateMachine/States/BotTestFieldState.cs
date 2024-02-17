using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class BotTestFieldState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly UIStateMachine _uiStateMachine;
        private readonly CameraFactory _cameraFactory;

        public BotTestFieldState(GameStateMachine stateMachine, UIStateMachine uiStateMachine, CameraFactory cameraFactory)
        {
            _stateMachine = stateMachine;
            _uiStateMachine = uiStateMachine;
            _cameraFactory = cameraFactory;
        }
        
        public void Enter()
        {
            var camera = _cameraFactory.SpawnCamera(CameraFactory.CameraType.Editor);
            //TODO: spawn saved bot
            _uiStateMachine.SwitchState(UIStateType.Game);
        }

        public void Exit()
        {
        }
    }
}