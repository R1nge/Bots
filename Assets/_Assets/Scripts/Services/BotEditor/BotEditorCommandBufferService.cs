using System.Collections.Generic;
using _Assets.Scripts.Services.BotEditor.Commands;
using UnityEngine;

namespace _Assets.Scripts.Services.BotEditor
{
    public class BotEditorCommandBufferService
    {
        private readonly CircularBuffer<IEditorCommand> _commands = new(10);
        private readonly CircularBuffer<IEditorCommand> _undoCommands = new(10);

        public bool HasCommands() => _commands.Count > 0;
        public bool HasUndoCommands() => _undoCommands.Count > 0;

        public void Execute(IEditorCommand command)
        {
            Debug.Log("Add");
            command.Execute();
            _commands.PushLast(command);
        }

        public void Undo()
        {
            var command = _commands.Last();
            _commands.PopLast();
            command.Undo();
            _undoCommands.PushLast(command);
        }

        public void Redo()
        {
            var command = _undoCommands.Last();
            _undoCommands.PopLast();
            command.Execute();
            _commands.PushLast(command);
        }
    }
}