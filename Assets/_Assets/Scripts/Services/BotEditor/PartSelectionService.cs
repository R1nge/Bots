using System;
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
        [Inject] private BotEditorGridService _botEditorGridService;
        private BotPart _selectedPart;
        private BotEditorMarkers _botEditorMarkers;
        private EditMode _editMode;
        private Vector3 _mouseStartPosition;

        private void Update()
        {
            SelectMode();

            if (_selectedPart != null)
            {
                ProcessObject();
            }

            if (Input.GetMouseButtonDown(0))
            {
                var ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    Debug.Log("Hit position: " + hit.point);
                    var gridCell = _botEditorGridService.GetGridCell(hit.point);
                    Debug.Log("Cell position: " + gridCell.position);
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
            if (Input.GetKeyDown(KeyCode.W))
            {
                _editMode = EditMode.Move;
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                _editMode = EditMode.Rotate;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                _editMode = EditMode.Scale;
            }
        }

        private void ProcessObject()
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
        
        //Actually, I can use 2 planes for a 3 dimensional editing,
        //One for the Z axis, and one for the X and Y axis.

        private void Move()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mouseStartPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _mouseStartPosition = Vector3.zero;
            }

            if (Input.GetMouseButton(0))
            {
                var mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
                var delta = mousePosition - _mouseStartPosition;
                _botEditorMarkers.Move(delta, BotEditorMarker.MarkerAxisType.X);
            }
        }

        private void Rotate()
        {
            var mouseWorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            _botEditorMarkers.Rotate(mouseWorldPosition.x, BotEditorMarker.MarkerAxisType.X);
        }

        private void Scale()
        {
        }

        private enum EditMode : byte
        {
            Move = 0,
            Rotate = 1,
            Scale = 2
        }
    }
}