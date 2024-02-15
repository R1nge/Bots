using System;
using UnityEngine;

namespace _Assets.Scripts.Services.BotEditor
{
    public class BotEditorMarkers : MonoBehaviour
    {
        [SerializeField] private BotEditorMarker[] markers;

        public void Move(Vector3 position, BotEditorMarker.MarkerAxisType markerAxisType)
        {
            foreach (var marker in markers)
            {
                if (marker.MarkerAxis == markerAxisType)
                {
                    marker.Move(position);
                }
            }
        }

        public void Rotate(float distance, BotEditorMarker.MarkerAxisType markerAxisType)
        {
            foreach (var marker in markers)
            {
                if (marker.MarkerAxis == markerAxisType)
                {
                    marker.Rotate(distance);
                }
            }
        }
    }
}