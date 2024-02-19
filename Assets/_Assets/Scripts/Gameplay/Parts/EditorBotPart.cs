using UnityEngine;

namespace _Assets.Scripts.Gameplay.Parts
{
    public class EditorBotPart : BotPart
    {
        [SerializeField] private float rayDistance;
        [SerializeField] private Transform[] rayTransforms;
        [SerializeField] private Material forbid;
        [SerializeField] private MeshRenderer meshRenderer;
        private Material _currentMaterial;
        private bool _canBePlaced = true;
        public bool CanBePlaced => _canBePlaced;

        public override void Awake() => _currentMaterial = meshRenderer.material;

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

            meshRenderer.material = _canBePlaced ? _currentMaterial : forbid;
        }
    }
}