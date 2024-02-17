using _Assets.Scripts.Services.StateMachines.BotTestFieldStateMachine.States;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.StateMachines.BotTestFieldStateMachine
{
    public class BotTestFieldStatesFactory
    {
        private readonly UIStateMachine _uiStateMachine;
        private readonly CameraFactory _cameraFactory;

        public BotTestFieldStatesFactory(UIStateMachine uiStateMachine, CameraFactory cameraFactory)
        {
            _uiStateMachine = uiStateMachine;
            _cameraFactory = cameraFactory;
        }
        
        public IBotTestFieldState CreateBotTestFieldState(BotTestFieldStateMachine botTestFieldStateMachine)
        {
            return new BotTestFieldState(botTestFieldStateMachine, _uiStateMachine, _cameraFactory);
        }
    }
}