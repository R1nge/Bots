using UnityEngine;

namespace _Assets.Scripts.Services.BotEditor.Commands
{
    public class MoveCommand : IEditorCommand
    {
        private readonly Transform _transform;
        private readonly Vector3 _startPosition;
        private readonly Vector3 _endPosition;
        
        public MoveCommand(Transform transform, Vector3 startPosition, Vector3 endPosition)
        {
            _transform = transform;
            _startPosition = startPosition;
            _endPosition = endPosition;
        }
        
        public void Execute() => _transform.position = _endPosition;

        public void Undo() => _transform.position = _startPosition;
    }
}