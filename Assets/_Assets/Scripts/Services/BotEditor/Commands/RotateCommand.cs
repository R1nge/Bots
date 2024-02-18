using UnityEngine;

namespace _Assets.Scripts.Services.BotEditor.Commands
{
    public class RotateCommand : IEditorCommand
    {
        private readonly Transform _transform;
        private readonly Quaternion _startRotation;
        private readonly Quaternion _endRotation;
        
        public RotateCommand(Transform transform, Quaternion startRotation, Quaternion endRotation)
        {
            _transform = transform;
            _startRotation = startRotation;
            _endRotation = endRotation;
        }
        
        public void Execute() => _transform.rotation = _endRotation;

        public void Undo() => _transform.rotation = _startRotation;
    }
}