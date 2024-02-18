namespace _Assets.Scripts.Services.BotEditor.Commands
{
    public interface IEditorCommand
    {
        void Execute();
        void Undo();
    }
}