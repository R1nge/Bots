using System.Collections.Generic;
using _Assets.Scripts.Services.BotEditor.Commands;
using UnityEngine;

namespace _Assets.Scripts.Services.BotEditor
{
    public class BotEditorCommandBufferService
    {
        private readonly CircularBuffer<IEditorCommand> _commands = new(10);
        private readonly CircularBuffer<IEditorCommand> _undoCommands = new(10);

        public bool HasCommands() => _commands.Size > 0;
        public bool HasUndoCommands() => _undoCommands.Size > 0;
        
        //TODO: limit command count

        public void Execute(IEditorCommand command)
        {
            Debug.Log("Add");
            command.Execute();
            _commands.PushBack(command);
        }

        public void Undo()
        {
            var command = _commands.Back();
            _commands.PopBack();
            command.Undo();
            _undoCommands.PushBack(command);
        }

        public void Redo()
        {
            var command = _undoCommands.Back();
            _undoCommands.PopBack();
            command.Execute();
            _commands.PushBack(command);
        }
    }
}