using System;
using UnityEngine;

namespace _Assets.Scripts.Services.BotEditor
{
    public class BotEditorMarker : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        [SerializeField] private MarkerAxisType markerAxis;
        private EditMode _editMode;
        private bool _isDragging;
        private Vector3 _previousPosition;
        private Camera _camera;

        public void StartDragging(Camera camera)
        {
            _camera = camera;
            _isDragging = true;
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

        public void StopDragging() => _isDragging = false;

        private void Move()
        {
            if (_isDragging)
            {
                var plane = new Plane(-_camera.transform.forward, transform.root.position);

                var ray = _camera.ScreenPointToRay(Input.mousePosition);

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
                var ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (plane.Raycast(ray, out var distance))
                {
                    var targetPoint = ray.GetPoint(distance);

                    switch (markerAxis)
                    {
                        case MarkerAxisType.X:
                            RotateAroundAxis(targetPoint.x - _previousPosition.x, Vector3.right);
                            break;
                        case MarkerAxisType.Y:
                            RotateAroundAxis(targetPoint.y - _previousPosition.y, Vector3.up);
                            break;
                        case MarkerAxisType.Z:
                            RotateAroundAxis(targetPoint.z - _previousPosition.z, Vector3.forward);
                            break;
                    }

                    _previousPosition = targetPoint;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0) && _camera != null)
                {
                    var plane = new Plane(-_camera.transform.forward, transform.root.position);
                    var ray = _camera.ScreenPointToRay(Input.mousePosition);
                    if (plane.Raycast(ray, out var distance))
                    {
                        _previousPosition = ray.GetPoint(distance);
                    }
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

                var ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (plane.Raycast(ray, out var distance))
                {
                    var targetPoint = ray.GetPoint(distance);
                    var currentScale = transform.root.localScale;
                    var directionToTarget = targetPoint - transform.root.position;

                    // Convert direction from world space to local space
                    var localDirectionToTarget = transform.root.InverseTransformDirection(directionToTarget) * 0.01f;

                    // Apply scaling only along the selected local axis
                    switch (markerAxis)
                    {
                        case MarkerAxisType.X:
                            currentScale.x += localDirectionToTarget.x;
                            break;
                        case MarkerAxisType.Y:
                            currentScale.y += localDirectionToTarget.y;
                            break;
                        case MarkerAxisType.Z:
                            currentScale.z += localDirectionToTarget.z;
                            break;
                    }

                    currentScale.x = Mathf.Max(currentScale.x, 0.1f);
                    currentScale.y = Mathf.Max(currentScale.y, 0.1f);
                    currentScale.z = Mathf.Max(currentScale.z, 0.1f);

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