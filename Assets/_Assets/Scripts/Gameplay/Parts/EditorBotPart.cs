using UnityEngine;

namespace _Assets.Scripts.Gameplay.Parts
{
    public class EditorBotPart : BotPart
    {
        [SerializeField] private float rayDistance;
        [SerializeField] private Transform[] rayTransforms;
        [SerializeField] private Material forbid;
        [SerializeField] private MeshRenderer[] meshRenderers;
        private Material[] _currentMaterials;
        private bool _canBePlaced = true;
        public bool CanBePlaced => _canBePlaced;

        public override void Awake()
        {
            _currentMaterials = new Material[meshRenderers.Length];

            for (int i = 0; i < meshRenderers.Length; i++)
            {
                _currentMaterials[i] = meshRenderers[i].material;
            }
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

            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].material = _canBePlaced ? _currentMaterials[i] : forbid;
            }
        }
    }
}