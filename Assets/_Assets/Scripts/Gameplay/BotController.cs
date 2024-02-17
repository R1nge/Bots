using System.Collections.Generic;
using _Assets.Scripts.Gameplay.Parts;
using _Assets.Scripts.Gameplay.Parts.Attack;
using _Assets.Scripts.Gameplay.Parts.Move;
using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    public class BotController : BotPart
    {
        private List<IMovingPart> _moveParts;
        private List<IAttackingPart> _attackParts;

        private void Start()
        {
            var moveParts = GetComponentsInChildren<IMovingPart>();
            for (int i = 0; i < moveParts.Length; i++)
            {
                AddMovePart(_moveParts[i]);
            }

            var attackParts = GetComponentsInChildren<IAttackingPart>();
            for (int i = 0; i < attackParts.Length; i++)
            {
                AddAttackPart(_attackParts[i]);
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

        private void AddMovePart(IMovingPart movingPart) => _moveParts.Add(movingPart);

        public void RemoveMovePart(IMovingPart movingPart) => _moveParts.Remove(movingPart);

        private void AddAttackPart(IAttackingPart attackHammerPart) => _attackParts.Add(attackHammerPart);

        private void RemoveAttackPart(IAttackingPart attackHammerPart) => _attackParts.Remove(attackHammerPart);
    }
}