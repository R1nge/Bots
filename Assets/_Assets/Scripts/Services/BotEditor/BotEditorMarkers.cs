using UnityEngine;

namespace _Assets.Scripts.Services.BotEditor
{
    public class BotEditorMarkers : MonoBehaviour
    {
        [SerializeField] private BotEditorMarker[] moveMarkers;
        [SerializeField] private BotEditorMarker[] rotateMarkers;
        [SerializeField] private BotEditorMarker[] scaleMarkers;

        public void ChangeVisuals(EditMode editMode)
        {
            foreach (var marker in moveMarkers)
            {
                marker.gameObject.SetActive(false);
            }

            foreach (var marker in rotateMarkers)
            {
                marker.gameObject.SetActive(false);
            }

            foreach (var marker in scaleMarkers)
            {
                marker.gameObject.SetActive(false);
            }

            switch (editMode)
            {
                case EditMode.Move:
                    foreach (var marker in moveMarkers)
                    {
                        marker.gameObject.SetActive(true);
                        marker.UpdateEditMode(EditMode.Move);
                    }

                    break;
                case EditMode.Rotate:
                    foreach (var marker in rotateMarkers)
                    {
                        marker.gameObject.SetActive(true);
                        marker.UpdateEditMode(EditMode.Rotate);
                    }

                    break;
                case EditMode.Scale:
                    foreach (var marker in scaleMarkers)
                    {
                        marker.gameObject.SetActive(true);
                        marker.UpdateEditMode(EditMode.Scale);
                    }

                    break;
            }
        }
    }
}