using System;
using _Assets.Scripts.Gameplay.Parts;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Services.BotEditor
{
    public class PartPreview : BotPart
    {
        [SerializeField] private float rayDistance;
        [SerializeField] private Transform[] rayTransforms;
        [SerializeField] private Material allow, forbid;
        [SerializeField] private MeshRenderer meshRenderer;
        private Material _currentMaterial;
        private bool _canBePlaced;
        private PartData.PartType _partType;
        [Inject] private BotEditorService _botEditorService;

        private void Awake()
        {
            _currentMaterial = meshRenderer.material;
        }

        public void SetPartType(PartData.PartType type)
        {
            _partType = type;
        }

        private void Update()
        {
            var canPlace = true;

            foreach (var rayTransform in rayTransforms)
            {
                if (Physics.Raycast(rayTransform.position, rayTransform.forward, out var hit, rayDistance))
                {
                    if (!hit.transform.TryGetComponent(out IPlaceablePart placeablePart))
                    {
                        canPlace = false;
                    }
                }
                else
                {
                    canPlace = false;
                }
            }

            _canBePlaced = canPlace;

            meshRenderer.material = _canBePlaced ? allow : forbid;


            if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.F))
            {
                if (_canBePlaced)
                {
                    _botEditorService.SpawnNew(transform.position, _partType);
                    _botEditorService.Destroy(this);
                }
            }
        }
    }
}