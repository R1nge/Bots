using _Assets.Scripts.Gameplay.Parts;
using _Assets.Scripts.Misc;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.BotEditor
{
    public class PartSelectionService : MonoBehaviour
    {
        [SerializeField] private BotEditorMarkers botEditorMarkers;
        [SerializeField] private LayerMask editorLayer;
        [Inject] private IObjectResolver _objectResolver;
        private Camera _camera;
        private BotPart _selectedPart;
        private BotEditorMarkers _botEditorMarkers;
        private EditMode _editMode;
        private bool _initialized;

        public void Init(Camera camera)
        {
            _camera = camera;
            _initialized = true;
        }

        private void Update()
        {
            if (!_initialized)
                return;

            SelectMode();

            if (Input.GetMouseButtonDown(0))
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    ProcessHit(hit);
                }
                else
                {
                    DeselectPart();
                }
            }

            if (Input.GetMouseButtonDown(0) && _selectedPart != null)
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, float.MaxValue, layerMask: editorLayer))
                {
                    if (hit.transform.TryGetComponent(out BotEditorMarker marker))
                    {
                        marker.StartDragging(_camera, hit.point);
                        _camera.GetComponent<FlyCamera>().enabled = false;
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0) && _selectedPart != null)
            {
                var markers = _selectedPart.GetComponentsInChildren<BotEditorMarker>();
                foreach (var marker in markers)
                {
                    marker.StopDragging();
                }

                _camera.GetComponent<FlyCamera>().enabled = true;
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
                        HideEditorMarkers();
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
                HideEditorMarkers();
                _selectedPart = null;
            }
        }

        private void ShowEditorMarkers(Transform parent) => _botEditorMarkers = _objectResolver.Instantiate(botEditorMarkers, parent);

        private void HideEditorMarkers() => Destroy(_botEditorMarkers.gameObject);

        private void SelectMode()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _editMode = EditMode.Move;
                UpdateEditMode();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _editMode = EditMode.Rotate;
                UpdateEditMode();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _editMode = EditMode.Scale;
                UpdateEditMode();
            }
        }

        private void UpdateEditMode()
        {
            if (_botEditorMarkers != null)
            {
                _botEditorMarkers.ChangeVisuals(_editMode);
            }
        }

        //Or can try different approach
        //Let the player spawn the body
        //And use surface normals to spawn objects
        //But would need to account for the occupied positions somehow
        //Since most of the parts are box-like, can raycast at the edges?
        //Or better, raycast from the center,
        //If hit the placable part, spawn the part
        //If hit the non-placable part, do nothing
    }
}