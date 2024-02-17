using System;
using UnityEngine;

namespace _Assets.Scripts.Services.BotEditor
{
    public class BotEditorMarker : MonoBehaviour
    {
        [SerializeField] private MarkerAxisType markerAxis;
        public MarkerAxisType MarkerAxis => markerAxis;
        private bool _isDragging;
        private Vector3 _screenPoint;
        private Camera _camera;

        public void StartDragging(Camera camera, EditMode editMode)
        {
            _camera = camera;
            _isDragging = true;

            _screenPoint = _camera.WorldToScreenPoint(gameObject.transform.position);

            UpdateMode(editMode);
        }

        private void UpdateMode(EditMode editMode)
        {
            switch (editMode)
            {
                case EditMode.Move:
                    break;
                case EditMode.Rotate:
                    break;
                case EditMode.Scale:
                    break;
            }
        }

        private void Update()
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
                            transform.root.position = new Vector3(targetPoint.x, transform.position.y, transform.position.z);
                            break;
                        case MarkerAxisType.Y:
                            transform.root.position = new Vector3(transform.position.x, targetPoint.y, transform.position.z);
                            break;
                        case MarkerAxisType.Z:
                            transform.root.position = new Vector3(transform.position.x, transform.position.y, targetPoint.z);
                            break;
                    }
                }
            }
        }

        public void StopDragging()
        {
            _isDragging = false;
        }

        public void Rotate(float distance)
        {
            switch (markerAxis)
            {
                case MarkerAxisType.X:
                    transform.root.Rotate(Vector3.one * distance);
                    break;
                case MarkerAxisType.Y:
                    transform.root.Rotate(Vector3.up * distance);
                    break;
                case MarkerAxisType.Z:
                    transform.root.Rotate(Vector3.forward * distance);
                    break;
            }
        }

        public enum MarkerAxisType
        {
            X,
            Y,
            Z
        }
    }
}