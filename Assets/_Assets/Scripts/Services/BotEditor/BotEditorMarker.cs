using System;
using UnityEngine;

namespace _Assets.Scripts.Services.BotEditor
{
    public class BotEditorMarker : MonoBehaviour
    {
        [SerializeField] private MarkerAxisType markerAxis;
        public MarkerAxisType MarkerAxis => markerAxis;

        public void Move(Vector3 position)
        {
            switch (markerAxis)
            {
                case MarkerAxisType.X:
                    transform.root.position = position;
                    //transform.root.position += Vector3.one * distance;
                    break;
            }
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