using _Assets.Scripts.Configs;
using _Assets.Scripts.Gameplay.Parts;
using _Assets.Scripts.Services.BotEditor.Commands;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace _Assets.Scripts.Services.BotEditor
{
    public class BotEditorMarker : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        [SerializeField] private MarkerAxisType markerAxis;
        private EditMode _editMode;
        private bool _isDragging;
        private Vector3 _mouseStartPosition;
        private Vector3 _startPosition;
        private Vector3 _startScale;
        private Quaternion _startRotation;
        private Camera _camera;
        [Inject] private ConfigProvider _configProvider;
        [Inject] private BotEditorCommandBufferService _botEditorCommandBufferService;

        public void StartDragging(Camera camera, Vector3 mousePosition)
        {
            _camera = camera;
            _isDragging = true;
            _mouseStartPosition = mousePosition;
            _startPosition = transform.root.position;
            _startRotation = transform.root.rotation;
            _startScale = transform.root.localScale;
        }

        public void UpdateEditMode(EditMode editMode) => _editMode = editMode;

        private void Update()
        {
            switch (_editMode)
            {
                case EditMode.Move:
                    Move();
                    break;
                case EditMode.Rotate:
                    Rotate();
                    break;
                case EditMode.Scale:
                    Scale();
                    break;
            }
        }

        public void StopDragging()
        {
            if (!_isDragging)
                return;
            
            _isDragging = false;

            switch (_editMode)
            {
                case EditMode.Move:
                    _botEditorCommandBufferService.Execute(new MoveCommand(transform.root, _startPosition, transform.root.position));
                    break;
                case EditMode.Rotate:
                    _botEditorCommandBufferService.Execute(new RotateCommand(transform.root, _startRotation, transform.root.rotation));
                    break;
                case EditMode.Scale:
                    _botEditorCommandBufferService.Execute(new ScaleCommand(transform.root, _startScale, transform.root.localScale));
                    break;
            }
        }

        private void Move()
        {
            if (_isDragging)
            {
                var plane = new Plane(-_camera.transform.forward, transform.root.position);

                var ray = _camera.ScreenPointToRay(Mouse.current.position.value);

                if (plane.Raycast(ray, out var distance))
                {
                    var targetPoint = ray.GetPoint(distance);
                    var currentPosition = transform.root.position;
                    var directionToTarget = targetPoint - currentPosition;

                    var localRight = transform.root.right;
                    var localUp = transform.root.up;
                    var localForward = transform.root.forward;

                    switch (markerAxis)
                    {
                        case MarkerAxisType.X:
                            var projectedX = Vector3.Project(directionToTarget, localRight);
                            currentPosition += projectedX;
                            break;
                        case MarkerAxisType.Y:
                            var projectedY = Vector3.Project(directionToTarget, localUp);
                            currentPosition += projectedY;
                            break;
                        case MarkerAxisType.Z:
                            var projectedZ = Vector3.Project(directionToTarget, localForward);
                            currentPosition += projectedZ;
                            break;
                    }

                    transform.root.position = currentPosition;
                }
            }
        }

        private void Rotate()
        {
            if (_isDragging)
            {
                var plane = new Plane(-_camera.transform.forward, transform.root.position);
                var ray = _camera.ScreenPointToRay(Mouse.current.position.value);

                if (plane.Raycast(ray, out var distance))
                {
                    var targetPoint = ray.GetPoint(distance);

                    switch (markerAxis)
                    {
                        case MarkerAxisType.X:
                            RotateAroundAxis(targetPoint.x - _mouseStartPosition.x, Vector3.right);
                            break;
                        case MarkerAxisType.Y:
                            RotateAroundAxis(targetPoint.y - _mouseStartPosition.y, Vector3.up);
                            break;
                        case MarkerAxisType.Z:
                            RotateAroundAxis(targetPoint.z - _mouseStartPosition.z, Vector3.forward);
                            break;
                    }

                    _mouseStartPosition = targetPoint;
                }
            }
        }

        private void RotateAroundAxis(float delta, Vector3 axis)
        {
            var angle = delta * rotationSpeed;
            transform.root.Rotate(axis, angle);
        }

        private void Scale()
        {
            if (_isDragging)
            {
                var plane = new Plane(_camera.transform.forward, transform.root.position);

                var ray = _camera.ScreenPointToRay(Mouse.current.position.value);

                if (plane.Raycast(ray, out var distance))
                {
                    var targetPoint = ray.GetPoint(distance);
                    var currentScale = transform.root.localScale;
                    var directionToTarget = targetPoint - transform.root.position;

                    // Convert direction from world space to local space
                    var localDirectionToTarget = transform.root.InverseTransformDirection(directionToTarget) * 0.01f;

                    currentScale.x += localDirectionToTarget.x;
                    currentScale.y += localDirectionToTarget.x;
                    currentScale.z += localDirectionToTarget.x;

                    if (transform.root.TryGetComponent(out BotPart botPart))
                    {
                        var data = _configProvider.PartsConfig.GetPart(botPart.PartType);
                        currentScale.x = Mathf.Clamp(currentScale.x, data.minScale.x, data.maxScale.x);
                        currentScale.y = Mathf.Clamp(currentScale.y, data.minScale.y, data.maxScale.y);
                        currentScale.z = Mathf.Clamp(currentScale.z, data.minScale.z, data.maxScale.z);
                    }

                    transform.root.localScale = currentScale;
                }
            }
        }

        private enum MarkerAxisType
        {
            X,
            Y,
            Z
        }
    }
}