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
        private Vector3 _offset;
        private Camera _camera;

        public void StartDragging(Camera camera)
        {
            _camera = camera;
            _isDragging = true;
            
            _screenPoint = _camera.WorldToScreenPoint(gameObject.transform.position);

            //It assumes that the camera is not moving during the drag.
            _offset = gameObject.transform.position - _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
        }

        private void Update()
        {
            if (_isDragging)
            {
                var currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);

                // Convert the screen point to world point plus the calculated offset.
                var currentPosition = _camera.ScreenToWorldPoint(currentScreenPoint) + _offset;
                
                switch (markerAxis)
                {
                    case MarkerAxisType.X:
                        var position = new Vector3(currentPosition.x, transform.root.position.y, transform.root.position.z);
                        transform.root.position = position;
                        break;
                    case MarkerAxisType.Y:
                        var position2 = new Vector3(transform.root.position.x, currentPosition.y, transform.root.position.z);
                        transform.root.position = position2;
                        break;
                    case MarkerAxisType.Z:
                        var position3 = new Vector3(transform.root.position.x, transform.root.position.y, currentPosition.x);
                        transform.root.position = position3;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
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