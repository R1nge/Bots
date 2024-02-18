using System.Collections.Generic;
using _Assets.Scripts.Services.BotEditor.Commands;
using UnityEngine;

namespace _Assets.Scripts.Services.BotEditor
{
    public class BotEditorCommandBufferService
    {
        private readonly Stack<IEditorCommand> _commands = new();
        private readonly Stack<IEditorCommand> _undoCommands = new();

        public bool HasCommands() => _commands.Count > 0;
        public bool HasUndoCommands() => _undoCommands.Count > 0;

        public void Execute(IEditorCommand command)
        {
            Debug.Log("Add");
            command.Execute();
            _commands.Push(command);
        }

        public void Undo()
        {
            var command = _commands.Pop();
            command.Undo();
            _undoCommands.Push(command);
        }

        public void Redo()
        {
            var command = _undoCommands.Pop();
            command.Execute();
            _commands.Push(command);
        }
    }
}