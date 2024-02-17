using System;
using UnityEngine;

namespace _Assets.Scripts.Gameplay.Parts
{
    public abstract class BotPart : MonoBehaviour, IDamageable
    {
        [SerializeField] private float rayDistance;
        [SerializeField] private Transform[] rayTransforms;
        [SerializeField] private Material forbid;
        [SerializeField] private MeshRenderer meshRenderer;
        private Material _currentMaterial;
        private bool _canBePlaced;
        private PartData.PartType _partType;
        [SerializeField] private int maxHealth;
        private int _currentHealth;

        private void Awake()
        {
            _currentMaterial = meshRenderer.material;
        }

        public void TakeDamage(int damage)
        {
            if (damage <= 0)
            {
                Debug.LogWarning("Damage must be greater than 0", this);
                return;
            }

            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);

            if (_currentHealth <= 0)
            {
                Debug.LogWarning("Part has been destroyed", this);
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

            meshRenderer.material = _canBePlaced ? _currentMaterial : forbid;
        }
    }
}