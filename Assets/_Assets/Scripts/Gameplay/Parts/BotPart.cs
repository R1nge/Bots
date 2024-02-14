using UnityEngine;

namespace _Assets.Scripts.Gameplay.Parts
{
    public abstract class BotPart : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth;
        private int _currentHealth;

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
    }
}