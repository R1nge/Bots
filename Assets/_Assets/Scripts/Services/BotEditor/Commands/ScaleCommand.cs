using UnityEngine;

namespace _Assets.Scripts.Services.BotEditor.Commands
{
    public class ScaleCommand : IEditorCommand
    {
        private readonly Transform _transform;
        private readonly Vector3 _startScale;
        private readonly Vector3 _endScale;

        public ScaleCommand(Transform transform, Vector3 startScale, Vector3 endScale)
        {
            _transform = transform;
            _startScale = startScale;
            _endScale = endScale;
        }
        
        public void Execute() => _transform.localScale = _endScale;

        public void Undo() => _transform.localScale = _startScale;
    }
}