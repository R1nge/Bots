using System;
using System.Collections.Generic;
using _Assets.Scripts.Gameplay.Parts;
using _Assets.Scripts.Gameplay.Parts.Attack;
using _Assets.Scripts.Gameplay.Parts.Move;
using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    public class BotController : BotPart
    {
        [SerializeField] private List<MovePart> _moveParts;
        [SerializeField] private List<AttackPart> _attackParts;

        private void Start()
        {
            var moveParts = GetComponentsInChildren<MovePart>();
            for (int i = 0; i < moveParts.Length; i++)
            {
                AddMovePart(_moveParts[i]);
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

            if (Input.GetMouseButtonDown(0))
            {
                foreach (var attackPart in _attackParts)
                {
                    attackPart.Attack();
                }
            }
        }

        public void AddMovePart(MovePart movePart) => _moveParts.Add(movePart);

        public void RemoveMovePart(MovePart movePart) => _moveParts.Remove(movePart);
    }
}