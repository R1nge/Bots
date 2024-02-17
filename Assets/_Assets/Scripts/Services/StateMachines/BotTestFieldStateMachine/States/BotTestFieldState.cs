using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.StateMachines.BotTestFieldStateMachine.States
{
    public class BotTestFieldState : IBotTestFieldState
    {
        private readonly BotTestFieldStateMachine _stateMachine;
        private readonly UIStateMachine _uiStateMachine;
        private readonly CameraFactory _cameraFactory;

        public BotTestFieldState(BotTestFieldStateMachine stateMachine, UIStateMachine uiStateMachine, CameraFactory cameraFactory)
        {
            _stateMachine = stateMachine;
            _uiStateMachine = uiStateMachine;
            _cameraFactory = cameraFactory;
        }
        
        public void Enter()
        {
            var camera = _cameraFactory.SpawnCamera(CameraFactory.CameraType.Editor);
            //TODO: spawn saved bot
            //_uiStateMachine.SwitchState(UIStateType.Game);
        }

        public void Exit()
        {
        }
    }
}