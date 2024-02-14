using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    public class BotController : MonoBehaviour, IDamageable
    {
        //So, bot has different parts
        //Attack stuff
        //Move stuff
        //Each part has health to it
        [SerializeField] private int maxHealth;
        private int _currentHealth;
        [SerializeField] private List<MoveBotPart> _moveParts;


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
                Debug.LogWarning("Bot has been destroyed", this);
            }
        }

        private void Update()
        {
            foreach (var moveBotPart in _moveParts)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    moveBotPart.Move(Vector3.forward);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    moveBotPart.Move(Vector3.left);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    moveBotPart.Move(Vector3.back);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    moveBotPart.Move(Vector3.right);
                }
            }
        }
    }
}