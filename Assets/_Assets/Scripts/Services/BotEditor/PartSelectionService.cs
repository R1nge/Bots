using _Assets.Scripts.Gameplay.Parts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.BotEditor
{
    public class PartSelectionService : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private BotEditorMarkers botEditorMarkers;
        [Inject] private IObjectResolver _objectResolver;
        private BotPart _selectedPart;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    ProcessHit(hit);
                }
                else
                {
                    DeselectPart();
                }
            }
        }

        private void ProcessHit(RaycastHit hit)
        {
            if (hit.transform.TryGetComponent(out BotPart botPart))
            {
                if (_selectedPart != null)
                {
                    if (!_selectedPart.Equals(botPart))
                    {
                        HideEditorMarkers(_selectedPart.transform);
                        SelectPart(botPart);
                    }
                }
                else
                {
                    SelectPart(botPart);
                }
            }
            else if (_selectedPart != null)
            {
                DeselectPart();
            }
        }

        private void SelectPart(BotPart botPart)
        {
            _selectedPart = botPart;
            ShowEditorMarkers(_selectedPart.transform);
        }

        private void DeselectPart()
        {
            if (_selectedPart != null)
            {
                HideEditorMarkers(_selectedPart.transform);
                _selectedPart = null;
            }
        }

        private void ShowEditorMarkers(Transform parent)
        {
            _objectResolver.Instantiate(botEditorMarkers, parent);
        }

        private void HideEditorMarkers(Transform parent)
        {
            var editorMarkers = parent.GetComponentInChildren<BotEditorMarkers>();
            Destroy(editorMarkers.gameObject);
        }
    }
}