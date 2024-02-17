using _Assets.Scripts.Services.StateMachines.BotEditorStateMachine;

namespace _Assets.Scripts.Services.StateMachines.BotTestFieldStateMachine
{
    public interface IBotTestFieldState : IExitState
    {
        void Enter();
    }
}