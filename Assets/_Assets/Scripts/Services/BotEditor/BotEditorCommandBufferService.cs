using System.Collections.Generic;
using _Assets.Scripts.Services.BotEditor.Commands;
using UnityEngine;

namespace _Assets.Scripts.Services.BotEditor
{
    public class BotEditorCommandBufferService
    {
        private readonly Stack<IEditorCommand> _commands = new();
        
        public bool HasCommands() => _commands.Count > 0;

        public void Execute(IEditorCommand command)
        {
            Debug.Log("Add");
            command.Execute();
            _commands.Push(command);
        }

        public void Undo() => _commands.Pop().Undo();
    }
}