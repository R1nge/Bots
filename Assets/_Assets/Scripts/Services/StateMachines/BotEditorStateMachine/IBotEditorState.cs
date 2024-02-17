namespace _Assets.Scripts.Services.StateMachines.BotEditorStateMachine
{
    public interface IBotEditorState : IExitState
    {
        void Enter();
    }

    public interface IExitState
    {
        void Exit();
    }
}